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
            End If
            Dim qry = "Select  Id as CategoryId,Name as Category,ShowOrder from tblCategories where Active='1' and isnull(IsDeleted,0)=0 " &
                       " and Id in (select CategoryId from tblContent where Active='1' and Type='ANL' and isnull(IsDeleted,0)=0 and " + SeachFilter + ") " &
                      " order by ShowOrder"
            Dim dtAnlyticsCategory As DataTable = DBManager.Getdatatable(qry)
            lvAnlyticsCategories.DataSource = dtAnlyticsCategory
            lvAnlyticsCategories.DataBind()

            lvCategories.DataSource = dtAnlyticsCategory
            lvCategories.DataBind()

            For Each item As ListViewItem In lvCategories.Items
                Dim CategoryId As String = CType(item.FindControl("lblCategoryId"), Label).Text
                Dim lvAnalytics As ListView = CType(item.FindControl("lvAnalytics"), ListView)
                Dim dtAnlytics As DataTable = DBManager.Getdatatable("Select * from tblContent where CategoryId='" + CategoryId + "' and Active='1' and Type='ANL' and isnull(IsDeleted,0)=0 and " + SeachFilter + " order by ShowOrder desc")
                lvAnalytics.DataSource = dtAnlytics
                lvAnalytics.DataBind()
            Next
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Protected Function ActiveCategory(CategoryId As String, index As Integer, Type As String) As String
        Try
            Dim ActiveCatClass As String = ""
            Dim NotActiveCatClass As String = ""
            Select Case Type
                Case "1"
                    ActiveCatClass = "nav-link acive active"
                    NotActiveCatClass = "nav-link"
                Case "2"
                    ActiveCatClass = "tab-pane fade show active"
                    NotActiveCatClass = "tab-pane fade"
            End Select

            'check query string and set class depend on selected category else get first category
            If Request.QueryString("CategoryId") IsNot Nothing Then
                Dim QueryCategory As String = Request.QueryString("CategoryId").ToString.Replace("-", " ")
                If QueryCategory = CategoryId Then
                    Return ActiveCatClass
                End If
            Else
                If index = 0 Then
                    Return ActiveCatClass
                End If
            End If
            Return NotActiveCatClass
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Function
End Class
