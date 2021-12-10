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
            If Request.QueryString("ID") IsNot Nothing Then
                Dim AlbumId = Val(Request.QueryString("ID"))
                Dim dt As DataTable = DBManager.Getdatatable("Select * from vw_Allbum where Type='A' and Active=1 and Id=" & AlbumId)
                If dt.Rows.Count > 0 Then
                    'Fill Master
                    lblAlbumTitle.InnerHtml = dt.Rows(0).Item("Title").ToString
                    divAlbumDescription.InnerHtml = dt.Rows(0).Item("Description").ToString
                    If divAlbumDescription.InnerHtml = "" Then
                        divAlbumDescription.Visible = False
                    End If
                    'Fill Details
                    dt = DBManager.Getdatatable(AlbumTable & " where  AlbumId=" & AlbumId)
                    lvGallery.DataSource = dt
                    lvGallery.DataBind()
                Else
                    Response.Redirect("/")
                End If
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
End Class
