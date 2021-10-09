﻿#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class News

    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblRes.Visible = False

            If Not Page.IsPostBack Then
                FillNews()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Sub FillNews()
        Try
            Dim dtNews As DataTable = DBManager.Getdatatable("Select * from tblContent where Active='1' and Type='NEW' and isnull(IsDeleted,0)=0 order by ShowOrder")
            lvNews.DataSource = dtNews
            lvNews.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
End Class

