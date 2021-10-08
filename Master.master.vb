
Imports BusinessLayer.BusinessLayer

Partial Class Master
    Inherits System.Web.UI.MasterPage
#Region "Page_Load"
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                FillContent()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillContent()
        Dim dtProfile = DBManager.Getdatatable("Select * from tblProfile")
        lvSocialMedia.DataSource = dtProfile
        lvSocialMedia.DataBind()
    End Sub

#End Region
End Class
