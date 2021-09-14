Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports System.Data.SqlClient
Imports System.Net
Public Class ExecuteQuery
    Dim _sqltransaction As SqlTransaction
    Private _sqlConnection As New SqlConnection(DBManager.GetConnectionString)


    Property __SqlConnection As SqlConnection
        Get
            Return _sqlConnection
        End Get
        Set(value As SqlConnection)
            _sqlConnection = value
        End Set
    End Property

    Public Function ExecuteAlCommands(ByVal _sqlCommand As SqlCommand) As Boolean

        Try
            _sqlConnection.Open()
            _sqlCommand.Connection = _sqlConnection
            _sqlCommand.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            _sqlConnection.Close()
        End Try

    End Function
    Public Shared Function ExecuteQueryAndReturnDataTable(ByVal sqlQuery As String, ByRef _SqlConnection As SqlConnection, ByRef _SqlTransaction As SqlTransaction) As DataTable
        Dim DT As New DataTable
        If sqlQuery Is Nothing Then Return Nothing
        DT.Clear()
        Try
            Dim sqlCmd As New SqlCommand(sqlQuery, _SqlConnection, _SqlTransaction)
            Dim SqlDadpt As New SqlDataAdapter(sqlCmd)
            SqlDadpt.Fill(DT)
        Catch ex As Exception
            _SqlTransaction.Rollback()
            _SqlConnection.Close()
            Throw ex
        Finally

        End Try

        Return DT
    End Function
    Public Shared Function ExecuteAlCommands(ByRef _sqltransaction As SqlTransaction, ByRef _sqlConnection As SqlConnection, ByVal _sqlCommand As SqlCommand) As Boolean

        Try

            _sqlCommand.Connection = _sqlConnection
            _sqlCommand.Transaction = _sqltransaction
            _sqlCommand.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            RollBackTransaction(_sqltransaction)
            Throw ex
        Finally

        End Try

    End Function

    Public Function ExecuteAlCommands(ByVal _sqlCommand() As SqlCommand) As Boolean

        Dim _sqltransaction As SqlTransaction

        Try
            _sqlConnection.Open()
            _sqltransaction = _sqlConnection.BeginTransaction()
            For i As Integer = 0 To _sqlCommand.Length - 1
                Try
                    If _sqlCommand(i).CommandText = String.Empty Then
                        Continue For
                    End If
                    _sqlCommand(i).Connection = _sqlConnection
                    _sqlCommand(i).Transaction = _sqltransaction
                    _sqlCommand(i).ExecuteNonQuery()
                Catch ex As Exception
                    RollBackTransaction(_sqltransaction) : Return False
                End Try
            Next
            _sqltransaction.Commit()
            Return True
        Catch ex As Exception
            RollBackTransaction(_sqltransaction) : Return False
        Finally
            _sqlConnection.Close()
        End Try

    End Function
#Region "khairllah"


    Public Shared Function ExecuteAlCommands(ByRef _sqltransaction As SqlTransaction, ByRef _sqlConnection As SqlConnection, ByVal _sqlCommand() As SqlCommand) As Boolean
        Try

            For i As Integer = 0 To _sqlCommand.Length - 1
                Try
                    If _sqlCommand(i) Is Nothing OrElse _sqlCommand(i).CommandText = "" OrElse _sqlCommand(i).CommandText = String.Empty Then
                        Continue For
                    End If
                    _sqlCommand(i).Connection = _sqlConnection
                    _sqlCommand(i).Transaction = _sqltransaction
                    _sqlCommand(i).ExecuteNonQuery()
                Catch ex As Exception
                    RollBackTransaction(_sqltransaction) : _sqlConnection.Close() : Return False
                End Try
            Next
            Return True
        Catch ex As Exception
            RollBackTransaction(_sqltransaction) : _sqlConnection.Close() : Return False
        Finally

        End Try

    End Function


    Public Shared Sub RollBackTransaction(ByRef _sqltransaction As SqlTransaction)
        If Not _sqltransaction Is Nothing AndAlso Not _sqltransaction.Connection Is Nothing AndAlso _sqltransaction.Connection.State = ConnectionState.Open Then
            _sqltransaction.Rollback()
        End If
    End Sub

    Public Shared Sub CloseConnection(ByRef _sqlConn As SqlConnection)
        Try

            If _sqlConn.State = ConnectionState.Open Then
                _sqlConn.Close()
            End If

        Catch ex As Exception

        End Try
    End Sub
    '#Region "GenerateCodeOFunit"


    '    Public Shared Function GenerateCodeOFunit(ByRef _SQLConnection As SqlConnection, ByVal category As String, ByVal TableName As String, Optional ByVal GoToTemp As Boolean = False) As String


    '        Dim cls As New ClsCategoryType
    '        Dim GCode As String = cls.ReturnLetter(LCase(category))

    '        Dim strSql = "Select isnull('" & GCode(0) & "'+convert(varchar,max(cast(substring(Code,2,3)as int))),'') " & _
    '               "from  " & TableName & "   where  substring (Code,1,1)='" & GCode(0) & "' and len(Code)>= 4 "


    '        'Dim strSql = "Select isnull(convert(varchar,max(PropCode)),'') from  " & TableName & "  where " & _
    '        '    " substring (PropCode,1,1)='" & GCode(o) & "' and len(PropCode)= 4 "


    '        Dim sqlCommand As New SqlCommand(strSql, _SQLConnection)
    '        Dim Result As String
    '        _SQLConnection.Open()
    '        Try
    '            Result = sqlCommand.ExecuteScalar
    '        Catch ex As Exception
    '            Result = vbNullString
    '        End Try
    '        _SQLConnection.Close()
    '        If Result = vbNullString Then
    '            GCode += "001"


    '        Else

    '            If Result.Length >= 4 Then
    '                If Result.Substring(1, Result.Length - 1) >= 999 Then
    '                    Dim maxvalue As String = (CInt(Result.Substring(1, Result.Length - 1)) + 1).ToString
    '                    maxvalue = "0000" + maxvalue
    '                    GCode = GCode(0) + maxvalue(maxvalue.Length - 4) + maxvalue(maxvalue.Length - 3) + maxvalue(maxvalue.Length - 2) + maxvalue(maxvalue.Length - 1)
    '                Else
    '                    Dim maxvalue As String = (CInt(Result.Substring(1, Result.Length - 1)) + 1).ToString
    '                    maxvalue = "000" + maxvalue
    '                    GCode = GCode(0) + maxvalue(maxvalue.Length - 3) + maxvalue(maxvalue.Length - 2) + maxvalue(maxvalue.Length - 1)
    '                End If
    '            Else
    '                Dim maxvalue As String = (CInt(Result.Substring(1, Result.Length - 1)) + 1).ToString
    '                maxvalue = "000" + maxvalue
    '                GCode = GCode(0) + maxvalue(maxvalue.Length - 3) + maxvalue(maxvalue.Length - 2) + maxvalue(maxvalue.Length - 1)
    '            End If
    '        End If
    '        'If GoToTemp = True And IsDefined("Select count(*) from TmptblProperty  where PropCode ='" & GCode & "' ") = True Then
    '        '    Return GenerateCode(category, "TmptblProperty", False)
    '        'End If
    '        Return GCode


    '    End Function
    '    Public Shared Function GenerateCodeOFunit(ByRef _SQLConnection As SqlClient.SqlConnection, ByRef _SQLTransaction As SqlClient.SqlTransaction, ByVal category As String, ByVal TableName As String) As String


    '        Dim cls As New ClsCategoryType
    '        Dim GCode As String = cls.ReturnLetter(LCase(category))

    '        Dim strSql = "Select isnull('" & GCode(0) & "'+convert(varchar,max(cast(substring(Code,2,3)as int))),'') " & _
    '               "from  " & TableName & "   where  substring (Code,1,1)='" & GCode(0) & "' and len(Code)>= 4 "


    '        'Dim strSql = "Select isnull(convert(varchar,max(PropCode)),'') from  " & TableName & "  where " & _
    '        '    " substring (PropCode,1,1)='" & GCode(o) & "' and len(PropCode)= 4 "


    '        Dim sqlCommand As New SqlClient.SqlCommand(strSql, _SQLConnection, _SQLTransaction)
    '        Dim Result As String
    '        Try
    '            Result = sqlCommand.ExecuteScalar
    '        Catch ex As Exception
    '            Result = vbNullString
    '            _SQLConnection.Close()
    '        End Try

    '        If Result = vbNullString Then
    '            GCode += "001"
    '        Else

    '            If Result.Length >= 4 Then
    '                If Result.Substring(1, Result.Length - 1) >= 999 Then
    '                    Dim maxvalue As String = (CInt(Result.Substring(1, Result.Length - 1)) + 1).ToString
    '                    maxvalue = "0000" + maxvalue
    '                    GCode = GCode(0) + maxvalue(maxvalue.Length - 4) + maxvalue(maxvalue.Length - 3) + maxvalue(maxvalue.Length - 2) + maxvalue(maxvalue.Length - 1)
    '                Else
    '                    Dim maxvalue As String = (CInt(Result.Substring(1, Result.Length - 1)) + 1).ToString
    '                    maxvalue = "000" + maxvalue
    '                    GCode = GCode(0) + maxvalue(maxvalue.Length - 3) + maxvalue(maxvalue.Length - 2) + maxvalue(maxvalue.Length - 1)
    '                End If
    '            Else
    '                Dim maxvalue As String = (CInt(Result.Substring(1, Result.Length - 1)) + 1).ToString
    '                maxvalue = "000" + maxvalue
    '                GCode = GCode(0) + maxvalue(maxvalue.Length - 3) + maxvalue(maxvalue.Length - 2) + maxvalue(maxvalue.Length - 1)
    '            End If
    '        End If

    '        Return GCode

    '    End Function
    '#End Region
#End Region
End Class
