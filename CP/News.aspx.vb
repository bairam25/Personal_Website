﻿
#Region "Import"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class News
    Inherits System.Web.UI.Page
#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim UserId As String = "1"
    Dim ContentTitle As String
    Dim ContentDate As String
    Dim Description As String
    Dim Photo As String
    Dim OrderNo As String
    Dim ContentTable As String = "select   * from tblContent where Type='NEW'"
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
            ContentTitle = txtTitle.Text
            Description = txtDescription.TextValue
            Photo = HiddenContentImg.Text
            ContentDate = PublicFunctions.DateFormat(txtContentDate.Text, "dd/MM/yyyy")
            OrderNo = PublicFunctions.IntFormat(txtOrderNo.Text)
            FillImages()
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
                txtFilterFromDate.Text = DateTime.Now.ToShortDateString
                txtFilterToDate.Text = DateTime.Now.ToShortDateString
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
            Dim dt As DataTable = DBManager.Getdatatable(ContentTable & " and isnull(IsDeleted,0)=0 and " & CollectConditions(sender, e))
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then

                    ViewState("dtNewsTable") = dt
                    ' Initialize the sorting expression.
                    ViewState("SortExpression") = "ShowOrder DESC"
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
                    lvContent.DataSource = Nothing
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
        Dim DateFrom As String = IIf(txtFilterFromDate.Text = "", "1=1", "  Date >= '" + PublicFunctions.DateFormat(txtFilterFromDate.Text, "yyyy/MM/dd") + " 00:00:00'")
        Dim DateTo As String = IIf(txtFilterToDate.Text = "", "1=1", "  Date <= '" + PublicFunctions.DateFormat(txtFilterToDate.Text, "yyyy/MM/dd") + " 23:59:59'")

        'btnClearSearch.Visible = False
        'If txtSearch.Text <> String.Empty Then
        '    btnClearSearch.Visible = True
        'End If
        Dim Search As String = IIf(txtSearch.Text = String.Empty, "1=1", "(Title like N'%" & txtSearch.Text & "%')")

        Return Search + " and " + DateFrom + " and " + DateTo

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
                CType(lvContent.FindControl("Date"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvContent.FindControl("Title"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvContent.FindControl("ShowOrder"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
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
            Updated = DBManager.ExcuteQuery("Update tblContent set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate() where Id='" + ItemId + "' ")
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
            Updated = DBManager.ExcuteQuery("Update tblContent set ShowInHome ='" + chk.Checked.ToString + "',ModifiedDate=getdate() where Id='" + ItemId + "' ")
            If Updated = 1 Then
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.CUSTOMSuccess, Page, Nothing, MSG)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Filter Listview by Date Rage
    ''' </summary>
    Protected Sub lbFilterDate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not IsDate(txtFilterFromDate.Text) Or Not IsDate(txtFilterToDate.Text) Then
                clsMessages.ShowAlertMessgage(lblRes, "Please select valid dates", Me)
                txtFilterFromDate.Text = DateTime.Now.ToShortDateString
                txtFilterToDate.Text = DateTime.Now.ToShortDateString
                Exit Sub
            End If
            If CheckDates(txtFilterFromDate.Text, txtFilterToDate.Text) = False Then
                txtFilterFromDate.Text = DateTime.Now.ToShortDateString
                txtFilterToDate.Text = DateTime.Now.ToShortDateString
                Exit Sub
            End If
            FillGrid(sender, New EventArgs)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Function CheckDates(ByVal DateFrom As String, ByVal DateTo As String) As Boolean
        Try
            Dim Date1 As String = DateFrom
            Dim Date2 As String = DateTo
            If IsDate(Date1) And IsDate(Date2) Then
                If Convert.ToDateTime(Date1) > Convert.ToDateTime(Date2) Then
                    clsMessages.ShowAlertMessgage(lblRes, " Date from should be less than Date to", Me)
                    Return False
                End If
                Return True
            End If
            Return False
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
            Return False
        End Try
    End Function

#End Region

#Region "New"

    ''' <summary>
    ''' Handle add button(form grid) click event.
    ''' </summary>
    Protected Sub Add(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "add"
            lblContentId.Text = ""
            imgContent.ImageUrl = "~/images/img-up.png"
            HiddenContentImg.Text = ""
            pf.ClearAll(pnlForm)
            txtDescription.TextValue = String.Empty
            Enabler(True)
            chkActive.Checked = True
            txtContentDate.Text = DateTime.Now.ToShortDateString
            txtOrderNo.Text = DBManager.SelectMax("ShowOrder", "tblContent where isnull(isDeleted,0)=0 and Type='NEW'")
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
            dt = DBManager.Getdatatable("select * from tblContent where isnull(Isdeleted,0)=0  and id='" + lblContentId.Text + "'")
            If dt.Rows.Count <> 0 Then
                txtTitle.Text = dt.Rows(0).Item("Title").ToString
                txtContentDate.Text = PublicFunctions.DateFormat(dt.Rows(0).Item("Date").ToString, "dd/MM/yyyy")
                txtDescription.TextValue = dt.Rows(0).Item("Description").ToString
                txtOrderNo.Text = dt.Rows(0).Item("ShowOrder").ToString
                HiddenContentImg.Text = dt.Rows(0).Item("Photo").ToString
                chkActive.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("Active"))

                FillImages()

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
            Dim ContentId As String = Sender.commandargument

            If DBManager.ExcuteQuery("update tblContent SET  Isdeleted = 'True' where Id= '" + ContentId + "'") = 1 Then
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Delete, Me.Page)
                FillGrid(Sender, e)
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub CheckAll(ByVal sender As CheckBox, e As EventArgs)
        Try
            For Each item As ListViewItem In lvContent.Items
                CType(item.FindControl("chkSelect"), CheckBox).Checked = sender.Checked
            Next
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub DeleteAll(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            If PublicFunctions.DeleteAllSelected(lvContent) Then
                ShowMessage(lblRes, MessageTypesEnum.CUSTOMSuccess, Me, Nothing, "تم حذف السجلات المحددة بنجاح")
                FillGrid(Sender, e)
            Else
                clsMessages.ShowInfoMessgage(lblRes, "لا توجد سجلات محددة للحذف", Me)
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
            Dim daTabeFactory As New TblContentFactory
            Dim dtTable As New TblContent
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
                dtTable = daTabeFactory.GetAllBy(TblContent.TblContentFields.Id, lblContentId.Text)(0)
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
    Protected Function FillDT(ByRef dtContent As TblContent) As Boolean
        Try
            If Not IsNumeric(OrderNo) Then
                clsMessages.ShowInfoMessgage(lblRes, "Invalid Order", Me)
                txtOrderNo.Focus()
                Return False
            End If
            If Not IsDate(ContentDate) Then
                clsMessages.ShowInfoMessgage(lblRes, "Invalid Date", Me)
                txtContentDate.Focus()
                Return False
            End If
            dtContent.Type = "NEW"
            dtContent.Title = ContentTitle
            dtContent.Date = ContentDate
            dtContent.Description = Description
            dtContent.Active = chkActive.Checked
            dtContent.Photo = Photo
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

            imgContent.ImageUrl = "~/images/img-up.png"
            HiddenContentImg.Text = ""
            lblContentId.Text = ""
            Enabler(False)
            FillGrid(Sender, e)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
#Region "Uploader"
    Protected Sub ContentPhotoUploaded(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Check that there is a file
            If fuPhoto.PostedFile IsNot Nothing Then

                Dim filePath As String = "~/ContentPhotos/" & lblDateContent.Text + "_" + fuPhoto.FileName

                ' Check file size (mustn’t be 0)
                Dim myFile As HttpPostedFile = fuPhoto.PostedFile
                Dim nFileLen As Integer = myFile.ContentLength
                If (nFileLen > 0) Then
                    ' Read file into a data stream
                    Dim myData As Byte() = New [Byte](nFileLen - 1) {}
                    myFile.InputStream.Read(myData, 0, nFileLen)
                    myFile.InputStream.Dispose()

                    ' Save the stream to disk as temporary file. make sure the path is unique!
                    Dim newFile As New System.IO.FileStream(Server.MapPath(filePath & "_temp" + System.IO.Path.GetExtension(myFile.FileName).ToLower() + ""), System.IO.FileMode.Create)
                    newFile.Write(myData, 0, myData.Length)


                    ' run ALL the image optimisations you want here..... make sure your paths are unique
                    ' you can use these booleans later if you need the results for your own labels or so.
                    ' dont call the function after the file has been closed.
                    ''''''''''''''''''''''''''''''''' Main Photo Size'''''''''''''''''''''''''''''''

                    Dim MainWidth As String = "800"
                    Dim MainHeight As String = "600"

                    ImgResize.ResizeImageAndUpload(newFile, filePath, MainHeight, MainWidth)


                    ' tidy up and delete the temp file.
                    newFile.Close()
                    System.IO.File.Delete(Server.MapPath(filePath & "_temp" + System.IO.Path.GetExtension(myFile.FileName).ToLower() + ""))
                End If
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub FillImages()
        Try
            If HiddenContentImg.Text IsNot Nothing And HiddenContentImg.Text <> "" Then
                imgContent.ImageUrl = HiddenContentImg.Text
            Else
                HiddenContentImg.Text = ""
                imgContent.ImageUrl = "~/images/img-up.png"
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
End Class
