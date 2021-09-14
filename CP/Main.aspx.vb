
#Region "Imports"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region

Partial Class c_panel_Main
    Inherits System.Web.UI.Page
#Region "Global Variables"
    Dim UserId As String = "0"
    Dim Client_Id As String = "1001"
#End Region

#Region "Page Load"
    ''' <summary>
    ''' Handle page load event.
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        lblRes.Visible = False
        UserId = PublicFunctions.GetUserId(Page)
        Try
            If Page.IsPostBack = False Then
                If Not PublicFunctions.CheckLogged() Then
                    Response.Redirect("Login.aspx")
                End If

            End If
        Catch ex As Exception
            ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Logout"
    ''' <summary>
    ''' Handle Logout event.
    ''' </summary>
    Protected Sub Logout(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Request.Cookies("ProfApp") IsNot Nothing Then
                Dim C As HttpCookie = Request.Cookies("ProfApp")
                C.Expires = Now.Date.AddDays(-10)
                Response.Cookies.Add(C)
            End If
        Catch ex As Exception
            ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
        'Redirect to login
        Response.Redirect("login.aspx")
    End Sub

#End Region
End Class
