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
            Dim dtNews As DataTable = DBManager.Getdatatable("Select * from tblContent where Active='1' and ShowInHome='1' and Type='NEW' and isnull(IsDeleted,0)=0 order by ShowOrder desc")
            lvNews.DataSource = dtNews
            lvNews.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillVediosGallary()
        Try
            Dim dtGallery As DataTable = DBManager.Getdatatable("Select * from vw_Allbum where Type='V' and Active='1' and ShowInHome='1'  order by ShowOrder desc")
            lvGallery.DataSource = dtGallery
            lvGallery.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillPhotoGallary()
        Try
            Dim dtGallery As DataTable = DBManager.Getdatatable("Select  * from vw_Allbum where Type='A' and Active='1' and ShowInHome='1'  order by ShowOrder desc")
            lvPhotos.DataSource = dtGallery
            lvPhotos.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillSeminars()
        Try
            Dim dtCon As DataTable = DBManager.Getdatatable("Select  * from tblContent where Active='1' and ShowInHome='1' and Type='SEM' and isnull(IsDeleted,0)=0 order by ShowOrder desc")
            lvSeminars.DataSource = dtCon
            lvSeminars.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillCourses()
        Try
            Dim dtCon As DataTable = DBManager.Getdatatable("Select  * from tblContent where Active='1' and ShowInHome='1' and Type='CUR' and isnull(IsDeleted,0)=0 order by ShowOrder desc")
            lvCourses.DataSource = dtCon
            lvCourses.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Sub FillConferences()
        Try
            Dim dtCon As DataTable = DBManager.Getdatatable("Select  * from tblContent where Active='1' and ShowInHome='1' and Type='COF' and isnull(IsDeleted,0)=0 order by ShowOrder desc")
            lvConferences.DataSource = dtCon
            lvConferences.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub FillCategories()
        Try
            Dim qry = "Select  Id as CategoryId,Name as Category,ShowOrder from tblCategories where Active='1' and ShowInHome='1' and isnull(IsDeleted,0)=0 " &
                      " and Id in (select CategoryId from tblContent where Active='1' and ShowInHome='1' and Type='ANL' and isnull(IsDeleted,0)=0 ) " &
                      " order by ShowOrder"
            Dim dtAnlyticsCategory As DataTable = DBManager.Getdatatable(qry)
            lvAnlyticsCategories.DataSource = dtAnlyticsCategory
            lvAnlyticsCategories.DataBind()
            lvCategories.DataSource = dtAnlyticsCategory
            lvCategories.DataBind()
            If dtAnlyticsCategory.Rows.Count > 0 Then
                lbShowMoreTechAnalysis.HRef = "Technical_Analysis.aspx?CategoryId=" & dtAnlyticsCategory.Rows(0).Item("CategoryId").ToString.Replace(" ", "-")
            End If
            For Each item As ListViewItem In lvCategories.Items
                Dim CategoryId As String = CType(item.FindControl("lblCategoryId"), Label).Text
                Dim lvAnalytics As ListView = CType(item.FindControl("lvAnalytics"), ListView)
                Dim dtAnlytics As DataTable = DBManager.Getdatatable("Select * from tblContent where CategoryId='" + CategoryId + "' and Active='1' and ShowInHome='1' and Type='ANL' and isnull(IsDeleted,0)=0 order by ShowOrder desc")
                lvAnalytics.DataSource = dtAnlytics
                lvAnalytics.DataBind()
            Next
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


End Class
