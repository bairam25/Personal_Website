
#Region "Imports"
Imports System.Data.SqlClient
Imports System.Data
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region

Partial Class ControlPanel_users
    Inherits System.Web.UI.Page
#Region "Global Variable"
    Dim pf As New PublicFunctions
    Dim UserID As String = "0"
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim FullName As String = String.Empty
    Dim Email As String = String.Empty
    Dim Username As String = String.Empty
    Dim Password As String = String.Empty
    Dim UserGroup As String = String.Empty
    Dim UserType As String = String.Empty
    Dim Mobile As String = String.Empty
    Dim Active As Boolean = False
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
                Permissions.CheckPermisions(gvUsers, lbAdd, txtSearch, lbSearchIcon, Me.Page, UserID)
                clsLogs.AddSystemLogs("Access")
                FillGrid()
                clsBindDDL.BindCustomDDLs("select Id,Name from TblCPUsersGroups where ISNULL(isDeleted,0)=0 and SystemGroup <> '1' ", "Name", "Id", ddlUserGroup, True)
            End If
            'Set Default values of controls
            SetControlFields()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub



#End Region

#Region "Public Functions"

    ''' <summary>
    ''' hide and show panels
    ''' </summary>
    Protected Sub Enabler(ByVal b As Boolean)
        Try
            pnlConfirm.Visible = b
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
            FullName = txtFullName.Text
            Email = txtEmail.Text
            Username = txtUsername.Text

            Password = txtPassword.Text
            txtPassword.Attributes.Add("value", txtPassword.Text)
            txtPasswordConfirm.Attributes.Add("value", txtPasswordConfirm.Text)

            Mobile = txtMobile.Text
            UserGroup = ddlUserGroup.SelectedValue
            UserType = ddlUserType.SelectedValue
            Active = PublicFunctions.BoolFormat(chkUserActivation.Checked)

            FillUserPhoto()

            If txtSearch.Visible = False Then
                cmdClear.Visible = False
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Fill Grid"

    ''' <summary>
    ''' Fill gridview with data from TblCPUsers.
    ''' </summary>
    Sub FillGrid()
        Try
            Dim dtUsers As DataTable = DBManager.Getdatatable("select * from TblCPUsers where isnull(isdeleted,0)=0  and SystemUser <> '1'  and " + CollectConditions() + "")
            If dtUsers.Rows.Count > 0 Then
                pgPanel.Visible = True

                ' Initialize the sorting expression.
                If SortExpression.Value = String.Empty Then
                    SortExpression.Value = "UserID ASC"
                End If

                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtUsers)

                ' Set the sort column and sort order.
                dv.Sort = SortExpression.Value

                ' Bind the GridView control.
                gvUsers.DataSource = dv
                gvUsers.DataBind()
            Else
                pgPanel.Visible = False
                gvUsers.DataSource = Nothing
                gvUsers.DataBind()
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
            Dim Search As String = IIf(txtSearch.Text = "", "1=1", " (Username Like '%" + txtSearch.Text + "%')")
            Return Search
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return result
        End Try
    End Function
    ''' <summary>
    ''' Handle Grid view Sorting
    ''' </summary>
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
                gvUsers.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvUsers, e.SortExpression)).CssClass = "faDown"
            Else
                gvUsers.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvUsers, e.SortExpression)).CssClass = "faUp"
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Handle gridview paging
    ''' </summary>
    Protected Sub gvUser_PageIndexChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            gvUsers.PageIndex = e.NewPageIndex
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
            chkUserActivation.Checked = True
            ddlUserType.SelectedIndex = 0
            ddlUserGroup.SelectedIndex = 0
            lblUserId.Text = String.Empty
            Enabler(True)
            FillPermissions()
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
            lblUserId.Text = CType(sender, LinkButton).CommandArgument
            btnSave.CommandArgument = "Edit"
            If FillForm() Then
                Enabler(True)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub

    ''' <summary>
    ''' Fill Form with Object datas
    ''' </summary>
    Function FillForm() As Boolean
        Try
            Dim dt As DataTable = DBManager.Getdatatable("Select * from TblCPUsers where ISNULL(isDeleted,0)=0 and UserID='" + lblUserId.Text + "'")
            Dim dr As DataRow = dt.Rows(0)

            txtFullName.Text = dr("FullName").ToString()

            Dim UserActive As String = dr("Active").ToString()
            If UserActive <> String.Empty Then
                chkUserActivation.Checked = UserActive
            End If

            Dim UserType As String = dr("UserType").ToString()
            If dr("UserType").ToString() <> String.Empty Then
                ddlUserType.SelectedValue = UserType
            End If

            ddlUserGroup.SelectedValue = dr("GroupId").ToString()
            txtUsername.Text = dr("UserName").ToString()
            txtEmail.Text = dr("Email").ToString()
            txtMobile.Text = dr("MobileNo").ToString()
            Dim Password As String = PublicFunctions.Decrypt(dr("Password").ToString())
            txtPassword.Text = Password
            txtPasswordConfirm.Text = Password
            txtPassword.Attributes.Add("value", txtPassword.Text)
            txtPasswordConfirm.Attributes.Add("value", txtPasswordConfirm.Text)

            imgIcon.ImageUrl = dr("Photo").ToString()
            HiddenIcon.Text = dr("Photo").ToString()
            FillUserPhoto()

            pnlUserDt.Enabled = True

            FillPermissions()

            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

#End Region

#Region "Delete"
    ''' <summary>
    ''' Handle Delete Event
    ''' </summary>
    Protected Sub Delete(sender As Object, e As EventArgs)
        Try
            Dim CPUserId As String = sender.CommandArgument
            Dim username As String = CType(sender.parent.FindControl("lblUserName"), Label).Text
            If username = "system" Then
                Exit Sub
            End If
            Dim str As String = "Update TblCPUsers set isDeleted=1,DeletedBy='" + UserID + "',DeletedDate=GetDate() where UserID=" + CPUserId + ";"
            str += " Update TblCPUserPermissions Set IsDeleted=1,DeletedBy=" + UserID + ",DeletedDate=GetDate() where UserId=" + CPUserId
            If DBManager.ExcuteQuery(str) > 0 Then
                clsLogs.AddSystemLogs("Delete", "TblCPUsers", CPUserId)
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
        Dim dtUsers As New TblCPUsers
        Dim daUsers As New TblCPUsersFactory
        Try
            If PublicFunctions.IsGroupValid("vUsers", Page) = False Then
                Exit Sub
            End If
            If btnSave.CommandArgument.ToLower() = "add" Then
                'Insert Case
                'fill new details which entered
                If Not FillDt(dtUsers) Then
                    Exit Sub
                End If

                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction()

                If Not daUsers.InsertTrans(dtUsers, _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Exit Sub
                End If
                'Save User Permissions
                Dim newUserId As String = PublicFunctions.GetIdentity(_sqlconn, _sqltrans)
                If Not SaveUserPermission(newUserId) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Exit Sub
                End If
                clsLogs.AddSystemLogs("Insert", _sqlconn, _sqltrans, "TblCPUsers", newUserId)

                _sqltrans.Commit()
                _sqlconn.Close()
                ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
            Else
                'Get Updated user details
                dtUsers = daUsers.GetAllBy(TblCPUsers.TblCPUsersFields.UserID, lblUserId.Text)(0)
                dtUsers.UserID = lblUserId.Text
                'Fill new details which updated
                If Not FillDt(dtUsers) Then
                    Exit Sub
                End If
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction()

                If Not daUsers.UpdateTrans(dtUsers, _sqlconn, _sqltrans) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Exit Sub
                End If
                'Save Permissions
                If Not SaveUserPermission(lblUserId.Text) Then
                    _sqltrans.Rollback()
                    _sqlconn.Close()
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, "Error.")
                    Exit Sub
                End If
                clsLogs.AddSystemLogs("Update", _sqlconn, _sqltrans, "TblCPUsers", lblUserId.Text)

                _sqltrans.Commit()
                _sqlconn.Close()
                ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Me.Page)
            End If

            lblUserId.Text = String.Empty
            HiddenIcon.Text = String.Empty
            FillGrid()
            Enabler(False)

        Catch ex As Exception
            _sqltrans.Rollback()
            _sqlconn.Close()
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


    ''' <summary>
    ''' Fill User Object
    ''' </summary>
    Protected Function FillDt(ByRef dtUsers As TblCPUsers) As Boolean
        Try
            dtUsers.GroupId = UserGroup

            dtUsers.FullName = FullName
            dtUsers.UserName = Username
            dtUsers.Email = Email
            dtUsers.Active = Active
            dtUsers.UserType = UserType
            dtUsers.Password = PublicFunctions.Encrypt(Password)

            dtUsers.MobileNo = Mobile


            dtUsers.Photo = HiddenIcon.Text


            If btnSave.CommandArgument.ToLower() = "add" Then
                dtUsers.CreatedBy = UserID
                dtUsers.CreatedDate = DateTime.Now
            End If
            dtUsers.ModifiedBy = UserID
            dtUsers.ModifiedDate = DateTime.Now

            dtUsers.IsDeleted = False

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

            lblUserId.Text = String.Empty
            HiddenIcon.Text = String.Empty
            Enabler(False)

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)

        End Try
    End Sub

#End Region

#Region "Permission"

    Protected Sub ShowPermission(sender As Object, e As EventArgs)
        Try

            Dim GroupId As String = "0"
            Dim CPUserId As String = CType(sender, LinkButton).CommandArgument
            btnSavePermissionPopup.CommandArgument = CPUserId
            Dim dt As DataTable
            Dim dtUsers As DataTable = DBManager.Getdatatable("select * from TblCPUsers where isnull(isdeleted,0)=0  and UserID='" + CPUserId + "' ")
            If dtUsers.Rows.Count > 0 Then
                GroupId = dtUsers.Rows(0).Item("GroupId").ToString()
            End If
            If GroupId = "0" Then
                gvUserPermissionPopup.Enabled = True
                dt = DBManager.Getdatatable("select F.FormTitle as FormTitle , UP.UserId,UP.FormID,UP.PAccess,UP.PAdd,UP.PUpdate,UP.PDelete,UP.PSearch,UP.PActive from dbo.TblCPUserPermissions UP left outer Join tblCPForms F on UP.FormID=F.ID where UserId=" + CPUserId + " and isnull(UP.isDeleted,0)=0")
                pnlPopupSave.Visible = True
            Else
                dt = DBManager.Getdatatable("select F.FormTitle as FormTitle, GP.GroupId,GP.FormID,GP.PAccess,GP.PAdd,GP.PUpdate,GP.PDelete,GP.PSearch,GP.PActive from dbo.TblCPGroupPermissions GP left outer Join tblCPForms F on GP.FormID=F.ID where GroupId=" + GroupId + " and isnull(GP.isDeleted,0)=0")
                gvUserPermissionPopup.Enabled = False
                pnlPopupSave.Visible = False
            End If
            gvUserPermissionPopup.DataSource = dt
            gvUserPermissionPopup.DataBind()
            mpePermissionPopup.Show()
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType, "loadScroll", "topFunction();", True)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Fill Permissions depend on group selected or not
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub FillPermissions()
        Try
            Dim dt As DataTable
            If ddlUserGroup.Text = "0" Then
                gvUserPermissions.Enabled = True
                If btnSave.CommandArgument.ToLower = "add" Then
                    dt = DBManager.Getdatatable("select ID as FormID ,FormTitle as FormTitle,0 as PAccess,0 as PAdd,0 as PUpdate,0 as PDelete,0 as PSearch,0 as PActive from tblCPForms where ISNULL(isDeleted,0)=0 ")
                Else
                    dt = DBManager.Getdatatable("select F.FormTitle as FormTitle, UP.UserId,UP.FormID,UP.PAccess,UP.PAdd,UP.PUpdate,UP.PDelete,UP.PSearch,UP.PActive from dbo.TblCPUserPermissions UP left outer Join tblCPForms F on UP.FormID=F.ID where UserId=" + lblUserId.Text + " and isnull(UP.isDeleted,0)=0 ")
                    If dt.Rows.Count = 0 Then
                        dt = DBManager.Getdatatable("select ID as FormID ,FormTitle as FormTitle,0 as PAccess,0 as PAdd,0 as PUpdate,0 as PDelete,0 as PSearch,0 as PActive from tblCPForms where ISNULL(isDeleted,0)=0 ")
                    End If
                End If
            Else
                dt = DBManager.Getdatatable("select F.FormTitle as FormTitle, GP.GroupId,GP.FormID,GP.PAccess,GP.PAdd,GP.PUpdate,GP.PDelete,GP.PSearch,GP.PActive from dbo.TblCPGroupPermissions GP left outer Join tblCPForms F on GP.FormID=F.ID where GroupId=" + ddlUserGroup.Text + " and isnull(GP.isDeleted,0)=0")
                gvUserPermissions.Enabled = False
            End If
            gvUserPermissions.DataSource = dt
            gvUserPermissions.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Save User Permissions
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SaveUserPermission(ObjId As String, Optional IsPopup As Boolean = False) As Boolean
        Try
            Dim UPObj As New TblCPUserPermissions
            Dim UPDa As New TblCPUserPermissionsFactory()
            If Not UPDa.DeleteTrans(TblCPUserPermissions.TblCPUserPermissionsFields.UserId, ObjId, _sqlconn, _sqltrans) Then
                Return False
            End If
            If ddlUserGroup.Text <> 0 Then
                Return True
            End If
            Dim Gv As GridView
            If IsPopup Then
                Gv = gvUserPermissionPopup
            Else
                Gv = gvUserPermissions
            End If
            For Each gvrow As GridViewRow In Gv.Rows
                UPObj = New TblCPUserPermissions
                UPObj.CreatedBy = UserID
                UPObj.CreatedDate = DateTime.Now
                UPObj.IsDeleted = False

                UPObj.UserId = ObjId
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


    ''' <summary>
    ''' Handle Save Permissions event from popup
    ''' </summary>
    Protected Sub SavePermission(sender As Object, e As EventArgs)
        Try
            Dim CPUserId As String = CType(sender, Button).CommandArgument
            If SaveUserPermission(CPUserId, True) Then
                clsMessages.ShowMessage(lblRes, MessageTypesEnum.CUSTOMSuccess, Me, Nothing, "Permissions Updated Successfully")
            End If

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

                Dim filePath As String = "~/CPUsers_Photos/" & fuPhoto.FileName

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

                    Dim MainWidth As String = "400"
                    Dim MainHeight As String = "300"

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
    Sub FillUserPhoto()
        Try
            'Check User photo path has value or not to set the default value
            If HiddenIcon.Text IsNot Nothing And HiddenIcon.Text <> "" Then
                imgIcon.ImageUrl = HiddenIcon.Text
            Else
                HiddenIcon.Text = "~/images/user.jpg"
                imgIcon.ImageUrl = "~/images/user.jpg"
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

End Class
