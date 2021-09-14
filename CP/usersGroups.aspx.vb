
#Region "Imports"
Imports System.Data.SqlClient
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region

Partial Class UsersGroups
    Inherits System.Web.UI.Page
#Region "Global Variable"
    Dim pf As New PublicFunctions
    Dim UserID As String = "0"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim Name As String = String.Empty
    Dim Remarks As String = String.Empty
    Dim Active As Boolean = False

#End Region

#Region "Public Functions"

    ''' <summary>
    ''' hide and show panels
    ''' </summary>
    Protected Sub Enabler(ByVal b As Boolean)
        Try
            pnlConfirmTop.Visible = b
            pnlOps.Visible = Not b
            pnlForm.Visible = b
            pnlGV.Visible = Not b
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub

    ''' <summary>
    ''' Set Default controls Values 
    ''' </summary>
    Sub SetControlFields()
        Try
            Name = txtGroupName.Text
            Remarks = txtRemarks.Text
            Active = PublicFunctions.BoolFormat(chkActive.Checked)

            If txtSearch.Visible = False Then
                cmdClear.Visible = False
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Page Load"
    ''' <summary>
    ''' Handle Page Load Events
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblRes.Visible = False
        UserID = PublicFunctions.GetUserId(Page)
        Try
            If Page.IsPostBack = False Then
                Permissions.CheckPermisions(gvUsersGroups, lbAdd, txtSearch, lbSearchIcon, Me.Page, UserID)
                clsLogs.AddSystemLogs("Access")
                FillGrid()
            End If
            'set default values of controls
            SetControlFields()

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Fill Grid"

    ''' <summary>
    ''' Fill gridview with data from TblCPUsersGroups.
    ''' </summary>
    Sub FillGrid()
        Try
            Dim dtGroups As DataTable = DBManager.Getdatatable("select * from TblCPUsersGroups where isnull(isdeleted,0)=0  and SystemGroup <> '1'  and " + CollectConditions() + "")
            If dtGroups.Rows.Count > 0 Then
                pgPanel.Visible = True

                ' Initialize the sorting expression.
                If SortExpression.Value = String.Empty Then
                    SortExpression.Value = "Id ASC"
                End If

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtGroups)

                ' Set the sort column and sort order.
                dv.Sort = SortExpression.Value

                ' Bind the GridView control.
                gvUsersGroups.DataSource = dv
                gvUsersGroups.DataBind()
            Else
                pgPanel.Visible = False
                gvUsersGroups.DataSource = Nothing
                gvUsersGroups.DataBind()
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
            Dim Search As String = IIf(txtSearch.Text = "", "1=1", " (Name Like '%" + txtSearch.Text + "%')")
            Return Search
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return result
        End Try
    End Function

    Protected Sub Gv_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            Dim strSortExpression As String() = SortExpression.Value.ToString().Split(" "c)

            ' If the sorting column is the same as the previous one, 
            ' then change the sort order.
            If strSortExpression(0) = e.SortExpression Then
                If strSortExpression(1) = "ASC" Then
                    SortExpression.Value = Convert.ToString(e.SortExpression) & " " & "DESC"
                Else
                    SortExpression.Value = Convert.ToString(e.SortExpression) & " " & "ASC"
                End If
            Else
                ' If sorting column is another column, 
                ' then specify the sort order to "Ascending".
                SortExpression.Value = Convert.ToString(e.SortExpression) & " " & "DESC"
            End If

            ' Rebind the GridView control to show sorted data.
            FillGrid()

            ' add sorting Arrows.
            If strSortExpression(1) = "ASC" Then
                gvUsersGroups.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvUsersGroups, e.SortExpression)).CssClass = "faDown"
            Else
                gvUsersGroups.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvUsersGroups, e.SortExpression)).CssClass = "faUp"
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub

    Protected Sub gvUser_PageIndexChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            gvUsersGroups.PageIndex = e.NewPageIndex
            FillGrid()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub

    ''' <summary>
    ''' Set Number of rows at every page
    ''' </summary>
    Protected Sub PageSize_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            FillGrid()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub


#End Region

#Region "Add"

    ''' <summary>
    ''' hide Add Event, Clear control and show Form Panel
    ''' </summary>
    Protected Sub Add(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            btnSave.CommandArgument = "add"
            pf.ClearAll(pnlForm)
            chkActive.Checked = True
            lblID.Text = String.Empty
            Enabler(True)
            FillForms()
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
    Protected Sub Edit(sender As Object, e As EventArgs)
        Try

            pf.ClearAll(pnlForm)
            Dim UsernameId As String = CType(sender, LinkButton).CommandArgument
            btnSave.CommandArgument = "Edit"
            FillForm(UsernameId)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub

    ''' <summary>
    ''' Fill Form with Object datas
    ''' </summary>
    Protected Sub FillForm(Id As String)
        Try
            Dim dt As DataTable = DBManager.Getdatatable("Select * from TblCPUsersGroups  where ISNULL(isDeleted,0)=0 and Id='" + Id + "'")
            Dim dr As DataRow = dt.Rows(0)

            txtGroupName.Text = dr("Name").ToString()
            If dr("Active").ToString() <> "" Then
                chkActive.Checked = dr("Active")
            End If
            txtRemarks.Text = dr("Remarks").ToString()

            lblID.Text = Id

            pnlGroupDT.Enabled = True
            Enabler(True)
            FillForms()

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub


#End Region

#Region "Delete"
    Protected Sub Delete(sender As Object, e As EventArgs)
        Try

            Dim GroupId As String = sender.CommandArgument

            Dim str As String = "Update TblCPUsersGroups set isDeleted=1,DeletedBy='" + UserID + "',DeletedDate=GetDate() where Id=" + GroupId + ";"
            str += " Update TblCPGroupPermissions Set IsDeleted=1,DeletedBy=" + UserID + ",DeletedDate=GetDate() where GroupId=" + GroupId
            If DBManager.ExcuteQuery(str) > 0 Then
                clsLogs.AddSystemLogs("Delete", "TblCPUsersGroups", GroupId)
                ShowMessage(lblRes, MessageTypesEnum.Delete, Me)
                FillGrid()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub
#End Region

#Region "Save"
    ''' <summary>
    ''' Save User Data
    ''' </summary>
    Protected Sub Save(Sender As Object, e As EventArgs)

        Dim GroupObj As New TblCPUsersGroups
        Dim Da As New TblCPUsersGroupsFactory
        Try

            If btnSave.CommandArgument.ToLower() = "add" Then
                If Not FillDt(GroupObj) Then
                    Exit Sub
                End If
                'Insert Case
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction()

                If Not Da.InsertTrans(GroupObj, _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Exit Sub
                End If
                Dim newGroupId As String = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                'Save permissions
                If Not SaveGroupPermission(newGroupId) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Exit Sub
                End If
                clsLogs.AddSystemLogs("Insert", _sqlconn, _sqltrans, "TblCPUsersGroups", newGroupId)
                _sqltrans.Commit()
                _sqlconn.Close()
                ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
            Else
                'Update Case
                'get dtTable for Updated GroupId
                GroupObj = Da.GetAllBy(TblCPUsersGroups.TblCPUsersGroupsFields.ID, lblID.Text)(0)

                If Not FillDt(GroupObj) Then
                    Exit Sub
                End If
                GroupObj.ID = lblID.Text

                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction()

                If Not Da.UpdateTrans(GroupObj, _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Exit Sub
                End If
                'Save group permissions
                If Not SaveGroupPermission(lblID.Text) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Exit Sub
                End If
                clsLogs.AddSystemLogs("Update", _sqlconn, _sqltrans, "TblCPUsersGroups", lblID.Text)
                _sqltrans.Commit()
                _sqlconn.Close()
                ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Me.Page)
            End If

            lblID.Text = String.Empty
            FillGrid()
            Enabler(False)

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub
    ''' <summary>
    ''' Fill User Object
    ''' </summary>
    Protected Function FillDt(ByRef GroupObj As TblCPUsersGroups) As Boolean
        Try
            GroupObj.Name = Name
            GroupObj.Remarks = Remarks
            GroupObj.ACTIVE = Active
            If btnSave.CommandArgument.ToLower() = "add" Then
                GroupObj.CreatedBy = UserID
                GroupObj.CreatedDate = DateTime.Now
            End If
            GroupObj.ModifiedBy = UserID
            GroupObj.ModifiedDate = DateTime.Now
            GroupObj.IsDeleted = False
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
    Protected Sub Cancel(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            lblID.Text = String.Empty
            Enabler(False)

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub

#End Region

#Region "Permission"
    ''' <summary>
    ''' Show Permission of system forms
    ''' </summary>
    Protected Sub ShowPermission(sender As Object, e As EventArgs)
        Try
            Dim ObjId As String = CType(sender, LinkButton).CommandArgument
            btnSavePermissionPopup.CommandArgument = ObjId
            Dim dt As DataTable
            dt = DBManager.Getdatatable("select F.FormTitle as FormTitle, UP.GroupId,UP.FormID,UP.PAccess,UP.PAdd,UP.PUpdate,UP.PDelete,UP.PSearch,UP.PActive from dbo.TblCPGroupPermissions UP left outer Join tblCPForms F on UP.FormID=F.ID  where GroupId='" + ObjId + "' and isnull(UP.isDeleted,0)=0 ")
            btnSavePermissionPopup.Visible = True
            gvGroupPermissionPopup.DataSource = dt
            gvGroupPermissionPopup.DataBind()
            mpePermissionPopup.Show()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "loadScroll", "topFunction();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Save User Permissions
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SaveGroupPermission(ObjId As String, Optional IsPopup As Boolean = False) As Boolean
        Try
            Dim UPObj As New TblCPGroupPermissions
            Dim UPDa As New TblCPGroupPermissionsFactory()
            'Delete old permissions
            If Not UPDa.DeleteTrans(TblCPGroupPermissions.TblCPGroupPermissionsFields.GroupId, ObjId, _sqlconn, _sqltrans) Then
                Return False
            End If

            Dim Gv As GridView
            If IsPopup Then
                Gv = gvGroupPermissionPopup
            Else
                Gv = gvGroupPermissions
            End If
            For Each gvrow As GridViewRow In Gv.Rows
                UPObj = New TblCPGroupPermissions
                UPObj.CreatedBy = UserID
                UPObj.CreatedDate = DateTime.Now
                UPObj.IsDeleted = False
                UPObj.GroupId = ObjId
                UPObj.FormID = CType(gvrow.FindControl("lblFormId"), Label).Text
                UPObj.PAccess = CType(gvrow.FindControl("chkAccess"), CheckBox).Checked
                UPObj.PAdd = CType(gvrow.FindControl("chkAdd"), CheckBox).Checked
                UPObj.PUpdate = CType(gvrow.FindControl("chkUpdate"), CheckBox).Checked
                UPObj.PDelete = CType(gvrow.FindControl("chkDelete"), CheckBox).Checked
                UPObj.PSearch = CType(gvrow.FindControl("chkSearch"), CheckBox).Checked
                UPObj.PActive = CType(gvrow.FindControl("chkActive"), CheckBox).Checked
                If Not UPDa.InsertTrans(UPObj, _sqlconn, _sqltrans) Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

            Return False
        End Try
    End Function

    Protected Sub FillForms()
        Try
            Dim dt As DataTable

            If btnSave.CommandArgument.ToLower = "add" Then
                dt = DBManager.Getdatatable("select ID as FormID ,FormTitle as FormTitle,0 as PAccess,0 as PAdd,0 as PUpdate,0 as PDelete,0 as PSearch,0 as PActive from tblCPForms where ISNULL(isDeleted,0)=0 ")
            Else
                dt = DBManager.Getdatatable("select F.FormTitle as FormTitle, UP.GroupId,UP.FormID,UP.PAccess,UP.PAdd,UP.PUpdate,UP.PDelete,UP.PSearch,UP.PActive from dbo.TblCPGroupPermissions UP left outer Join tblCPForms F on UP.FormID=F.ID where GroupId=" + lblID.Text + " and isnull(UP.isDeleted,0)=0 ")
                If dt.Rows.Count = 0 Then
                    dt = DBManager.Getdatatable("select ID as FormID ,FormTitle as FormTitle,0 as PAccess,0 as PAdd,0 as PUpdate,0 as PDelete,0 as PSearch,0 as PActive from tblCPForms where ISNULL(isDeleted,0)=0 ")
                End If
            End If

            gvGroupPermissions.DataSource = dt
            gvGroupPermissions.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    'Save permissions from popup
    Protected Sub SavePermission(sender As Object, e As EventArgs)
        Try
            Dim ObjId As String = CType(sender, Button).CommandArgument
            If SaveGroupPermission(ObjId, True) Then
                clsMessages.ShowMessage(lblRes, MessageTypesEnum.CUSTOMSuccess, Me, Nothing, "Permissions Updated Successfully")
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub
#End Region


End Class
