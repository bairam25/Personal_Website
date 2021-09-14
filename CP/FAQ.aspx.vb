#Region "Signature"
'################################### Signature ######################################
'############# Date:01-07-2019
'############# Form Name: FAQ
'############# Your Name: Ahmed Adel
'################################ End of Signature ##################################

#End Region

#Region "Import"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class CPFAQ
    Inherits System.Web.UI.Page
#Region "Global_Varaibles"
    Dim pf As New PublicFunctions
    Dim UserId As String = "0"
    Dim Category As String
    Dim URL As String
    Dim Question As String
    Dim QuestionAR As String
    Dim QuestionUR As String
    Dim Description As String
    Dim DescriptionAR As String
    Dim DescriptionUR As String
    Dim Answer As String
    Dim AnswerAR As String
    Dim AnswerUR As String
    Dim FAQTable As String = "Select * from tblFAQ where isnull(IsDeleted,0)=0"

#End Region
#Region "Public Functions"

    ''' <summary>
    ''' hide and show panels of values
    ''' </summary>
    Protected Sub Enabler(ByVal b As Boolean)
        Try
            pnlForm.Visible = b
            pnlList.Visible = Not b
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set Default controls Values 
    ''' </summary>
    Sub SetControlFields()
        Try
            Question = txtQuestion.Text
            QuestionAR = txtQuestionAR.Text
            Answer = txtAnswer.TextValue
            AnswerAR = txtAnswerAr.TextValue
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Page_Load"
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            UserId = PublicFunctions.GetUserId(Page)
            lblRes.Visible = False
            If Not Page.IsPostBack Then
                Permissions.CheckPermisions(New GridView, New LinkButton, New TextBox, New LinkButton, Me.Page, UserId)
                FillGrid(sender, e)
            End If
            SetControlFields()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Add"
    ''' <summary>
    ''' Add new FAQ
    ''' </summary>
    Protected Sub Add(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "add"
            Enabler(True)
            pf.ClearAll(pnlForm)
            txtShowOrder.Text = Val(DBManager.Getdatatable("select count(*) from tblFAQ where isnull(IsDeleted,0)=0 ").Rows(0).Item(0).ToString) + 1

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Save"
    ''' <summary>
    ''' Save FAQ 
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim daFAQ As New TblFAQFactory
        Dim dtFAQ As New TblFAQ
        Try
            If cmdSave.CommandArgument.ToLower() = "add" Then
                'Insert Case
                'Fill object tblFAQ
                If Not FillDT(dtFAQ) Then
                    Return
                End If
                'insert at tblFAQ
                If daFAQ.Insert(dtFAQ) Then
                    ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Return
                End If
            Else
                'Update Case
                dtFAQ = daFAQ.GetAllBy(TblFAQ.TblFAQFields.Id, lblFAQID.Text)(0)
                'Fill object of tblFAQ
                If Not FillDT(dtFAQ) Then
                    Return
                End If
                'Update tblFAQ
                If daFAQ.Update(dtFAQ) Then
                    ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Return
                End If

            End If
            Cancel(Sender, e)
            Enabler(False)
            FillGrid(Sender, e)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Fill tblFAQ Object
    ''' </summary>
    Function FillDT(ByVal dtFAQ As TblFAQ) As Boolean
        Try
            '2)Validation
            If Not IsNumeric(txtShowOrder.Text) Then
                clsMessages.ShowInfoMessgage(lblRes, "ترتيب غير صحيح", Me)
                Return False
            End If
            dtFAQ.Question = Question
            dtFAQ.QuestionAR = QuestionAR
            dtFAQ.Answer = Answer
            dtFAQ.AnswerAR = AnswerAR
            dtFAQ.ShowOrder = Val(txtShowOrder.Text)
            If cmdSave.CommandArgument.ToLower() = "add" Then
                dtFAQ.CreatedDate = DateTime.Now
                dtFAQ.CreatedBy = UserId
                dtFAQ.Active = False
            End If
            dtFAQ.UpdatedDate = DateTime.Now
            dtFAQ.UpdatedBy = UserId
            dtFAQ.IsDeleted = False
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

#End Region

#Region "Fill Grid"

    ''' <summary>
    ''' Fill Listview with data.
    ''' </summary>
    Sub FillGrid(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dt As DataTable = DBManager.Getdatatable(FAQTable & " And " & CollectConditions(sender, e))
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then

                    ViewState("dtFAQTable") = dt
                    ' Initialize the sorting expression.
                    ViewState("SortExpression") = "Id desc"
                    ' Populate the GridView.
                    If sender.id = "txtSearch" Or sender.id = "lbSearchIcon" Then
                        'Reset Pager to First Index
                        dplvFAQs.SetPageProperties(0, ddlPager.SelectedValue, True)
                    End If
                    BindListView()
                    dplvFAQs.Visible = False
                    If dt.Rows.Count > ddlPager.SelectedValue Then
                        dplvFAQs.Visible = True
                    End If
                Else
                    lvFAQs.DataSource = Nothing
                    lvFAQs.DataBind()
                    dplvFAQs.Visible = False
                End If
                lblTotalCount.Text = dt.Rows.Count
            End If
            'ScriptManager.RegisterClientScriptBlock(UP, Me.[GetType](), Guid.NewGuid().ToString(), "LoadJquery();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Collect condition string to fill lvFAQs
    ''' </summary>
    Public Function CollectConditions(ByVal sender As Object, ByVal e As System.EventArgs) As String
        txtSearch.Text = txtSearch.Text.TrimStart.TrimEnd

        Dim Search As String = IIf(txtSearch.Text = String.Empty, "1=1", "(Question like '%" & txtSearch.Text & "%' )")

        Return Search

    End Function

    ''' <summary>
    ''' Load faq data from [Tblfaqs] table into the Listview.
    ''' </summary>
    Private Sub BindListView()
        Try
            If ViewState("dtFAQTable") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtFAQTable As DataTable = DirectCast(ViewState("dtFAQTable"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtFAQTable)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("SortExpression").ToString()

                ' Bind the Listview control.
                lvFAQs.DataSource = dv
                lvFAQs.DataBind()
                If dtFAQTable.Rows.Count > 0 Then
                    dplvFAQs.DataBind()
                End If
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Set Number of rows at every page
    ''' </summary>
    Protected Sub PageSize_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            dplvFAQs.SetPageProperties(0, ddlPager.SelectedValue, True)
            FillGrid(sender, New EventArgs)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' set Pager
    ''' </summary>
    Protected Sub OnPagePropertiesChanging(sender As Object, e As PagePropertiesChangingEventArgs)
        Try
            dplvFAQs.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
            FillGrid(sender, New EventArgs)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Sorting Listview
    ''' </summary>
    Protected Sub lv_Sorting(sender As Object, e As ListViewSortEventArgs)
        Try
            Dim strSortExpression As String() = ViewState("SortExpression").ToString().Split(" "c)

            ' If the sorting column is the same as the previous one, 
            ' then change the sort order.
            If strSortExpression(0) = e.SortExpression Then
                If strSortExpression(1) = "DESC" Then
                    ViewState("SortExpression") = Convert.ToString(e.SortExpression) & " " & "ASC"
                Else
                    ViewState("SortExpression") = Convert.ToString(e.SortExpression) & " " & "DESC"
                End If
            Else
                ' If sorting column is another column, 
                ' then specify the sort order to "Ascending".
                ViewState("SortExpression") = Convert.ToString(e.SortExpression) & " " & "ASC"
            End If
            ' Rebind the listview control to show sorted data.
            BindListView()
            ' add sorting Arrows.
            ResetArrows()

            If strSortExpression(1) = "ASC" Then
                CType(lvFAQs.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faDown")
            Else
                CType(lvFAQs.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faUp")
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    Sub ResetArrows()
        Try
            Dim i As Integer = 0
            While i < lvFAQs.Items.Count
                CType(lvFAQs.FindControl("ShowOrder"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvFAQs.FindControl("Question"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                'CType(lvFAQs.FindControl("Description"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                'CType(lvFAQs.FindControl("Answer"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                i += 1
            End While

        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error Code: " & Application("Errors").Select("exType='" & ex.GetType().Name.ToString & "'")(0).ItemArray(2) & "</br> " & Application("Errors").Select("exType='" & ex.GetType().Name.ToString & "'")(0).ItemArray(3), Me)
        End Try

    End Sub

    ''' <summary>
    ''' Update item to be activated or not.
    ''' </summary>
    Sub UpdateActive(sender As Object, e As EventArgs)
        Try
            Dim parent = sender.parent.parent
            Dim ItemId As String = DirectCast(parent.FindControl("lblQuestionID"), Label).Text
            Dim StatusName As String = "Active"
            Dim MSG As String = ""
            Dim chk As CheckBox = DirectCast(sender, CheckBox)

            Dim Updated As Integer = 0
            If chk.Checked Then
                StatusName = "Active"
                MSG = "تم التفعيل بنجاح"
            Else
                StatusName = "Deactive"
                MSG = "تم إالغاء التفعيل بنجاح"
            End If
            Updated = DBManager.ExcuteQuery("Update tblFAQ set Active ='" + chk.Checked.ToString + "',UpdatedDate=getdate(),UpdatedBy='" + UserId + "' where Id='" + ItemId + "' ")
            If Updated = 1 Then
                clsLogs.AddSystemLogs(StatusName, "tblFAQ", ItemId)
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.CUSTOMSuccess, Page, Nothing, MSG)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Edit"
    ''' <summary>
    ''' Handle Update Event
    ''' Get Object details and Populate it in pnl forms
    ''' </summary>
    Protected Sub Edit(ByVal Sender As Object, ByVal e As System.EventArgs)
        cmdSave.CommandArgument = "Edit"
        lblFAQID.Text = Sender.commandargument
        Try
            Dim dt As DataTable = DBManager.Getdatatable(FAQTable & " And Id ='" & lblFAQID.Text & "'")
            If dt.Rows.Count > 0 Then
                txtQuestion.Text = dt.Rows(0).Item("Question").ToString
                txtQuestionAR.Text = dt.Rows(0).Item("QuestionAR").ToString
                '---------------------------------------------------
                txtAnswer.TextValue = dt.Rows(0).Item("Answer").ToString
                txtAnswerAr.TextValue = dt.Rows(0).Item("AnswerAR").ToString
                txtShowOrder.Text = dt.Rows(0).Item("ShowOrder").ToString
                Enabler(True)
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Delete"
    ''' <summary>
    ''' Handle Delete Event
    ''' </summary>
    Protected Sub Delete(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim Id As String = Sender.commandargument.ToString
        Try
            If DBManager.ExcuteQuery("Update tblFAQ set IsDeleted=1,DeletedDate=getdate() where id= '" & Id & "'") = 1 Then
                FillGrid(Sender, e)
                clsMessages.ShowMessage(lblRes, MessageTypesEnum.Delete, Me)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region


#Region "Cancel"
    ''' <summary>
    ''' hide and show panels
    ''' </summary>
    Protected Sub Cancel(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            lblFAQID.Text = String.Empty
            pf.ClearAll(pnlForm)
            Enabler(False)
            txtAnswer.TextValue = String.Empty
            txtAnswerAr.TextValue = String.Empty
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region
#Region "Permissions"
    Private Sub ListView_DataBound(sender As Object, e As EventArgs) Handles lvFAQs.DataBound
        Try
            Permissions.CheckPermisions(lvFAQs, lbNew, txtSearch, lbSearchIcon, Me.Page, UserId)     
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region
End Class
