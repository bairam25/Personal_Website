
#Region "Import"
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region

Partial Class Settings
    Inherits System.Web.UI.Page

#Region "Global_Varaibles"
    Dim pf As New PublicFunctions
    Dim UserId As String = "0"
    Dim DataTypeValue As String
    Dim DataTypeCode As String
    Dim DataTypeValueDesc As String
    Dim DataTypeValueRT As String
    Dim DataTypeValueColor As String
#End Region

#Region "Public Functions"

    ''' <summary>
    ''' hide and show panels of types
    ''' </summary>
    Protected Sub EnablerType(ByVal b As Boolean)
        Try
            pnlTypeValues.Visible = b
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' hide and show panels of values
    ''' </summary>
    Protected Sub EnablerValue(ByVal b As Boolean)
        Try
            pnlNewValue.Visible = b
            pnlGvValues.Visible = Not b
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set Default controls Values 
    ''' </summary>
    Sub SetControlFields()
        Try
            DataTypeValue = txtValue.Text
            DataTypeCode = txtCode.Text
            DataTypeValueDesc = txtDescription.TextValue
            If pnlRType.Visible Then
                DataTypeValueRT = ddlRType.SelectedValue
            End If
            DataTypeValueColor = txtColor.Text
            FillIconPhoto()
            If txtSearchAll.Visible = False Then
                cmdClear.Visible = False
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Function GetItemsDT(ByVal Filter As String) As DataTable
        Try
            Dim dtItems As DataTable = DBManager.Getdatatable("select Category,SubCategory,Collection,Color,Size from tblItems where isnull(IsDeleted,0)=0  and " & Filter & "")
            Return dtItems
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Function CheckLookupItem(ByVal LookupId As String) As Boolean
        Try
            Select Case lblTypesName.Text
                Case "Item Category"
                    If GetItemsDT("Category='" & LookupId & "'").Rows.Count > 0 Then
                        Return False
                    End If
                Case "Item Sub Category"
                    If GetItemsDT("SubCategory='" & LookupId & "'").Rows.Count > 0 Then
                        Return False
                    End If
                Case "Collection"
                    If GetItemsDT("Collection='" & LookupId & "'").Rows.Count > 0 Then
                        Return False
                    End If
                Case "Item Size"
                    If GetItemsDT("Size='" & LookupId & "'").Rows.Count > 0 Then
                        Return False
                    End If
                Case "Item Color"
                    If GetItemsDT("Color='" & LookupId & "'").Rows.Count > 0 Then
                        Return False
                    End If
            End Select
            txtCode.Enabled = True
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function
#End Region

#Region "Page_Load"
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblRes.Visible = False
        UserId = PublicFunctions.GetUserId(Page)
        Try
            If Page.IsPostBack = False Then
                Permissions.CheckPermisions(gvValues, lbNewValue, txtSearchAll, New LinkButton, Me.Page, UserId)
                clsLogs.AddSystemLogs("Access")
                FillDataTypes()
            End If
            SetControlFields()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "FillGrid Data Types"
    ''' <summary>
    ''' Fill gridview with data from tblLookup.
    ''' </summary>
    Sub FillDataTypes()
        Try
            Dim dtDataTypes As DataTable = DBManager.Getdatatable("Select * from tblLookup where isnull(IsDeleted,0)=0 and id not in ('25','26','29','30','31','32','33')  and " + CollectConditions())
            If dtDataTypes.Rows.Count > 0 Then
                ' Initialize the sorting expression.
                If DataTypesSortExp.Value = String.Empty Then
                    DataTypesSortExp.Value = "Type DESC"
                End If

                Dim dv As New DataView(dtDataTypes)

                ' Set the sort column and sort order.
                dv.Sort = DataTypesSortExp.Value.ToString()

                ' Bind the GridView control.
                gvDataTypes.DataSource = dv
                gvDataTypes.DataBind()
            Else
                gvDataTypes.DataSource = Nothing
                gvDataTypes.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Collect condition string to fill grid
    ''' </summary>
    Public Function CollectConditions() As String
        Dim result As String = "1=1"
        Try
            Dim Search As String = IIf(txtSearchAll.Text = "", "1=1", " (Type Like '%" + txtSearchAll.Text + "%')")
            Return Search
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return result
        End Try
    End Function

    ''' <summary>
    ''' Sorting gvDataTypes
    ''' </summary>
    Protected Sub gvDataTypes_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim strSortExpression As String() = DataTypesSortExp.Value.ToString().Split(" "c)

            ' If the sorting column is the same as the previous one, then change the sort order.
            If strSortExpression(0) = e.SortExpression Then
                If strSortExpression(1) = "ASC" Then
                    DataTypesSortExp.Value = Convert.ToString(e.SortExpression) & " " & "DESC"
                Else
                    DataTypesSortExp.Value = Convert.ToString(e.SortExpression) & " " & "ASC"
                End If
            Else
                ' If sorting column is another column, then specify the sort order to "Ascending".
                DataTypesSortExp.Value = Convert.ToString(e.SortExpression) & " " & "DESC"
            End If

            ' Rebind the GridView control to show sorted data.
            FillDataTypes()

            ' add sorting Arrows.
            If strSortExpression(1) = "ASC" Then
                gvDataTypes.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvDataTypes, e.SortExpression)).CssClass = "faDown"
            Else
                gvDataTypes.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvDataTypes, e.SortExpression)).CssClass = "faUp"
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Set Number of rows at every page
    ''' </summary>
    Protected Sub PageSize_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            FillDataTypes()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Paging gvDataTypes
    ''' </summary>
    Protected Sub gvDataTypes_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            gvDataTypes.PageIndex = e.NewPageIndex
            FillDataTypes()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "FillGrid Values"

    ''' <summary>
    ''' Fill gridview with data from tblLookupValue of selected Type.
    ''' </summary>
    Private Sub FillValues()
        Try
            Dim dtValues As DataTable = DBManager.Getdatatable("Select * ,dbo.GetEnValue(RelatedValueId)  as ParentType from tblLookupValue where isnull(IsDeleted,0)=0  and LookupId='" + lblLookupId.Text + "'")

            If dtValues.Rows.Count > 0 Then
                ' Initialize the sorting expression.
                If ValuesSortExp.Value = String.Empty Then
                    ValuesSortExp.Value = "Value ASC"
                End If

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtValues)

                ' Set the sort column and sort order.
                dv.Sort = ValuesSortExp.Value.ToString()

                ' Bind the GridView control.
                gvValues.DataSource = dv
                gvValues.DataBind()

                If dtValues.Rows(0)("RelatedValueId").ToString() = "" Then
                    gvValues.Columns(3).Visible = False
                Else
                    gvValues.Columns(3).Visible = True
                End If
                gvValues.Visible = True

            Else
                gvValues.DataSource = Nothing
                gvValues.DataBind()

            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    ''' <summary>
    ''' handle rowdataBound event of gvValues
    ''' </summary>
    Protected Sub gv_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvValues.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim Color As String = CType(e.Row.FindControl("lblColor"), Label).Text
                If Color <> vbNullString Then
                    CType(e.Row.FindControl("pnlColor"), Panel).BackColor = System.Drawing.ColorTranslator.FromHtml("#" + Color)
                End If
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    ''' <summary>
    ''' Sorting gvValues
    ''' </summary>
    Protected Sub gvValues_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim strSortExpression As String() = ValuesSortExp.Value.ToString().Split(" "c)

            ' If the sorting column is the same as the previous one, 
            ' then change the sort order.
            If strSortExpression(0) = e.SortExpression Then
                If strSortExpression(1) = "ASC" Then
                    ValuesSortExp.Value = Convert.ToString(e.SortExpression) & " " & "DESC"
                Else
                    ValuesSortExp.Value = Convert.ToString(e.SortExpression) & " " & "ASC"
                End If
            Else
                ' If sorting column is another column, 
                ' then specify the sort order to "Ascending".
                ValuesSortExp.Value = Convert.ToString(e.SortExpression) & " " & "DESC"
            End If

            ' Rebind the GridView control to show sorted data.
            FillValues()

            ' add sorting Arrows.
            If strSortExpression(1) = "ASC" Then
                gvValues.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvValues, e.SortExpression)).CssClass = "faDown"
            Else
                gvValues.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvValues, e.SortExpression)).CssClass = "faUp"
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Paging gvValues
    ''' </summary>
    Protected Sub GvValues_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            gvValues.PageIndex = e.NewPageIndex
            FillValues()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Add"
    ''' <summary>
    ''' Show Data type values
    ''' </summary>
    Protected Sub ShowValues(ByVal Sender As Object, ByVal e As System.EventArgs)
        lblLookupId.Text = CType(Sender, LinkButton).CommandArgument.ToString()
        EnablerType(True)
        EnablerValue(False)

        Try
            BindRelatedValues("Id='" + lblLookupId.Text + "'")
            FillValues()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Add new data type value
    ''' </summary>
    Protected Sub NewValue(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            btnSave.CommandArgument = "add"
            EnablerValue(True)
            pf.ClearAll(pnlNewValue)
            lblLookupValueId.Text = String.Empty
            HiddenIcon.Text = String.Empty
            txtCode.Enabled = True
            txtDescription.TextValue = String.Empty
            BindRelatedValues("Id='" + lblLookupId.Text + "'")
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Check if there are related value for this type and bind its ddl
    ''' </summary>
    Sub BindRelatedValues(ByVal Filter As String)
        Try
            Dim dtRType = DBManager.Getdatatable("Select RelatedTypeId,Type,Description from TblLookup where " + Filter + "  and Isnull(IsDeleted,0)=0 ")
            If dtRType.Rows.Count > 0 Then
                lblTypesName.Text = dtRType.Rows(0)("Description").ToString()
                Dim RType As String = dtRType.Rows(0)("RelatedTypeId").ToString()
                If RType <> String.Empty Then
                    pnlRType.Visible = True
                    lblType.Text = dtRType.Rows(0)("Type").ToString()

                    clsBindDDL.BindCustomDDLs("Select Id,Value from TblLookupValue where LookupId='" + RType + "' and Isnull(IsDeleted,0)=0 ", "Value", "Id", ddlRType, False)
                Else
                    pnlRType.Visible = False
                End If


            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


#End Region

#Region "Edit"
    ''' <summary>
    ''' Handle Update Event
    ''' Get Object details and Populate it in pnl forms
    ''' </summary>
    Protected Sub Edit(ByVal Sender As Object, ByVal e As System.EventArgs)
        btnSave.CommandArgument = "Edit"
        lblLookupValueId.Text = Sender.commandargument
        Try
            Dim dt As DataTable = DBManager.Getdatatable("SELECT * From tblLookupValue where  Id ='" + lblLookupValueId.Text + "' ")
            If dt.Rows.Count <> 0 Then

                txtDescription.TextValue = dt.Rows(0).Item("Description").ToString.Trim
                txtValue.Text = dt.Rows(0).Item("Value").ToString.Trim
                txtCode.Text = dt.Rows(0).Item("Code").ToString.Trim
                txtColor.Text = dt.Rows(0).Item("Color").ToString
                If txtColor.Text <> String.Empty Then
                    txtColorSample.BackColor = System.Drawing.ColorTranslator.FromHtml("#" + txtColor.Text)
                Else
                    txtColorSample.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff")
                End If
                HiddenIcon.Text = dt.Rows(0).Item("Icon").ToString
                FillIconPhoto()

                EnablerValue(True)
                If pnlRType.Visible = True Then
                    Dim RType As String = dt.Rows(0)("RelatedValueId").ToString()
                    If RType <> String.Empty Then
                        ddlRType.SelectedValue = RType
                    End If
                End If
                If Not CheckLookupItem(lblLookupValueId.Text) Then
                    txtCode.Enabled = False
                End If

            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Delete"
    ''' <summary>
    ''' Handle Delete Event
    ''' </summary>
    Protected Sub Delete(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim LookupValueId As String = Sender.commandargument.ToString
        Try
            If Not CheckLookupItem(LookupValueId) Then
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.CUSTOMInfo, Page, Nothing, "Can't delete it, already used for items")
                Return
            End If
            If DBManager.ExcuteQuery("Update tblLookupValue set IsDeleted=1,DeletedDate=getdate(),DeletedBy='" + UserId + "' where id= '" + LookupValueId + "'") = 1 Then
                clsLogs.AddSystemLogs("Delete", "TblLookupValue", LookupValueId)
                FillValues()
                clsMessages.ShowMessage(lblRes, MessageTypesEnum.Delete, Me)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Save"
    ''' <summary>
    ''' Check lookup value not exist
    ''' </summary>
    Function CheckValueNotExist(ByVal Operation As String) As Boolean
        Dim dtType As DataTable = New DataTable
        Dim RTypeFilter As String = "1=1"
        If ddlRType.SelectedValue <> String.Empty Then
            RTypeFilter = "RelatedValueId='" + ddlRType.SelectedValue + "'"
        End If
        Try
            Select Case Operation
                Case "Insert"
                    dtType = DBManager.Getdatatable("select Id from tblLookupValue where Value ='" + txtValue.Text + "' and LookupId ='" + lblLookupId.Text + "' and " + RTypeFilter + " and isnull(isDeleted,0)=0  ")
                Case "Update"
                    dtType = DBManager.Getdatatable("select Id from tblLookupValue where Value ='" + txtValue.Text + "' and LookupId ='" + lblLookupId.Text + "' and " + RTypeFilter + " and ID <>'" + lblLookupValueId.Text + "' and isnull(isDeleted,0)=0   ")
            End Select

            If dtType.Rows.Count > 0 Then
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.INFO, Me.Page)
                Return False
            End If
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Check lookup Code not exist
    ''' </summary>
    Function CheckCodeNotExist(ByVal Operation As String) As Boolean
        Dim dtType As DataTable = New DataTable
        Try
            If txtCode.Text <> String.Empty Then
                Select Case Operation
                    Case "Insert"
                        dtType = DBManager.Getdatatable("select Id from tblLookupValue where Code ='" + txtCode.Text + "' and LookupId ='" + lblLookupId.Text + "' and isnull(isDeleted,0)=0  ")
                    Case "Update"
                        dtType = DBManager.Getdatatable("select Id from tblLookupValue where Code ='" + txtCode.Text + "' and LookupId ='" + lblLookupId.Text + "' and ID <>'" + lblLookupValueId.Text + "' and isnull(isDeleted,0)=0 ")
                End Select

                If dtType.Rows.Count > 0 Then
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.INFO, Me.Page)
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Save lookup values 
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim daLookup As New TblLookupValueFactory
        Dim dtLookup As New TblLookupValue
        Try

            If btnSave.CommandArgument.ToLower() = "add" Then
                'Insert Case
                'Check value not exit
                If CheckValueNotExist("Insert") = False Then
                    Exit Sub
                End If
                'Check Code not exit
                'If CheckCodeNotExist("Insert") = False Then
                '    Exit Sub
                'End If
                'Fill object of lookup values
                If FillDT(dtLookup) = False Then
                    Exit Sub
                End If
                'insert at tbllookupvalue
                If daLookup.Insert(dtLookup) Then
                    Dim RefId As String = PublicFunctions.GetIdentity("TblLookupValue")
                    clsLogs.AddSystemLogs("Insert", "TblLookupValue", RefId)
                    ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                End If
            Else
                'Update Case
                'Check value not exit
                If CheckValueNotExist("Update") = False Then
                    Exit Sub
                End If
                'Check Code not exit
                'If CheckCodeNotExist("Update") = False Then
                '    Exit Sub
                'End If
                'get lookup value details of update row
                dtLookup = daLookup.GetAllBy(TblLookupValue.TblLookupValueFields.Id, lblLookupValueId.Text)(0)
                'Fill object of lookup with new values
                If FillDT(dtLookup) = False Then
                    Exit Sub
                End If
                dtLookup.Id = lblLookupValueId.Text
                'update tbllookupvalue
                If daLookup.Update(dtLookup) Then
                    clsLogs.AddSystemLogs("Update", "TblLookupValue", lblLookupValueId.Text)
                    ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                End If

            End If
            lblLookupValueId.Text = String.Empty
            HiddenIcon.Text = String.Empty
            EnablerValue(False)
            FillValues()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Fill lookup value Object
    ''' </summary>
    Function FillDT(ByVal dtLookup As TblLookupValue) As Boolean
        Try
            dtLookup.LookupId = lblLookupId.Text
            dtLookup.Value = DataTypeValue
            dtLookup.Code = DataTypeCode
            If pnlRType.Visible Then
                dtLookup.RelatedValueId = DataTypeValueRT
            End If
            dtLookup.Description = DataTypeValueDesc
            dtLookup.Color = DataTypeValueColor
            dtLookup.Icon = HiddenIcon.Text

            If btnSave.CommandArgument.ToLower() = "add" Then
                dtLookup.CreatedBy = UserId
                dtLookup.CreatedDate = DateTime.Now
            End If
            dtLookup.ModifiedBy = UserId
            dtLookup.ModifiedDate = DateTime.Now

            dtLookup.IsDeleted = False

            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

#End Region

#Region "Cancel"
    ''' <summary>
    ''' hide and show panels
    ''' </summary>
    Protected Sub Cancel(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            lblLookupValueId.Text = String.Empty
            HiddenIcon.Text = String.Empty
            EnablerValue(False)

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Uploader"
    ''' <summary>
    ''' Handle uploader event.
    ''' </summary>
    Protected Sub PhotoUploaded(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Check that there is a file
            If fuPhoto.PostedFile IsNot Nothing Then

                Dim filePath As String = "~/Settings_Icons/" & fuPhoto.FileName

                ' Check file size (mustn’t be 0)
                Dim myFile As HttpPostedFile = fuPhoto.PostedFile
                Dim nFileLen As Integer = myFile.ContentLength
                If (nFileLen > 0) Then
                    ' Read file into a data stream
                    Dim myData As Byte() = New [Byte](nFileLen - 1) {}
                    myFile.InputStream.Read(myData, 0, nFileLen)
                    myFile.InputStream.Dispose()

                    ' Save the stream to disk as temporary file. make sure the path is unique!
                    Dim newFile As New System.IO.FileStream(Server.MapPath(filePath & "_temp" + System.IO.Path.GetExtension(myFile.FileName).ToLower() + ""), System.IO.FileMode.Create)
                    newFile.Write(myData, 0, myData.Length)


                    ' run ALL the image optimisations you want here..... make sure your paths are unique
                    ' you can use these booleans later if you need the results for your own labels or so.
                    ' dont call the function after the file has been closed.
                    ''''''''''''''''''''''''''''''''' Main Photo Size'''''''''''''''''''''''''''''''

                    Dim MainWidth As String = "275"
                    Dim MainHeight As String = "417"

                    ImgResize.ResizeImageAndUpload(newFile, filePath, MainHeight, MainWidth)


                    ' tidy up and delete the temp file.
                    newFile.Close()
                    System.IO.File.Delete(Server.MapPath(filePath & "_temp" + System.IO.Path.GetExtension(myFile.FileName).ToLower() + ""))
                End If
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' set User image depend on uploded one, if not exist set the default one
    ''' </summary>
    Sub FillIconPhoto()
        Try
            'Check User photo path has value or not to set the default value
            If HiddenIcon.Text IsNot Nothing And HiddenIcon.Text <> "" Then
                imgIcon.ImageUrl = HiddenIcon.Text
            Else
                HiddenIcon.Text = "~/images/noimage.jpg"
                imgIcon.ImageUrl = "~/images/noimage.jpg"
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

End Class