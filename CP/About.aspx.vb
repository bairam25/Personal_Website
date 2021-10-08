
#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class Profile
    Inherits System.Web.UI.Page
#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim UserId As String = "1"
    Dim Name As String
    Dim Email As String
    Dim Mobile As String
    Dim Tel As String
    Dim Age As String
    Dim Country As String
    Dim City As String
    Dim Address As String
    Dim Bio As String
    Dim Degree As String
    Dim Experience As String
    Dim Certificate As String
    Dim skills As String
    Dim Vision As String
    Dim PHoto As String
    Dim FaceboolURL As String
    Dim YoutubeURL As String
    Dim InstagramURL As String
    Dim LinkedInURL As String
    Dim TwitterURL As String
    Dim CVURL As String
    Dim ContentTable As String = "select * from tblProfile "

    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region
#Region "Public_Functions"

    ''' <summary>
    ''' If b is true : hide gridview, Operation panel bar and show panel form, Confirmation panel bar.
    ''' If b is false : hide panel form, Confirmation panel bar and show gridview, Operation panel bar.
    ''' </summary>
    Protected Sub Enabler(ByVal b As Boolean)
        Try
            pnlConfirm.Visible = b
            'pnlOps.Visible = Not b
            pnlForm.Enabled = b
            pnlForm.Visible = b
            'pnlGV.Visible = Not b
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
            Email = txtEmail.Text
            Mobile = txtMobile.Text
            Tel = txtPhone.Text
            Age = txtAge.Text
            Country = txtCountry.Text
            City = txtCity.Text
            Address = txtAddress.Text
            Bio = txtBio.TextValue
            Degree = txtDegree.TextValue
            Experience = txtExperience.TextValue
            Certificate = txtCertificates.TextValue
            skills = txtSkills.TextValue
            Vision = txtVision.TextValue
            FaceboolURL = txtFacebook.Text
            YoutubeURL = txtYouTube.Text
            InstagramURL = txtInstagram.Text
            LinkedInURL = txtLinkedIn.Text
            TwitterURL = txtTwitter.Text
            CVURL = txtCVURL.Text
            PHoto = HiddenContentImg.Text

            FillImages()
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
                FillForm()
            End If
            'Set Default values of controls
            SetControlFields()
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
            dt = DBManager.Getdatatable(ContentTable)
            If dt.Rows.Count <> 0 Then
                txtName.Text = dt.Rows(0).Item("Name").ToString
                txtEmail.Text = dt.Rows(0).Item("Email").ToString
                txtMobile.Text = dt.Rows(0).Item("Mobile").ToString
                txtPhone.Text = dt.Rows(0).Item("Tel").ToString
                txtAge.Text = dt.Rows(0).Item("Age").ToString
                txtCountry.Text = dt.Rows(0).Item("Country").ToString
                txtCity.Text = dt.Rows(0).Item("City").ToString
                txtAddress.Text = dt.Rows(0).Item("Address").ToString
                txtBio.TextValue = dt.Rows(0).Item("Bio").ToString
                txtDegree.TextValue = dt.Rows(0).Item("Degree").ToString
                txtExperience.TextValue = dt.Rows(0).Item("Experience").ToString
                txtCertificates.TextValue = dt.Rows(0).Item("Certificates").ToString
                txtSkills.TextValue = dt.Rows(0).Item("skills").ToString
                txtVision.TextValue = dt.Rows(0).Item("Vision").ToString
                txtFacebook.Text = dt.Rows(0).Item("FacebookURL").ToString
                txtYouTube.Text = dt.Rows(0).Item("YoutubeURL").ToString
                txtInstagram.Text = dt.Rows(0).Item("InstagramURL").ToString
                txtLinkedIn.Text = dt.Rows(0).Item("LinkedInURL").ToString
                txtTwitter.Text = dt.Rows(0).Item("TwitterURL").ToString
                txtCVURL.Text = dt.Rows(0).Item("CVURL").ToString
                PHoto = HiddenContentImg.Text = dt.Rows(0).Item("Photo").ToString
                HiddenContentImg.Text = dt.Rows(0).Item("Photo").ToString
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




#Region "Cancel"
    Protected Sub cancel(ByVal Sender As Object, ByVal e As System.EventArgs)

    End Sub
#End Region

#Region "Save"

    ''' <summary>
    ''' Handle save button(form grid) click event.
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim daProfile As New TblProfileFactory
        Dim dtProfile As New TblProfile
        Try
            If Not FillDT(dtProfile) Then
                Exit Sub
            End If
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            'Delete old contacts
            ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("Truncate Table TblProfile"))
            If Not daProfile.InsertTrans(dtProfile, _sqlconn, _sqltrans) Then
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
    ''' Fill dtContent from controls in the panel form.
    ''' </summary>
    Protected Function FillDT(ByRef dtProfile As TblProfile) As Boolean
        Try
            dtProfile.Name = Name
            dtProfile.Photo = PHoto
            dtProfile.Tel = Tel
            dtProfile.Mobile = Mobile
            dtProfile.Email = Email
            dtProfile.Bio = Bio
            dtProfile.Skills = skills
            dtProfile.Country = Country
            dtProfile.City = City
            dtProfile.Address = Address
            dtProfile.Degree = Degree
            dtProfile.Experience = Experience
            dtProfile.Certificates = Certificate
            dtProfile.FacebookURL = FaceboolURL
            dtProfile.YoutubeURL = YoutubeURL
            dtProfile.InstagramURL = InstagramURL
            dtProfile.LinkedinURL = LinkedInURL
            dtProfile.CVURL = CVURL
            dtProfile.TwitterURL = TwitterURL
            dtProfile.Vision = Vision
            dtProfile.Age = Age
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function
#End Region



#Region "Uploader"
    Protected Sub ContentPhotoUploaded(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Check that there is a file
            If fuPhoto.PostedFile IsNot Nothing Then

                Dim filePath As String = "~/ContentPhotos/" & fuPhoto.FileName

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
            If HiddenContentImg.Text IsNot Nothing And HiddenContentImg.Text <> "" Then
                imgContent.ImageUrl = HiddenContentImg.Text
            Else
                HiddenContentImg.Text = ""
                imgContent.ImageUrl = "~/images/img-up.png"
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
End Class
