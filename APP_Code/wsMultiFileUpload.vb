#Region "Import"
Imports System.Web.Services
Imports System.IO
#End Region
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class wsMultiFileUpload
    Inherits System.Web.Services.WebService

    ''' <summary>
    ''' Upload file into a specific directory on the server
    ''' </summary>
    <WebMethod(True)> _
    <System.Web.Script.Services.ScriptMethod()>
    Public Function UploadFile(ByVal args As Object) As Array
        Try
            Dim FilePath As String = args("_fileId")
            Dim FolderName As String = args("_fileName").ToString.Split("|")(0)
            Dim FileName As String = args("_fileName").ToString.Split("|")(1)
            Dim OriginalName As String = args("_fileName").ToString.Split("|")(1)
            Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", FolderName))

            If Not Directory.Exists(directoryPath) Then
                Directory.CreateDirectory(directoryPath)
            End If

            Dim fp As String = Path.GetTempPath() & "_AjaxFileUpload" & "\" & FilePath & "\" & FileName
            If File.Exists(fp) Then
                Dim rnd As New Random
                FileName = rnd.Next(1000, 9999).ToString & FileName

                Dim virtualPath As String = FolderName & "\" & FileName
                Dim thumbPath As String = FolderName & "\" & "Thumb_" & FileName

                Dim FileExtension As String = FileName.Split(".").Last

                If FileExtension.ToLower = "jpg" OrElse FileExtension.ToLower = "jpeg" OrElse FileExtension.ToLower = "png" OrElse FileExtension.ToLower = "gif" Then
                    ImgResize.PrepareImages(fp, virtualPath, thumbPath)
                Else
                    File.Copy(fp, directoryPath & FileName)
                    File.Delete(fp)
                End If

            End If

            Dim arr As String() = {OriginalName, FolderName & FileName, FilePath}
            Return arr
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' delete file from the server
    ''' </summary>
    <WebMethod(True)> _
    <System.Web.Script.Services.ScriptMethod()>
    Public Function DeleteFile(ByVal val As Object) As String
        Try
            'Delete File from Directory
            Dim filepath As String = Server.MapPath(String.Format("~/{0}", val))
            If File.Exists(filepath) Then
                File.Delete(filepath)
                Return val
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

End Class