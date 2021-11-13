#Region "Import"
Imports System.Net.Mail
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class Contact
    Inherits System.Web.UI.Page
    Dim ContentTable As String = "select * from tblProfile "
#Region "Page Load"
    ''' <summary>
    ''' Handle page_load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblRes.Visible = False
        Try
            If Page.IsPostBack = False Then
                FillForm()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Fill controls in the panel form with the data.
    ''' </summary>
    Private Sub FillForm()
        Try
            rpAbout.DataSource = DBManager.Getdatatable(ContentTable)
            rpAbout.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Send Email"
    Private Function isValidEmail(ByVal strCheck As String) As Boolean
        Try
            Dim vEmailAddress As New System.Net.Mail.MailAddress(strCheck)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Protected Sub Send(sender As Object, e As EventArgs)
        Try
            Dim Email = txtEmail.Text.Trim
            Dim Name = txtUsername.Text.Trim
            Dim mobile = txtMobile.Text.Trim
            Dim title = txtTitle.Text.Trim
            Dim msg = txtMessage.Text

            If Not isValidEmail(Email) Then
                ShowInfoMessgage(lblRes, "Email Address is not valid!", Me)
                Exit Sub
            End If
            'Dim ToEmail = "elsayed@elsayedhussein.com"
            'Dim Smtp_Server As New SmtpClient
            'Dim e_mail As New MailMessage()
            'Smtp_Server.UseDefaultCredentials = False
            'Smtp_Server.Credentials = New Net.NetworkCredential("info@elsayedhussein.com", "Sayed@789123")
            'Smtp_Server.Port = 25
            ''Smtp_Server.EnableSsl = True
            'Smtp_Server.Host = "relay-hosting.secureserver.net"

            'e_mail = New MailMessage()
            'e_mail.From = New MailAddress(Email)
            'e_mail.To.Add(ToEmail)
            'e_mail.Subject = title
            'e_mail.IsBodyHtml = True
            'e_mail.Body = "Name : " & Name & "<br/> Mobile : " & mobile & "<br/> Email : " & Email & "<br/> Message : " & msg
            'Smtp_Server.Send(e_mail)

            Dim Mail As New MailMessage
            Mail.Subject = title
            Mail.To.Add("elsayed@elsayedhussein.com")
            Mail.From = New MailAddress("info@elsayedhussein.com", "elsayedhussein")
            Mail.Body = "Name : " & Name & "<br/> Mobile : " & mobile & "<br/> Email : " & Email & "<br/> Message : " & msg
            Mail.IsBodyHtml = True

            Dim SMTP As New SmtpClient("relay-hosting.secureserver.net")
            SMTP.Credentials = New System.Net.NetworkCredential("elsayedhussein.website@gmail.com", "Sayed@789123")
            SMTP.Port = "25"
            SMTP.Send(Mail)

            ShowSuccessMessgage(lblRes, "شكراً لتواصلكم معنا", Me)
            Clear()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Private Sub Clear()
        txtEmail.Text = String.Empty
        txtUsername.Text = String.Empty
        txtMobile.Text = String.Empty
        txtTitle.Text = String.Empty
        txtMessage.Text = String.Empty
    End Sub
#End Region
End Class
