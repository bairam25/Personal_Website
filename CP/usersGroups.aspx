<%@ Page Language="VB" Theme="Theme1" AutoEventWireup="false" CodeFile="UsersGroups.aspx.vb" Inherits="UsersGroups" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>مجموعات المستخدمين</title>
    <link rel="shortcut icon" href="../images/logo/favi.png" />
    <!-- Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Resource style -->
    <link rel="stylesheet" href="css/c-scroll.css" />
    <link rel="stylesheet" href="css/themify-icons.css" />
    <link rel="stylesheet" href="css/cpcustom.css" />
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800|Poppins:300,400,500,600,700,800|Roboto:300,400,500,700,900" rel="stylesheet" />

    <!-- Resource script -->
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <script src="js/respond.js" type="text/javascript"></script>
    <script src="js/matchmedia.polyfill.js" type="text/javascript"></script>
    <script src="js/sidebar-nav.min.js" type="text/javascript"></script>
    <script src="JSCode/KeypressValidators.js"></script>
    <script src="JSCode/Popup.js"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" autocomplete="off">
        <asp:ToolkitScriptManager ID="TKSM" runat="server" ScriptMode="Release">
            <Services>
                <asp:ServiceReference Path="~/WebService.asmx" />
            </Services>
        </asp:ToolkitScriptManager>

        <!--============================ Page-header =============================-->
        <div class="container-fluid">
            <div class="page-header">
                <div class="row">
                    <div class="col-sm-6 col-xs-5 text-left">
                        <h4>مجموعات المستخدمين</h4>
                    </div>
                    <div class="col-sm-6 col-xs-7">
                        <ol class="breadcrumb">
                            <li><a href="Dashboards.aspx"><i class="ti-home"></i></a></li>
                            <li>مجموعات المستخدمين</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>


        <asp:UpdatePanel ID="upUsersGroups" runat="server" ClientIDMode="Static" RenderMode="Inline" ScriptMode="Release">
            <ContentTemplate>
                <div id="wrapper">

                    <!--============================ Page-content =============================-->
                    <div id="page-wrapper" style="min-height: 684px;">

                        <div class="pnlPermissionPopup">
                            <asp:Button ID="btnPerm" runat="server" Style="display: none" />
                            <asp:ModalPopupExtender ID="mpePermissionPopup" runat="server" PopupControlID="pnlPermissionPopup" ClientIDMode="AutoID" TargetControlID="btnPerm" BackgroundCssClass="modalBackground" CancelControlID="btnClose">
                            </asp:ModalPopupExtender>

                            <asp:Panel ID="pnlPermissionPopup" runat="server" CssClass="modal-dialog modal-dialog-scrollable top5" Style="display: none">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <h4 style="margin-top: 7px;">مجموعات المستخدمين</h4>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="pull-right">
                                                    <asp:LinkButton SkinID="btn-close" runat="server" ID="btnClose"><i class="ti-close icon-close"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-body">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvGroupPermissionPopup" CssClass="table" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Style="opacity: 10;">
                                                <Columns>
                                                    <asp:TemplateField HeaderText=" إسم الشاشة ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFormTitle" Text='<%#Eval("FormTitle")%>' runat="server"></asp:Label>
                                                            <asp:Label ID="lblFormId" runat="server" Text='<%# Eval("FormId")%>' Visible="False"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllAccess" ClientIDMode="Static" Text=" دخول " runat="server" onclick="CheckAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAccess" onclick="Select(this);" ClientIDMode="Static" Text=" " runat="server" Checked='<%#Eval("PAccess")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllAdd" Text=" إضافة " runat="server"
                                                                onclick="CheckAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkAdd" onclick="Select(this);" Text=" " runat="server" Checked='<%#Eval("PAdd")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllUpdate" Text=" تحديث " runat="server"
                                                                onclick="CheckAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkUpdate" onclick="Select(this);" Text=" " runat="server" Checked='<%#Eval("PUpdate")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllDelete" Text=" حذف " runat="server"
                                                                onclick="CheckAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDelete" onclick="Select(this);" Text=" " runat="server" Checked='<%#Eval("PDelete")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllSearch" Text=" بحث " runat="server"
                                                                onclick="CheckAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSearch" onclick="Select(this);" Text=" " runat="server" Checked='<%#Eval("PSearch")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllActive" Text=" تفعيل " runat="server"
                                                                onclick="CheckAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkActive" onclick="Select(this);" Text=" " runat="server" Checked='<%#Eval("PActive")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    NoDataFound
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Panel ID="pnlPopupSave" runat="server">
                                                    <span style="position: relative;">
                                                        <i class="fa-check fa icon-save"></i>
                                                        <asp:Button SkinID="btn-save" ID="btnSavePermissionPopup" OnClick="SavePermission" runat="server" Text=" حفظ " UseSubmitBehavior="false" OnClientClick="SaveClick(this,'');" />
                                                    </span>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                        <div class="container-fluid">
                            <div class="uploader">
                                <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="upUsersGroups" ClientIDMode="AutoID">
                                    <ProgressTemplate>
                                        <asp:Image ID="imgLoader" class="update-progress " runat="server" ImageUrl="images/ajax-loader.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>

                            <div class="row">
                                <asp:Panel ID="pnlOps" runat="server">
                                    <div class="col-md-12">
                                        <div class="table-top-panel">
                                            <div class="tbl-top-panel-left">
                                                <div class="row">
                                                    <asp:Panel ID="pgPanel" CssClass="input-180 input-in" runat="server">
                                                        <div class="input-group mt5">
                                                            <asp:DropDownList runat="server" CssClass="form-control ltr" ID="ddlPager" OnSelectedIndexChanged="PageSize_Changed" AutoPostBack="true">
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="input-group-addon" id="basic-addon1">سجلات / الصفحة</span>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>

                                            <div class="tbl-top-panel-right">
                                                <div class="row">
                                                    <div class="input-280 input-in searchContiner pull-right">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtSearch" runat="server" type="text" placeholder=" Search " AutoPostBack="true" OnTextChanged="FillGrid" onkeypress="return isString(event);" onkeyup="ValidateChars(this);ShowHideClearSearch(this.value);" ToolTip="Search by Username"></asp:TextBox>
                                                            <asp:Button ID="btnSearch" runat="server" Style="display: none" ClientIDMode="Static" OnClick="FillGrid" />
                                                            <asp:LinkButton runat="server" SkinID="clear-search" ID="cmdClear" title="مسح" OnClientClick="$('#txtSearch').val('');">&times;</asp:LinkButton>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lbSearchIcon" runat="server" SkinID="btn-search" type="button" OnClick="FillGrid"> <i class="fa-search fa"></i> </asp:LinkButton>
                                                            </span>
                                                        </div>
                                                        <!-- /input-group -->
                                                    </div>
                                                    <asp:LinkButton ID="lbAdd" OnClick="Add" runat="server" SkinID="btn-new" ToolTip="إضافة">إضافة <i class="ti-plus"></i></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="col-md-12">
                                    <asp:Label runat="server" ID="lblRes"></asp:Label>
                                </div>

                                <asp:Panel ID="pnlGV" runat="server">
                                    <div class="col-md-12">
                                        <div class="table-layer" style="box-shadow: 0px 1px 15px 1px rgba(69,65,78,0.08);">
                                            <div class="table-responsive mt5">
                                                <asp:HiddenField ID="SortExpression" runat="server" />
                                                <asp:GridView ID="gvUsersGroups" CssClass="table Pagination-table" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnPageIndexChanging="gvUser_PageIndexChanged"
                                                    AllowPaging="true" PageSize='<%# ddlPager.Text%>' OnSorting="gv_Sorting">
                                                    <Columns>


                                                        <asp:TemplateField HeaderText=" إسم المجموعة " SortExpression="Name" HeaderStyle-CssClass="upnDownArrow">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" Text='<%# Eval("Id")%>' runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblName" Text='<%# Eval("Name")%>' runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="آخر تحديث" SortExpression="ModifiedDate" HeaderStyle-CssClass="upnDownArrow">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblModifiedDate" Text='<%# Eval("ModifiedDate")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الصلاحيات">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbPermission" CssClass="btni-xxxs btn-blue brd-50" CommandArgument='<%#Eval("Id")%>' runat="server" ToolTip="Permissions" OnClick="ShowPermission"> <i class="fa-key fa"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="تحديث">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbUpdate" CssClass="btni-xxxs btn-blue brd-50" OnClick="Edit" ToolTip="Edit" runat="server" CommandArgument='<%#Eval("Id")%>'> <i class="fa-edit fa"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="حذف">
                                                            <ItemTemplate>
                                                                <a href="#" id="hrefDelete" title="Delete" class="btni-xxxs btn-red brd-50"
                                                                    onclick="ShowConfirmPopup('<%# CType(Container, GridViewRow).FindControl("mpConfirmDelete").ClientID.ToString%>','<%# CType(Container, GridViewRow).FindControl("pnlConfirmExtenderDelete").ClientID.ToString%>');return false;"><i class="fa fa-trash"></i></a>
                                                                <asp:HiddenField ID="hfDelete" runat="server" />
                                                                <asp:ModalPopupExtender ID="mpConfirmDelete" runat="server" PopupControlID="pnlConfirmExtenderDelete" TargetControlID="hfDelete"
                                                                    CancelControlID="lbNoDelete" BackgroundCssClass="modalBackground">
                                                                </asp:ModalPopupExtender>
                                                                <asp:Panel ID="pnlConfirmExtenderDelete" runat="server" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                                    <div class="header">
                                                                        رسالة تاكيد
                                                                    </div>
                                                                    <div class="body">
                                                                        <label>هل تريد حذف هذة المجموعة ؟</label>
                                                                    </div>

                                                                    <div class="footer">
                                                                        <ul class="btn-uls mb0">
                                                                            <li class="btn-lis">
                                                                                <asp:LinkButton ID="lbYesDelete" runat="server" CssClass="btn-main btn-green" CommandArgument='<%# Eval("Id") %>' OnClick="Delete">نعم<i class="ti-check"></i></asp:LinkButton></li>
                                                                            <%--OnClick="Delete"--%>
                                                                            <li class="btn-lis">
                                                                                <a id="lbNoDelete" class="btn-main btn-red" onclick="CloseConfirmPopup('<%# CType(Container, GridViewRow).FindControl("mpConfirmDelete").ClientID.ToString%>');return false;">لا<i class="ti-close"></i></a>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        لا توجد بيانات
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>

                            <div class="col-md-12 p0 mb10">
                                <asp:Panel ID="pnlConfirmTop" runat="server" Visible="false">
                                    <ul class="btn-uls pull-right">
                                        <li class="btn-lis">
                                            <span style="position: relative;">
                                                <i class="fa-check fa icon-save"></i>
                                                <asp:Button ID="btnSave" runat="server" SkinID="btn-save" ValidationGroup="Main" OnClick="Save" UseSubmitBehavior="false" OnClientClick="SaveClick(this,'Main');" Text="حفظ" />
                                            </span>
                                        </li>
                                        <li class="btn-lis">
                                            <asp:Panel runat="server" ID="pnlCancel">
                                                <a href="#" title="Cancel" class="btn-main btn-red" data-toggle="modal" data-placement="bottom" data-original-title="Cancel"
                                                    onclick="ShowConfirmPopup('mpConfirmCancel','pnlConfirmExtenderCancel');return false;">إلغاء<i class="ti-close"></i></a>
                                                <asp:HiddenField ID="hfCancel" runat="server" />
                                                <asp:ModalPopupExtender ID="mpConfirmCancel" ClientIDMode="Static" runat="server" PopupControlID="pnlConfirmExtenderCancel" TargetControlID="hfCancel"
                                                    CancelControlID="lbNoCancel" BackgroundCssClass="modalBackground">
                                                </asp:ModalPopupExtender>

                                            </asp:Panel>
                                            <asp:Panel ID="pnlConfirmExtenderCancel" runat="server" ClientIDMode="Static" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                <div class="header">
                                                    رسالة تاكيد
                                                </div>
                                                <div class="body">
                                                    <label>تاكيد الإلغاء ؟</label>
                                                </div>

                                                <div class="footer">
                                                    <ul class="btn-uls mb0">
                                                        <li class="btn-lis">
                                                            <asp:LinkButton ID="lbYesCancel" runat="server" SkinID="btn-green" OnClick="Cancel" CausesValidation="false">نعم<i class="ti-check"></i></asp:LinkButton></li>
                                                        <li class="btn-lis">
                                                            <asp:LinkButton ID="lbNoCancel" runat="server" SkinID="btn-red" OnClientClick="CloseConfirmPopup('mpConfirmCancel');return false;">لا<i class="ti-close"></i></asp:LinkButton></li>
                                                    </ul>
                                                </div>
                                            </asp:Panel>
                                        </li>
                                    </ul>
                                </asp:Panel>
                            </div>
                        </div>

                        <asp:Panel ID="pnlForm" Visible="false" runat="server">
                            <div class="container-fluid">
                                <asp:ValidationSummary ID="ValidationSummary" CssClass="dis-none" ClientIDMode="Static" DisplayMode="BulletList" ValidationGroup="Main" EnableClientScript="true" runat="server" Font-Size="Medium" ForeColor="#CC0000" />

                                <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">
                                    <div class="row">
                                        <div class="user-flex-panel">
                                            <div class="col-md-12">
                                                <div class="panel panel-default mb10">
                                                    <div class="panel-heading" role="tab" id="headingOne">
                                                        <h4 class="panel-title">
                                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Details</a>
                                                        </h4>
                                                    </div>

                                                    <div id="collapseOne" class="panel-collapse collapse in">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-4">
                                                                        <label class="required">الأسم</label>
                                                                        <asp:TextBox ID="txtGroupName" runat="server" MaxLength="500" onkeypress="return isString(event);" onkeyup="ValidateChars(this);"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                            TargetControlID="txtGroupName" FilterType="Custom" InvalidChars="!@#$%^&*()_+=-/?\><~»«" FilterMode="InvalidChars">
                                                                        </asp:FilteredTextBoxExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Main" ControlToValidate="txtGroupName"
                                                                            ErrorMessage=" أدخل الأسم " Display="Dynamic" Text="أدخل الأسم"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <label>الوصف</label>
                                                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" MaxLength="500" onkeypress="return isString(event);" onkeyup="ValidateChars(this);"></asp:TextBox>

                                                                    </div>
                                                                    <div class="col-md-4">
                                                                        <div class="cust-rad-btns">
                                                                            <label class="mb10">تفعيل</label>
                                                                            <div class="c-check">
                                                                                <asp:CheckBox ID="chkActive" runat="server" Checked="true" Text=" Active " />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <asp:Panel ID="pnlGroupDT" runat="server">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading" role="tab" id="headingTwo">
                                                            <h4 class="panel-title">
                                                                <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">صلاحيات المجموعة</a>
                                                            </h4>
                                                        </div>

                                                        <div id="collapseTwo" class="panel-collapse collapse in">
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-12">
                                                                            <div class="table-responsive">
                                                                                <asp:GridView ID="gvGroupPermissions" CssClass="table" ClientIDMode="Static" runat="server" AutoGenerateColumns="False">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText=" إسم الشاشة ">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblFormTitle" Text='<%#Eval("FormTitle")%>' runat="server"></asp:Label>
                                                                                                <asp:Label ID="lblFormId" runat="server" Text='<%# Eval("FormId")%>' Visible="False"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox ID="chkAllAccess" ClientIDMode="Static" Text="دخول" runat="server" onclick="CheckAll(this);" />
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkAccess" onclick="Select(this);" ClientIDMode="Static" Text=" " runat="server" Checked='<%#Eval("PAccess")%>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox ID="chkAllAdd" Text="إضافة" runat="server"
                                                                                                    onclick="CheckAll(this);" />
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkAdd" onclick="Select(this);" Text=" " runat="server" Checked='<%#Eval("PAdd")%>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox ID="chkAllUpdate" Text="تحديث" runat="server"
                                                                                                    onclick="CheckAll(this);" />
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkUpdate" onclick="Select(this);" runat="server" Text=" " Checked='<%#Eval("PUpdate")%>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox ID="chkAllDelete" Text="حذف" runat="server"
                                                                                                    onclick="CheckAll(this);" />
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkDelete" onclick="Select(this);" Text=" " runat="server" Checked='<%#Eval("PDelete")%>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox ID="chkAllSearch" Text="بحث" runat="server"
                                                                                                    onclick="CheckAll(this);" />
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkSearch" onclick="Select(this);" Text=" " runat="server" Checked='<%#Eval("PSearch")%>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderStyle-CssClass="gvPermissionCheckBox">
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox ID="chkAllActive" Text="تفعيل" runat="server"
                                                                                                    onclick="CheckAll(this);" />
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkActive" onclick="Select(this);" Text=" " runat="server" Checked='<%#Eval("PActive")%>' />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <EmptyDataTemplate>
                                                                                        NoDataFound
                                                                                    </EmptyDataTemplate>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <div style="display: none;">
                        <asp:Label ID="lblID" runat="server" ClientIDMode="Static"></asp:Label>
                    </div>
                    <script src="JSCode/CPUsersGroups.js"></script>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>


