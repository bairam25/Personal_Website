
#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class News
    Inherits System.Web.UI.Page
#Region "Global_Varaibles"
    Dim pf As New PublicFunctions
    Dim UserID As String = "0"
    Dim ShowOrder As String
    Dim NewsDate As String
    Dim STitle As String
    Dim Description As String
    Dim Category As String = String.Empty
    Dim SubCategory As String = String.Empty
    Dim NewsTable As String = "select * from vw_News"
    Dim ItemsImgs As New List(Of TblMedia)
    Dim daItemsImgs As New TblMediaFactory
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
            STitle = txtTitle.Text
            Description = txtDescription.TextValue
            Category = Val(ddlCategory.SelectedValue)
            SubCategory = Val(ddlSubCategory.SelectedValue)
            ShowOrder = txtShowOrder.Text
            NewsDate = PublicFunctions.DateFormat(txtNewsDate.Text, "dd/MM/yyyy")
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
            lblRes.Visible = False
            UserID = PublicFunctions.GetUserId(Page)
            lblResPopupImages.Visible = False
            If Not Page.IsPostBack Then
                FillGrid(sender, e)
                '2) Fill Category DDL
                clsBindDDL.BindLookupDDLs("NewsCategory", ddlCategory, True)
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
    ''' <summary>
    ''' handles change account category and bind the related sub categories
    ''' in case of not selected , remove sub categories items and show --Select-- text with 0 value
    ''' </summary>
    Protected Sub CategoryChanged(ByVal Sender As Object, ByVal e As System.EventArgs)
        If ddlCategory.SelectedValue = 0 Then
            ddlSubCategory.Items.Clear()
            ddlSubCategory.Items.Add(New ListItem("--Select--", 0))
            Return
        End If
        clsBindDDL.BindLookupDDLs("NewsSubCategory", ddlSubCategory, False, "--Select--", "ASC", ddlCategory.SelectedValue)
    End Sub
#End Region

#Region "Add"
    ''' <summary>
    ''' Add News
    ''' </summary>
    Protected Sub Add(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "add"
            Enabler(True)
            pf.ClearAll(pnlForm)
            txtDescription.TextValue = String.Empty
            'Reset Category & Sub Category
            ddlCategory.SelectedIndex = 0
            CategoryChanged(Sender, e)
            txtNewsDate.Text = DateTime.Now.ToShortDateString
            txtShowOrder.Text = Val(DBManager.Getdatatable("select count(*) from tblNews where isnull(IsDeleted,0)=0 ").Rows(0).Item(0).ToString) + 1
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Save"
    ''' <summary>
    ''' Save News 
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim daNews As New TblNewsFactory
        Dim dtNews As New TblNews
        Try
            If cmdSave.CommandArgument.ToLower() = "add" Then
                'Insert Case
                'Fill object of News
                If Not FillDT(dtNews) Then
                    Exit Sub
                End If
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                If daNews.InsertTrans(dtNews, _sqlconn, _sqltrans) Then
                    'Add News Photos
                    If SaveNewsImgs("Insert") Then
                        clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Page)
                    End If

                Else
                    _sqltrans.Rollback()
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                    Exit Sub
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
            Else
                'Update Case
                dtNews = daNews.GetAllBy(TblNews.TblNewsFields.Id, lblNewsId.Text)(0)
                'Fill object with new values
                If Not FillDT(dtNews) Then
                    Exit Sub
                End If
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                If daNews.UpdateTrans(dtNews, _sqlconn, _sqltrans) Then
                    'Update News Photos
                    If SaveNewsImgs("Update") Then
                        clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Page)
                    End If
                Else
                    _sqltrans.Rollback()
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                    Exit Sub
                End If
                _sqltrans.Commit()
                _sqlconn.Close()

            End If
            Cancel(Sender, e)
            Enabler(False)
            FillGrid(Sender, e)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Fill News Object
    ''' </summary>
    Function FillDT(ByVal dtNews As TblNews) As Boolean
        Try
            '1)Validate 
            If Not IsNumeric(ShowOrder) Then
                clsMessages.ShowInfoMessgage(lblRes, "Invalid Order", Me)
                txtShowOrder.Focus()
                Return False
            End If
            If Not IsDate(NewsDate) Then
                clsMessages.ShowInfoMessgage(lblRes, "Invalid Date", Me)
                txtNewsDate.Focus()
                Return False
            End If
            dtNews.ShowOrder = txtShowOrder.Text
            dtNews.NewsDate = NewsDate
            dtNews.Title = STitle
            dtNews.Description = Description
            dtNews.Category = Category
            dtNews.SubCategory = SubCategory
            If cmdSave.CommandArgument.ToLower() = "add" Then
                dtNews.CreatedBy = UserID
                dtNews.CreatedDate = DateTime.Now
            End If
            dtNews.ModifiedDate = DateTime.Now
            dtNews.IsDeleted = False
            dtNews.Active = False
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Fill dtNewsImgs from controls in the panel photos.
    ''' </summary>
    Protected Function SaveNewsImgs(ByVal Operation As String) As Boolean
        Dim daImgs As New TblMediaFactory
        Dim dtImgs As New TblMedia
        Dim ItemId As String = String.Empty
        Try
            Select Case Operation.ToLower
                Case "insert"
                    ItemId = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                Case "update"
                    ItemId = lblNewsId.Text
                    'delete old images in case of update
                    ' daImgs.DeleteTrans(TblMedia.TblMediaFields.SourceId, ItemId, _sqlconn, _sqltrans)
                    ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("delete from TblMedia where SourceId='" & ItemId & "' and Type='N'"))
            End Select
            'loop to get images details from images gridview
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                dtImgs.SourceId = ItemId
                dtImgs.Type = "N"
                dtImgs.MediaPath = CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                dtImgs.MediaThumbPath = dtImgs.MediaPath.Replace(dtImgs.MediaPath.Split("/").Last, "Thumb_" + dtImgs.MediaPath.Split("/").Last)
                dtImgs.ShowOrder = CType(gvRow.FindControl("ddlShowOrder"), System.Web.UI.WebControls.DropDownList).SelectedValue
                dtImgs.Title = CType(gvRow.FindControl("txtTitle"), System.Web.UI.WebControls.TextBox).Text
                dtImgs.Description = CType(gvRow.FindControl("txtDescription"), System.Web.UI.WebControls.TextBox).Text
                dtImgs.CreatedDate = Date.Now
                dtImgs.Main = CType(gvRow.FindControl("rblSelect"), RadioButton).Checked
                dtImgs.MediaType = "I"
                dtImgs.IsDeleted = False
                If Not daImgs.InsertTrans(dtImgs, _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            _sqltrans.Rollback()
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
            Dim dt As DataTable = DBManager.Getdatatable(NewsTable & " where " & CollectConditions(sender, e))
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then

                    ViewState("dtNewsTable") = dt
                    ' Initialize the sorting expression.
                    ViewState("SortExpression") = "ModifiedDate DESC"
                    ' Populate the GridView.
                    BindListView()
                    dplvNews.Visible = False
                    If dt.Rows.Count > ddlPager.SelectedValue Then
                        dplvNews.Visible = True
                    End If
                Else
                    lvNews.DataSource = Nothing
                    lvNews.DataBind()
                    dplvNews.Visible = False
                End If
                lblTotalCount.Text = dt.Rows.Count
            End If



            'ScriptManager.RegisterClientScriptBlock(UP, Me.[GetType](), Guid.NewGuid().ToString(), "LoadJquery();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Collect condition string to fill lvNews
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
                lvNews.DataSource = dv
                lvNews.DataBind()
                If dtNewsTable.Rows.Count > 0 Then
                    dplvNews.DataBind()
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
            dplvNews.SetPageProperties(0, ddlPager.SelectedValue, True)
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
            dplvNews.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
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
                CType(lvNews.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faDown")
            Else
                CType(lvNews.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faUp")
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    Sub ResetArrows()
        Try
            Dim i As Integer = 0
            While i < lvNews.Items.Count
                CType(lvNews.FindControl("NewsDate"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvNews.FindControl("Title"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvNews.FindControl("MediaCount"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvNews.FindControl("ShowOrder"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
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
            Dim ItemId As String = DirectCast(parent.FindControl("lblNewsID"), Label).Text
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
            Updated = DBManager.ExcuteQuery("Update TblNews set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate(),ModifiedBy='" + UserID + "' where Id='" + ItemId + "' ")
            If Updated = 1 Then
                clsLogs.AddSystemLogs(StatusName, "TblNews", ItemId)
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
        lblNewsId.Text = Sender.commandargument
        pf.ClearAll(pnlForm)
        Try
            Dim dt As DataTable = DBManager.Getdatatable(NewsTable & " where Id ='" & lblNewsId.Text & "'")
            If dt.Rows.Count <> 0 Then
                txtTitle.Text = dt.Rows(0).Item("Title").ToString
                Dim Category As String = dt.Rows(0).Item("Category").ToString
                Dim SubCategory As String = dt.Rows(0).Item("SubCategory").ToString
                If ddlCategory.Items.FindByValue(Category) IsNot Nothing Then
                    ddlCategory.SelectedValue = Category
                End If
                CategoryChanged(Nothing, New EventArgs)
                If ddlSubCategory.Items.FindByValue(SubCategory) IsNot Nothing Then
                    ddlSubCategory.SelectedValue = SubCategory
                End If
                txtDescription.TextValue = dt.Rows(0).Item("Description").ToString

                txtShowOrder.Text = dt.Rows(0).Item("ShowOrder").ToString
                txtNewsDate.Text = PublicFunctions.DateFormat(dt.Rows(0).Item("NewsDate").ToString, "dd/MM/yyyy")
                'Fill News Photos
                FillPhotos(lblNewsId.Text)
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
        Dim NewsId As String = Sender.commandargument.ToString
        Try
            If DBManager.ExcuteQuery("update tblNews SET Isdeleted = 'True',DeletedDate=GETDATE() where Id= '" & NewsId & "';Update tblMedia SET Isdeleted = 'True',DeletedDate=GETDATE() where sourceid= '" & NewsId & "' and Type='N'") = 1 Then
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
        Dim NewsId As String = DirectCast(parent.FindControl("lblNewsId"), Label).Text
        Dim Main As Boolean = PublicFunctions.BoolFormat(DirectCast(parent.FindControl("lblMain"), Label).Text)
        Try
            If DBManager.ExcuteQuery("update tblMedia SET Isdeleted = 'True',DeletedDate=GETDATE() where Id= '" & MediaId & "' and sourceid='" & NewsId & "'") = 1 Then
                BindNewsPhotos(NewsId)
                If lvImages.Items.Count > 0 Then
                    'Reset Show Order
                    For Each gvRow As ListViewItem In lvImages.Items
                        Dim id = DirectCast(gvRow.FindControl("lblMediaId"), Label).Text
                        Dim serialNo = DirectCast(gvRow.FindControl("srialNo"), Label).Text
                        DBManager.ExcuteQuery("update tblMedia SET ShowOrder = " & serialNo & " where Id= '" & id & "' and sourceid='" & NewsId & "'")
                    Next

                    'If Main is deleting , then set first row as the new main one
                    If Main Then
                        Dim FirstPhotoId As String = DirectCast(lvImages.Items(0).FindControl("lblMediaId"), Label).Text
                        DBManager.ExcuteQuery("update tblMedia SET Main=1 where Id= '" & FirstPhotoId & "' and sourceid='" & NewsId & "'")
                        BindNewsPhotos(NewsId)
                    End If

                    BindNewsPhotos(NewsId)
                End If
                clsMessages.ShowMessage(lblResPopupImages, MessageTypesEnum.Delete, Me)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Private Sub BindNewsPhotos(ByVal NewsId As String)
        Try
            Dim dtNewsImgs As DataTable = DBManager.Getdatatable("select Id,sourceid,ShowOrder,Main,MediaPath,MediaThumbPath from tblMedia where isnull(Isdeleted,0)=0 and sourceid='" + NewsId + "'")
            lvImages.DataSource = dtNewsImgs
            lvImages.DataBind()
            mpPopupImgs.Show()
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
            lblNewsId.Text = String.Empty
            txtDescription.TextValue = String.Empty
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
    Sub FillPhotos(ByVal NewsId As String)
        Try
            Dim dtItemImgs As DataTable = DBManager.Getdatatable("select * from tblMedia where isnull(Isdeleted,0)=0 and sourceid='" + NewsId + "' and Type='N'")
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
                If DirectCast(gvItemsImgs.Rows(c).FindControl("lblShowOrder"), Label).Text = String.Empty Then
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
            dtItems = DBManager.Getdatatable("select MediaPath from tblMedia where Main='1' and sourceid='" + lblNewsId.Text + "'")
            If dtItems.Rows.Count <> 0 Then
                Dim Photo = dtItems.Rows(0).Item("MediaPath").ToString
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
            'Dim parent = sender.parent.parent
            Dim NewsId As String = sender.CommandArgument
            'Dim ImgbigPhoto As Image = DirectCast(parent.FindControl("ImgbigPhoto"), Image)
            'imgMain.ImageUrl = ImgbigPhoto.ImageUrl
            Dim dtNewsImgs As DataTable = DBManager.Getdatatable("select Id,SourceId,ShowOrder,Main,MediaPath,MediaThumbPath from tblMedia where isnull(Isdeleted,0)=0 and SourceId='" + NewsId + "'")
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
                    ddl.SelectedValue = OldOrder.Text
                    lblOrder.Text = OldOrder.Text
                End If
            Next
            OldOrder.Text = NewOrderValue
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try

    End Sub
#End Region

#Region "Permissions"
    Private Sub ListView_DataBound(sender As Object, e As EventArgs) Handles lvNews.DataBound
        Try
            Permissions.CheckPermisions(lvNews, lbNew, txtSearch, lbSearchIcon, Me.Page, UserID)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region
End Class
