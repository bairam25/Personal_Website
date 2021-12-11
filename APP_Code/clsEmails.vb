
#Region "Imports"

Imports Microsoft.VisualBasic
Imports BusinessLayer.BusinessLayer
Imports System.Net.Mail
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics

#End Region

Public Class clsEmails

#Region "Class Attributes"

    Private Shared CompanyName As String = "WeSynape"
    Private Shared CompanyEmail As String = "elsayedhussein.website@gmail.com"
    Private Shared CompanyPassword As String = "Sayed@789123"

#End Region

#Region "Class Methods"

    Public Shared Function SendExceptionMessage(ByRef ex As Exception) As Boolean
        Try
            'WriteInFile(ex)
            'If url.ToLower() = "localhost" Then
            ' Return False
            'End If

            Dim Mailto As String = ""
            Dim frmName As String = ""
            Dim UserName As String = ""
            Dim formUrl As String = ""
            Dim exceptionType As String = ""
            Dim trace As New System.Diagnostics.StackTrace(ex, True)
            Dim stTrace As String = ""
            Dim SubroutineName As String = ""
            Dim sf As New StackFrame
            Dim filename As String = ""
            Dim exMessage As String = ""
            Dim LineNo As String = 0
            Dim ClientName As String = "Elsayedhussein"
            Dim InnerEx As String = ""
            formUrl = System.Web.HttpContext.Current.Request.Url.Host
            frmName = GetPageName(HttpContext.Current.Request.Url.ToString)
            exceptionType = ex.GetType().Name.ToString
            stTrace = ex.StackTrace.ToString
            LineNo = stTrace.Split(":").Last.Split(" ").Last()
            exMessage = ex.Message.ToString
            If ex.InnerException IsNot Nothing Then
                InnerEx = ex.InnerException.Message
            End If

            If trace.GetFrame(1) IsNot Nothing Then
                SubroutineName = trace.GetFrame(1).GetMethod().ToString
                sf = trace.GetFrame(1)
            Else
                SubroutineName = trace.GetFrame(0).GetMethod().ToString
                sf = trace.GetFrame(0)
            End If




            Dim BodyText As String = "<table width='100%' border='1' cellpadding='5'> <tr> <td width='18%'><strong>TblException ID:</strong></td> <td width='82%'></td> </tr>" &
                  "<tr> <td><strong>Form Name:</strong></td> <td>" & frmName & "</td></tr>" &
                  " <tr> <td><strong>Exception Type:</strong></td> <td>" & exceptionType & "</td> </tr>" &
                  "<tr> <td><strong>Subroutine Name:</strong></td> <td>" & SubroutineName & "</td></tr>" &
                  "<tr>  <td><strong>Line No :</strong></td> <td>" & LineNo & "</td></tr>" &
                  "<tr>  <td><strong>Message:</strong></td> <td>" & exMessage & "</td></tr>" &
                  "<tr>  <td><strong>inner Message:</strong></td> <td>" & InnerEx & "</td></tr>" &
                  "<tr>  <td><strong>Stack Trace:</strong></td> <td>" & stTrace & "</td></tr>" &
                  " <tr> <td><strong>URL:</strong></td><td>" & formUrl & "</td> </tr>" &
                   " <tr> <td><strong>Client Id:</strong></td><td></td> </tr>" &
                    " <tr> <td><strong>Client Name:</strong></td><td>" & ClientName & "</td> </tr>" &
                  " <tr>  <td><strong>User Name:</strong></td>  <td>" & UserName & "</td> </tr></table>"


            Dim Mail As New MailMessage
            Mail.Subject = "Bug Report- " & ClientName & "-" & frmName
            Mail.To.Add("ahmed_dl_90@yahoo.com")
            Mail.CC.Add("mohamed.khairala@linkinsoft.com")
            Mail.From = New MailAddress("info@elsayedhussein.com", "Elsayedhussein")
            Mail.Body = BodyText
            Mail.IsBodyHtml = True

            Dim SMTP As New SmtpClient("relay-hosting.secureserver.net")
            SMTP.Credentials = New System.Net.NetworkCredential("info@elsayedhussein.com", "Sayed@789123$")
            SMTP.Port = "25"
            SMTP.Send(Mail)
            Return True
        Catch ex2 As Exception
            Return False
        End Try
    End Function

    Public Shared Function GetPageName(ByVal Url As String) As String
        Try
            Dim pageName As String = Url.ToString.Split("/").Last.Split("?")(0)
            If pageName <> "" Then
                pageName = pageName.Replace(".aspx", "")
                Return pageName
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function

#End Region




End Class