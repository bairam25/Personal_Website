#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class Technical_Analysis
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblRes.Visible = False

            If Not Page.IsPostBack Then
                FillCategories()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub FillCategories()
        Try
            Dim SeachFilter As String = "1=1"
            If Request.QueryString("search") IsNot Nothing Then
                Dim searchValue As String = Request.QueryString("search").ToString.Replace("-", " ")
                SeachFilter = "(Title like N'%" + searchValue + "%' or Description like N'%" + searchValue + "%')"
            ElseIf Request.QueryString("More") IsNot Nothing Then
                Dim searchValue As String = Request.QueryString("More").ToString.Replace("-", " ")
                SeachFilter = "(Category = N'" + searchValue + "')"
            End If
            Dim dtAnlyticsCategory As DataTable = DBManager.Getdatatable("Select  distinct Category as Category,ShowOrder from tblContent where Active='1'  and Type='ANL' and isnull(IsDeleted,0)=0 and " + SeachFilter + " order by ShowOrder desc")
            Dim dv As DataView = New DataView(dtAnlyticsCategory)
            Dim distinctValues As DataTable = dv.ToTable(True, "Category")
            lvAnlyticsCategories.DataSource = distinctValues
            lvAnlyticsCategories.DataBind()

            lvCategories.DataSource = distinctValues
            lvCategories.DataBind()

            For Each item As ListViewItem In lvCategories.Items
                Dim Category As String = CType(item.FindControl("lblCategory"), Label).Text
                Dim lvAnalytics As ListView = CType(item.FindControl("lvAnalytics"), ListView)
                Dim dtAnlytics As DataTable = DBManager.Getdatatable("Select * from tblContent where Category=N'" + Category + "' and Active='1' and Type='ANL' and isnull(IsDeleted,0)=0 and " + SeachFilter + " order by ShowOrder desc")
                lvAnalytics.DataSource = dtAnlytics
                lvAnalytics.DataBind()
            Next
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
End Class
