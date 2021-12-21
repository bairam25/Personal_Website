#Region "Imports"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.IO
Imports System.Globalization
Imports clsMessages
#End Region

Public Class PublicFunctions


#Region "Global Functions"
    Public Shared Function ServerURL() As String
        Dim URL As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & HttpContext.Current.Request.ApplicationPath
        Return URL
    End Function
    Public Shared Function BoolFormat(ByVal Value As Object) As Boolean
        Try
            Value = Value.ToString
            If Value = "" OrElse Value Is Nothing OrElse Value = vbNullString Then
                Return False
            Else
                Return Value
            End If
        Catch ex As Exception

            Return False
        End Try
    End Function
    Public Shared Function IntFormat(ByVal Value As Object) As Integer
        Try
            Value = Value.ToString
            If Value = "" OrElse Value Is Nothing OrElse Value = vbNullString Then
                Return "0"
            Else
                Return Val(Value).ToString("N2")
            End If
        Catch ex As Exception

            Return "0"
        End Try
    End Function
    Public Shared Function DecimalFormat(ByVal Value As Object) As Object
        Try
            Value = Value.ToString
            If Value = "" OrElse Value Is Nothing OrElse Value = vbNullString Then
                Return "0"
            Else
                Return CDbl(Value).ToString("N2")
            End If
        Catch ex As Exception

            Return "0"
        End Try
    End Function
    Public Shared Function RemoveSpecialChars(ByVal value As String) As String
        Try
            Dim strChars = New Regex("[-~!@#$%^&*/?:{}|';._<>]")
            value = strChars.Replace(value, " ")
            Return value
        Catch ex As Exception

            Return value
        End Try
    End Function
    Public Shared Function DateFormat(ByVal Value As Object) As Object
        Try
            If Value = "" OrElse Value Is Nothing OrElse Value = vbNullString Then
                Return String.Empty
            Else
                Return Convert.ToDateTime(Value).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
            End If
        Catch ex As Exception

            Return Nothing
        End Try
    End Function
    Public Shared Function DateFormat(ByVal Value As Object, ByVal Format As String, Optional lang As String = "EN") As Object
        Try
            If Value = "" OrElse Value Is Nothing OrElse Value = vbNullString Then
                Return String.Empty
            Else
                Select Case lang
                    Case "EN"
                        Return Convert.ToDateTime(Value).ToString(Format, CultureInfo.InvariantCulture)
                    Case "AR"
                        Return Convert.ToDateTime(Value).ToString(Format, New CultureInfo("ar-AE"))
                End Select

            End If
        Catch ex As Exception

            Return Nothing
        End Try
    End Function
    Public Shared Function CheckNoDatesConfilct(ByVal DateFrom As String, ByVal DateTo As String) As Boolean
        Try
            Dim Date1 As String = DateFrom
            Dim Date2 As String = DateTo
            If IsDate(Date1) And IsDate(Date2) Then
                If Convert.ToDateTime(Date1) > Convert.ToDateTime(Date2) Then
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function


    Public Function RandomNumber(ByVal MaxNumber As Integer,
Optional ByVal MinNumber As Integer = 0) As Integer

        'initialize random number generator
        Dim r As New Random(System.DateTime.Now.Millisecond)

        'if passed incorrect arguments, swap them
        'can also throw exception or return 0

        If MinNumber > MaxNumber Then
            Dim t As Integer = MinNumber
            MinNumber = MaxNumber
            MaxNumber = t
        End If

        Return r.Next(MinNumber, MaxNumber)

    End Function
    Public Shared Function GetReturnURL(FullUrl As String) As String
        Try
            If FullUrl.Contains("returnUrl") Then
                Dim FilterURL As String = FullUrl.Split(New String() {"returnUrl="}, StringSplitOptions.None)(1)
                Return FilterURL.Replace(".aspx", "")
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function


    Public Function ClearAll(ByRef Container As Control) As Boolean
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim k As Integer = 0
            Dim h As Integer = 0
            While Container.Controls.Count > i
                Dim O As Control = Container.Controls(i)
                If TypeOf O Is TextBox Then
                    CType(O, TextBox).Text = ""
                ElseIf TypeOf O Is GridView Then
                    CType(O, GridView).DataSource = Nothing
                    CType(O, GridView).DataBind()
                ElseIf TypeOf O Is CheckBox Then
                    CType(O, CheckBox).Checked = False
                ElseIf TypeOf O Is RadioButton Then
                    CType(O, RadioButton).Checked = False
                    'ElseIf TypeOf O Is RadioButtonList Then
                    '    CType(O, RadioButtonList).SelectedIndex = -1
                    'ElseIf TypeOf O Is CheckBoxList Then
                    '    CType(O, CheckBoxList).SelectedIndex = -1
                Else
                    While O.Controls.Count > j
                        Dim B As Control = O.Controls(j)
                        If TypeOf B Is TextBox Then
                            CType(B, TextBox).Text = ""
                        ElseIf TypeOf B Is GridView Then
                            CType(B, GridView).DataSource = Nothing
                            CType(B, GridView).DataBind()
                            'ElseIf TypeOf B Is DropDownList Then
                            '    CType(B, DropDownList).SelectedValue = "0"
                        ElseIf TypeOf O Is CheckBox Then
                            CType(O, CheckBox).Checked = False
                        Else
                            While B.Controls.Count > k
                                Dim C As Control = B.Controls(k)
                                If TypeOf C Is TextBox Then
                                    CType(C, TextBox).Text = ""
                                    'ElseIf TypeOf C Is DropDownList Then
                                    '    CType(C, DropDownList).SelectedValue = "0"
                                ElseIf TypeOf O Is CheckBox Then
                                    CType(O, CheckBox).Checked = False
                                ElseIf TypeOf C Is GridView Then
                                    CType(C, GridView).DataSource = Nothing
                                    CType(C, GridView).DataBind()
                                Else
                                    While C.Controls.Count > h
                                        Dim D As Control = C.Controls(h)
                                        If TypeOf D Is TextBox Then
                                            CType(D, TextBox).Text = ""
                                        ElseIf TypeOf D Is GridView Then
                                            CType(D, GridView).DataSource = Nothing
                                            CType(D, GridView).DataBind()
                                            'ElseIf TypeOf D Is DropDownList Then
                                            '    CType(D, DropDownList).SelectedValue = "0"
                                        ElseIf TypeOf O Is CheckBox Then
                                            CType(O, CheckBox).Checked = False
                                        End If
                                        h += 1
                                    End While
                                End If
                                k += 1
                            End While
                        End If
                        j += 1
                    End While
                End If
                i += 1
                j = 0
                k = 0
                h = 0
            End While
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function

    '#End Region

    '#Region "Get From DB"

    '#Region "Look Up"

    Public Shared Function GetMaxLockup(Type As String) As String
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select isnull(max(orderNo),0) as orderNoMax from TblLookup where Type ='" + Type + "' And isNull(IsDeleted,0)=0")
            If dt.Rows.Count > 0 Then
                Dim orderMax As Integer = dt.Rows(0).Item("orderNoMax") + 1
                Return orderMax
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function GetLockupIdTrans(ByVal Value As String, ByVal Type As String, ByRef _SqlConnection As SqlConnection, ByRef _SqlTransaction As SqlTransaction) As String
        Try
            Dim dt As New DataTable
            dt = ExecuteQuery.ExecuteQueryAndReturnDataTable("select * from tblLookupValue where lookupId=(select top 1 ID from tbllookup where TYPE='" + Type + "') and value='" + Value + "' and  (ISNULL(IsDeleted, 0) = 0)", _SqlConnection, _SqlTransaction)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("Id")
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function GetLockupDT(ByVal Type As String) As DataTable
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select Id,Value,Icon,Code from tblLookupValue where lookupId=(select top 1 ID from tbllookup where TYPE='" + Type + "') and  (ISNULL(IsDeleted, 0) = 0)")
            Return dt
        Catch ex As Exception

            Return Nothing
        End Try
    End Function
    Public Shared Function GetLockupId(ByVal Value As String, ByVal Type As String) As String
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select Id from tblLookupValue where lookupId=(select top 1 ID from tbllookup where TYPE='" + Type + "') and value='" + Value + "' and  (ISNULL(IsDeleted, 0) = 0)")
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("Id")
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function GetLockupValue(ByVal LookupId As String) As String
        Try
            If LookupId <> "" Then

                Dim dt As New DataTable
                dt = DBManager.Getdatatable("Select value from tblLookupValue where Id='" & LookupId & "' And isNULL(IsDeleted,0)=0 ")


                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("value")
                Else
                    Return String.Empty
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function GetLockupCode(ByVal LookupId As String) As String
        Try
            If LookupId <> "" Then

                Dim dt As New DataTable
                dt = DBManager.Getdatatable("Select Code from tblLookupValue where Id='" & LookupId & "' And isNULL(IsDeleted,0)=0 ")


                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("Code")
                Else
                    Return String.Empty
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function GetFirstLockupValueByType(ByVal Type As String) As String
        Try


            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select top 1 value from tblLookupValue where lookupId=(select top 1 ID from tbllookup where TYPE='" + Type + "') and  (ISNULL(IsDeleted, 0) = 0) ")


            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("value")
            Else
                Return String.Empty
            End If

        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function GetLockupParentId(ByVal Value As String, ByVal Type As String) As String
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select RelatedValueId from tblLookupValue where lookupId=(select top 1 ID from tbllookup where TYPE='" + Type + "') and value='" + Value + "' and  (ISNULL(IsDeleted, 0) = 0)")
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("RelatedValueId")
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function CountryIdByCode(ByVal Code As String) As String
        Try
            Dim Country As String = GetLockupParentId(Code, "CountryExtensions")
            If Country = String.Empty Then
                Return "0"
            End If
            Return Country
        Catch ex As Exception

            Return "0"
        End Try
    End Function
    ''' <summary>
    ''' This function to check the null values in the retrieved data from data table and set it to empty string
    ''' </summary>
    Public Shared Function CheckDT(ByRef dt As DataTable) As DataTable
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim ob As New Object
            While i < dt.Rows.Count
                While j < dt.Columns.Count
                    If IsDBNull(dt.Rows(i).Item(j)) Then

                        If TypeOf (dt.Rows(i).Item(j)) Is Integer Then
                            dt.Rows(i).Item(j) = 0
                        ElseIf TypeOf (dt.Rows(i).Item(j)) Is DateTime Then
                            dt.Rows(i).Item(j) = ""
                        Else
                            dt.Rows(i).Item(j) = 0
                        End If






                    End If
                    j += 1
                End While
                i += 1
            End While

            Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Shared Function Check_DT_Values(ByVal dtValue As Object, ByVal dtLookup As Boolean) As String
        Try
            If TypeOf (dtValue) Is String Then
                If dtLookup Then
                    Return PublicFunctions.GetLockupValue(dtValue)
                Else
                    Return dtValue
                End If
            ElseIf TypeOf (dtValue) Is Integer Then
                If dtLookup Then
                    Return PublicFunctions.GetLockupValue(dtValue)
                Else
                    Return dtValue
                End If
            ElseIf TypeOf (dtValue) Is Double Then
                If dtLookup Then
                    Return PublicFunctions.GetLockupValue(dtValue)
                Else
                    Return dtValue
                End If
            ElseIf TypeOf (dtValue) Is Decimal Then
                If dtLookup Then
                    Return PublicFunctions.GetLockupValue(dtValue)
                Else
                    Return dtValue
                End If
            ElseIf TypeOf (dtValue) Is DateTime Then
                Return dtValue
            ElseIf TypeOf (dtValue) Is Boolean Then
                Return dtValue
            ElseIf TypeOf (dtValue) Is Single Then
                Return dtValue
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function

    Public Shared Function GetIdentity(ByRef _SqlConnection As SqlConnection, ByRef _SqlTransaction As SqlTransaction) As String
        Try
            If _SqlConnection.State <> ConnectionState.Open Then
                _SqlConnection.Open()
            End If
            Dim dt As DataTable = ExecuteQuery.ExecuteQueryAndReturnDataTable("SELECT @@Identity AS identityId", _SqlConnection, _SqlTransaction)
            If dt.Rows.Count > 0 Then
                Dim identity As String = dt.Rows(0).Item("identityId").ToString
                Return identity
            Else
                _SqlTransaction.Rollback()
                _SqlConnection.Close()
                Return String.Empty
            End If
        Catch ex As Exception

            _SqlTransaction.Rollback()
            _SqlConnection.Close()
            Return String.Empty
        End Try
    End Function

    Public Shared Function GetIdentity(ByVal TableName As String) As String
        Try
            Dim dt As DataTable = DBManager.Getdatatable("SELECT  isnull(IDENT_CURRENT('" + TableName + "'),0) AS identityId")
            If dt.Rows.Count > 0 Then
                Dim identity As String = dt.Rows(0).Item("identityId").ToString
                Return identity
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function GetNextIdentity(ByVal TableName As String) As String
        Try
            Dim dt As DataTable = DBManager.Getdatatable("SELECT  isnull(IDENT_CURRENT('" + TableName + "'),0)+1 AS identityId")
            If dt.Rows.Count > 0 Then
                Dim identity As String = dt.Rows(0).Item("identityId").ToString
                Return identity
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function FindIndex(ByVal dt As DataTable, ByVal value As String, ByVal ColumnName As String) As Integer
        Try
            Dim i As Integer = 0
            While i < dt.Rows.Count
                If dt.Rows(i).Item(ColumnName) = value Then
                    Return i
                End If
                i += 1
            End While
        Catch ex As Exception

        End Try
    End Function

    Public Shared Function RemoveMobileZeros(ByVal MobileNo As String) As String
        MobileNo = MobileNo.TrimStart("0")
        Return MobileNo
    End Function
#End Region

#Region "Grid View Selection"
    Public Shared Function DeleteAllSelected(lvContent As ListView) As Boolean
        Try
            Dim ContentIds As String = " "
            Dim Count As Integer = 0
            For Each item As ListViewItem In lvContent.Items
                Dim chkSelect As CheckBox = CType(item.FindControl("chkSelect"), CheckBox)
                Dim ContentId As String = CType(item.FindControl("lblContentId"), Label).Text
                If chkSelect.Checked Then
                    Count += 1
                    ContentIds += "'" + ContentId + "',"
                End If
            Next
            ContentIds = ContentIds.Remove(ContentIds.Length - 1, 1)
            If Count > 0 Then
                Dim Deleted As Integer = DBManager.ExcuteQuery("update tblContent SET  Isdeleted = 'True' where Id in (" & ContentIds & ")")
                If Deleted = 1 Then
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckAddressSelected(ByVal lv As ListView) As Boolean
        Try
            Dim i As Integer = 0
            Dim Count As Integer = 0

            While i < lv.Items.Count
                If CType(lv.Items(i).FindControl("rbDefaultAddress"), RadioButton).Checked = True Then
                    Count += 1
                End If
                i += 1
            End While
            If Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception

            Return False
        End Try
    End Function

    Public Function ChecksCountIsNotZero(ByRef pnl As Panel, ByVal GV As GridView, ByRef cmdDelete As LinkButton, ByRef cmdUpdate As LinkButton) As Boolean
        Try
            Dim i As Integer = 0
            Dim Count As Integer = 0

            While i < GV.Rows.Count
                If CType(GV.Rows(i).FindControl("chkSelect"), CheckBox).Checked = True Then
                    Count += 1
                End If
                i += 1
            End While
            If Count <> 0 Then
                cmdDelete.Enabled = True
                If Count = 1 Then
                    cmdUpdate.Enabled = True
                Else
                    cmdUpdate.Enabled = False
                End If
            Else
                cmdDelete.Enabled = False
                cmdUpdate.Enabled = False
            End If
            pnl.Enabled = True
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function

    Public Function GetSelectedItemID(ByVal gv As GridView) As Integer
        Try
            Dim i As Integer = 0

            While i < gv.Rows.Count
                If CType(gv.Rows(i).FindControl("chkSelect"), CheckBox).Checked = True Then
                    Return Val(CType(gv.Rows(i).FindControl("chkSelect"), CheckBox).ToolTip)
                End If
                i += 1
            End While
            Return 0
        Catch ex As Exception

            Return 0
        End Try
    End Function
    Public Shared Function GetColumnIndex(ByVal gv As GridView, sortexpression As String) As Integer
        If gv.HeaderRow Is Nothing Then
            Return 0
        End If
        Dim i As Integer = 0
        While i < gv.Columns.Count
            If gv.HeaderRow.Cells(i).CssClass = "faUp" Or gv.HeaderRow.Cells(i).CssClass = "faDown" Then
                gv.HeaderRow.Cells(i).CssClass = "upnDownArrow"
            End If

            i += 1
        End While
        i = 0
        If sortexpression = "" Then
            Return 0
        End If
        For Each c As DataControlField In gv.Columns
            If c.SortExpression = sortexpression Then
                gv.HeaderRow.Cells(i).CssClass = ""
                Exit For
            End If

            i += 1

        Next
        Return i


    End Function

#End Region

#Region "Table & Column"

    ''' <summary>
    ''' Return Id from tableName where FieldName = Value.
    ''' </summary>
    Public Shared Function getID(ByVal tableName As String, ByVal FieldName As String, ByVal Value As String) As String
        Try

            Dim dt As New DataTable
            dt = DBManager.Getdatatable("SELECT Id FROM " + tableName + " WHERE " + FieldName + " = '" + Value + "' AND ISNULL(IsDeleted, 0) = 0   ")
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("Id")
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function getIDTrans(ByVal tableName As String, ByVal FieldName As String, ByVal Value As String, ByRef _SqlConnection As SqlConnection, ByRef _SqlTransaction As SqlTransaction) As String
        Try
            Dim dt As New DataTable
            dt = ExecuteQuery.ExecuteQueryAndReturnDataTable("SELECT Id FROM " + tableName + " WHERE " + FieldName + " = '" + Value + "' AND ISNULL(IsDeleted, 0) = 0 ", _SqlConnection, _SqlTransaction)
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("Id")
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    ''' <summary>
    ''' Return value of selectionColumn from tableName where Id = Value.
    ''' </summary>
    Public Shared Function getValue(ByVal selectedColumn As String, ByVal tableName As String, ByVal Value As String) As String
        Try

            Dim dt As New DataTable
            'Dim dd As String = "select " + selectedColumn + " from " + tableName + "  where Id='" + Value + "'"
            dt = DBManager.Getdatatable("SELECT " + selectedColumn + " FROM " + tableName + " WHERE Id = '" + Value + "' AND ISNULL(IsDeleted, 0) = 0  ")
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(selectedColumn)
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function

#End Region

#Region "Page Operations"

    Public Shared Function GetPageName(ByVal Url As String) As String
        Try
            Dim pageName As String = Url.ToString.Split("/").Last.Split("?")(0)
            If pageName <> "" Then
                pageName = pageName.Replace(".aspx", "")
                Return pageName
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function

    Public Shared Function GetPageOperation(ByVal Url As String) As String
        Try
            Dim Operation As String = Url.ToString.Split("/").Last
            If Operation <> "" And Operation.Contains("?") Then
                If Operation.Split("?")(1).Split("=")(0) = "Operation" Then
                    Operation = Operation.Split("?")(1).Split("=")(1)
                    Return Operation
                Else
                    Return String.Empty
                End If
            Else
                Return String.Empty
            End If
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function


    Function GetQueryString(ByVal url As String, ByVal lbText As String, ByVal CodeValue As String) As String
        Try
            Dim enc As String
            If lbText = "New" Then
                'enc = QueryStringModule.Encrypt("Operation=Add&code=" + CodeValue + "")
                enc = "Operation=Add&code=" + CodeValue + ""
            Else
                'enc = QueryStringModule.Encrypt("Operation=Search&code=" + CodeValue + "")
                enc = "Operation=Search&code=" + CodeValue + ""
            End If
            url += "?" + enc
            Return url
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function

#End Region

#Region "Encryption"

    Public Shared Function Encrypt(ByVal clearText As String) As String
        Try
            Dim EncryptionKey As String = "MAKV2SPBNI99010"
            Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(clearText)
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
                 &H65, &H64, &H76, &H65, &H64, &H65,
                 &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                        cs.Write(clearBytes, 0, clearBytes.Length)
                        cs.Close()
                    End Using
                    clearText = Convert.ToBase64String(ms.ToArray())
                End Using
            End Using
            Return clearText
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function

    Public Shared Function Decrypt(ByVal cipherText As String) As String
        Try
            Dim EncryptionKey As String = "MAKV2SPBNI99010"
            Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText.Replace(" ", "+"))
            Using encryptor As Aes = Aes.Create()
                Dim pdb As New Rfc2898DeriveBytes(EncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
                 &H65, &H64, &H76, &H65, &H64, &H65,
                 &H76})
                encryptor.Key = pdb.GetBytes(32)
                encryptor.IV = pdb.GetBytes(16)
                Using ms As New MemoryStream()
                    Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                        cs.Write(cipherBytes, 0, cipherBytes.Length)
                        cs.Close()
                    End Using
                    cipherText = Encoding.Unicode.GetString(ms.ToArray())
                End Using
            End Using
            Return cipherText
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function

#End Region

#Region "User"
    Public Shared Function GetUserId(ByVal Username As String) As String
        Dim UserId As String = "0"
        Try

            Dim dtUsers As DataTable = DBManager.Getdatatable("select UserId from tblCPUsers where Isnull(IsDeleted,0)=0  and username='" + Username + "'")
            If dtUsers.Rows.Count > 0 Then
                UserId = dtUsers.Rows(0)(0).ToString
            End If
            Return UserId
        Catch ex As Exception

            Return "0"
        End Try
    End Function
    Public Shared Function GetUsername(ByVal UserId As String) As String
        Dim Username As String = String.Empty
        Try
            Dim dtUsers As DataTable = DBManager.Getdatatable("select Username from tblCPUsers where Isnull(IsDeleted,0)=0  and UserId='" + UserId + "'")
            If dtUsers.Rows.Count > 0 Then
                Username = dtUsers.Rows(0)(0).ToString
            End If
            Return Username
        Catch ex As Exception

            Return String.Empty
        End Try
    End Function
    Public Shared Function GetUserType(ByVal UserId As String) As String
        Dim UserType As String = "0"
        Try
            Dim dtUsers As DataTable = DBManager.Getdatatable("select UserType from tblCPUsers where Isnull(IsDeleted,0)=0 and UserId='" + UserId + "'")
            If dtUsers.Rows.Count > 0 Then
                UserType = dtUsers.Rows(0)(0).ToString
            End If
            Return UserType
        Catch ex As Exception

            Return "0"
        End Try
    End Function

    Public Shared Function GetUserId() As String
        Try
            If HttpContext.Current.Request.Cookies.Get("ProfApp") IsNot Nothing Then
                Dim UserId As String = HttpContext.Current.Request.Cookies("ProfApp")("UserId")
                Return UserId
            End If

        Catch ex As Exception
            RemoveCPCookie()
            Return "0"
        End Try
        Return "0"
    End Function

    Public Shared Function GetUserId(ByVal page As Page) As String
        Try
            If HttpContext.Current.Request.Cookies.Get("ProfApp") IsNot Nothing Then
                Dim UserId As String = HttpContext.Current.Request.Cookies("ProfApp")("UserId")
                Return UserId
            Else
                RemoveCPCookie()
                page.Response.Redirect("Login.aspx", True)
                Return "0"
            End If
        Catch ex As Exception
            RemoveCPCookie()
            page.Response.Redirect("Login.aspx", True)
            Return "0"
        End Try
    End Function

    Public Shared Function RemoveCPCookie() As Boolean
        Try
            Dim UELPDCookies As New HttpCookie("ProfApp")
            UELPDCookies.Expires = DateTime.Now.AddDays(-1D)
            HttpContext.Current.Response.Cookies.Add(UELPDCookies)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function GetFullNameFirstWord(ByVal FullName As String) As String
        Try
            If FullName.Contains(" ") Then
                FullName = FullName.Split(" ")(0)
                Dim NameLength As String = FullName.Length.ToString
                If NameLength > 10 Then
                    FullName = FullName.Substring(0, Math.Min(FullName.Length, 10)) & "..."
                End If
            Else
                Dim NameLength As String = FullName.Length.ToString
                If NameLength > 10 Then
                    FullName = FullName.Substring(0, Math.Min(FullName.Length, 10)) & "..."
                End If
            End If
            Dim pattern As String = "\b(\w|['-])+\b"
            Dim result As String = Regex.Replace(FullName, pattern,
                                   Function(m) m.Value(0).ToString().ToUpper() & m.Value.Substring(1))
            Return result
        Catch ex As Exception
            Return FullName
        End Try
    End Function
#End Region

#Region "Login"
    ''' <summary>
    ''' Check cookies exsit or no for login is valid
    ''' </summary>

    Public Shared Function CheckLogged() As Boolean
        Try
            If HttpContext.Current.Request.Cookies.Get("ProfApp") Is Nothing Then
                Return False
            End If
            Dim CPUserId As String = HttpContext.Current.Request.Cookies("ProfApp")("UserId")
            Dim CPUsername As String = HttpContext.Current.Request.Cookies("ProfApp")("Username")

            If CPUserId <> String.Empty Then
                Return True
            Else
                'remove that cookies
                RemoveCPCookie()
                Return False
            End If
            Return False
        Catch ex As Exception

            Return False
        End Try

    End Function
    Public Shared Function CheckUserActive(ByVal CPUserId As String, ByVal CPUsername As String) As Boolean
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select UserId from tblCPUsers where isnull(isdeleted,0)=0 and Active=1 and UserId=@Par1 and Username=@Par2", CPUserId, CPUsername)
            If dt.Rows.Count = 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception

            Return False
        End Try
    End Function


#End Region

#Region "Validation"
    Public Shared Function IsGroupValid(ByVal sValidationGroup As String, ByVal page As Page) As Boolean
        For Each validator As BaseValidator In page.Validators
            If (validator.ValidationGroup = sValidationGroup) Then
                Dim fValid As Boolean = validator.IsValid
                If fValid Then
                    validator.Validate()
                    fValid = validator.IsValid
                    validator.IsValid = True
                End If
                If Not fValid Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function
#End Region
End Class