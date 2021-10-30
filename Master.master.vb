
Imports BusinessLayer.BusinessLayer

Partial Class Master
    Inherits System.Web.UI.MasterPage
#Region "Page_Load"
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Redirect("~/comingsoon/index.html")
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

#Region "Search"
    Sub Search(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim FilterType As String = ddlFilterType.SelectedValue
        Dim SearchValue As String = txtSearch.Text.Replace(" ", "-")
        If String.IsNullOrEmpty(SearchValue) Then
            Exit Sub
        End If
        Select Case FilterType
            Case "A"
                Response.Redirect("~/Technical_Analysis.aspx?search=" + SearchValue + "")
            Case "N"
                Response.Redirect("~/News.aspx?search=" + SearchValue + "")
            Case "V"
                Response.Redirect("~/Album_Videos.aspx?search=" + SearchValue + "")
        End Select
    End Sub
#End Region

End Class
