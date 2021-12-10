#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class Home
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblRes.Visible = False

            If Not Page.IsPostBack Then
                FillMasterProfile()
                FillNews()
                FillCourses()
                FillSeminars()
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
            Dim dtNews As DataTable = DBManager.Getdatatable("Select * from tblContent where Active='1' and ShowInHome='1' and Type='NEW' and isnull(IsDeleted,0)=0 order by ShowOrder")
            lvNews.DataSource = dtNews
            lvNews.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillVediosGallary()
        Try
            Dim dtGallery As DataTable = DBManager.Getdatatable("Select * from vw_Allbum where Type='V' and Active='1' and ShowInHome='1'  order by ShowOrder")
            lvGallery.DataSource = dtGallery
            lvGallery.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillPhotoGallary()
        Try
            Dim dtGallery As DataTable = DBManager.Getdatatable("Select  * from vw_Allbum where Type='A' and Active='1' and ShowInHome='1'  order by ShowOrder")
            lvPhotos.DataSource = dtGallery
            lvPhotos.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillSeminars()
        Try
            Dim dtCon As DataTable = DBManager.Getdatatable("Select  * from tblContent where Active='1' and ShowInHome='1' and Type='SEM' and isnull(IsDeleted,0)=0 order by ShowOrder")
            lvSeminars.DataSource = dtCon
            lvSeminars.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillCourses()
        Try
            Dim dtCon As DataTable = DBManager.Getdatatable("Select  * from tblContent where Active='1' and ShowInHome='1' and Type='CUR' and isnull(IsDeleted,0)=0 order by ShowOrder")
            lvCourses.DataSource = dtCon
            lvCourses.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillConferences()
        Try
            Dim dtCon As DataTable = DBManager.Getdatatable("Select  * from tblContent where Active='1' and ShowInHome='1' and Type='COF' and isnull(IsDeleted,0)=0 order by ShowOrder")
            lvConferences.DataSource = dtCon
            lvConferences.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub FillCategories()
        Try
            Dim dtAnlyticsCategory As DataTable = DBManager.Getdatatable("Select  distinct Category as Category,ShowOrder from tblContent where Active='1' and ShowInHome='1' and Type='ANL' and isnull(IsDeleted,0)=0 order by ShowOrder")
            Dim dv As DataView = New DataView(dtAnlyticsCategory)
            Dim distinctValues As DataTable = dv.ToTable(True, "Category")
            lvAnlyticsCategories.DataSource = distinctValues
            lvAnlyticsCategories.DataBind()
            lvCategories.DataSource = dtAnlyticsCategory
            lvCategories.DataBind()
            For Each item As ListViewItem In lvCategories.Items
                Dim Category As String = CType(item.FindControl("lblCategory"), Label).Text
                Dim lvAnalytics As ListView = CType(item.FindControl("lvAnalytics"), ListView)
                Dim dtAnlytics As DataTable = DBManager.Getdatatable("Select * from tblContent where Category=N'" + Category + "' and Active='1' and ShowInHome='1' and Type='ANL' and isnull(IsDeleted,0)=0 order by ShowOrder")
                lvAnalytics.DataSource = dtAnlytics
                lvAnalytics.DataBind()
            Next
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


End Class
