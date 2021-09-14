
#Region "Import"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region

Partial Class cp_login
    Inherits System.Web.UI.Page
#Region "Global Variables"
    Dim UserId As String = "0"
#End Region

#Region "Page Load"

    ''' <summary>
    ''' Handle page load event.
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            lblRes.Visible = False
            If PublicFunctions.CheckLogged() Then
                Response.Redirect("Main.aspx")
                Exit Sub
            End If
            If Page.IsPostBack = False Then
                mvLogin.SetActiveView(vwLogin)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub

#End Region

#Region "Login"

    ''' <summary>
    ''' Check if user with entered username and password is exist or not.
    ''' </summary>
    Protected Sub CheckLogin(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim Username As String = txtUserName.Text
            Dim Password As String = txtPassword.Text

            If Username = String.Empty Then
                clsMessages.ShowMessage(lblRes, MessageTypesEnum.CUSTOMInfo, Me, Nothing, "أدخل اسم المستخدم")
                Exit Sub
            End If
            If Password = String.Empty Then
                clsMessages.ShowMessage(lblRes, MessageTypesEnum.CUSTOMInfo, Me, Nothing, "أدخل كلمة المرور")
                Exit Sub
            End If
            'Admin@AppProf_2050
            If Username.ToLower = "admin" And Password = "12345" Then
                If CreateCookie(Username) Then
                    Response.Redirect("Main.aspx")
                Else
                    clsMessages.ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "")
                    Exit Sub
                End If
            Else
                clsMessages.ShowMessage(lblRes, MessageTypesEnum.CUSTOMInfo, Me, Nothing, "كلمة المرور غير صحيحة")
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


    ''' <summary>
    ''' If user exist then fill cookie based on user type (Employee or Manager) and return true.
    ''' If not exist return false.
    ''' </summary>
    Private Function CreateCookie(Username As String) As Boolean
        Try
            Dim userCookie As HttpCookie = New HttpCookie("ProfApp")
            userCookie("Username") = Username
            userCookie("UserId") = "1"


            If chklogin.Checked = True Then
                userCookie.Expires = Today.AddDays(30)
            End If
            Response.Cookies.Add(userCookie)
            Return True
        Catch ex As Exception
            Return False
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Function

#End Region

End Class

