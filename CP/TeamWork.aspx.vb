
#Region "Import"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class TeamWork
    Inherits System.Web.UI.Page
#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim UserId As String = "1"
    Dim Name As String
    Dim TeamWorkTitle As String
    Dim Description As String
    Dim Photo As String
    Dim CV As String
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
            Name = txtName.Text
            TeamWorkTitle = txtTitle.Text
            Description = txtDescription.Text
            OrderNo = PublicFunctions.IntFormat(txtOrderNo.Text)
            Photo = HiddenTeamWorkImg.Text
            CV = HiddenCV.Text
            FillImages()
            FillCVImage()
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
            Updated = DBManager.ExcuteQuery("Update TblTeamWork set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate(),ModifiedBy='" + UserId + "' where Id='" + ItemId + "' ")
            If Updated = 1 Then
                clsLogs.AddSystemLogs(StatusName, "TblTeamWork", ItemId)
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
                Permissions.CheckPermisions(gvTeamWork, cmdNew, txtSearch, cmdSearch, Me.Page, UserId)
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
    ''' Fill gridview with data from TblTeamWork.
    ''' </summary>
    Sub FillGrid()
        Try
            Dim dtTeamWork As DataTable = DBManager.Getdatatable("select * from TblTeamWork where isnull(Isdeleted,0)=0  and " + CollectConditions() + "")
            If dtTeamWork.Rows.Count > 0 Then
                pgPanel.Visible = True
                ' Initialize the sorting expression.
                If SortExpression.Value = String.Empty Then
                    SortExpression.Value = "Id ASC"
                End If
                ' Populate the GridView.
                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtTeamWork)

                ' Set the sort column and sort order.
                dv.Sort = SortExpression.Value.ToString()

                ' Bind the GridView control.
                gvTeamWork.DataSource = dv
                gvTeamWork.DataBind()
            Else
                pgPanel.Visible = False
                gvTeamWork.DataSource = Nothing
                gvTeamWork.DataBind()
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
            Dim Search As String = IIf(txtSearch.Text = "", "1=1", " (Name Like '%" + txtSearch.Text + "%' OR Title Like '%" + txtSearch.Text + "%')")
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
                gvTeamWork.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvTeamWork, e.SortExpression)).CssClass = "faDown"
            Else
                gvTeamWork.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvTeamWork, e.SortExpression)).CssClass = "faUp"
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
    Protected Sub GvTeamWork_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            gvTeamWork.PageIndex = e.NewPageIndex
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
            lblTeamWorkId.Text = ""
            imgTeamWork.ImageUrl = "~/images/img-up.png"
            HiddenTeamWorkImg.Text = ""
            HiddenCV.Text = ""
            pf.ClearAll(pnlForm)
            Enabler(True)
            txtOrderNo.Text = DBManager.SelectMax("ShowOrder", "tblTeamWork where isnull(isDeleted,0)=0")
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
            lblTeamWorkId.Text = Sender.commandargument.ToString
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
            dt = DBManager.Getdatatable("select * from TblTeamWork where isnull(Isdeleted,0)=0  and id='" + lblTeamWorkId.Text + "'")
            If dt.Rows.Count <> 0 Then
                txtName.Text = dt.Rows(0).Item("Name").ToString
                txtTitle.Text = dt.Rows(0).Item("Title").ToString
                txtDescription.Text = dt.Rows(0).Item("Description").ToString
                txtOrderNo.Text = dt.Rows(0).Item("ShowOrder").ToString
                HiddenTeamWorkImg.Text = dt.Rows(0).Item("Photo").ToString
                HiddenCV.Text = dt.Rows(0).Item("CV").ToString
                FillCVImage()
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
            Dim TeamWorkId As String = Sender.commandargument

            If DBManager.ExcuteQuery("update TblTeamWork SET  Isdeleted = 'True' where Id= '" + TeamWorkId + "'") = 1 Then
                clsLogs.AddSystemLogs("Delete", "TblTeamWork", TeamWorkId)
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
            Dim daTabeFactory As New TblTeamWorkFactory
            Dim dtTable As New TblTeamWork
            If cmdSave.CommandArgument = "add" Then
                If FillDT(dtTable) = False Then
                    Exit Sub
                End If

                If daTabeFactory.Insert(dtTable) Then
                    Dim RefId As String = PublicFunctions.GetIdentity("TblTeamWork")
                    clsLogs.AddSystemLogs("Insert", "TblTeamWork", RefId)
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    Exit Sub
                End If

            Else
                dtTable = daTabeFactory.GetAllBy(TblTeamWork.TblTeamWorkFields.Id, lblTeamWorkId.Text)(0)
                If FillDT(dtTable) = False Then
                    Exit Sub
                End If
                dtTable.Id = lblTeamWorkId.Text

                If daTabeFactory.Update(dtTable) Then
                    clsLogs.AddSystemLogs("Update", "TblTeamWork", lblTeamWorkId.Text)
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
    ''' Fill dtTeamWork from controls in the panel form.
    ''' </summary>
    Protected Function FillDT(ByRef dtTeamWork As TblTeamWork) As Boolean
        Try
            dtTeamWork.Name = Name
            dtTeamWork.Title = TeamWorkTitle
            dtTeamWork.Description = Description
            dtTeamWork.ShowOrder = OrderNo
            dtTeamWork.Photo = Photo
            dtTeamWork.CV = CV
            If cmdSave.CommandArgument = "add" Then
                dtTeamWork.CreatedBy = UserId
                dtTeamWork.CreatedDate = DateTime.Now
            End If
            dtTeamWork.ModifiedBy = UserId
            dtTeamWork.ModifiedDate = DateTime.Now

            dtTeamWork.IsDeleted = False
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

            imgTeamWork.ImageUrl = "~/images/img-up.png"
            HiddenTeamWorkImg.Text = ""
            HiddenCV.Text = ""
            lblTeamWorkId.Text = ""
            Enabler(False)
            FillGrid()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
#Region "Uploader"
    Protected Sub TeamWorkPhotoUploaded(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Check that there is a file
            If fuPhoto.PostedFile IsNot Nothing Then

                Dim filePath As String = "~/TeamWorkPhotos/" & fuPhoto.FileName

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

                    Dim MainWidth As String = "400"
                    Dim MainHeight As String = "300"

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
            If HiddenTeamWorkImg.Text IsNot Nothing And HiddenTeamWorkImg.Text <> "" Then
                imgTeamWork.ImageUrl = HiddenTeamWorkImg.Text
            Else
                HiddenTeamWorkImg.Text = ""
                imgTeamWork.ImageUrl = "~/images/img-up.png"
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "CV Uploader"
    Protected Sub CVUploaded(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Path As String = ""
        Dim fu As New AjaxControlToolkit.AsyncFileUpload
        Try
            fu = fuCV
            Path = Server.MapPath("~/TeamWorkCVs/")
            Dim contentType1 As String = fu.ContentType
            If contentType1 = "image/jpeg" OrElse contentType1 = "image/gif" OrElse contentType1 = "image/png" OrElse contentType1 = "application/vnd.openxmlformats-officedocument.wordprocessingml.document" OrElse contentType1 = "application/msword" OrElse contentType1 = "application/pdf" Then
                HiddenCV.Text = "~/TeamWorkCVs/" & fu.FileName
                fu.SaveAs(Path & fu.FileName)
            Else
                HiddenCV.Text = ""
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillCVImage()
        Try
            If HiddenCV.Text IsNot Nothing And HiddenCV.Text <> "" Then
                If HiddenCV.Text.ToString.Split(".").Last.ToLower = "pdf" Then
                    imgCV.ImageUrl = "~/images/pdf_icon.jpg"
                ElseIf HiddenCV.Text.ToString.Split(".").Last.ToLower = "doc" Then
                    imgCV.ImageUrl = "~/images/word.png"
                ElseIf HiddenCV.Text.ToString.Split(".").Last.ToLower = "docx" Then
                    imgCV.ImageUrl = "~/images/word.png"
                Else
                    imgCV.ImageUrl = HiddenCV.Text
                End If
            Else
                HiddenCV.Text = ""
                imgCV.ImageUrl = "~/images/noDoc.png"
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub ClearCV(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            HiddenCV.Text = ""
            imgCV.ImageUrl = "~/images/noDoc.png"
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region
End Class
