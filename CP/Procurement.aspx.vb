
#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages

#End Region
Partial Class Procurement
    Inherits System.Web.UI.Page
#Region "Global Variables"
    Dim pf As New PublicFunctions
    Dim ItemsImgs As New List(Of TblMedia)
    Dim daItemsImgs As New TblMediaFactory
    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
    Dim UserId As String = "1"
    Dim ProcurementType As String
    Dim ProcurementTitle As String
    Dim Description As String
    Dim IssueDate As String
    Dim ExpiryDate As String
    Dim OfficeName As String
    Dim IsVacancy As String
    Dim hasWin As Boolean
    Dim WinnerName As String
    Dim AreaId As String
#End Region
#Region "Public_Functions"

    ''' <summary>
    ''' If b is true : hide gridview, Operation panel bar and show panel form, Confirmation panel bar.
    ''' If b is false : hide panel form, Confirmation panel bar and show gridview, Operation panel bar.
    ''' </summary>
    Protected Sub Enabler(ByVal b As Boolean)
        Try
            pnlConfirm.Visible = b
            pnlOps.Visible = Not b
            pnlForm.Enabled = b
            pnlForm.Visible = b
            pnlGV.Visible = Not b
            If b Then
                pnlFiles.Attributes.Add("style", "display:block;")
            Else
                pnlFiles.Attributes.Add("style", "display:none;")
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Set Default controls Values 
    ''' </summary>
    Sub SetControlFields()
        Try
            ProcurementType = rblProcurementType.SelectedValue
            ProcurementTitle = txtTitle.Text
            Description = txtDescription.Text
            IssueDate = PublicFunctions.DateFormat(txtIssueDate.Text, "dd/MM/yyyy")
            ExpiryDate = PublicFunctions.DateFormat(txtExpiryDate.Text, "dd/MM/yyyy")
            IsVacancy = PublicFunctions.BoolFormat(chkIsVacancy.Checked)
            OfficeName = txtOfficeName.Text
            hasWin = PublicFunctions.BoolFormat(chkWin.Checked)
            WinnerName = txtWinnerName.Text
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Update item to be activated or not.
    ''' </summary>
    Sub UpdateActive(sender As Object, e As EventArgs)
        Try
            Dim parent = sender.parent.parent
            Dim ItemId As String = DirectCast(parent.FindControl("lblId"), Label).Text
            Dim StatusName As String = "Active"
            Dim MSG As String = ""
            Dim chk As CheckBox = DirectCast(sender, CheckBox)

            Dim Updated As Integer = 0
            If chk.Checked Then
                StatusName = "Active"
                MSG = "تم التفعيل بنجاح"
            Else
                StatusName = "Deactive"
                MSG = "تم إالغاء التفعيل بنجاح"
            End If
            Updated = DBManager.ExcuteQuery("Update TblProcurement set Active ='" + chk.Checked.ToString + "',ModifiedDate=getdate(),ModifiedBy='" + UserId + "' where Id='" + ItemId + "' ")
            If Updated = 1 Then
                clsLogs.AddSystemLogs(StatusName, "TblProcurement", ItemId)
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.CUSTOMSuccess, Page, Nothing, MSG)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region
#Region "Page Load"
    ''' <summary>
    ''' Handle page_load event
    ''' </summary>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblRes.Visible = False
        UserId = PublicFunctions.GetUserId(Page)
        Try
            If Page.IsPostBack = False Then
                Permissions.CheckPermisions(gvProcurement, cmdNew, txtSearch, cmdSearch, Me.Page, UserId)
                clsLogs.AddSystemLogs("Access")
                FillGrid()
                clsBindDDL.BindLookupDDLs("ProcurementType", rblProcurementType, False)
                FillAreasDDLs()
            End If
            'Set Default values of controls
            SetControlFields()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Private Sub FillAreasDDLs()
        Try

            Dim dtM As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where isnull(ParentId,0)=0 and isnull(isdeleted,0)=0")
            If dtM IsNot Nothing Then
                ddlMainDistribution.DataTextField = "Name"
                ddlMainDistribution.DataValueField = "Id"
                ddlMainDistribution.AppendDataBoundItems = True
                ddlMainDistribution.DataSource = dtM
                ddlMainDistribution.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region
#Region "Fill Grid"

    ''' <summary>
    ''' Fill gridview with data from TblProcurement.
    ''' </summary>
    Sub FillGrid()
        Try
            Dim dtProcurement As DataTable = DBManager.Getdatatable("select * from TblProcurement where isnull(Isdeleted,0)=0  and " + CollectConditions() + "")
            If dtProcurement.Rows.Count > 0 Then
                pgPanel.Visible = True
                ' Initialize the sorting expression.
                If SortExpression.Value = String.Empty Then
                    SortExpression.Value = "Id ASC"
                End If
                ' Populate the GridView.
                ' Convert the DataTable to DataView.
                Dim dv As New DataView(dtProcurement)

                ' Set the sort column and sort order.
                dv.Sort = SortExpression.Value.ToString()

                ' Bind the GridView control.
                gvProcurement.DataSource = dv
                gvProcurement.DataBind()
            Else
                pgPanel.Visible = False
                gvProcurement.DataSource = Nothing
                gvProcurement.DataBind()
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
#End Region
#Region "Sorting"

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
                gvProcurement.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvProcurement, e.SortExpression)).CssClass = "faDown"
            Else
                gvProcurement.HeaderRow.Cells(PublicFunctions.GetColumnIndex(gvProcurement, e.SortExpression)).CssClass = "faUp"
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
#Region "Paging"

    ''' <summary>
    ''' Paging function
    ''' </summary>
    Protected Sub GvProcurement_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            gvProcurement.PageIndex = e.NewPageIndex
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
#Region "New"

    ''' <summary>
    ''' Handle add button(form grid) click event.
    ''' </summary>
    Protected Sub Add(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "add"
            lblProcurementId.Text = ""
            pf.ClearAll(pnlForm)
            Enabler(True)
            rblProcurementType.SelectedIndex = 0
            chkIsVacancy.Enabled = True
            gvItemsImgs.DataSource = Nothing
            gvItemsImgs.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region
#Region "Edit"

    ''' <summary>
    ''' Handle edit button click event.
    ''' </summary>
    Protected Sub Edit(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            cmdSave.CommandArgument = "edit"
            lblProcurementId.Text = Sender.commandargument.ToString
            pf.ClearAll(pnlForm)
            If FillForm() Then
                Enabler(True)
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    ''' <summary>
    ''' Fill controls in the panel form with the data.
    ''' </summary>
    Private Function FillForm() As Boolean
        Try
            Dim dt As New DataTable
            dt = DBManager.Getdatatable("select * from TblProcurement where isnull(Isdeleted,0)=0  and id='" + lblProcurementId.Text + "'")
            If dt.Rows.Count <> 0 Then
                Dim ProcurementId As String = dt.Rows(0).Item("Id").ToString
                Dim AreaId As String = dt.Rows(0).Item("AreaId").ToString
                Dim ProcurementType As String = dt.Rows(0).Item("Type").ToString
                If ProcurementType <> String.Empty Then
                    rblProcurementType.SelectedValue = ProcurementType
                End If
                txtTitle.Text = dt.Rows(0).Item("Name").ToString
                txtDescription.Text = dt.Rows(0).Item("Description").ToString
                txtIssueDate.Text = PublicFunctions.DateFormat(dt.Rows(0).Item("IssueDate").ToString, "dd/MM/yyyy")
                txtExpiryDate.Text = PublicFunctions.DateFormat(dt.Rows(0).Item("ExpiryDate").ToString, "dd/MM/yyyy")
                txtOfficeName.Text = dt.Rows(0).Item("OfficeName").ToString
                chkIsVacancy.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("IsVacancy").ToString)
                chkWin.Checked = PublicFunctions.BoolFormat(dt.Rows(0).Item("Win").ToString)
                txtWinnerName.Text = dt.Rows(0).Item("WinnerName").ToString
                lblIsVacancy.Text = chkIsVacancy.Checked
                If chkIsVacancy.Checked Then
                    chkIsVacancy.Enabled = False
                End If
                If chkWin.Checked Then
                    chkWin_CheckedChanged(chkWin, New EventArgs)
                End If
                FillAreas(AreaId)
                FillFiles(ProcurementId)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function
    Private Sub FillFiles(ProcurementId As String)
        Try
            Dim dtFiles As DataTable = DBManager.Getdatatable("select *,Path as MediaPath, 0 as Main , 0 as ShowOrder, '' as Description from tblDocuments where SourceId='" & ProcurementId & "' and TableName='tblProcurement' and isnull(isdeleted,0)=0")
            gvItemsImgs.DataSource = dtFiles
            gvItemsImgs.DataBind()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    ''' <summary>
    ''' Fill Areas
    ''' </summary>
    Private Sub FillAreas(ByVal AreaId As String)
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select * from tblWorkArea where id='" + AreaId + "'")
            If dt.Rows.Count > 0 Then
                Dim SelectedValue As String = dt.Rows(0).Item("Type").ToString
                rblAreas.SelectedValue = SelectedValue
                rblAreas_SelectedIndexChanged(Nothing, New EventArgs)
                SetDDLsValues(AreaId)
                Select Case SelectedValue
                    Case "M"
                        ddlMainDistribution.SelectedValue = AreaId
                        ddlMainDistribution_SelectedIndexChanged(Nothing, New EventArgs)
                    Case "T"
                        ddlgov.SelectedValue = AreaId
                        ddlgov_TextChanged(Nothing, New EventArgs)
                    Case "C"
                        ddlCenter.SelectedValue = AreaId
                        ddlCenter_TextChanged(Nothing, New EventArgs)
                    Case "U"
                        ddlUnits.SelectedValue = AreaId
                        ddlUnits_TextChanged(Nothing, New EventArgs)
                    Case "V"
                        ddlVillage.SelectedValue = AreaId
                End Select
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
    Private Sub SetDDLsValues(ByVal WorkAreaID As String)
        Try
            Dim parentdt As DataTable = DBManager.Getdatatable("select * from vw_WorkAreaParent where Id=" & WorkAreaID)
            If parentdt.Rows.Count > 0 Then
                Dim IDs() As String = parentdt.Rows(0).Item("ParentPath").Split("-")
                Select Case IDs.Length
                    Case 0
                        pnlDDLs.Visible = False
                    Case 1
                        ddlMainDistribution.SelectedValue = Val(IDs(0))
                        ddlMainDistribution_SelectedIndexChanged(Nothing, New EventArgs)
                    Case 2
                        ddlMainDistribution.SelectedValue = IDs(0)
                        ddlMainDistribution_SelectedIndexChanged(Nothing, New EventArgs)
                        ddlgov.SelectedValue = IDs(1)
                        ddlgov_TextChanged(Nothing, New EventArgs)
                    Case 3
                        ddlMainDistribution.SelectedValue = IDs(0)
                        ddlMainDistribution_SelectedIndexChanged(Nothing, New EventArgs)
                        ddlgov.SelectedValue = IDs(1)
                        ddlgov_TextChanged(Nothing, New EventArgs)
                        ddlCenter.SelectedValue = IDs(2)
                        ddlCenter_TextChanged(Nothing, New EventArgs)
                    Case 4
                        ddlMainDistribution.SelectedValue = IDs(0)
                        ddlMainDistribution_SelectedIndexChanged(Nothing, New EventArgs)
                        ddlgov.SelectedValue = IDs(1)
                        ddlgov_TextChanged(Nothing, New EventArgs)
                        ddlCenter.SelectedValue = IDs(2)
                        ddlCenter_TextChanged(Nothing, New EventArgs)
                        ddlUnits.SelectedValue = IDs(3)
                End Select

            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region
#Region "Delete"
    ''' <summary>
    ''' Handle delete button form grid.
    ''' </summary>
    Protected Sub Delete(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            Dim ProcurementId As String = Sender.commandargument

            If DBManager.ExcuteQuery("update TblProcurement SET  Isdeleted = 'True' where Id= '" + ProcurementId + "'") = 1 Then
                clsLogs.AddSystemLogs("Delete", "TblProcurement", ProcurementId)
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Delete, Me.Page)
                FillGrid()
            End If

        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Save"

    ''' <summary>
    ''' Handle save button(form grid) click event.
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            Dim daTabeFactory As New TblProcurementFactory
            Dim dtTable As New TblProcurement
            Dim RefId As String = ""
            If cmdSave.CommandArgument = "add" Then
                If FillDT(dtTable) = False Then
                    Exit Sub
                End If
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                If daTabeFactory.InsertTrans(dtTable, _sqlconn, _sqltrans) Then
                    RefId = PublicFunctions.GetIdentity("TblProcurement")
                    If chkIsVacancy.Checked Then
                        If Not SaveVacancy(dtTable, _sqlconn, _sqltrans) Then
                            _sqltrans.Rollback()
                            Exit Sub
                        End If
                    End If
                    'Save Files
                    If Not SaveFiles(RefId, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                        Exit Sub
                    End If
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Insert, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    _sqltrans.Rollback()
                    Exit Sub
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                clsLogs.AddSystemLogs("Insert", "TblProcurement", RefId)
            Else
                dtTable = daTabeFactory.GetAllBy(TblProcurement.TblProcurementFields.Id, lblProcurementId.Text)(0)
                If FillDT(dtTable) = False Then
                    Exit Sub
                End If
                dtTable.Id = lblProcurementId.Text
                _sqlconn.Open()
                _sqltrans = _sqlconn.BeginTransaction
                If daTabeFactory.UpdateTrans(dtTable, _sqlconn, _sqltrans) Then
                    If chkIsVacancy.Checked Then
                        If Not SaveVacancy(dtTable, _sqlconn, _sqltrans) Then
                            _sqltrans.Rollback()
                            Exit Sub
                        End If
                    End If
                    'Save Files
                    If Not SaveFiles(lblProcurementId.Text, _sqlconn, _sqltrans) Then
                        _sqltrans.Rollback()
                        ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                        Exit Sub
                    End If
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Me.Page)
                Else
                    ShowMessage(lblRes, MessageTypesEnum.CustomErr, Me, Nothing, ".خطاء")
                    _sqltrans.Rollback()
                    Exit Sub
                End If
                _sqltrans.Commit()
                _sqlconn.Close()
                clsLogs.AddSystemLogs("Update", "TblProcurement", lblProcurementId.Text)
            End If
            Cancel(Sender, New EventArgs)
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            _sqltrans.Rollback()
        End Try
    End Sub

    ''' <summary>
    ''' Fill dtProcurement from controls in the panel form.
    ''' </summary>
    Protected Function FillDT(ByRef dtProcurement As TblProcurement) As Boolean
        Try
            dtProcurement.Type = ProcurementType
            dtProcurement.Name = ProcurementTitle
            dtProcurement.Description = Description
            dtProcurement.IssueDate = IssueDate
            dtProcurement.ExpiryDate = ExpiryDate
            dtProcurement.OfficeName = OfficeName
            dtProcurement.IsVacancy = IsVacancy
            dtProcurement.Win = hasWin
            dtProcurement.WinnerName = Nothing
            If hasWin Then
                dtProcurement.WinnerName = WinnerName
            End If
            Dim SelectedValue As String = rblAreas.SelectedValue
            Dim AreaId As String = Nothing
            Select Case SelectedValue
                Case "M"
                    AreaId = ddlMainDistribution.SelectedValue
                Case "T"
                    AreaId = ddlgov.SelectedValue
                Case "C"
                    AreaId = ddlCenter.SelectedValue
                Case "U"
                    AreaId = ddlUnits.SelectedValue
                Case "V"
                    AreaId = ddlVillage.SelectedValue
            End Select
            'Insert only area id with value not zero 
            'Ensure User select any value from any ddl , if not so keep it DBNull
            If AreaId <> 0 Then
                dtProcurement.AreaId = AreaId
            End If
            If cmdSave.CommandArgument = "add" Then
                dtProcurement.CreatedBy = UserId
                dtProcurement.CreatedDate = DateTime.Now
            End If
            dtProcurement.ModifiedBy = UserId
            dtProcurement.ModifiedDate = DateTime.Now

            dtProcurement.IsDeleted = False
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Insert Vacancy
    ''' </summary>
    Private Function SaveVacancy(dtProcurement As TblProcurement, _sqlconn As SqlConnection, _sqltrans As SqlTransaction) As Boolean
        Try

            Dim dtVacancy As New TblVacancy
            Dim daVacancy As New TblVacancyFactory

            daVacancy.DeleteTrans(TblVacancy.TblVacancyFields.ProcurementId, dtProcurement.Id, _sqlconn, _sqltrans)
            dtVacancy.ProcurementId = dtProcurement.Id
            dtVacancy.Name = dtProcurement.Name
            dtVacancy.IssueDate = dtProcurement.IssueDate
            dtVacancy.ExpiryDate = dtProcurement.ExpiryDate
            dtVacancy.Description = dtProcurement.Description
            dtVacancy.Active = dtProcurement.Active

            dtVacancy.CreatedBy = UserId
            dtVacancy.CreatedDate = DateTime.Now
            dtVacancy.IsDeleted = False
            If Not daVacancy.InsertTrans(dtVacancy, _sqlconn, _sqltrans) Then
                Return False
            End If


            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Insert Files
    ''' </summary>
    Private Function SaveFiles(ProcurementID As String, _sqlconn As SqlConnection, _sqltrans As SqlTransaction) As Boolean
        Try
            Dim daDoc As New TblDocumentsFactory
            Dim dtDoc As New TblDocuments
            If cmdSave.CommandArgument <> "add" Then
                'delete old files in case of update
                If Not ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("delete from TblDocuments where SourceId='" & ProcurementID & "' and TableName='tblProcurement'")) Then
                    Return False
                End If
            End If
            'loop to get images details from images gridview
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                dtDoc.SourceId = ProcurementID
                dtDoc.Path = CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                dtDoc.Title = CType(gvRow.FindControl("txtTitle"), System.Web.UI.WebControls.TextBox).Text
                dtDoc.TableName = "tblProcurement"
                dtDoc.CreatedDate = DateTime.Now
                dtDoc.IsDeleted = False
                If Not daDoc.InsertTrans(dtDoc, _sqlconn, _sqltrans) Then
                    clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function
#End Region
#Region "Cancel"

    ''' <summary>
    ''' Handle cancel button click event
    ''' </summary>
    Protected Sub Cancel(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            Enabler(False)
            FillGrid()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region


#Region "Area Changes"

    Protected Sub rblAreas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim SelectedValue As String = rblAreas.SelectedValue
            pnlDDLs.Visible = True
            'ddlMainDistribution.Items.Clear()
            'ddlMainDistribution.Items.Add(New ListItem("--اختر--", "0"))
            'FillAreasDDLs()
            'ddlgov.Items.Clear()
            'ddlgov.Items.Add(New ListItem("--اختر--", "0"))
            'ddlCenter.Items.Clear()
            'ddlCenter.Items.Add(New ListItem("--اختر--", "0"))
            'ddlUnits.Items.Clear()
            'ddlUnits.Items.Add(New ListItem("--اختر--", "0"))
            Select Case SelectedValue
                Case "M"
                    pnlMainDistribution.Visible = True
                    pnlGov.Visible = False
                    pnlCenter.Visible = False
                    pnlUnits.Visible = False
                    pnlVillage.Visible = False
                Case "T"
                    pnlMainDistribution.Visible = True
                    pnlGov.Visible = True
                    pnlCenter.Visible = False
                    pnlUnits.Visible = False
                    pnlVillage.Visible = False
                Case "C"
                    pnlMainDistribution.Visible = True
                    pnlGov.Visible = True
                    pnlCenter.Visible = True
                    pnlUnits.Visible = False
                    pnlVillage.Visible = False
                Case "U"
                    pnlMainDistribution.Visible = True
                    pnlGov.Visible = True
                    pnlCenter.Visible = True
                    pnlUnits.Visible = True
                    pnlVillage.Visible = False
                Case "V"
                    pnlMainDistribution.Visible = True
                    pnlGov.Visible = True
                    pnlCenter.Visible = True
                    pnlUnits.Visible = True
                    pnlVillage.Visible = True
            End Select
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Protected Sub ddlMainDistribution_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim selected As String = ""
            If ddlMainDistribution.SelectedValue <> 0 Then
                selected = ddlMainDistribution.SelectedValue
            End If
            ddlgov.Items.Clear()
            ddlgov.Items.Add(New ListItem("--اختر--", "0"))
            ddlCenter.Items.Clear()
            ddlCenter.Items.Add(New ListItem("--اختر--", "0"))
            ddlUnits.Items.Clear()
            ddlUnits.Items.Add(New ListItem("--اختر--", "0"))
            ddlVillage.Items.Clear()
            ddlVillage.Items.Add(New ListItem("--اختر--", "0"))
            Dim dtM As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where ParentId='" & selected & "' and isnull(isdeleted,0)=0")
            If dtM IsNot Nothing Then
                ddlgov.DataTextField = "Name"
                ddlgov.DataValueField = "Id"
                ddlgov.AppendDataBoundItems = True
                ddlgov.DataSource = dtM
                ddlgov.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Protected Sub ddlgov_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim selected As String = ""
            If ddlgov.SelectedValue <> 0 Then
                selected = ddlgov.SelectedValue
            End If
            ddlCenter.Items.Clear()
            ddlCenter.Items.Add(New ListItem("--اختر--", "0"))
            Dim dt As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where ParentId='" & selected & "' and isnull(isdeleted,0)=0")
            If dt IsNot Nothing Then
                ddlCenter.DataTextField = "Name"
                ddlCenter.DataValueField = "Id"
                ddlCenter.AppendDataBoundItems = True
                ddlCenter.DataSource = dt
                ddlCenter.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

    Protected Sub ddlCenter_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim selected As String = ""
            If ddlCenter.SelectedValue <> 0 Then
                selected = ddlCenter.SelectedValue
            End If
            ddlUnits.Items.Clear()
            ddlUnits.Items.Add(New ListItem("--اختر--", "0"))
            Dim dt As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where ParentId='" & selected & "' and isnull(isdeleted,0)=0")
            If dt IsNot Nothing Then
                ddlUnits.DataTextField = "Name"
                ddlUnits.DataValueField = "Id"
                ddlUnits.AppendDataBoundItems = True
                ddlUnits.DataSource = dt
                ddlUnits.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub

    Protected Sub ddlUnits_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim selected As String = ""
            If ddlUnits.SelectedValue <> 0 Then
                selected = ddlUnits.SelectedValue
            End If
            ddlVillage.Items.Clear()
            ddlVillage.Items.Add(New ListItem("--اختر--", "0"))
            Dim dt As DataTable = DBManager.Getdatatable("Select Id,Name from tblWorkArea where ParentId='" & selected & "' and isnull(isdeleted,0)=0")
            If dt IsNot Nothing Then
                ddlVillage.DataTextField = "Name"
                ddlVillage.DataValueField = "Id"
                ddlVillage.AppendDataBoundItems = True
                ddlVillage.DataSource = dt
                ddlVillage.DataBind()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub
#End Region

#Region "Files Upload"
    ''' <summary>
    '''Fill Photos gridview.
    ''' </summary>
    Sub FillPhotos(ByVal SourceId As String)
        Try
            Dim dtItemImgs As DataTable = DBManager.Getdatatable("select * from tblDocuments where isnull(Isdeleted,0)=0 and  SourceId='" & SourceId & "' and TableName='tblProcurement'")
            If dtItemImgs.Rows.Count > 0 Then
                gvItemsImgs.DataSource = dtItemImgs
                gvItemsImgs.DataBind()

                'Fill DDl ShowOrder with all values
                ItemsImgs = GetItemImgs()
                BindShowOrder()
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub

    ''' <summary>
    '''Add Uploaded photos to the gridview.
    ''' </summary>
    Protected Sub AddFiles(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim dt As DataTable = MultiFileUpload.getUploadedFilesDetails(lblUploadedFilesDetails.Value)
            If dt IsNot Nothing Then
                ItemsImgs = GetItemImgs()
                If dt.Rows.Count > 0 Then
                    Dim i As Integer = 0
                    While i < dt.Rows.Count
                        Dim dtDoc As New TblMedia
                        dtDoc.MediaPath = "~/" + dt(i)("URL").ToString
                        dtDoc.Id = i + 1
                        ItemsImgs.Add(dtDoc)
                        i += 1
                    End While
                    gvItemsImgs.Visible = True
                    gvItemsImgs.DataSource = ItemsImgs
                    gvItemsImgs.DataBind()
                    'Fill Order DDL and set selected value based on row index
                    BindShowOrder()
                End If
                lblUploadedFilesDetails.Value = ""
                BindMainImg()
            Else
                gvItemsImgs.DataSource = Nothing
                gvItemsImgs.DataBind()
            End If

        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub

    ''' <summary>
    '''Check radio button with main photo
    ''' </summary>
    Sub BindShowOrder()
        Try
            Dim OrderList As New List(Of ListItem)
            Dim c As Integer = 0
            While c < ItemsImgs.Count
                Dim itm As New ListItem
                itm.Text = c + 1
                itm.Value = c + 1
                OrderList.Add(itm)
                Dim lblShowOrder = DirectCast(gvItemsImgs.Rows(c).FindControl("lblShowOrder"), Label).Text
                'If empty (case add new photo) or not exist in ddl after delete OrElse Not OrderList.Contains(New ListItem(lblShowOrder)) 
                If lblShowOrder = String.Empty Then
                    DirectCast(gvItemsImgs.Rows(c).FindControl("lblShowOrder"), Label).Text = c + 1
                End If
                c += 1
            End While

            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                Dim lblOrder As Label = DirectCast(gvRow.FindControl("lblShowOrder"), Label)
                Dim dd As DropDownList = DirectCast(gvRow.FindControl("ddlShowOrder"), DropDownList)
                dd.DataSource = OrderList
                dd.DataBind()
                dd.SelectedValue = lblOrder.Text
            Next
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub

    ''' <summary>
    '''Check radio button with main photo
    ''' </summary>
    Sub BindMainImg()
        Try
            'Dim dtItems As New DataTable
            'dtItems = DBManager.Getdatatable("select MediaPath from tblMedia where Main='1' and SourceId='" + lblProcurementID.Text + "'")
            'If dtItems.Rows.Count <> 0 Then
            '    Dim Photo = dtItems.Rows(0).Item("MediaPath").ToString
            '    SelectMainImg(Photo)
            'Else
            SelectMainImg("0")
            'End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub
    Sub SelectMainImg(ByVal img As String)
        Try
            Dim Count As Integer = 0
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                Dim chk As RadioButton = DirectCast(gvRow.FindControl("rblSelect"), RadioButton)
                If CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl = img Then
                    chk.Checked = True
                    Count += 1
                End If
            Next
            If Count = 0 Then
                For Each gvRow As GridViewRow In gvItemsImgs.Rows
                    Dim chk As RadioButton = DirectCast(gvRow.FindControl("rblSelect"), RadioButton)
                    chk.Checked = True
                    Exit Sub
                Next
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub
    ''' <summary>
    '''check if there are exist photos and get them to add the new photos above them
    ''' </summary>
    Private Function GetItemImgs() As List(Of TblMedia)
        Dim DocList As New List(Of TblMedia)
        Try
            Dim AttObj As TblMedia
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                AttObj = New TblMedia
                AttObj.Id = CType(gvRow.FindControl("lblId"), Label).Text
                AttObj.MediaPath = CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                AttObj.ShowOrder = CType(gvRow.FindControl("lblShowOrder"), System.Web.UI.WebControls.Label).Text
                AttObj.Title = CType(gvRow.FindControl("txtTitle"), System.Web.UI.WebControls.TextBox).Text
                AttObj.Description = CType(gvRow.FindControl("txtDescription"), System.Web.UI.WebControls.TextBox).Text
                AttObj.CreatedDate = Date.Now
                AttObj.Main = CType(gvRow.FindControl("rblSelect"), RadioButton).Checked
                AttObj.MediaType = "I"
                AttObj.IsDeleted = False
                DocList.Add(AttObj)
            Next
            Return DocList
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
            Return DocList
        End Try
    End Function
    ''' <summary>
    '''Delete Image from gridview.
    ''' </summary>
    Protected Sub DeleteImg(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim parent As GridViewRow = sender.parent.parent.parent
            Dim dt As New List(Of TblMedia)
            Try
                dt = GetItemImgs()
                dt.Remove(dt.Item(parent.RowIndex))
                gvItemsImgs.DataSource = dt
                gvItemsImgs.DataBind()
                BindMainImg()
                ItemsImgs = GetItemImgs()
                'Reset DDL Show Order
                Dim c As Integer = 0
                While c < ItemsImgs.Count
                    DirectCast(gvItemsImgs.Rows(c).FindControl("lblShowOrder"), Label).Text = c + 1
                    c += 1
                End While
                BindShowOrder()
            Catch ex As Exception
                clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
            End Try
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub
    ''' <summary>
    '''return Main Image to save it at tblitems.
    ''' </summary>
    Function getMainImg() As String
        Dim Count As Integer = 0
        Try
            If gvItemsImgs.Rows.Count > 0 Then
                For Each gvRow As GridViewRow In gvItemsImgs.Rows
                    Dim chk As RadioButton = DirectCast(gvRow.FindControl("rblSelect"), RadioButton)
                    If chk.Checked Then
                        Count += 1
                        Return CType(gvRow.FindControl("lblImg"), System.Web.UI.WebControls.Image).ImageUrl
                    End If
                Next
                Return "0"
            Else
                Return String.Empty
            End If
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
            Return String.Empty
        End Try
    End Function
    ''' <summary>
    '''allow user to select only one image.
    ''' </summary>
    Protected Sub SelectRBL(ByVal sender As Object, ByVal e As EventArgs)
        Try
            For Each row As GridViewRow In gvItemsImgs.Rows
                Dim chk As RadioButton = DirectCast(row.FindControl("rblSelect"), RadioButton)
                If chk.ClientID <> sender.ClientID Then
                    chk.Checked = False
                End If
            Next
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub
    ''' <summary>
    ''' View Photos of news.
    ''' </summary>
    Protected Sub ViewPhotos(ByVal sender As Object, ByVal e As EventArgs)
        Try


        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try
    End Sub

    ''' <summary>
    ''' Handle Changing photo order
    ''' </summary>
    Protected Sub ddlShowOrder_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim Parent = sender.Parent
            Dim NewOrderValue As String = CType(Parent.FindControl("ddlShowOrder"), DropDownList).SelectedValue
            Dim OldOrder As Label = DirectCast(Parent.FindControl("lblShowOrder"), Label)
            For Each gvRow As GridViewRow In gvItemsImgs.Rows
                Dim lblOrder As Label = DirectCast(gvRow.FindControl("lblShowOrder"), Label)
                Dim ddl As DropDownList = DirectCast(gvRow.FindControl("ddlShowOrder"), DropDownList)
                If NewOrderValue = lblOrder.Text Then
                    If ddl.Items.Count > 0 Then
                        If ddl.Items.Contains(New ListItem(OldOrder.Text)) Then
                            ddl.SelectedValue = OldOrder.Text
                            lblOrder.Text = OldOrder.Text
                        End If
                    End If
                End If
            Next
            OldOrder.Text = NewOrderValue
        Catch ex As Exception
            clsMessages.ShowErrorMessgage(lblRes, "Error: " & ex.ToString, Me)
        End Try

    End Sub
    Sub ResetImgGridColumns(ByVal b As Boolean)
        Try
            Dim i As Integer
            While i < gvItemsImgs.Columns.Count
                If gvItemsImgs.Columns(i).HeaderText = "Delete" Then
                    gvItemsImgs.Columns(i).Visible = b
                End If
                i += 1
            End While
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub


#End Region


    Protected Sub gvItemsImgs_DataBound(sender As Object, e As EventArgs)
        For Each gvRow As GridViewRow In gvItemsImgs.Rows
            Dim lblImg As String = DirectCast(gvRow.FindControl("lblImg"), Image).ImageUrl
            If lblImg.ToString.Split(".").Last.ToLower = "doc" OrElse lblImg.ToString.Split(".").Last.ToLower = "docx" Then
                CType(gvRow.FindControl("lblMedia"), System.Web.UI.WebControls.Image).ImageUrl = "images/word.png"
            ElseIf lblImg.ToString.Split(".").Last.ToLower = "pdf" Then
                CType(gvRow.FindControl("lblMedia"), System.Web.UI.WebControls.Image).ImageUrl = "images/pdf_icon.jpg"
            ElseIf lblImg.ToString.Split(".").Last.ToLower = "xls" OrElse lblImg.ToString.Split(".").Last.ToLower = "xlsx" Then
                CType(gvRow.FindControl("lblMedia"), System.Web.UI.WebControls.Image).ImageUrl = "images/icon-xlsx.png"
            Else
                CType(gvRow.FindControl("lblMedia"), System.Web.UI.WebControls.Image).ImageUrl = lblImg
            End If

        Next
    End Sub
    Protected Sub chkWin_CheckedChanged(sender As Object, e As EventArgs)
        pnlWinner.Visible = sender.checked
    End Sub
End Class
