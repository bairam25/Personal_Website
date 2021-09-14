
Imports System.Drawing


Public Class ImgResize

    Public Shared Function ResizeImageAndUpload(ByVal thisImage As Drawing.Image, ByVal folderPathAndFilenameNoExtension As String, ByVal maxHeight As Double, ByVal maxWidth As Double) As Boolean

        Try

            ' Declare variable for the conversion
            Dim ratio As Single

            ' Create variable to hold the image
            '  Dim thisImage As System.Drawing.Image = System.Drawing.Image.FromFile(File)

            ' Get height and width of current image
            Dim width As Integer = CInt(thisImage.Width)
            Dim height As Integer = CInt(thisImage.Height)

            ' Ratio and conversion for new size
            If width > maxWidth Then
                ratio = CSng(width) / CSng(maxWidth)
                width = CInt(Math.Truncate(width / ratio))
                height = CInt(Math.Truncate(height / ratio))
            End If

            ' Ratio and conversion for new size
            If height > maxHeight Then
                ratio = CSng(height) / CSng(maxHeight)
                height = CInt(Math.Truncate(height / ratio))
                width = CInt(Math.Truncate(width / ratio))
            End If

            ' Create "blank" image for drawing new image
            If width < 200 Or height < 200 Then
                width = 200
                height = 200
            End If
            Dim outImage As New Bitmap(width, height)
            Dim outGraphics As Graphics = Graphics.FromImage(outImage)
            Dim sb As New SolidBrush(System.Drawing.Color.White)


            ' Fill "blank" with new sized image

            outGraphics.FillRectangle(sb, 0, 0, outImage.Width, outImage.Height)
            outGraphics.DrawImage(thisImage, 0, 0, outImage.Width, outImage.Height)
            sb.Dispose()
            outGraphics.Dispose()
            thisImage.Dispose()

            ' Save new image as jpg
            outImage.Save(System.Web.HttpContext.Current.Server.MapPath(folderPathAndFilenameNoExtension))
            outImage.Dispose()


            Return True
        Catch generatedExceptionName As Exception
            Return False
        End Try
    End Function
    Public Shared Function ResizeImageAndUpload(ByVal newFile As System.IO.FileStream, ByVal folderPathAndFilenameNoExtension As String, ByVal maxHeight As Double, ByVal maxWidth As Double) As Boolean

        Try

            ' Declare variable for the conversion
            Dim ratio As Single

            ' Create variable to hold the image
            Dim thisImage As System.Drawing.Image = System.Drawing.Image.FromStream(newFile)

            ' Get height and width of current image
            Dim width As Integer = CInt(thisImage.Width)
            Dim height As Integer = CInt(thisImage.Height)

            ' Ratio and conversion for new size
            If width > maxWidth Then
                ratio = CSng(width) / CSng(maxWidth)
                width = CInt(Math.Truncate(width / ratio))
                height = CInt(Math.Truncate(height / ratio))
            End If

            ' Ratio and conversion for new size
            If height > maxHeight Then
                ratio = CSng(height) / CSng(maxHeight)
                height = CInt(Math.Truncate(height / ratio))
                width = CInt(Math.Truncate(width / ratio))
            End If

            ' Create "blank" image for drawing new image
            Dim outImage As New Bitmap(width, height)
            Dim outGraphics As Graphics = Graphics.FromImage(outImage)
            Dim sb As New SolidBrush(System.Drawing.Color.White)


            ' Fill "blank" with new sized image
            outGraphics.FillRectangle(sb, 0, 0, outImage.Width, outImage.Height)
            outGraphics.DrawImage(thisImage, 0, 0, outImage.Width, outImage.Height)
            sb.Dispose()
            outGraphics.Dispose()
            thisImage.Dispose()

            ' Save new image as jpg
            outImage.Save(System.Web.HttpContext.Current.Server.MapPath(folderPathAndFilenameNoExtension), System.Drawing.Imaging.ImageFormat.Jpeg)
            outImage.Dispose()


            Return True
        Catch generatedExceptionName As Exception
            Return False
        End Try
    End Function

    Public Shared Sub PrepareImages(ByVal fp As String, ByVal virtualPath As String, ByVal thumbPath As String)
        Try
            ''''''''''''''''''''''''''''''''' Main Photo Size'''''''''''''''''''''''''''''''
            Dim MainWidth As String = "1080" 'dtMainWidth.value
            Dim MainHeight As String = "1440" 'dtMainHeight.Value
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''''''Thumb Photo Size''''''''''''''''''''''''''''''
            Dim ThumbWidth As String = "378" ' dtThumbWidth.Value
            Dim ThumbHeight As String = "504"  'dtThumbHeight.Value
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim image__Main As Drawing.Image = Drawing.Image.FromFile(fp)
            If ImgResize.ResizeImageAndUpload(image__Main, virtualPath, MainHeight, MainWidth) Then
                Dim image__Thumb As Drawing.Image = Drawing.Image.FromFile(fp)
                ImgResize.ResizeImageAndUpload(image__Thumb, thumbPath, ThumbHeight, ThumbWidth)
            End If
            ' delete the temp file.
            System.IO.File.Delete(fp)
        Catch ex As Exception

        End Try
    End Sub

End Class
