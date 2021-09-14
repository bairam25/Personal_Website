
#Region "Imports"

Imports System.Data
Imports BusinessLayer.BusinessLayer

#End Region
Public Class clsBindDDL
    ''' <summary>
    ''' bind ddl controls for lookup Types
    ''' </summary>
    Public Shared Function BindLookupDDLs(ByVal DataType As String, ByRef ddlList As DropDownList, ByVal SelectOption As Boolean, Optional SelectOptionText As String = "-- Select --", Optional Sort As String = "ASC", Optional RelatedIdValue As Integer = 0) As Boolean
        Try
            ddlList.Items.Clear()
            ddlList.AppendDataBoundItems = True
            Dim list As New ListItem
            list.Value = "0"
            list.Text = SelectOptionText
            If SelectOption Then
                ddlList.Items.Add(list)
            End If
            Dim dtLookupValues As DataTable = DBManager.Getdatatable("select Id,value from tblLookupValue where lookupId=(select top 1 ID from tbllookup where TYPE='" + DataType + "') and (ISNULL(IsDeleted, 0) = 0)   order by value " + Sort + "")
            If RelatedIdValue <> 0 Then
                dtLookupValues = DBManager.Getdatatable("select Id,value from tblLookupValue where lookupId=(select top 1 ID from tbllookup where TYPE='" + DataType + "') and (ISNULL(IsDeleted, 0) = 0)  and RelatedValueId='" + RelatedIdValue.ToString + "'  order by value " + Sort + "")
            End If
            If dtLookupValues.Rows.Count <> 0 Then
                ddlList.DataSource = dtLookupValues
                ddlList.DataTextField = "Value"
                ddlList.DataValueField = "Id"
                ddlList.DataBind()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Shared Function BindLookupDDLs(ByVal DataType As String, ByRef ddlList As RadioButtonList, ByVal SelectOption As Boolean, Optional SelectOptionText As String = "-- Select --", Optional Sort As String = "ASC") As Boolean
        Try
            ddlList.Items.Clear()
            ddlList.AppendDataBoundItems = True
            Dim list As New ListItem
            list.Value = "0"
            list.Text = SelectOptionText
            If SelectOption Then
                ddlList.Items.Add(list)
            End If

            Dim dtLookupValues As DataTable = DBManager.Getdatatable("select Id,value from tblLookupValue where lookupId=(select top 1 ID from tbllookup where TYPE='" + DataType + "')  and (ISNULL(IsDeleted, 0) = 0)   order by value " + Sort + "")

            If dtLookupValues.Rows.Count <> 0 Then
                ddlList.DataSource = dtLookupValues
                ddlList.DataTextField = "Value"
                ddlList.DataValueField = "Id"
                ddlList.DataBind()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    ''' <summary>
    ''' custom bind ddl controls with query, DataTextField ,DataValueField
    ''' </summary>
    Public Shared Function BindCustomDDLs(ByVal Query As String, ByVal DataTextField As String, ByVal DataValueField As String, ByRef ddlList As DropDownList, ByVal SelectOption As Boolean, Optional SelectOptionText As String = "-- Select --", Optional Sort As String = "ASC", Optional Selected As String = "") As Boolean
        Try
            ddlList.Items.Clear()
            ddlList.AppendDataBoundItems = True
            Dim list As New ListItem
            list.Value = "0"
            list.Text = SelectOptionText
            If SelectOption Then
                ddlList.Items.Add(list)
            End If
            Dim dt As DataTable = DBManager.Getdatatable(Query + " order by " + DataTextField + " " + Sort + "  ")
            If dt.Rows.Count <> 0 Then
                ddlList.DataSource = dt
                ddlList.DataTextField = DataTextField
                ddlList.DataValueField = DataValueField
                ddlList.DataBind()
            End If
            If Selected <> "" Then
                ddlList.SelectedValue = Selected
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
