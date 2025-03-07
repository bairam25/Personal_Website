﻿
#Region "Import"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class Categories
    Inherits System.Web.UI.Page
#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim UserId As String = "1"
    Dim CategoryName As String
    Dim Description As String
    Dim OrderNo As String
    Dim ContentTable As String = "select * from tblCategories where "
#End Region
#Region "Public_Functions"

    ''' <summary>
    ''' If b is true : hide gridview, Operation panel bar and show panel form, Confirmation panel bar.
    ''' If b is false : hide panel form, Confirmation panel bar and show gridview, Operation panel bar.
    ''' </summary>
    Protected Sub Enabler(ByVal b As Boolean)
        Try
            pnlConfirm.Visible = b
            pnlOps.Visible = Not b
            pnlForm.Enabled = b
            pnlForm.Visible = b
            pnlGV.Visible = Not b
            lblDateContent.Text = DateTime.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "")
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set Default controls Values 
    ''' </summary>
    Sub SetControlFields()
        Try
            CategoryName = txtCategory.Text
            Description = Nothing 'txtDescription.TextValue
            OrderNo = PublicFunctions.IntFormat(txtOrderNo.Text)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
#Region "Page Load"
    ''' <summary>
    ''' Handle page_load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblRes.Visible = False
        UserId = PublicFunctions.GetUserId(Page)
        Try
            If Page.IsPostBack = False Then
                lblDateContent.Text = DateTime.Now.ToString.Replace("/", "").Replace(":", "").Replace(".", "").Replace(" ", "")
                FillGrid(sender, e)
            End If
            'Set Default values of controls
            SetControlFields()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


#End Region
#Region "Fill Grid"

    ''' <summary>
    ''' Fill Listview with data.
    ''' </summary>
    Sub FillGrid(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dt As DataTable = DBManager.Getdatatable(ContentTable & "  isnull(IsDeleted,0)=0 and " & CollectConditions(sender, e))
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then

                    ViewState("dtNewsTable") = dt
                    ' Initialize the sorting expression.
                    ViewState("SortExpression") = "ShowOrder"
                    ' Populate the GridView.
                    If sender.parent IsNot Nothing Then
                        If sender.parent.clientid = "pnlOps" Or sender.id.ToString.ToLower.Contains("delete") Then
                            'Reset Pager to First Index
                            dplvContent.SetPageProperties(0, ddlPager.SelectedValue, True)
                        End If
                    End If


                    BindListView()
                    dplvContent.Visible = False
                    If dt.Rows.Count > ddlPager.SelectedValue Then
                        dplvContent.Visible = True
                    End If
                Else
                    lvContent.DataSource = New DataTable
                    lvContent.DataBind()
                    dplvContent.Visible = False
                End If
                lblTotalCount.Text = dt.Rows.Count
            End If



            'ScriptManager.RegisterClientScriptBlock(UP, Me.[GetType](), Guid.NewGuid().ToString(), "LoadJquery();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Collect condition string to fill lvContent
    ''' </summary>
    Public Function CollectConditions(ByVal sender As Object, ByVal e As System.EventArgs) As String
        txtSearch.Text = txtSearch.Text.TrimStart.TrimEnd
        'btnClearSearch.Visible = False
        'If txtSearch.Text <> String.Empty Then
        '    btnClearSearch.Visible = True
        'End If
        Dim Search As String = IIf(txtSearch.Text = String.Empty, "1=1", "(Name like N'%" & txtSearch.Text & "%')")
        Return Search

    End Function



    ''' <summary>
    ''' Load Adjustment data from [TblItemAdjustments] table into the Listview.
    ''' </summary>
    Private Sub BindListView()
        Try
            If ViewState("dtNewsTable") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtNewsTable As DataTable = DirectCast(ViewState("dtNewsTable"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtNewsTable)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("SortExpression").ToString()

                ' Bind the Listview control.
                lvContent.DataSource = dv
                lvContent.DataBind()
                If dtNewsTable.Rows.Count > 0 Then
                    dplvContent.DataBind()
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
            dplvContent.SetPageProperties(0, ddlPager.SelectedValue, True)
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
            dplvContent.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
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
                CType(lvContent.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faDown")
            Else
                CType(lvContent.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faUp")
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    Sub ResetArrows()
        Try
            Dim i As Integer = 0
            While i < lvContent.Items.Count
                CType(lvContent.FindControl("Name"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvContent.FindControl("Active"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvContent.FindControl("ShowOrder"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvContent.FindControl("ShowInHome"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                i += 1
            End While

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Update item to be activated or not.
    ''' </summary>
    Sub UpdateActive(sender As Object, e As EventArgs)
        Try
            Dim parent = sender.parent.parent
            Dim ItemId As String = DirectCast(parent.FindControl("lblContentId"), Label).Text
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
            Updated = DBManager.ExcuteQuery("Update TblCategories set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate() where Id='" + ItemId + "' ")
            If Updated = 1 Then
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.CUSTOMSuccess, Page, Nothing, MSG)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Update item to be Show in home or not.
    ''' </summary>
    Sub UpdateShowHome(sender As Object, e As EventArgs)
        Try
            Dim parent = sender.parent.parent
            Dim ItemId As String = DirectCast(parent.FindControl("lblContentId"), Label).Text
            Dim StatusName As String = "Show"
            Dim MSG As String = ""
            Dim chk As CheckBox = DirectCast(sender, CheckBox)

            Dim Updated As Integer = 0
            If chk.Checked Then
                StatusName = "Show"
                MSG = "تم العرض بنجاح"
            Else
                StatusName = "Hide"
                MSG = "تم إالغاء العرض بنجاح"
            End If
            Updated = DBManager.ExcuteQuery("Update tblCategories set ShowInHome ='" + chk.Checked.ToString + "',ModifiedDate=getdate() where Id='" + ItemId + "' ")
            If Updated = 1 Then
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.CUSTOMSuccess, Page, Nothing, MSG)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "New"

    ''' <summary>
    ''' Handle add button(form grid) click event.
    ''' </summary>
    Protected Sub Add(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "add"
            lblContentId.Text = ""
            pf.ClearAll(pnlForm)
            'txtDescription.TextValue = String.Empty
            Enabler(True)
            chkActive.Checked = True
            txtOrderNo.Text = DBManager.SelectMax("ShowOrder", "tblCategories where isnull(isDeleted,0)=0 ")

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
#Region "Edit"

    ''' <summary>
    ''' Handle edit button click event.
    ''' </summary>
    Protected Sub Edit(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "edit"
            lblContentId.Text = Sender.commandargument.ToString
            pf.ClearAll(pnlForm)

            If FillForm() Then
                Enabler(True)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Fill controls in the panel form with the data.
    ''' </summary>
    Private Function FillForm() As Boolean
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select * from tblCategories where isnull(Isdeleted,0)=0  and id='" + lblContentId.Text + "'")
            If dt.Rows.Count <> 0 Then
                txtCategory.Text = dt.Rows(0).Item("name").ToString
                'txtDescription.TextValue = dt.Rows(0).Item("Description").ToString
                txtOrderNo.Text = dt.Rows(0).Item("ShowOrder").ToString
                chkActive.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("Active"))

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

#End Region
#Region "Delete"
    ''' <summary>
    ''' Handle delete button form grid.
    ''' </summary>
    Protected Sub Delete(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            Dim CategoryID As String = Sender.commandargument
            'Check if category has anaylisys 
            Dim dtAnalysis = DBManager.Getdatatable("Select top 1 * from TblContent where Type = 'ANL' and CategoryID='" & CategoryID & "' and isnull(isdeleted,0)=0")
            If dtAnalysis.Rows.Count > 0 Then
                clsMessages.ShowInfoMessgage(lblRes, "يجب مسح جميع التحليلات لهذا التصنيف اولا", Me)
                Exit Sub
            End If
            If DBManager.ExcuteQuery("update tblCategories SET  Isdeleted = 'True' where Id= '" + CategoryID + "'") = 1 Then
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Delete, Me.Page)
                FillGrid(Sender, e)
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Save"

    ''' <summary>
    ''' Handle save button(form grid) click event.
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            Dim daTabeFactory As New TblCategoriesFactory
            Dim dtTable As New TblCategories
            If cmdSave.CommandArgument = "add" Then
                If FillDT(dtTable) = False Then
                    Exit Sub
                End If

                If daTabeFactory.Insert(dtTable) Then
                    Dim RefId As String = dtTable.Id
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    Exit Sub
                End If

            Else
                dtTable = daTabeFactory.GetAllBy(TblCategories.TblCategoriesFields.Id, lblContentId.Text)(0)
                If FillDT(dtTable) = False Then
                    Exit Sub
                End If
                dtTable.Id = lblContentId.Text

                If daTabeFactory.Update(dtTable) Then
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    Exit Sub
                End If

            End If
            Cancel(Sender, New EventArgs)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Fill dtContent from controls in the panel form.
    ''' </summary>
    Protected Function FillDT(ByRef dtContent As TblCategories) As Boolean
        Try
            If Not IsNumeric(OrderNo) Then
                clsMessages.ShowInfoMessgage(lblRes, "Invalid Order", Me)
                txtOrderNo.Focus()
                Return False
            End If

            dtContent.Name = CategoryName
            dtContent.Description = Description
            dtContent.Active = chkActive.Checked
            dtContent.ShowOrder = OrderNo
            If cmdSave.CommandArgument = "add" Then
                dtContent.CreatedDate = DateTime.Now
            End If
            dtContent.ModifiedDate = DateTime.Now

            dtContent.IsDeleted = False
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function
#End Region
#Region "Cancel"

    ''' <summary>
    ''' Handle cancel button click event
    ''' </summary>
    Protected Sub Cancel(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try

            lblContentId.Text = ""
            'txtDescription.TextValue = String.Empty
            Enabler(False)
            FillGrid(Sender, e)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

End Class
