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
Partial Class Album_Photos_Details
    Inherits System.Web.UI.Page
#Region "Global_Varaibles"

    Dim AlbumTable As String = "select * from tblAlbumDetails"

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

            Dim AlbumId = 0
            If Request.QueryString("ID") IsNot Nothing Then
                AlbumId = Val(Request.QueryString("ID"))
            End If
            Dim dt As DataTable = DBManager.Getdatatable(AlbumTable & " where Type='I' and AlbumId=" & AlbumId)
            lvGallery.DataSource = dt
            lvGallery.DataBind()

            lblAlbumTitle.Text = PublicFunctions.getValue("Title", "tblAlbum", AlbumId)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub



#End Region
End Class
