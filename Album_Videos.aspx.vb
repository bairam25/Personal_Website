#Region "Signature"
'################################### Signature ######################################
'############# Date:02-07-2019
'############# Form Name: Gallary 
'############# Your Name: Ahmed Adel
'################################ End of Signature ##################################
#End Region

#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class Album_Videos
    Inherits System.Web.UI.Page
#Region "Global_Varaibles"

    Dim AlbumTable As String = "select * from vw_Allbum where Type='V' and Active=1 "

#End Region
#Region "Page_Load"
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            lblRes.Visible = False

            If Not Page.IsPostBack Then
                FillGrid(sender, e)

            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region


#Region "Fill Grid"

    ''' <summary>
    ''' Fill Listview with data.
    ''' </summary>
    Sub FillGrid(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim SeachFilter As String = "1=1"
            If Request.QueryString("search") IsNot Nothing Then
                Dim searchValue As String = Request.QueryString("search").ToString.Replace("-", " ")
                SeachFilter = "(Title like N'%" + searchValue + "%' or Description like N'%" + searchValue + "%')"
            End If
            Dim dt As DataTable = DBManager.Getdatatable(AlbumTable + " and " + SeachFilter)
            lvGallery.DataSource = dt
            lvGallery.DataBind()

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


#End Region
End Class
