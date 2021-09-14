
#Region "Imports"

Imports System.Web.Services
Imports System.Data
Imports BusinessLayer.BusinessLayer
#End Region

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class WebService
    Inherits System.Web.Services.WebService
    Dim pf As New PublicFunctions
#Region "Users"

    ''' <summary>
    ''' Get user Names
    ''' </summary>
    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()>
    Public Function GetUserNames(ByVal prefixText As String) As List(Of String)
        Dim Users As New List(Of String)
        prefixText = prefixText.Trim()
        Dim dt As New DataTable
        Try
            If prefixText = "*" Then
                dt = DBManager.Getdatatable("Select UserID,UserName from tblCPUsers where Isnull(IsDeleted,0)=0 ")
            Else
                dt = DBManager.Getdatatable("Select UserID,UserName from tblCPUsers where Isnull(IsDeleted,0)=0 and UserName like '" & prefixText & "%' ")
            End If

            For Each row As DataRow In dt.Rows
                Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(row.Item("UserName").ToString, row.Item("UserID").ToString)
                Users.Add(item)
            Next
            Return Users
        Catch ex As Exception
            Return Users
        End Try
    End Function

    ''' <summary>
    ''' Check User Name
    ''' </summary>
    ''' <param name="prefixText"></param>
    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()>
    Public Function CheckUserNameUnique(ByVal prefixText As String, ByVal ID As String) As Boolean
        Try
            prefixText = prefixText.Trim()
            Dim dt As New DataTable
            If ID <> "" Then
                dt = DBManager.Getdatatable("select * from tblCPUsers where UserName='" & prefixText & "' and UserID <> " & ID & " and isNULL(isDeleted,0)=0 ")
            Else
                dt = DBManager.Getdatatable("select * from tblCPUsers where UserName='" & prefixText & "'  and isNULL(isDeleted,0)=0 ")
            End If
            If dt.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Check Email Address
    ''' </summary>
    ''' <param name="prefixText"></param>
    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()>
    Public Function CheckEmailUnique(ByVal prefixText As String, ByVal ID As String) As Boolean
        Try
            prefixText = prefixText.Trim()
            Dim dt As New DataTable
            If ID <> "" Then
                dt = DBManager.Getdatatable("select * from tblCPUsers where Email='" & prefixText & "' and UserID <> " & ID & " and isNULL(isDeleted,0)=0 ")
            Else
                dt = DBManager.Getdatatable("select * from tblCPUsers where Email='" & prefixText & "'  and isNULL(isDeleted,0)=0 ")
            End If
            If dt.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region "Lookup"
    ''' <summary>
    ''' Return all DataType from LookUp .
    ''' Handles Settings form search.
    ''' </summary> 
    <System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()>
    Public Function GetLookupDataTypes(ByVal prefixText As String) As System.String()
        Dim LookupList As New List(Of String)
        Dim dt As New DataTable

        Try

            dt = DBManager.Getdatatable("select Type from tblLookup where isnull(IsDeleted,0)=0  and  Type like '%" + prefixText + "%' ")

            If dt.Rows.Count > 0 Then
                Dim i As Integer = 0
                While i < dt.Rows.Count And i < 10
                    LookupList.Add(dt.Rows(i)(0).ToString())
                    i += 1
                End While
                Return LookupList.ToArray()
            Else
                LookupList.Add("No results found!")
                Return LookupList.ToArray
            End If
        Catch ex As Exception
            LookupList.Clear()
            LookupList.Add("No results found!")
            Return LookupList.ToArray()
        End Try
    End Function

#End Region

End Class