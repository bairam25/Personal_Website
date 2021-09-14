#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class Projects
    Inherits System.Web.UI.Page

#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim ItemsImgs As New List(Of TblMedia)
    Dim daItemsImgs As New TblMediaFactory
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim UserId As String = "1"
    Dim WorkArea As String
    Dim Type As String
    Dim Description As String
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
            If b Then
                pnlForm.Attributes.Add("style", "display:block;")
            Else
                pnlForm.Attributes.Add("style", "display:none;")
            End If
            pnlList.Visible = Not b

            'pnlPhotos.Enabled = b
            pnlUpload.Visible = b
            ResetImgGridColumns(b)
            Dim Display As String = "block"
            If b = False Then
                Display = "none"
                'gvItemsImgs.DataSource = Nothing
                'gvItemsImgs.DataBind()
            End If
            'ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "newfile", "document.getElementById('pnlPhotos').style.display='" + Display + "';", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set Default controls Values 
    ''' </summary>
    Sub SetControlFields()
        Try
            WorkArea = Val(ddlWorkArea.SelectedValue)
            Type = Val(ddlType.SelectedValue)
            Description = txtDescription.Text
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
            Dim WorkAreaID As String = DirectCast(parent.FindControl("lblPlanID"), Label).Text
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
            Updated = DBManager.ExcuteQuery("Update tblWorkAreaPlans set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate(),ModifiedBy='" + UserId + "' where Id='" + WorkAreaID + "' ")
            If Updated = 1 Then
                clsLogs.AddSystemLogs(StatusName, "tblWorkAreaPlans", WorkAreaID)
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

#End Region

#Region "Fill Grid"

    ''' <summary>
    ''' Fill Listview with data.
    ''' </summary>
    Sub FillGrid(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select *,(select value from tblLookupValue where id=tblWorkAreaPlans.PlanType ) as _PlanType,(Select Name from tblworkArea where id=tblWorkAreaPlans.WorkAreaId) as _WorkArea from tblWorkAreaPlans  where isnull(isdeleted,0)=0 and " & CollectConditions(sender, e))
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then

                    ViewState("dtWorkAreaPlans") = dt
                    ' Initialize the sorting expression.
                    ViewState("SortExpression") = "ModifiedDate DESC"
                    ' Populate the GridView.
                    BindListView()
                    dplvProjects.Visible = False
                    If dt.Rows.Count > ddlPager.SelectedValue Then
                        dplvProjects.Visible = True
                    End If
                Else
                    lvProjects.DataSource = Nothing
                    lvProjects.DataBind()
                    dplvProjects.Visible = False
                End If
                lblTotalCount.Text = dt.Rows.Count
            End If
            'ScriptManager.RegisterClientScriptBlock(UP, Me.[GetType](), Guid.NewGuid().ToString(), "LoadJquery();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Collect condition string to fill lvProjects
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
            If ViewState("dtWorkAreaPlans") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtWorkAreaPlans As DataTable = DirectCast(ViewState("dtWorkAreaPlans"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtWorkAreaPlans)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("SortExpression").ToString()

                ' Bind the Listview control.
                lvProjects.DataSource = dv
                lvProjects.DataBind()
                If dtWorkAreaPlans.Rows.Count > 0 Then
                    dplvProjects.DataBind()
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
            dplvProjects.SetPageProperties(0, ddlPager.SelectedValue, True)
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
            dplvProjects.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
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
                CType(lvProjects.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faDown")
            Else
                CType(lvProjects.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faUp")
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    Sub ResetArrows()
        Try
            Dim i As Integer = 0
            While i < lvProjects.Items.Count
                CType(lvProjects.FindControl("_WorkArea"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvProjects.FindControl("PlanType"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvProjects.FindControl("ActiveHeader"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
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
            lblPlanID.Text = ""
            imgSection.ImageUrl = "~/images/img-up.png"
            HiddenWorkAreaImg.Text = ""
            pf.ClearAll(pnlForm)
            ddlWorkArea.SelectedIndex = 0
            ddlType.SelectedIndex = 0
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
            lblPlanID.Text = Sender.commandargument.ToString
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
            dt = DBManager.Getdatatable("select * from tblWorkAreaPlans where isnull(Isdeleted,0)=0  and id='" + lblPlanID.Text + "'")
            If dt.Rows.Count <> 0 Then
                ddlWorkArea.SelectedValue = dt.Rows(0).Item("WorkAreaId").ToString
                ddlType.SelectedValue = dt.Rows(0).Item("PlanType").ToString
                txtDescription.Text = dt.Rows(0).Item("Description").ToString
                HiddenWorkAreaImg.Text = dt.Rows(0).Item("Photo").ToString
                'Fill Project Logo
                FillImages()

                'Fill Project Files
                FillProjectFiles(lblPlanID.Text)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

    Private Sub FillProjectFiles(ProjID As String)
        Try
            Dim dtFiles As DataTable = DBManager.Getdatatable("select *,Path as MediaPath, 0 as Main , 0 as ShowOrder, '' as Description from tblDocuments where SourceId='" & ProjID & "' and TableName='tblWorkAreaPlans' and isnull(isdeleted,0)=0")
            gvItemsImgs.DataSource = dtFiles
            gvItemsImgs.DataBind()
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
            Dim PlanID As String = Sender.commandargument

            If DBManager.ExcuteQuery("update tblWorkAreaPlans SET  Isdeleted = 'True' where Id= '" + PlanID + "'") = 1 Then
                clsLogs.AddSystemLogs("Delete", "tblWorkAreaPlans", PlanID)
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
            Dim daTabeFactory As New TblWorkAreaPlansFactory
            Dim dtTable As New TblWorkAreaPlans
            Dim daDetails As New TblProjectDetailsFactory
            If cmdSave.CommandArgument = "add" Then
                If Not FillDT(dtTable) Then
                    Exit Sub
                End If
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                If daTabeFactory.InsertTrans(dtTable, _sqlconn, _sqltrans) Then
                    Dim ProjectID As String = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                    dtTable.Id = ProjectID

                    'Save Project Files
                    If Not SaveProjectFiles(ProjectID, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                        Exit Sub
                    End If
                Else
                    _sqltrans.Rollback()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    Exit Sub
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                clsLogs.AddSystemLogs("Insert", "tblWorkAreaPlans", dtTable.Id.ToString)
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
            Else
                dtTable = daTabeFactory.GetAllBy(TblWorkAreaPlans.TblWorkAreaPlansFields.Id, lblPlanID.Text)(0)
                If Not FillDT(dtTable) Then
                    Exit Sub
                End If
                dtTable.Id = lblPlanID.Text
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                If daTabeFactory.UpdateTrans(dtTable, _sqlconn, _sqltrans) Then
                    daDetails.DeleteTrans(TblProjectDetails.TblProjectDetailsFields.ProjectId, lblPlanID.Text, _sqlconn, _sqltrans)

                    'Save Project Files
                    If Not SaveProjectFiles(lblPlanID.Text, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                        Exit Sub
                    End If
                Else
                    _sqltrans.Rollback()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    Exit Sub
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                clsLogs.AddSystemLogs("Update", "tblWorkAreaPlans", lblPlanID.Text)
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Me.Page)
            End If
            Cancel(Sender, New EventArgs)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Fill dtSection from controls in the panel form.
    ''' </summary>
    Protected Function FillDT(ByRef dtWorkAreaPlans As TblWorkAreaPlans) As Boolean
        Try
            dtWorkAreaPlans.WorkAreaId = WorkArea
            dtWorkAreaPlans.Description = Description
            dtWorkAreaPlans.Photo = Photo
            dtWorkAreaPlans.PlanType = Type

            If cmdSave.CommandArgument = "add" Then
                dtWorkAreaPlans.CreatedBy = UserId
                dtWorkAreaPlans.CreatedDate = DateTime.Now
            End If
            dtWorkAreaPlans.ModifiedBy = UserId
            dtWorkAreaPlans.ModifiedDate = DateTime.Now
            dtWorkAreaPlans.IsDeleted = False
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Insert Project Files
    ''' </summary>
    Private Function SaveProjectFiles(projectID As String, _sqlconn As SqlConnection, _sqltrans As SqlTransaction) As Boolean
        Try
            Dim daDoc As New TblDocumentsFactory
            Dim dtDoc As New TblDocuments
            If cmdSave.CommandArgument <> "add" Then
                'delete old files in case of update
                If Not ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("delete from TblDocuments where SourceId='" & projectID & "' and TableName='tblWorkAreaPlans'")) Then
                    Return False
                End If
            End If
            'loop to get images details from images gridview
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                dtDoc.SourceId = projectID
                dtDoc.Path = CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                dtDoc.Title = CType(gvRow.FindControl("txtTitle"), System.Web.UI.WebControls.TextBox).Text
                dtDoc.TableName = "tblWorkAreaPlans"
                dtDoc.Category = Nothing 'PublicFunctions.getlo("ProjectDocs", "PlanType")
                dtDoc.CreatedDate = DateTime.Now
                dtDoc.IsDeleted = False
                If Not daDoc.InsertTrans(dtDoc, _sqlconn, _sqltrans) Then
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                    Return False
                End If
            Next
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
            lblPlanID.Text = ""
            ddlType.SelectedIndex = 0
            Enabler(False)
            FillGrid(Sender, e)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Area Changes"
    Private Sub FillDDLs()
        Try
            'Fill Plan Types
            clsBindDDL.BindLookupDDLs("PlanType", ddlType, True, "--اختر--")
            'Fill Work Areas
            Dim dtM As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where Type='T' and isnull(isdeleted,0)=0")
            If dtM IsNot Nothing Then
                ddlWorkArea.DataTextField = "Name"
                ddlWorkArea.DataValueField = "Id"
                ddlWorkArea.AppendDataBoundItems = True
                ddlWorkArea.DataSource = dtM
                ddlWorkArea.DataBind()
            End If
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

                Dim filePath As String = "~/PlanFiles/" & fuPhoto.FileName

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

    Protected Sub gvItemsImgs_DataBound(sender As Object, e As EventArgs)
        For Each gvRow As GridViewRow In gvItemsImgs.Rows
            Dim lblImg As String = DirectCast(gvRow.FindControl("lblImg"), Image).ImageUrl
            If lblImg.ToString.Split(".").Last.ToLower = "doc" OrElse lblImg.ToString.Split(".").Last.ToLower = "docx" Then
                CType(gvRow.FindControl("lblMedia"), System.Web.UI.WebControls.Image).ImageUrl = "images/word.png"
            ElseIf lblImg.ToString.Split(".").Last.ToLower = "pdf" Then
                CType(gvRow.FindControl("lblMedia"), System.Web.UI.WebControls.Image).ImageUrl = "images/pdf_icon.jpg"
            ElseIf lblImg.ToString.Split(".").Last.ToLower = "xls" OrElse lblImg.ToString.Split(".").Last.ToLower = "xlsx" Then
                CType(gvRow.FindControl("lblMedia"), System.Web.UI.WebControls.Image).ImageUrl = "images/icon-xlsx.png"
            Else
                CType(gvRow.FindControl("lblMedia"), System.Web.UI.WebControls.Image).ImageUrl = lblImg
            End If

        Next
    End Sub
#End Region

#Region "Files Upload"
    ''' <summary>
    '''Fill Photos gridview.
    ''' </summary>
    Sub FillPhotos(ByVal SourceId As String)
        Try
            Dim dtItemImgs As DataTable = DBManager.Getdatatable("select * from tblDocuments where isnull(Isdeleted,0)=0 and  SourceId='" & SourceId & "' and TableName='tblWorkAreaPlans'")
            If dtItemImgs.Rows.Count > 0 Then
                gvItemsImgs.DataSource = dtItemImgs
                gvItemsImgs.DataBind()

                'Fill DDl ShowOrder with all values
                ItemsImgs = GetItemImgs()
                BindShowOrder()
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub

    ''' <summary>
    '''Add Uploaded photos to the gridview.
    ''' </summary>
    Protected Sub AddFiles(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dt As DataTable = MultiFileUpload.getUploadedFilesDetails(lblUploadedFilesDetails.Value)
            If dt IsNot Nothing Then
                ItemsImgs = GetItemImgs()
                If dt.Rows.Count > 0 Then
                    Dim i As Integer = 0
                    While i < dt.Rows.Count
                        Dim dtDoc As New TblMedia
                        dtDoc.MediaPath = "~/" + dt(i)("URL").ToString
                        dtDoc.Id = i + 1
                        ItemsImgs.Add(dtDoc)
                        i += 1
                    End While
                    gvItemsImgs.Visible = True
                    gvItemsImgs.DataSource = ItemsImgs
                    gvItemsImgs.DataBind()
                    'Fill Order DDL and set selected value based on row index
                    BindShowOrder()
                End If
                lblUploadedFilesDetails.Value = ""
                BindMainImg()
            Else
                gvItemsImgs.DataSource = Nothing
                gvItemsImgs.DataBind()
            End If

        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub

    ''' <summary>
    '''Check radio button with main photo
    ''' </summary>
    Sub BindShowOrder()
        Try
            Dim OrderList As New List(Of ListItem)
            Dim c As Integer = 0
            While c < ItemsImgs.Count
                Dim itm As New ListItem
                itm.Text = c + 1
                itm.Value = c + 1
                OrderList.Add(itm)
                Dim lblShowOrder = DirectCast(gvItemsImgs.Rows(c).FindControl("lblShowOrder"), Label).Text
                'If empty (case add new photo) or not exist in ddl after delete OrElse Not OrderList.Contains(New ListItem(lblShowOrder)) 
                If lblShowOrder = String.Empty Then
                    DirectCast(gvItemsImgs.Rows(c).FindControl("lblShowOrder"), Label).Text = c + 1
                End If
                c += 1
            End While

            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                Dim lblOrder As Label = DirectCast(gvRow.FindControl("lblShowOrder"), Label)
                Dim dd As DropDownList = DirectCast(gvRow.FindControl("ddlShowOrder"), DropDownList)
                dd.DataSource = OrderList
                dd.DataBind()
                dd.SelectedValue = lblOrder.Text
            Next
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub

    ''' <summary>
    '''Check radio button with main photo
    ''' </summary>
    Sub BindMainImg()
        Try
            'Dim dtItems As New DataTable
            'dtItems = DBManager.Getdatatable("select MediaPath from tblMedia where Main='1' and SourceId='" + lblPlanID.Text + "'")
            'If dtItems.Rows.Count <> 0 Then
            '    Dim Photo = dtItems.Rows(0).Item("MediaPath").ToString
            '    SelectMainImg(Photo)
            'Else
            SelectMainImg("0")
            'End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub
    Sub SelectMainImg(ByVal img As String)
        Try
            Dim Count As Integer = 0
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                Dim chk As RadioButton = DirectCast(gvRow.FindControl("rblSelect"), RadioButton)
                If CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl = img Then
                    chk.Checked = True
                    Count += 1
                End If
            Next
            If Count = 0 Then
                For Each gvRow As GridViewRow In gvItemsImgs.Rows
                    Dim chk As RadioButton = DirectCast(gvRow.FindControl("rblSelect"), RadioButton)
                    chk.Checked = True
                    Exit Sub
                Next
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub
    ''' <summary>
    '''check if there are exist photos and get them to add the new photos above them
    ''' </summary>
    Private Function GetItemImgs() As List(Of TblMedia)
        Dim DocList As New List(Of TblMedia)
        Try
            Dim AttObj As TblMedia
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                AttObj = New TblMedia
                AttObj.Id = CType(gvRow.FindControl("lblId"), Label).Text
                AttObj.MediaPath = CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                AttObj.ShowOrder = CType(gvRow.FindControl("lblShowOrder"), System.Web.UI.WebControls.Label).Text
                AttObj.Title = CType(gvRow.FindControl("txtTitle"), System.Web.UI.WebControls.TextBox).Text
                AttObj.Description = CType(gvRow.FindControl("txtDescription"), System.Web.UI.WebControls.TextBox).Text
                AttObj.CreatedDate = Date.Now
                AttObj.Main = CType(gvRow.FindControl("rblSelect"), RadioButton).Checked
                AttObj.MediaType = "I"
                AttObj.IsDeleted = False
                DocList.Add(AttObj)
            Next
            Return DocList
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
            Return DocList
        End Try
    End Function
    ''' <summary>
    '''Delete Image from gridview.
    ''' </summary>
    Protected Sub DeleteImg(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim parent As GridViewRow = sender.parent.parent.parent
            Dim dt As New List(Of TblMedia)
            Try
                dt = GetItemImgs()
                dt.Remove(dt.Item(parent.RowIndex))
                gvItemsImgs.DataSource = dt
                gvItemsImgs.DataBind()
                BindMainImg()
                ItemsImgs = GetItemImgs()
                'Reset DDL Show Order
                Dim c As Integer = 0
                While c < ItemsImgs.Count
                    DirectCast(gvItemsImgs.Rows(c).FindControl("lblShowOrder"), Label).Text = c + 1
                    c += 1
                End While
                BindShowOrder()
            Catch ex As Exception
                clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
            End Try
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub
    ''' <summary>
    '''return Main Image to save it at tblitems.
    ''' </summary>
    Function getMainImg() As String
        Dim Count As Integer = 0
        Try
            If gvItemsImgs.Rows.Count > 0 Then
                For Each gvRow As GridViewRow In gvItemsImgs.Rows
                    Dim chk As RadioButton = DirectCast(gvRow.FindControl("rblSelect"), RadioButton)
                    If chk.Checked Then
                        Count += 1
                        Return CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                    End If
                Next
                Return "0"
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
            Return String.Empty
        End Try
    End Function
    ''' <summary>
    '''allow user to select only one image.
    ''' </summary>
    Protected Sub SelectRBL(ByVal sender As Object, ByVal e As EventArgs)
        Try
            For Each row As GridViewRow In gvItemsImgs.Rows
                Dim chk As RadioButton = DirectCast(row.FindControl("rblSelect"), RadioButton)
                If chk.ClientID <> sender.ClientID Then
                    chk.Checked = False
                End If
            Next
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub
    ''' <summary>
    ''' View Photos of news.
    ''' </summary>
    Protected Sub ViewPhotos(ByVal sender As Object, ByVal e As EventArgs)
        Try


        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub

    ''' <summary>
    ''' Handle Changing photo order
    ''' </summary>
    Protected Sub ddlShowOrder_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim Parent = sender.Parent
            Dim NewOrderValue As String = CType(Parent.FindControl("ddlShowOrder"), DropDownList).SelectedValue
            Dim OldOrder As Label = DirectCast(Parent.FindControl("lblShowOrder"), Label)
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                Dim lblOrder As Label = DirectCast(gvRow.FindControl("lblShowOrder"), Label)
                Dim ddl As DropDownList = DirectCast(gvRow.FindControl("ddlShowOrder"), DropDownList)
                If NewOrderValue = lblOrder.Text Then
                    If ddl.Items.Count > 0 Then
                        If ddl.Items.Contains(New ListItem(OldOrder.Text)) Then
                            ddl.SelectedValue = OldOrder.Text
                            lblOrder.Text = OldOrder.Text
                        End If
                    End If
                End If
            Next
            OldOrder.Text = NewOrderValue
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try

    End Sub
    Sub ResetImgGridColumns(ByVal b As Boolean)
        Try
            Dim i As Integer
            While i < gvItemsImgs.Columns.Count
                If gvItemsImgs.Columns(i).HeaderText = "Delete" Then
                    gvItemsImgs.Columns(i).Visible = b
                End If
                i += 1
            End While
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


#End Region

#Region "Permissions"
    Private Sub ListView_DataBound(sender As Object, e As EventArgs) Handles lvProjects.DataBound
        Try
            Permissions.CheckPermisions(lvProjects, cmdNew, txtSearch, cmdSearch, Me.Page, UserId)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region
End Class
