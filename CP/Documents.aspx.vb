#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class Documents
    Inherits System.Web.UI.Page

#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim ItemsImgs As New List(Of TblMedia)
    Dim daItemsImgs As New TblMediaFactory
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim UserId As String = "1"
    Dim Cateogory As String
    Dim SubCategory As String

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
            Cateogory = ddlCategory.SelectedValue
            SubCategory = ddlSubCategory.SelectedValue
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
            Dim LibraryCategoryId As String = DirectCast(parent.FindControl("lbCategoryId"), Label).Text
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
            Updated = DBManager.ExcuteQuery("Update tblDocuments set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate(),ModifiedBy='" + UserId + "' where Category='" + LibraryCategoryId + "' ")
            If Updated = 1 Then
                clsLogs.AddSystemLogs(StatusName, "tblDocuments", LibraryCategoryId)
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
    Private Sub FillDDls()
        Try
            'Fill Document Category
            clsBindDDL.BindLookupDDLs("DocumentCategory", ddlCategory, True, "--اختر--")

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
            ddlSubCategory.Items.Add(New ListItem("--اختر--", 0))
            Return
        End If
        clsBindDDL.BindLookupDDLs("DocumentSubCategory", ddlSubCategory, True, "--اختر--", "ASC", ddlCategory.SelectedValue)
    End Sub
#End Region

#Region "Fill Grid"

    ''' <summary>
    ''' Fill Listview with data.
    ''' </summary>
    Sub FillGrid(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select  Category,dbo.GetEnValue(Category) as DocCategory, SubCategory,dbo.GetEnValue(SubCategory) as DocSubCategory,Active from tblDocuments where isnull(isdeleted,0)=0 and ISNULL(sourceid,0)=0 and  " & CollectConditions(sender, e) & "  group by Category,SubCategory,Active")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then

                    ViewState("dtDocs") = dt
                    ' Initialize the sorting expression.
                    ViewState("SortExpression") = "Category DESC"
                    ' Populate the GridView.
                    BindListView()
                    dplvLibrary.Visible = False
                    If dt.Rows.Count > ddlPager.SelectedValue Then
                        dplvLibrary.Visible = True
                    End If
                Else
                    lvLibrary.DataSource = Nothing
                    lvLibrary.DataBind()
                    dplvLibrary.Visible = False
                End If
                lblTotalCount.Text = dt.Rows.Count
            End If
            'ScriptManager.RegisterClientScriptBlock(UP, Me.[GetType](), Guid.NewGuid().ToString(), "LoadJquery();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Collect condition string to fill lvLibrary
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
            If ViewState("dtDocs") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtDocs As DataTable = DirectCast(ViewState("dtDocs"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtDocs)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("SortExpression").ToString()

                ' Bind the Listview control.
                lvLibrary.DataSource = dv
                lvLibrary.DataBind()
                If dtDocs.Rows.Count > 0 Then
                    dplvLibrary.DataBind()
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
            dplvLibrary.SetPageProperties(0, ddlPager.SelectedValue, True)
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
            dplvLibrary.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
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
                CType(lvLibrary.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faDown")
            Else
                CType(lvLibrary.FindControl(e.SortExpression), HtmlTableCell).Attributes.Add("class", "faUp")
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    Sub ResetArrows()
        Try
            Dim i As Integer = 0
            While i < lvLibrary.Items.Count
                CType(lvLibrary.FindControl("DocSubCategory"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvLibrary.FindControl("DocCategory"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                CType(lvLibrary.FindControl("ActiveHeader"), HtmlTableCell).Attributes.Add("class", "upnDownArrow")
                i += 1
            End While

        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error Code: " & Application("Errors").Select("exType='" & ex.GetType().Name.ToString & "'")(0).ItemArray(2) & "</br> " & Application("Errors").Select("exType='" & ex.GetType().Name.ToString & "'")(0).ItemArray(3), Me)
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

#Region "New"

    ''' <summary>
    ''' Handle add button(form grid) click event.
    ''' </summary>
    Protected Sub Add(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "add"
            lblLibraryCategoryID.Text = ""
            pf.ClearAll(pnlForm)
            ddlCategory.SelectedIndex = 0
            ddlSubCategory.SelectedIndex = 0
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
            lblLibraryCategoryID.Text = Sender.commandargument.ToString
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
            dt = DBManager.Getdatatable("select * from tblDocuments where  isnull(SourceId,0)=0 and isnull(TableName,'')='tblDocuments' and isnull(isdeleted,0)=0 and Category='" + lblLibraryCategoryID.Text + "'")
            If dt.Rows.Count <> 0 Then

                Dim Cat As String = dt.Rows(0).Item("Category").ToString
                Dim SubCat As String = dt.Rows(0).Item("SubCategory").ToString
                If ddlCategory.Items.FindByValue(Cat) IsNot Nothing Then
                    ddlCategory.SelectedValue = Cat
                End If
                CategoryChanged(Nothing, New EventArgs)
                If ddlSubCategory.Items.FindByValue(SubCat) IsNot Nothing Then
                    ddlSubCategory.SelectedValue = SubCat
                End If

                FillProjectFiles(Cat)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

    Private Sub FillProjectFiles(CategoryID As String)
        Try
            Dim dtFiles As DataTable = DBManager.Getdatatable("select *,Path as MediaPath, 0 as Main , 0 as ShowOrder, '' as Description from tblDocuments where isnull(SourceId,0)=0 and isnull(TableName,'')='tblDocuments' and isnull(isdeleted,0)=0 and Category=" & CategoryID)
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
            Dim CategoryID As String = Sender.commandargument

            If DBManager.ExcuteQuery("update tblDocuments SET  Isdeleted = 'True' where Category= '" + CategoryID + "'") = 1 Then
                clsLogs.AddSystemLogs("Delete", "tblDocuments", CategoryID)
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
            Dim daTabeFactory As New TblDocumentsFactory
            Dim dtTable As New TblDocuments
            Dim docList As New List(Of TblDocuments)
            If cmdSave.CommandArgument = "add" Then
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                If Not FillDT(dtTable, _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    Exit Sub
                End If
                _sqltrans.Commit()
                _sqlconn.Close()

                clsLogs.AddSystemLogs("Insert", "tblDocuments", dtTable.Id.ToString)
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
            Else
                docList = daTabeFactory.GetAllBy(TblDocuments.TblDocumentsFields.Category, lblLibraryCategoryID.Text)
                If docList.Count > 0 Then
                    dtTable = docList(0)
                End If
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                daTabeFactory.DeleteTrans(TblDocuments.TblDocumentsFields.Category, lblLibraryCategoryID.Text, _sqlconn, _sqltrans)
                If Not FillDT(dtTable, _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    Exit Sub
                End If
                _sqltrans.Commit()
                _sqlconn.Close()

                clsLogs.AddSystemLogs("Update", "tblDocuments", lblLibraryCategoryID.Text)
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
    Protected Function FillDT(ByRef dtDoc As TblDocuments, _sqlconn As SqlConnection, _sqltrans As SqlTransaction) As Boolean
        Try
            'loop to get images details from images gridview
            Dim daDoc As New TblDocumentsFactory
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                dtDoc.SourceId = Nothing
                dtDoc.Path = CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                dtDoc.Title = CType(gvRow.FindControl("txtTitle"), System.Web.UI.WebControls.TextBox).Text
                dtDoc.TableName = "tblDocuments"
                dtDoc.Category = ddlCategory.SelectedValue
                dtDoc.SubCategory = ddlSubCategory.SelectedValue
                dtDoc.CreatedDate = DateTime.Now
                dtDoc.ModifiedDate = DateTime.Now
                dtDoc.IsDeleted = False
                dtDoc.Active = False
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
            lblLibraryCategoryID.Text = ""
            ddlCategory.SelectedIndex = 0
            CategoryChanged(Sender, e)
            ddlSubCategory.SelectedIndex = 0
            Enabler(False)
            FillGrid(Sender, e)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Files Upload"
    ''' <summary>
    '''Fill Photos gridview.
    ''' </summary>
    Sub FillPhotos(ByVal SourceId As String)
        Try
            Dim dtItemImgs As DataTable = DBManager.Getdatatable("select * from tblDocuments where isnull(Isdeleted,0)=0 and  SourceId='" & SourceId & "' and TableName='tblDocuments'")
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
            'dtItems = DBManager.Getdatatable("select MediaPath from tblMedia where Main='1' and SourceId='" + lblLibraryCategoryID.Text + "'")
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
    Private Sub ListView_DataBound(sender As Object, e As EventArgs) Handles lvLibrary.DataBound
        Try
            Permissions.CheckPermisions(lvLibrary, cmdNew, txtSearch, cmdSearch, Me.Page, UserId)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

End Class
