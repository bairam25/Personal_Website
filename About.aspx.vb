#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class About
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
End Class
