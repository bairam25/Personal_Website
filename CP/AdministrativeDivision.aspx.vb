#Region "Import"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class AdministrativeDivision
    Inherits System.Web.UI.Page

#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim UserId As String = "1"
    Dim Name As String
    Dim TotalArea As String
    Dim CitizenCount As String
    Dim Description As String
    Dim LocationRemarks As String
    Dim Lat As String
    Dim lng As String
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
            Name = txtName.Text
            CitizenCount = txtCitizenCount.Text
            TotalArea = txtTotalArea.Text
            Description = txtDescription.Text
            Lat = txtLatitude.Text
            lng = txtLogitude.Text
            Photo = HiddenWorkAreaImg.Text
            LocationRemarks = txtLocationRemarks.Text
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
            Dim WorkAreaID As String = DirectCast(parent.FindControl("lblWorkAreaID"), Label).Text
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
            Updated = DBManager.ExcuteQuery("Update tblWorkArea set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate(),ModifiedBy='" + UserId + "' where Id='" + WorkAreaID + "' ")
            If Updated = 1 Then
                clsLogs.AddSystemLogs(StatusName, "tblWorkArea", WorkAreaID)
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
            Dim dtM As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where isnull(ParentId,0)=0 and isnull(isdeleted,0)=0")
            If dtM IsNot Nothing Then
                ddlMainDistribution.DataTextField = "Name"
                ddlMainDistribution.DataValueField = "Id"
                ddlMainDistribution.AppendDataBoundItems = True
                ddlMainDistribution.DataSource = dtM
                ddlMainDistribution.DataBind()
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
            Dim dt As DataTable = DBManager.Getdatatable("select * from tblWorkArea where isnull(isdeleted,0)=0 and " & CollectConditions(sender, e))
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
        Dim Search As String = IIf(txtSearch.Text = String.Empty, "1=1", "(Title like '%" & txtSearch.Text & "%')")

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
                CType(lvWorkAreas.FindControl("Name"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvWorkAreas.FindControl("CitizenCount"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvWorkAreas.FindControl("TotalArea"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvWorkAreas.FindControl("Latitude"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvWorkAreas.FindControl("longitude"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
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
            lblWorkAreaID.Text = ""
            imgSection.ImageUrl = "~/images/img-up.png"
            HiddenWorkAreaImg.Text = ""
            pf.ClearAll(pnlForm)
            rblAreas.ClearSelection()
            pnlControls.Visible = False
            ddlMainDistribution.SelectedIndex = 0
            ddlgov.SelectedIndex = 0
            ddlCenter.SelectedIndex = 0
            ddlUnits.SelectedIndex = 0
            pnlDDLs.Visible = False
            rblAreas.Enabled = True
            pf.ClearAll(pnlControls)
            Enabler(True)
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
            lblWorkAreaID.Text = Sender.commandargument.ToString
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
            dt = DBManager.Getdatatable("select * from tblWorkArea where isnull(Isdeleted,0)=0  and id='" + lblWorkAreaID.Text + "'")
            If dt.Rows.Count <> 0 Then
                txtName.Text = dt.Rows(0).Item("Name").ToString
                txtDescription.Text = dt.Rows(0).Item("Description").ToString
                txtLocationRemarks.Text = dt.Rows(0).Item("LocationRemarks").ToString
                txtCitizenCount.Text = dt.Rows(0).Item("CitizenCount").ToString
                txtTotalArea.Text = dt.Rows(0).Item("TotalArea").ToString
                txtLatitude.Text = dt.Rows(0).Item("Latitude").ToString
                txtLogitude.Text = dt.Rows(0).Item("longitude").ToString
                HiddenWorkAreaImg.Text = dt.Rows(0).Item("Logo").ToString
                rblAreas.SelectedValue = dt.Rows(0).Item("Type").ToString
                rblAreas_SelectedIndexChanged(Nothing, New EventArgs)
                SetDDLsValues(lblWorkAreaID.Text)
                FillImages()
                rblAreas.Enabled = False
                pnlControls.Visible = True
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

    Private Sub SetDDLsValues(ByVal WorkAreaID As String)
        Try
            Dim parentdt As DataTable = DBManager.Getdatatable("select * from vw_WorkAreaParent where Id=" & WorkAreaID)
            If parentdt.Rows.Count > 0 Then
                Dim IDs() As String = parentdt.Rows(0).Item("ParentPath").Split("-")
                Select Case IDs.Length
                    Case 0
                        pnlDDLs.Visible = False
                    Case 1
                        ddlMainDistribution.SelectedValue = Val(IDs(0))
                        ddlMainDistribution_SelectedIndexChanged(Nothing, New EventArgs)
                    Case 2
                        ddlMainDistribution.SelectedValue = IDs(0)
                        ddlMainDistribution_SelectedIndexChanged(Nothing, New EventArgs)
                        ddlgov.SelectedValue = IDs(1)
                        ddlgov_TextChanged(Nothing, New EventArgs)
                    Case 3
                        ddlMainDistribution.SelectedValue = IDs(0)
                        ddlMainDistribution_SelectedIndexChanged(Nothing, New EventArgs)
                        ddlgov.SelectedValue = IDs(1)
                        ddlgov_TextChanged(Nothing, New EventArgs)
                        ddlCenter.SelectedValue = IDs(2)
                        ddlCenter_TextChanged(Nothing, New EventArgs)
                    Case 4
                        ddlMainDistribution.SelectedValue = IDs(0)
                        ddlMainDistribution_SelectedIndexChanged(Nothing, New EventArgs)
                        ddlgov.SelectedValue = IDs(1)
                        ddlgov_TextChanged(Nothing, New EventArgs)
                        ddlCenter.SelectedValue = IDs(2)
                        ddlCenter_TextChanged(Nothing, New EventArgs)
                        ddlUnits.SelectedValue = IDs(3)
                End Select

            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Delete"
    ''' <summary>
    ''' Handle delete button form grid.
    ''' </summary>
    Protected Sub Delete(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            Dim AreaId As String = Sender.commandargument

            If DBManager.ExcuteQuery("update tblWorkArea SET  Isdeleted = 'True' where Id= '" + AreaId + "'") = 1 Then
                clsLogs.AddSystemLogs("Delete", "tblWorkArea", AreaId)
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
            Dim daTabeFactory As New TblWorkAreaFactory
            Dim dtTable As New TblWorkArea
            If cmdSave.CommandArgument = "add" Then
                If Not FillDT(dtTable) Then
                    Exit Sub
                End If

                If daTabeFactory.Insert(dtTable) Then
                    Dim RefId As String = PublicFunctions.GetIdentity("tblWorkArea")
                    clsLogs.AddSystemLogs("Insert", "tblWorkArea", RefId)
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    Exit Sub
                End If

            Else
                dtTable = daTabeFactory.GetAllBy(TblWorkArea.TblWorkAreaFields.Id, lblWorkAreaID.Text)(0)
                If Not FillDT(dtTable) Then
                    Exit Sub
                End If
                dtTable.Id = lblWorkAreaID.Text

                If daTabeFactory.Update(dtTable) Then
                    clsLogs.AddSystemLogs("Update", "tblWorkArea", lblWorkAreaID.Text)
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
    Protected Function FillDT(ByRef dtWorkArea As TblWorkArea) As Boolean
        Try
            dtWorkArea.Name = Name
            dtWorkArea.Description = Description
            dtWorkArea.LocationRemarks = LocationRemarks
            dtWorkArea.Logo = Photo
            If CitizenCount <> String.Empty Then
                dtWorkArea.CitizenCount = CitizenCount
            End If
            If TotalArea <> String.Empty Then
                dtWorkArea.TotalArea = TotalArea
            End If
            If Lat <> String.Empty Then
                dtWorkArea.Latitude = Lat
            End If
            If lng <> String.Empty Then
                dtWorkArea.Longitude = lng
            End If
            dtWorkArea.Type = rblAreas.SelectedValue
            Dim SelectedValue As String = rblAreas.SelectedValue
            Select Case SelectedValue
                Case "M"
                    dtWorkArea.ParentId = Nothing
                Case "T"
                    dtWorkArea.ParentId = ddlMainDistribution.SelectedValue
                Case "C"
                    dtWorkArea.ParentId = ddlgov.SelectedValue
                Case "U"
                    dtWorkArea.ParentId = ddlCenter.SelectedValue
                Case "V"
                    dtWorkArea.ParentId = ddlUnits.SelectedValue
            End Select

            If cmdSave.CommandArgument = "add" Then
                dtWorkArea.CreatedBy = UserId
                dtWorkArea.CreatedDate = DateTime.Now
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

#Region "Cancel"

    ''' <summary>
    ''' Handle cancel button click event
    ''' </summary>
    Protected Sub Cancel(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try

            imgSection.ImageUrl = "~/images/img-up.png"
            HiddenWorkAreaImg.Text = ""
            lblWorkAreaID.Text = ""
            rblAreas.ClearSelection()
            ddlMainDistribution.SelectedIndex = 0
            ddlgov.SelectedIndex = 0
            ddlCenter.SelectedIndex = 0
            ddlUnits.SelectedIndex = 0
            pnlDDLs.Visible = False
            pf.ClearAll(pnlControls)
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

#Region "Area Changes"

    Protected Sub rblAreas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim SelectedValue As String = rblAreas.SelectedValue
            pnlDDLs.Visible = True
            pnlControls.Visible = True
            ddlgov.Items.Clear()
            ddlgov.Items.Add(New ListItem("--اختر--", "0"))
            ddlCenter.Items.Clear()
            ddlCenter.Items.Add(New ListItem("--اختر--", "0"))
            ddlUnits.Items.Clear()
            ddlUnits.Items.Add(New ListItem("--اختر--", "0"))
            Select Case SelectedValue
                Case "M"
                    pnlDDLs.Visible = False
                Case "T"
                    pnlMainDistribution.Visible = True
                    pnlGov.Visible = False
                    pnlCenter.Visible = False
                    pnlUnits.Visible = False
                Case "C"
                    pnlMainDistribution.Visible = True
                    pnlGov.Visible = True
                    pnlCenter.Visible = False
                    pnlUnits.Visible = False
                Case "U"
                    pnlMainDistribution.Visible = True
                    pnlGov.Visible = True
                    pnlCenter.Visible = True
                    pnlUnits.Visible = False
                Case "V"
                    pnlMainDistribution.Visible = True
                    pnlGov.Visible = True
                    pnlCenter.Visible = True
                    pnlUnits.Visible = True
            End Select
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Protected Sub ddlMainDistribution_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim selected As String = ""
            If ddlMainDistribution.SelectedValue <> 0 Then
                selected = ddlMainDistribution.SelectedValue
            End If
            ddlgov.Items.Clear()
            ddlgov.Items.Add(New ListItem("--اختر--", "0"))
            Dim dtM As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where ParentId='" & selected & "' and isnull(isdeleted,0)=0")
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

    Protected Sub ddlgov_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim selected As String = ""
            If ddlgov.SelectedValue <> 0 Then
                selected = ddlgov.SelectedValue
            End If
            ddlCenter.Items.Clear()
            ddlCenter.Items.Add(New ListItem("--اختر--", "0"))
            Dim dt As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where ParentId='" & selected & "' and isnull(isdeleted,0)=0")
            If dt IsNot Nothing Then
                ddlCenter.DataTextField = "Name"
                ddlCenter.DataValueField = "Id"
                ddlCenter.AppendDataBoundItems = True
                ddlCenter.DataSource = dt
                ddlCenter.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Protected Sub ddlCenter_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim selected As String = ""
            If ddlCenter.SelectedValue <> 0 Then
                selected = ddlCenter.SelectedValue
            End If
            ddlUnits.Items.Clear()
            ddlUnits.Items.Add(New ListItem("--اختر--", "0"))
            Dim dt As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where ParentId='" & selected & "' and isnull(isdeleted,0)=0")
            If dt IsNot Nothing Then
                ddlUnits.DataTextField = "Name"
                ddlUnits.DataValueField = "Id"
                ddlUnits.AppendDataBoundItems = True
                ddlUnits.DataSource = dt
                ddlUnits.DataBind()
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
