#Region "Signature"
'################################### Signature ######################################
'############# Date:02-07-2019
'############# Form Name: Gallary 
'############# Your Name: Ahmed Adel
'################################ End of Signature ##################################
#End Region

#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class Gallery
    Inherits System.Web.UI.Page
#Region "Global_Varaibles"
    Dim UserID As String = "0"
    Dim pf As New PublicFunctions
    Dim ShowOrder As String
    Dim AlbumDate As String
    Dim STitle As String
    Dim Category As String
    Dim SubCategory As String
    Dim Description As String
    Dim ShowInHome As Boolean
    Dim AlbumTable As String = "select * from vw_Allbum"
    Dim ItemsImgs As New List(Of TblAlbumDetails)
    Dim daItemsImgs As New TblAlbumDetailsFactory
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region

#Region "Public Functions"


    ''' <summary>
    ''' hide and show panels of values
    ''' </summary>
    Protected Sub Enabler(ByVal b As Boolean)
        Try
            pnlForm.Visible = b
            pnlList.Visible = Not b


            pnlConfirm.Visible = b
            pnlOps.Visible = Not b
            pnlForm.Enabled = b
            pnlForm.Visible = b

            liSave.Visible = b
            pnlForm.Enabled = b
            pnlPhotos.Enabled = b
            pnlUpload.Visible = b
            ResetImgGridColumns(b)
            Dim Display As String = "block"
            If b = False Then
                Display = "none"
                gvItemsImgs.DataSource = Nothing
                gvItemsImgs.DataBind()
            End If
            ScriptManager.RegisterClientScriptBlock(Me, Me.[GetType](), "newfile", "document.getElementById('pnlPhotos').style.display='" + Display + "';", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set Default controls Values 
    ''' </summary>
    Sub SetControlFields()
        Try
            'Category = ddlCategory.SelectedValue
            'SubCategory = ddlSubCategory.SelectedValue
            ShowOrder = txtShowOrder.Text
            STitle = txtTitle.Text
            Description = txtDescription.Text
            AlbumDate = PublicFunctions.DateFormat(txtAlbumDate.Text, "dd/MM/yyyy")
            ShowInHome = chkShowInHome.Checked
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
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

#Region "Page_Load"
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            UserID = PublicFunctions.GetUserId(Page)
            lblResPopupImages.Visible = False
            lblRes.Visible = False
            lblResPopupImages.Visible = False
            If Not Page.IsPostBack Then
                FillGrid(sender, e)
                '2) Fill Category DDL
                'clsBindDDL.BindLookupDDLs("GalleryCategory", ddlCategory, True, "-- أختر --")
            Else
                If pnlForm.Visible Then
                    ScriptManager.RegisterClientScriptBlock(up, Me.[GetType](), "MyAction", "document.getElementById('pnlPhotos').style.display='Block';", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(up, Me.[GetType](), "MyAction", "document.getElementById('pnlPhotos').style.display='None';", True)
                End If
            End If
            SetControlFields()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Add"
    ''' <summary>
    ''' Add new album
    ''' </summary>
    Protected Sub Add(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "add"
            Enabler(True)
            pf.ClearAll(pnlForm)
            'Reset Category & Sub Category
            'ddlCategory.SelectedIndex = 0
            'CategoryChanged(Sender, e)
            txtAlbumDate.Text = DateTime.Now.ToShortDateString
            chkActive.Checked = True
            txtShowOrder.Text = Val(DBManager.Getdatatable("select count(*) from TblAlbum where Type='A' and isnull(IsDeleted,0)=0 ").Rows(0).Item(0).ToString) + 1
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' handles change account category and bind the related sub categories
    ''' in case of not selected , remove sub categories items and show -- أختر -- text with 0 value
    ''' </summary>
    Protected Sub CategoryChanged(ByVal Sender As Object, ByVal e As System.EventArgs)
        'If ddlCategory.SelectedValue = 0 Then
        '    ddlSubCategory.Items.Clear()
        '    ddlSubCategory.Items.Add(New ListItem("-- أختر --", 0))
        '    Return
        'End If
        'clsBindDDL.BindLookupDDLs("GallerySubCategory", ddlSubCategory, True, "-- أختر --", "ASC", ddlCategory.SelectedValue)
    End Sub
#End Region

#Region "Save"
    ''' <summary>
    ''' Save Album 
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim daAlbum As New TblAlbumFactory
        Dim dtAlbum As New TblAlbum
        Try
            If cmdSave.CommandArgument.ToLower() = "add" Then
                'Insert Case
                'Fill object of Album
                If Not FillDT(dtAlbum) Then
                    Return
                End If
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                If daAlbum.InsertTrans(dtAlbum, _sqlconn, _sqltrans) Then
                    'Add Album Media
                    If SaveAlbumMedia(dtAlbum, "Insert") Then
                        clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Page)
                        _sqltrans.Commit()
                        _sqlconn.Close()
                    Else
                        _sqltrans.Rollback()
                        clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                        Return
                    End If
                Else
                    _sqltrans.Rollback()
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                    Return
                End If
            Else
                'Update Case
                dtAlbum = daAlbum.GetAllBy(TblAlbum.TblAlbumFields.Id, lblAlbumId.Text)(0)
                'Fill object with new values
                If Not FillDT(dtAlbum) Then
                    Return
                End If
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                If daAlbum.UpdateTrans(dtAlbum, _sqlconn, _sqltrans) Then
                    'Update Album Media
                    If SaveAlbumMedia(dtAlbum, "Update") Then
                        clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Page)
                        _sqltrans.Commit()
                        _sqlconn.Close()
                    Else
                        _sqltrans.Rollback()
                        clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                        Return
                    End If
                Else
                    _sqltrans.Rollback()
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
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
    ''' Fill Album value Object
    ''' </summary>
    Function FillDT(ByVal dtAlbum As TblAlbum) As Boolean
        Try
            '1)Validate 
            If Not IsNumeric(ShowOrder) Then
                clsMessages.ShowInfoMessgage(lblRes, "Invalid Order", Me)
                txtShowOrder.Focus()
                Return False
            End If
            If Not IsDate(AlbumDate) Then
                clsMessages.ShowInfoMessgage(lblRes, "Invalid Date", Me)
                txtAlbumDate.Focus()
                Return False
            End If
            If gvItemsImgs.Rows.Count = 0 Then
                clsMessages.ShowInfoMessgage(lblRes, "Please upload media files", Me)
                Return False
            End If
            'dtAlbum.Category = Category
            'dtAlbum.SubCategory = SubCategory
            dtAlbum.ShowInHome = ShowInHome
            dtAlbum.Type = "A"
            dtAlbum.ShowOrder = ShowOrder
            dtAlbum.Date = AlbumDate
            dtAlbum.Title = STitle
            dtAlbum.Description = Description
            dtAlbum.MediaCount = gvItemsImgs.Rows.Count
            If cmdSave.CommandArgument.ToLower() = "add" Then
                dtAlbum.CreatedDate = DateTime.Now
            End If
            dtAlbum.Active = chkActive.Checked
            dtAlbum.ModifiedDate = DateTime.Now
            dtAlbum.IsDeleted = False
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Fill tblAlbumDetails from controls in the panel photos.
    ''' </summary>
    Protected Function SaveAlbumMedia(dtTable As TblAlbum, ByVal Operation As String) As Boolean
        Dim daImgs As New TblAlbumDetailsFactory
        Dim dtImgs As New TblAlbumDetails
        Dim AlbumId As Integer = 0
        Try
            Select Case Operation.ToLower
                Case "insert"
                    AlbumId = dtTable.Id
                Case "update"
                    AlbumId = lblAlbumId.Text
                    'delete old images in case of update
                    ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("delete from tblAlbumDetails where AlbumId='" & AlbumId & "'"))
                    'daImgs.DeleteTrans(tblAlbumDetails.tblAlbumDetailsFields.AlbumId, AlbumId, _sqlconn, _sqltrans)
            End Select
            'loop to get images details from images gridview
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                dtImgs.AlbumId = AlbumId
                'dtImgs.Type = "A"
                dtImgs.Path = CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                dtImgs.IsURL = False
                dtImgs.ShowOrder = CType(gvRow.FindControl("ddlShowOrder"), System.Web.UI.WebControls.DropDownList).SelectedValue
                dtImgs.Title = CType(gvRow.FindControl("txtTitle"), System.Web.UI.WebControls.TextBox).Text
                dtImgs.Description = CType(gvRow.FindControl("txtDescription"), System.Web.UI.WebControls.TextBox).Text
                dtImgs.CreatedDate = Date.Now
                dtImgs.Main = CType(gvRow.FindControl("rblSelect"), RadioButton).Checked
                dtImgs.Type = IIf(dtImgs.Path.ToString.Split(".").Last.ToLower = "mp4" OrElse dtImgs.Path.ToString.Split(".").Last.ToLower = "wmv" OrElse dtImgs.Path.ToString.Split(".").Last.ToLower = "webm", "V", "I")
                'If dtImgs.Type = "I" Then
                '    dtImgs.Path = dtImgs.Path.Replace(dtImgs.Path.Split("/").Last, "Thumb_" + dtImgs.Path.Split("/").Last)
                'End If
                dtImgs.IsDeleted = False
                If Not daImgs.InsertTrans(dtImgs, _sqlconn, _sqltrans) Then
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

#Region "Edit"
    ''' <summary>
    ''' Handle Update Event
    ''' Get Object details and Populate it in pnl forms
    ''' </summary>
    Protected Sub Edit(ByVal Sender As Object, ByVal e As System.EventArgs)
        cmdSave.CommandArgument = "Edit"
        lblAlbumId.Text = Sender.commandargument
        pf.ClearAll(pnlForm)
        Try
            Dim dt As DataTable = DBManager.Getdatatable(AlbumTable & " where Type='A' and Id ='" & lblAlbumId.Text & "' ")
            If dt.Rows.Count <> 0 Then
                txtTitle.Text = dt.Rows(0).Item("Title").ToString
                txtDescription.Text = dt.Rows(0).Item("Description").ToString
                txtShowOrder.Text = dt.Rows(0).Item("ShowOrder").ToString
                txtAlbumDate.Text = PublicFunctions.DateFormat(dt.Rows(0).Item("Date").ToString, "dd/MM/yyyy")
                chkShowInHome.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("ShowInHome").ToString)
                chkActive.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("Active"))

                'Dim Category As String = dt.Rows(0).Item("Category").ToString
                'Dim SubCategory As String = dt.Rows(0).Item("SubCategory").ToString
                'If ddlCategory.Items.FindByValue(Category) IsNot Nothing Then
                '    ddlCategory.SelectedValue = Category
                'End If
                'CategoryChanged(Nothing, New EventArgs)
                'If ddlSubCategory.Items.FindByValue(SubCategory) IsNot Nothing Then
                '    ddlSubCategory.SelectedValue = SubCategory
                'End If
                'Fill Album Media
                FillPhotos(lblAlbumId.Text)
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
        Dim AlbumId As String = Sender.commandargument.ToString
        Try
            If DBManager.ExcuteQuery("update TblAlbum SET Isdeleted = 'True',DeletedDate=GETDATE() where Id= '" & AlbumId & "';Update tblAlbumDetails SET Isdeleted = 'True',DeletedDate=GETDATE() where AlbumId= '" & AlbumId & "'") = 1 Then
                FillGrid(Sender, e)
                clsMessages.ShowMessage(lblRes, MessageTypesEnum.Delete, Me)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Handle Delete Event
    ''' </summary>
    Protected Sub DeletePhoto(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim parent = Sender.parent.parent
        Dim MediaId As String = Sender.commandargument.ToString
        Dim AlbumId As String = DirectCast(parent.FindControl("lblAlbumId"), Label).Text
        Dim Main As Boolean = PublicFunctions.BoolFormat(DirectCast(parent.FindControl("lblMain"), Label).Text)
        Try
            'Validate at least one media row in list for album
            If lvImages.Items.Count = 1 Then
                clsMessages.ShowInfoMessgage(lblResPopupImages, "At least one media file in album!", Me)
                mpPopupImgs.Show()
                Return
            End If
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction

            If ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("update tblAlbumDetails SET Isdeleted = 'True',DeletedDate=GETDATE() where Id= '" & MediaId & "'  and AlbumId='" & AlbumId & "';update TblAlbum SET MediaCount=MediaCount-1 where Id= '" & AlbumId & "'")) Then
                'Update MediaCount feild in the main table
                BindAlbumMedia(AlbumId, _sqlconn, _sqltrans)
                'If Main is deleting , then set first row as the new main one
                If Main Then
                    Dim FirstPhotoId As String = DirectCast(lvImages.Items(0).FindControl("lblMediaId"), Label).Text
                    If Not ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("update tblAlbumDetails SET Main=1 where Id= '" & FirstPhotoId & "' and AlbumId='" & AlbumId & "' ")) Then
                        _sqltrans.Rollback()
                        clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                    End If
                End If

                'Reset Show Order
                For Each gvRow As ListViewItem In lvImages.Items
                    Dim id = DirectCast(gvRow.FindControl("lblMediaId"), Label).Text
                    Dim serialNo = DirectCast(gvRow.FindControl("srialNo"), Label).Text
                    If Not ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("update tblAlbumDetails SET ShowOrder = " & serialNo & " where Id= '" & id & "' and AlbumId='" & AlbumId & "'")) Then
                        _sqltrans.Rollback()
                        clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                    End If
                Next
                BindAlbumMedia(AlbumId, _sqlconn, _sqltrans)
                _sqltrans.Commit()
                _sqlconn.Close()
                clsMessages.ShowMessage(lblResPopupImages, MessageTypesEnum.Delete, Me)
            Else
                _sqltrans.Rollback()
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
            End If
        Catch ex As Exception
            _sqltrans.Rollback()
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Private Sub BindAlbumMedia(ByVal AlbumId As String, ByRef _sqlconn As SqlConnection, ByRef _sqltrans As SqlTransaction)
        Try
            Dim dtNewsImgs As DataTable = ExecuteQuery.ExecuteQueryAndReturnDataTable("select Id,AlbumId,ShowOrder,Main,Path from tblAlbumDetails where isnull(Isdeleted,0)=0 and AlbumId='" + AlbumId + "'  ", _sqlconn, _sqltrans)
            lvImages.DataSource = dtNewsImgs
            lvImages.DataBind()
            mpPopupImgs.Show()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub CheckAll(ByVal sender As CheckBox, e As EventArgs)
        Try
            For Each item As ListViewItem In lvGallery.Items
                CType(item.FindControl("chkSelect"), CheckBox).Checked = sender.Checked
            Next
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub DeleteAll(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            If PublicFunctions.DeleteAllSelected(lvGallery) Then
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

#Region "Fill Grid"

    ''' <summary>
    ''' Fill Listview with data.
    ''' </summary>
    Sub FillGrid(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dt As DataTable = DBManager.Getdatatable(AlbumTable & " where Type='A' and " & CollectConditions(sender, e))
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    ViewState("dtAlbumTable") = dt
                    ' Initialize the sorting expression.
                    ViewState("SortExpression") = "ShowOrder DESC"
                    ' Populate the GridView.
                    If sender.parent IsNot Nothing Then
                        If sender.parent.clientid = "pnlOps" Or sender.id.ToString.ToLower.Contains("delete") Then
                            'Reset Pager to First Index
                            dplvGallery.SetPageProperties(0, ddlPager.SelectedValue, True)
                        End If
                    End If
                    BindListView()
                    dplvGallery.Visible = False
                    If dt.Rows.Count > ddlPager.SelectedValue Then
                        dplvGallery.Visible = True
                    End If
                Else
                    lvGallery.DataSource = Nothing
                    lvGallery.DataBind()
                    dplvGallery.Visible = False
                End If
                lblTotalCount.Text = dt.Rows.Count
            End If



            'ScriptManager.RegisterClientScriptBlock(UP, Me.[GetType](), Guid.NewGuid().ToString(), "LoadJquery();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Collect condition string to fill lvGallery
    ''' </summary>
    Public Function CollectConditions(ByVal sender As Object, ByVal e As System.EventArgs) As String
        txtSearch.Text = txtSearch.Text.TrimStart.TrimEnd
        Dim Search As String = IIf(txtSearch.Text = String.Empty, "1=1", "(Title like N'%" & txtSearch.Text & "%' )")

        Return Search

    End Function


    ''' <summary>
    ''' Load data into the Listview.
    ''' </summary>
    Private Sub BindListView()
        Try
            If ViewState("dtAlbumTable") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtAlbumTable As DataTable = DirectCast(ViewState("dtAlbumTable"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtAlbumTable)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("SortExpression").ToString()

                ' Bind the Listview control.
                lvGallery.DataSource = dv
                lvGallery.DataBind()
                If dtAlbumTable.Rows.Count > 0 Then
                    dplvGallery.DataBind()
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
            dplvGallery.SetPageProperties(0, ddlPager.SelectedValue, True)
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
            dplvGallery.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
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
                CType(lvGallery.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faDown")
            Else
                CType(lvGallery.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faUp")
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    Sub ResetArrows()
        Try
            Dim i As Integer = 0
            While i < lvGallery.Items.Count
                CType(lvGallery.FindControl("AlbumDate"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvGallery.FindControl("Title"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvGallery.FindControl("MediaCount"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvGallery.FindControl("ShowOrder"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
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
            Dim ItemId As String = DirectCast(parent.FindControl("lblAlbumId"), Label).Text
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
            Updated = DBManager.ExcuteQuery("Update TblAlbum set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate() where Id='" + ItemId + "' ")
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
            Dim ItemId As String = DirectCast(parent.FindControl("lblAlbumId"), Label).Text
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
            Updated = DBManager.ExcuteQuery("Update TblAlbum set ShowInHome ='" + chk.Checked.ToString + "',ModifiedDate=getdate() where Id='" + ItemId + "' ")
            If Updated = 1 Then
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.CUSTOMSuccess, Page, Nothing, MSG)
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
            lblAlbumId.Text = String.Empty
            pf.ClearAll(pnlForm)
            Enabler(False)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Photo Upload"
    ''' <summary>
    '''Fill Photos gridview.
    ''' </summary>
    Sub FillPhotos(ByVal AlbumId As String)
        Try
            Dim dtItemImgs As DataTable = DBManager.Getdatatable("select * from tblAlbumDetails where isnull(Isdeleted,0)=0 and  AlbumId='" & AlbumId & "' ")
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
                        Dim dtDoc As New TblAlbumDetails
                        dtDoc.Path = "~/" + dt(i)("URL").ToString
                        dtDoc.Type = dt(i)("Type").ToString
                        dtDoc.Id = i + 1
                        If dtDoc.Type = "jpeg" Or dtDoc.Type = "jpg" Or dtDoc.Type = "png" Or dtDoc.Type = "gif" Then
                            ItemsImgs.Add(dtDoc)
                        End If
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
            Dim dtItems As New DataTable
            dtItems = DBManager.Getdatatable("select Path from tblAlbumDetails where Main='1' and AlbumId='" + lblAlbumId.Text + "'")
            If dtItems.Rows.Count <> 0 Then
                Dim Photo = dtItems.Rows(0).Item("Path").ToString
                SelectMainImg(Photo)
            Else
                SelectMainImg("0")
            End If
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
    Private Function GetItemImgs() As List(Of TblAlbumDetails)
        Dim DocList As New List(Of TblAlbumDetails)
        Try
            Dim AttObj As TblAlbumDetails
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                AttObj = New TblAlbumDetails
                AttObj.Id = CType(gvRow.FindControl("lblId"), Label).Text
                AttObj.Path = CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                AttObj.ShowOrder = CType(gvRow.FindControl("lblShowOrder"), System.Web.UI.WebControls.Label).Text
                AttObj.Title = CType(gvRow.FindControl("txtTitle"), System.Web.UI.WebControls.TextBox).Text
                AttObj.Description = CType(gvRow.FindControl("txtDescription"), System.Web.UI.WebControls.TextBox).Text
                AttObj.CreatedDate = Date.Now
                AttObj.Main = CType(gvRow.FindControl("rblSelect"), RadioButton).Checked
                AttObj.Type = "I"
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
            Dim dt As New List(Of TblAlbumDetails)
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
            'Dim parent = sender.parent.parent
            Dim AlbumId As String = sender.CommandArgument
            'Dim ImgbigPhoto As Image = DirectCast(parent.FindControl("ImgbigPhoto"), Image)
            'imgMain.ImageUrl = ImgbigPhoto.ImageUrl
            Dim dtNewsImgs As DataTable = DBManager.Getdatatable("select Id,AlbumId,ShowOrder,Main,Path from tblAlbumDetails where isnull(Isdeleted,0)=0 and AlbumId='" + AlbumId + "'")
            lvImages.DataSource = dtNewsImgs
            lvImages.DataBind()
            mpPopupImgs.Show()

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
#End Region


End Class
