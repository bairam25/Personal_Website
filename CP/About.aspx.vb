
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
    Dim AboutProgram As String
    Dim ProgramSteps As String
    Dim ProgramComponent As String
    Dim ProgramOrgChart As String
    Dim ItemsImgs As New List(Of TblMedia)
    Dim daItemsImgs As New TblMediaFactory
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region

#Region "Public Functions"
    ''' <summary>
    ''' Set Default controls Values 
    ''' </summary>
    Sub SetControlFields()
        Try
            AboutProgram = txtAboutProg.TextValue
            ProgramSteps = txtProgramSteps.TextValue
            ProgramComponent = txtProgramComponent.TextValue
            ProgramOrgChart = txtProgramOrganisationalChart.TextValue
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
                Permissions.CheckPermisions(New GridView, New LinkButton, New TextBox, New LinkButton, Me.Page, UserID)
                FillData()
            End If
            SetControlFields()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Save"
    ''' <summary>
    ''' Save About 
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim daAbout As New TblAboutProgFactory
        Dim dtAbout As New TblAboutProg
        Try
            If Not FillDT(dtAbout) Then
                Exit Sub
            End If
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("delete from TblAboutProg"))
            If daAbout.InsertTrans(dtAbout, _sqlconn, _sqltrans) Then
                'Add News Photos
                If Not SaveNewsImgs("insert") Then
                    _sqltrans.Rollback()
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                    Exit Sub
                End If
            Else
                _sqltrans.Rollback()
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                Exit Sub
            End If
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Page)
            _sqltrans.Commit()
            _sqlconn.Close()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Fill News Object
    ''' </summary>
    Function FillDT(ByVal dtAbout As TblAboutProg) As Boolean
        Try
            dtAbout.ProgAbout = AboutProgram
            dtAbout.ProgComponents = ProgramComponent
            dtAbout.ProgSteps = ProgramSteps
            dtAbout.ProgChart = ProgramOrgChart
            dtAbout.CreatedBy = UserID
            dtAbout.CreatedDate = DateTime.Now
            dtAbout.ModifiedDate = DateTime.Now
            dtAbout.IsDeleted = False
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Fill dtAboutImgs from controls in the panel photos.
    ''' </summary>
    Protected Function SaveNewsImgs(ByVal Operation As String) As Boolean
        Dim daImgs As New TblMediaFactory
        Dim dtImgs As New TblMedia
        Dim ItemId As String = String.Empty
        Try
            Select Case Operation.ToLower
                Case "insert"
                    ItemId = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                    ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("delete from TblMedia where Type='b'"))

                Case "update"
                    ItemId = lblNewsId.Text
                    'delete old images in case of update
                    ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("delete from TblMedia where Type='b'"))
                    'daImgs.DeleteTrans(TblMedia.TblMediaFields.SourceId, ItemId, _sqlconn, _sqltrans)
            End Select
            'loop to get images details from images gridview
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                dtImgs.SourceId = ItemId
                dtImgs.Type = "b"
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

#Region "Fill Data"
    ''' <summary>
    ''' Handle Update Event
    ''' Get Object details and Populate it in pnl forms
    ''' </summary>
    Protected Sub FillData()
        pf.ClearAll(pnlForm)
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select * from tblAboutProg")
            If dt.Rows.Count <> 0 Then
                txtAboutProg.TextValue = dt.Rows(0).Item("ProgAbout").ToString
                txtProgramSteps.TextValue = dt.Rows(0).Item("ProgSteps").ToString
                txtProgramComponent.TextValue = dt.Rows(0).Item("ProgComponents").ToString
                txtProgramOrganisationalChart.TextValue = dt.Rows(0).Item("ProgChart").ToString
                Dim id As String = dt.Rows(0).Item("Id").ToString
                'Fill Photos
                FillPhotos(id)
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
            If DBManager.ExcuteQuery("update TblAboutProg SET Isdeleted = 'True',DeletedDate=GETDATE() where Id= '" & NewsId & "';Update tblMedia SET Isdeleted = 'True',DeletedDate=GETDATE() where sourceid= '" & NewsId & "' and Type='N'") = 1 Then
                'FillGrid(Sender, e)
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
            Dim dtAboutImgs As DataTable = DBManager.Getdatatable("select Id,sourceid,ShowOrder,Main,MediaPath,MediaThumbPath from tblMedia where isnull(Isdeleted,0)=0 and sourceid='" + NewsId + "'")
            lvImages.DataSource = dtAboutImgs
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
            pf.ClearAll(pnlForm)
            FillData()
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
            Dim dtItemImgs As DataTable = DBManager.Getdatatable("select * from tblMedia where isnull(Isdeleted,0)=0 and sourceid='" + NewsId + "' and Type='b'")
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
            Dim dtAboutImgs As DataTable = DBManager.Getdatatable("select Id,SourceId,ShowOrder,Main,MediaPath,MediaThumbPath from tblMedia where isnull(Isdeleted,0)=0 and SourceId='" + NewsId + "'")
            lvImages.DataSource = dtAboutImgs
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

End Class
