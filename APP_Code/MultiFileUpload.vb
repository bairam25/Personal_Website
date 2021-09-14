Imports System.Data

Public Class MultiFileUpload
    ''' <summary>
    ''' Get uploaded files details
    ''' </summary>
    Public Shared Function getUploadedFilesDetails(ByVal UploadedFilesDetails As String) As DataTable
        Try
            Dim dt As New DataTable
            dt.Columns.Add("ID", GetType(String))
            dt.Columns.Add("URL", GetType(String))
            dt.Columns.Add("Title", GetType(String))
            dt.Columns.Add("Type", GetType(String))

            UploadedFilesDetails = UploadedFilesDetails.Remove(0, 1)
            If UploadedFilesDetails <> String.Empty Then
                Dim FilesDetailsArr = UploadedFilesDetails.Split("|")
                For Each fileDetails As String In FilesDetailsArr
                    Dim row As DataRow = dt.NewRow
                    row("ID") = fileDetails.Split("?")(0)
                    row("URL") = fileDetails.Split("?")(1)
                    row("Title") = fileDetails.Split("?")(2).ToString.Split(".")(0)
                    row("Type") = fileDetails.Split("?")(2).ToString.Split(".").Last
                    dt.Rows.Add(row)
                Next
            End If
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Show Document File Icon which refer to document type.
    ''' </summary>
    Public Shared Sub ShowFileTypes(ByRef grid As GridView)
        Try
            For Each row As GridViewRow In grid.Rows
                Dim file As System.Web.UI.WebControls.Image = DirectCast(row.FindControl("file"), System.Web.UI.WebControls.Image)
                Dim fileIcon As System.Web.UI.WebControls.Image = DirectCast(row.FindControl("fileIcon"), System.Web.UI.WebControls.Image)
                Dim lView1 As HyperLink = DirectCast(row.FindControl("hylViewDoc"), HyperLink)
                Dim lview2 As HyperLink = DirectCast(row.FindControl("hylViewImg"), HyperLink)

                If file.ImageUrl.ToString.Split(".").Last.ToLower = "doc" Or file.ImageUrl.ToString.Split(".").Last.ToLower = "docx" Then
                    fileIcon.ImageUrl = "Images/word.png"
                    lView1.Visible = True
                    lview2.Visible = False
                ElseIf file.ImageUrl.ToString.Split(".").Last.ToLower = "xls" Or file.ImageUrl.ToString.Split(".").Last.ToLower = "xlsx" Then
                    fileIcon.ImageUrl = "Images/icon-xlsx.png"
                    lView1.Visible = True
                    lview2.Visible = False
                ElseIf file.ImageUrl.ToString.Split(".").Last.ToLower = "ppt" Or file.ImageUrl.ToString.Split(".").Last.ToLower = "pptx" Then
                    fileIcon.ImageUrl = "Images/powepoint.png"
                    lView1.Visible = True
                    lview2.Visible = False
                ElseIf file.ImageUrl.ToString.Split(".").Last.ToLower = "pdf" Then
                    fileIcon.ImageUrl = "Images/pdf_icon.jpg"
                    lView1.Visible = False
                    lview2.Visible = True
                ElseIf file.ImageUrl.ToString.Split(".").Last.ToLower = "txt" Then
                    fileIcon.ImageUrl = "Images/icon-text.gif"
                    lView1.Visible = False
                    lview2.Visible = True
                ElseIf file.ImageUrl.ToString.Split(".").Last.ToLower = "jpg" Or file.ImageUrl.ToString.Split(".").Last.ToLower = "jpeg" Or file.ImageUrl.ToString.Split(".").Last.ToLower = "png" Or file.ImageUrl.ToString.Split(".").Last.ToLower = "gif" Then
                    fileIcon.ImageUrl = file.ImageUrl
                    lView1.Visible = False
                    lview2.Visible = True
                Else
                    fileIcon.ImageUrl = "Images/doc_photo.png"
                    lView1.Visible = False
                    lview2.Visible = True
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
End Class
