
#Region "Import"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class Section
    Inherits System.Web.UI.Page
#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim UserId As String = "1"
    Dim SectionType As String
    Dim SectionTitle As String
    Dim Description As String
    Dim URL As String
    Dim Photo As String
    Dim OrderNo As String
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
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set Default controls Values 
    ''' </summary>
    Sub SetControlFields()
        Try
            SectionType = rblType.SelectedValue
            SectionTitle = txtTitle.Text
            Description = txtDescription.Text
            URL = txtURL.Text
            OrderNo = PublicFunctions.IntFormat(txtOrderNo.Text)
            Photo = HiddenSectionImg.Text
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
            Dim ItemId As String = DirectCast(parent.FindControl("lblId"), Label).Text
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
            Updated = DBManager.ExcuteQuery("Update TblSections set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate(),ModifiedBy='" + UserId + "' where Id='" + ItemId + "' ")
            If Updated = 1 Then
                clsLogs.AddSystemLogs(StatusName, "TblSections", ItemId)
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
            If Page.IsPostBack = False Then
                Permissions.CheckPermisions(gvSection, cmdNew, txtSearch, cmdSearch, Me.Page, UserId)
                clsLogs.AddSystemLogs("Access")
                FillGrid()
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
    ''' Fill gridview with data from TblSections.
    ''' </summary>
    Sub FillGrid()
        Try
            Dim dtSection As DataTable = DBManager.Getdatatable("select * from TblSections where isnull(Isdeleted,0)=0  and " + CollectConditions() + "")
            If dtSection.Rows.Count > 0 Then
                pgPanel.Visible = True
                ' Initialize the sorting expression.
                If SortExpression.Value = String.Empty Then
                    SortExpression.Value = "Id ASC"
                End If
                ' Populate the GridView.
                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtSection)

                ' Set the sort column and sort order.
                dv.Sort = SortExpression.Value.ToString()

                ' Bind the GridView control.
                gvSection.DataSource = dv
                gvSection.DataBind()
            Else
                pgPanel.Visible = False
                gvSection.DataSource = Nothing
                gvSection.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Collect condition string to fill grid
    ''' </summary>
    Public Function CollectConditions() As String
        Dim result As String = "1=1"
        Try
            Dim Search As String = IIf(txtSearch.Text = "", "1=1", " (Title Like '%" + txtSearch.Text + "%')")
            Return Search
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return result
        End Try
    End Function
#End Region
#Region "Sorting"

    Protected Sub Gv_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim strSortExpression As String() = SortExpression.Value.ToString().Split(" "c)

            ' If the sorting column is the same as the previous one, 
            ' then change the sort order.
            If strSortExpression(0) = e.SortExpression Then
                If strSortExpression(1) = "ASC" Then
                    SortExpression.Value = Convert.ToString(e.SortExpression) & " " & "DESC"
                Else
                    SortExpression.Value = Convert.ToString(e.SortExpression) & " " & "ASC"
                End If
            Else
                ' If sorting column is another column, 
                ' then specify the sort order to "Ascending".
                SortExpression.Value = Convert.ToString(e.SortExpression) & " " & "DESC"
            End If

            ' Rebind the GridView control to show sorted data.
            FillGrid()

            ' add sorting Arrows.
            If strSortExpression(1) = "ASC" Then
                gvSection.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvSection, e.SortExpression)).CssClass = "faDown"
            Else
                gvSection.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvSection, e.SortExpression)).CssClass = "faUp"
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
#Region "Paging"

    ''' <summary>
    ''' Paging function
    ''' </summary>
    Protected Sub GvSection_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            gvSection.PageIndex = e.NewPageIndex
            FillGrid()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Set Number of rows at every page
    ''' </summary>
    Protected Sub PageSize_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            FillGrid()
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
            lblSectionId.Text = ""
            imgSection.ImageUrl = "~/images/img-up.png"
            HiddenSectionImg.Text = ""
            pf.ClearAll(pnlForm)
            Enabler(True)
            rblType.SelectedIndex = 0
            txtOrderNo.Text = DBManager.SelectMax("ShowOrder", "tblSections where isnull(isDeleted,0)=0")
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
            lblSectionId.Text = Sender.commandargument.ToString
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
            dt = DBManager.Getdatatable("select * from TblSections where isnull(Isdeleted,0)=0  and id='" + lblSectionId.Text + "'")
            If dt.Rows.Count <> 0 Then
                rblType.SelectedValue = dt.Rows(0).Item("SectionType").ToString
                txtTitle.Text = dt.Rows(0).Item("Title").ToString
                txtDescription.Text = dt.Rows(0).Item("Description").ToString
                txtURL.Text = dt.Rows(0).Item("URL").ToString
                txtOrderNo.Text = dt.Rows(0).Item("ShowOrder").ToString
                HiddenSectionImg.Text = dt.Rows(0).Item("Photo").ToString
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
            Dim SectionId As String = Sender.commandargument

            If DBManager.ExcuteQuery("update TblSections SET  Isdeleted = 'True' where Id= '" + SectionId + "'") = 1 Then
                clsLogs.AddSystemLogs("Delete", "TblSections", SectionId)
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Delete, Me.Page)
                FillGrid()
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
            Dim daTabeFactory As New TblSectionsFactory
            Dim dtTable As New TblSections
            If cmdSave.CommandArgument = "add" Then
                If FillDT(dtTable) = False Then
                    Exit Sub
                End If

                If daTabeFactory.Insert(dtTable) Then
                    Dim RefId As String = PublicFunctions.GetIdentity("TblSections")
                    clsLogs.AddSystemLogs("Insert", "TblSections", RefId)
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    Exit Sub
                End If

            Else
                dtTable = daTabeFactory.GetAllBy(TblSections.TblSectionsFields.Id, lblSectionId.Text)(0)
                If FillDT(dtTable) = False Then
                    Exit Sub
                End If
                dtTable.Id = lblSectionId.Text

                If daTabeFactory.Update(dtTable) Then
                    clsLogs.AddSystemLogs("Update", "TblSections", lblSectionId.Text)
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
    Protected Function FillDT(ByRef dtSection As TblSections) As Boolean
        Try
            dtSection.SectionType = SectionType
            dtSection.Title = SectionTitle
            dtSection.Description = Description
            dtSection.Photo = Photo
            dtSection.URL = URL
            dtSection.ShowOrder = OrderNo

            If cmdSave.CommandArgument = "add" Then
                dtSection.CreatedBy = UserId
                dtSection.CreatedDate = DateTime.Now
            End If
            dtSection.ModifiedBy = UserId
            dtSection.ModifiedDate = DateTime.Now

            dtSection.IsDeleted = False
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
            HiddenSectionImg.Text = ""
            lblSectionId.Text = ""
            Enabler(False)
            FillGrid()
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

                Dim filePath As String = "~/SectionPhotos/" & fuPhoto.FileName

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
            If HiddenSectionImg.Text IsNot Nothing And HiddenSectionImg.Text <> "" Then
                imgSection.ImageUrl = HiddenSectionImg.Text
            Else
                HiddenSectionImg.Text = ""
                imgSection.ImageUrl = "~/images/img-up.png"
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
End Class
