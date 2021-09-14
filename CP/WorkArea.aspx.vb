#Region "Import"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class WorkArea
    Inherits System.Web.UI.Page

#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim UserId As String = "1"
    Dim WorkAreaId As String
    Dim Started As Boolean
    Dim StartDate As String
    Dim Counter1Title As String
    Dim Counter2Title As String
    Dim Counter3Title As String
    Dim Counter1Value As String
    Dim Counter2Value As String
    Dim Counter3Value As String
    Dim Counter1Active As String
    Dim Counter2Active As String
    Dim Counter3Active As String
    Dim URL As String
    Dim Photo As String
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
            WorkAreaId = ddlgov.SelectedValue
            Started = PublicFunctions.BoolFormat(chkWorkStarted.Checked)
            StartDate = txtStartDate.Text
            'Title
            Counter1Title = txtCounter1Title.Text
            Counter2Title = txtCounter2Title.Text
            Counter3Title = txtCounter3Title.Text
            'Value
            Counter1Value = txtCounter1Value.Text
            Counter2Value = txtCounter2Value.Text
            Counter3Value = txtCounter3Value.Text
            'Active
            Counter1Active = PublicFunctions.BoolFormat(chkCounter1Active.Checked)
            Counter2Active = PublicFunctions.BoolFormat(chkCounter2Active.Checked)
            Counter3Active = PublicFunctions.BoolFormat(chkCounter3Active.Checked)
            Photo = HiddenWorkAreaImg.Text
            FillImages()
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
            Dim ID As String = DirectCast(parent.FindControl("lblID"), Label).Text
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
            Updated = DBManager.ExcuteQuery("Update tblWorkAreaDetails set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate(),ModifiedBy='" + UserId + "' where Id='" + ID + "' ")
            If Updated = 1 Then
                clsLogs.AddSystemLogs(StatusName, "tblWorkAreaDetails", WorkAreaID)
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.CUSTOMSuccess, Page, Nothing, MSG)
            End If
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
            If Not Page.IsPostBack Then
                clsLogs.AddSystemLogs("Access")
                FillGrid(sender, e)
                FillDDLs()
            End If
            'Set Default values of controls
            SetControlFields()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Private Sub FillDDLs()
        Try
            Dim dtM As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where Type='T' and isnull(isdeleted,0)=0")
            If dtM IsNot Nothing Then
                ddlgov.DataTextField = "Name"
                ddlgov.DataValueField = "Id"
                ddlgov.AppendDataBoundItems = True
                ddlgov.DataSource = dtM
                ddlgov.DataBind()
            End If
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
            Dim dt As DataTable = DBManager.Getdatatable("select *,(Select Name from tblworkArea where id=tblWorkAreaDetails.WorkAreaId) as _WorkArea  from tblWorkAreaDetails where isnull(isdeleted,0)=0 and " & CollectConditions(sender, e))
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then

                    ViewState("dtWorkArea") = dt
                    ' Initialize the sorting expression.
                    ViewState("SortExpression") = "ModifiedDate DESC"
                    ' Populate the GridView.
                    BindListView()
                    dplvWorkAreas.Visible = False
                    If dt.Rows.Count > ddlPager.SelectedValue Then
                        dplvWorkAreas.Visible = True
                    End If
                Else
                    lvWorkAreas.DataSource = Nothing
                    lvWorkAreas.DataBind()
                    dplvWorkAreas.Visible = False
                End If
                lblTotalCount.Text = dt.Rows.Count
            End If
            'ScriptManager.RegisterClientScriptBlock(UP, Me.[GetType](), Guid.NewGuid().ToString(), "LoadJquery();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Collect condition string to fill lvWorkAreas
    ''' </summary>
    Public Function CollectConditions(ByVal sender As Object, ByVal e As System.EventArgs) As String
        txtSearch.Text = txtSearch.Text.TrimStart.TrimEnd
        'btnClearSearch.Visible = False
        'If txtSearch.Text <> String.Empty Then
        '    btnClearSearch.Visible = True
        'End If
        Dim Search As String = IIf(txtSearch.Text = String.Empty, "1=1", "(WorkAreaId=(select id from tblWorkArea where Name like '%" & txtSearch.Text & "%' and Type='T') )")

        Return Search

    End Function



    ''' <summary>
    ''' Load data into the Listview.
    ''' </summary>
    Private Sub BindListView()
        Try
            If ViewState("dtWorkArea") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtWorkArea As DataTable = DirectCast(ViewState("dtWorkArea"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtWorkArea)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("SortExpression").ToString()

                ' Bind the Listview control.
                lvWorkAreas.DataSource = dv
                lvWorkAreas.DataBind()
                If dtWorkArea.Rows.Count > 0 Then
                    dplvWorkAreas.DataBind()
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
            dplvWorkAreas.SetPageProperties(0, ddlPager.SelectedValue, True)
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
            dplvWorkAreas.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
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
                CType(lvWorkAreas.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faDown")
            Else
                CType(lvWorkAreas.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faUp")
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    Sub ResetArrows()
        Try
            Dim i As Integer = 0
            While i < lvWorkAreas.Items.Count
                CType(lvWorkAreas.FindControl("_WorkArea"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvWorkAreas.FindControl("WorkStarted"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvWorkAreas.FindControl("WorkStartDate"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvWorkAreas.FindControl("ActiveHeader"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                i += 1
            End While

        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error Code: " & Application("Errors").Select("exType='" & ex.GetType().Name.ToString & "'")(0).ItemArray(2) & "</br> " & Application("Errors").Select("exType='" & ex.GetType().Name.ToString & "'")(0).ItemArray(3), Me)
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
            lblID.Text = ""
            imgSection.ImageUrl = "~/images/img-up.png"
            HiddenWorkAreaImg.Text = ""
            pf.ClearAll(pnlForm)
            ddlgov.SelectedIndex = 0
            Enabler(True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Protected Sub chkWorkStarted_CheckedChanged(sender As Object, e As EventArgs)
        pnlStartDate.Visible = sender.checked
    End Sub
#End Region

#Region "Save"

    ''' <summary>
    ''' Handle save button(form grid) click event.
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            Dim daTabeFactory As New TblWorkAreaDetailsFactory
            Dim dtTable As New TblWorkAreaDetails
            If cmdSave.CommandArgument = "add" Then
                If Not FillDT(dtTable) Then
                    Exit Sub
                End If

                If daTabeFactory.Insert(dtTable) Then
                    Dim RefId As String = PublicFunctions.GetIdentity("TblWorkAreaDetails")
                    clsLogs.AddSystemLogs("Insert", "TblWorkAreaDetails", RefId)
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    Exit Sub
                End If

            Else
                dtTable = daTabeFactory.GetAllBy(TblWorkArea.TblWorkAreaFields.Id, lblID.Text)(0)
                If Not FillDT(dtTable) Then
                    Exit Sub
                End If
                dtTable.Id = lblID.Text

                If daTabeFactory.Update(dtTable) Then
                    clsLogs.AddSystemLogs("Update", "tblWorkAreaDetails", lblID.Text)
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
    ''' Fill dtSection from controls in the panel form.
    ''' </summary>
    Protected Function FillDT(ByRef dtWorkArea As TblWorkAreaDetails) As Boolean
        Try
            dtWorkArea.WorkAreaId = WorkAreaId
            dtWorkArea.WorkStarted = Started
            dtWorkArea.WorkStartDate = Nothing
            If Started Then
                If IsDate(StartDate) Then
                    dtWorkArea.WorkStartDate = StartDate
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CUSTOMInfo, Me, Nothing, "اختر تاريخ البدء")
                    txtStartDate.Focus()
                    Return False
                End If
            End If
            'Title
            dtWorkArea.CounterTitle1 = Counter1Title
            dtWorkArea.CounterTitle2 = Counter2Title
            dtWorkArea.CounterTitle3 = Counter3Title
            'Value
            dtWorkArea.CounterValue1 = Counter1Value
            dtWorkArea.CounterValue2 = Counter2Value
            dtWorkArea.CounterValue3 = Counter3Value
            'Active
            dtWorkArea.Counter1Active = Counter1Active
            dtWorkArea.Counter2Active = Counter2Active
            dtWorkArea.Counter3Active = Counter3Active

            If cmdSave.CommandArgument = "add" Then
                dtWorkArea.CreatedBy = UserId
                dtWorkArea.CreatedDate = DateTime.Now
                dtWorkArea.Active = False
            End If
            dtWorkArea.ModifiedBy = UserId
            dtWorkArea.ModifiedDate = DateTime.Now
            dtWorkArea.IsDeleted = False
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function
#End Region

#Region "Edit"

    ''' <summary>
    ''' Handle edit button click event.
    ''' </summary>
    Protected Sub Edit(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "edit"
            lblID.Text = Sender.commandargument.ToString
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
            dt = DBManager.Getdatatable("select * from TblWorkAreaDetails where isnull(Isdeleted,0)=0  and id='" + lblID.Text + "'")
            If dt.Rows.Count <> 0 Then

                ddlgov.SelectedValue = dt.Rows(0).Item("WorkAreaId").ToString
                chkWorkStarted.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("WorkStarted").ToString)
                txtStartDate.Text = PublicFunctions.DateFormat(dt.Rows(0).Item("WorkStartDate").ToString)
                chkWorkStarted_CheckedChanged(chkWorkStarted, New EventArgs)
                'Title
                txtCounter1Title.Text = dt.Rows(0).Item("CounterTitle1").ToString
                txtCounter2Title.Text = dt.Rows(0).Item("CounterTitle2").ToString
                txtCounter3Title.Text = dt.Rows(0).Item("CounterTitle3").ToString
                'Value
                txtCounter1Value.Text = dt.Rows(0).Item("CounterValue1").ToString
                txtCounter2Value.Text = dt.Rows(0).Item("CounterValue2").ToString
                txtCounter3Value.Text = dt.Rows(0).Item("CounterValue3").ToString
                'Active
                chkCounter1Active.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("Counter1Active").ToString)
                chkCounter2Active.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("Counter2Active").ToString)
                chkCounter3Active.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("Counter3Active").ToString)
                'HiddenWorkAreaImg.Text = dt.Rows(0).Item("Logo").ToString
                'FillImages()
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
            Dim AreaId As String = Sender.commandargument

            If DBManager.ExcuteQuery("update tblWorkAreaDetails SET  Isdeleted = 'True' where Id= '" + AreaId + "'") = 1 Then
                clsLogs.AddSystemLogs("Delete", "tblWorkAreaDetails", AreaId)
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Delete, Me.Page)
                FillGrid(Sender, e)
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Cancel"

    ''' <summary>
    ''' Handle cancel button click event
    ''' </summary>
    Protected Sub Cancel(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            imgSection.ImageUrl = "~/images/img-up.png"
            HiddenWorkAreaImg.Text = ""
            lblID.Text = ""
            ddlgov.SelectedIndex = 0
            pf.ClearAll(pnlForm)
            Enabler(False)
            FillGrid(Sender, e)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Uploader"
    Protected Sub SectionPhotoUploaded(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Check that there is a file
            If fuPhoto.PostedFile IsNot Nothing Then

                Dim filePath As String = "~/WorkAreaLogos/" & fuPhoto.FileName

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

                    Dim MainWidth As String = "1850"
                    Dim MainHeight As String = "1680"

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
            If HiddenWorkAreaImg.Text IsNot Nothing And HiddenWorkAreaImg.Text <> "" Then
                imgSection.ImageUrl = HiddenWorkAreaImg.Text
            Else
                HiddenWorkAreaImg.Text = ""
                imgSection.ImageUrl = "~/images/img-up.png"
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Permissions"
    Private Sub ListView_DataBound(sender As Object, e As EventArgs) Handles lvWorkAreas.DataBound
        Try
            Permissions.CheckPermisions(lvWorkAreas, cmdNew, txtSearch, cmdSearch, Me.Page, UserId)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region
End Class
