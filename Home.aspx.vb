#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class Home
    Inherits System.Web.UI.Page
#Region "Page_Load"
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblRes.Visible = False

            If Not Page.IsPostBack Then
                FillMasterProfile()
                FillNews()
                FillConferences()
                FillCategories()
                FillVediosGallary()
                FillPhotoGallary()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub FillMasterProfile()
        Try
            Dim dtGallery As DataTable = DBManager.Getdatatable("Select * from tblProfile")
            lvMaster.DataSource = dtGallery
            lvMaster.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub FillNews()
        Try
            Dim dtNews As DataTable = DBManager.Getdatatable("Select top 3 * from tblContent where Active='1' and Type='NEW' and isnull(IsDeleted,0)=0 order by ShowOrder")
            lvNews.DataSource = dtNews
            lvNews.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillVediosGallary()
        Try
            Dim dtGallery As DataTable = DBManager.Getdatatable("Select top 3 * from vw_Allbum where Type='V'  order by ShowOrder")
            lvGallery.DataSource = dtGallery
            lvGallery.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillPhotoGallary()
        Try
            Dim dtGallery As DataTable = DBManager.Getdatatable("Select top 3 * from vw_Allbum where Type='A'  order by ShowOrder")
            lvPhotos.DataSource = dtGallery
            lvPhotos.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub FillConferences()
        Try
            Dim dtCon As DataTable = DBManager.Getdatatable("Select top 3 * from tblContent where Active='1' and Type='COF' and isnull(IsDeleted,0)=0 order by ShowOrder")
            lvConferences.DataSource = dtCon
            lvConferences.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub FillCategories()
        Try
            Dim dtAnlyticsCategory As DataTable = DBManager.Getdatatable("Select  distinct Category as Category from tblContent where Active='1' and Type='ANL' and isnull(IsDeleted,0)=0 ")
            lvAnlyticsCategories.DataSource = dtAnlyticsCategory
            lvAnlyticsCategories.DataBind()
            lvCategories.DataSource = dtAnlyticsCategory
            lvCategories.DataBind()
            For Each item As ListViewItem In lvCategories.Items
                Dim Category As String = CType(item.FindControl("lblCategory"), Label).Text
                Dim lvAnalytics As ListView = CType(item.FindControl("lvAnalytics"), ListView)
                Dim dtAnlytics As DataTable = DBManager.Getdatatable("Select top 6 * from tblContent where Category='" + Category + "' and Active='1' and Type='ANL' and isnull(IsDeleted,0)=0 order by ShowOrder")
                lvAnalytics.DataSource = dtAnlytics
                lvAnalytics.DataBind()
            Next
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
End Class
