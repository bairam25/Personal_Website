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
Partial Class Album_Photos
    Inherits System.Web.UI.Page
#Region "Global_Varaibles"

    Dim AlbumTable As String = "select * from vw_Allbum where Type='A' and Active=1 "

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
            Dim dt As DataTable = DBManager.Getdatatable(AlbumTable)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    ViewState("dtAlbumTable") = dt
                    ' Initialize the sorting expression.
                    ViewState("SortExpression") = "ModifiedDate DESC"
                    ' Populate the GridView.
                    BindListView()
                    dplvGallery.Visible = False
                    If dt.Rows.Count > 6 Then
                        dplvGallery.Visible = True
                    End If
                Else
                    lvGallery.DataSource = Nothing
                    lvGallery.DataBind()
                    dplvGallery.Visible = False
                End If
            End If



            'ScriptManager.RegisterClientScriptBlock(UP, Me.[GetType](), Guid.NewGuid().ToString(), "LoadJquery();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub



    ''' <summary>
    ''' Load data into the Listview.
    ''' </summary>
    Private Sub BindListView()
        Try
            If ViewState("dtAlbumTable") IsNot Nothing Then
                ' Get the DataTable from ViewState.
                Dim dtAlbumTable As DataTable = DirectCast(ViewState("dtAlbumTable"), DataTable)

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtAlbumTable)

                ' Set the sort column and sort order.
                dv.Sort = ViewState("SortExpression").ToString()

                ' Bind the Listview control.
                lvGallery.DataSource = dv
                lvGallery.DataBind()
                If dtAlbumTable.Rows.Count > 0 Then
                    dplvGallery.DataBind()
                End If
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    '''' <summary>
    '''' Set Number of rows at every page
    '''' </summary>
    'Protected Sub PageSize_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        dplvGallery.SetPageProperties(0, ddlPager.SelectedValue, True)
    '        FillGrid(sender, New EventArgs)
    '    Catch ex As Exception
    '        clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
    '    End Try
    'End Sub
    '''' <summary>
    '''' set Pager
    '''' </summary>
    Protected Sub OnPagePropertiesChanging(sender As Object, e As PagePropertiesChangingEventArgs)
        Try
            dplvGallery.SetPageProperties(e.StartRowIndex, e.MaximumRows, False)
            FillGrid(sender, New EventArgs)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


#End Region
End Class
