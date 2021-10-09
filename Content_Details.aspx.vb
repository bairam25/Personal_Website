#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class News_Details
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblRes.Visible = False

            If Not Page.IsPostBack Then
                If Request.QueryString("Id") IsNot Nothing Then
                    lblContentId.Text = Request.QueryString("Id").ToString
                End If
                FillDetails()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillDetails()
        Try
            Dim dtDetails As DataTable = DBManager.Getdatatable("Select * from tblContent where Active='1' and Id='" & lblContentId.Text & "' and isnull(IsDeleted,0)=0 order by ShowOrder")
            lvContentDetails.DataSource = dtDetails
            lvContentDetails.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
End Class
