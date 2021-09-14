<%@ Page Language="VB" Theme="Theme1" AutoEventWireup="false" CodeFile="users.aspx.vb" Inherits="ControlPanel_users" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>المستخدمين</title>
    <link rel="shortcut icon" href="../images/logo/favi.png" />
    <!-- Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Resource style -->
    <link rel="stylesheet" href="css/c-scroll.css" />
    <link rel="stylesheet" href="css/themify-icons.css" />
    <link rel="stylesheet" href="css/multifile-up.css" />
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
    <script src="JSCode/UploadUserImg.js"></script>
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
                        <h4>المستخدمين</h4>
                    </div>
                    <div class="col-sm-6 col-xs-7">
                        <ol class="breadcrumb">
                            <li><a href="Dashboards.aspx"><i class="ti-home"></i></a></li>
                            <li>المستخدمين</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="upUsers" runat="server" ClientIDMode="Static" RenderMode="Inline" ScriptMode="Release">
            <ContentTemplate>
                <div id="wrapper">

                    <!--============================ Page-content =============================-->
                    <div id="page-wrapper" style="min-height: 684px;">
                        <div class="container-fluid">
                            <div class="uploader">
                                <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="upUsers" ClientIDMode="AutoID">
                                    <ProgressTemplate>
                                        <asp:Image ID="imgLoader" class="update-progress" runat="server" ImageUrl="images/ajax-loader.gif" />
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
                                                            <asp:TextBox ID="txtSearch" runat="server" type="text" placeholder="بحث بإسم المستخدم" AutoPostBack="true" OnTextChanged="FillGrid" onkeypress="return isString(event);" onkeyup="ValidateChars(this);ShowHideClearSearch(this.value);" ToolTip="بحث بإسم المستخدم"></asp:TextBox>
                                                            <asp:AutoCompleteExtender ID="aclSearch" CompletionListCssClass="acl" CompletionListHighlightedItemCssClass="li-hover" CompletionListItemCssClass="li" runat="server" ServiceMethod="GetUserNames" ServicePath="~/WebService.asmx" MinimumPrefixLength="1"
                                                                TargetControlID="txtSearch">
                                                            </asp:AutoCompleteExtender>
                                                            <asp:Button ID="btnSearch" runat="server" Style="display: none" ClientIDMode="Static" OnClick="FillGrid" />
                                                            <asp:LinkButton runat="server" SkinID="clear-search" ID="cmdClear" title="مسح" OnClientClick="$('#txtSearch').val('');">&times;</asp:LinkButton>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lbSearchIcon" runat="server" SkinID="btn-search" type="button" OnClick="FillGrid"> <i class="fa-search fa"></i> </asp:LinkButton>
                                                            </span>
                                                        </div>
                                                        <!-- /input-group -->
                                                    </div>
                                                    <asp:LinkButton ID="lbAdd" OnClick="Add" runat="server" SkinID="btn-new" ToolTip="إضافة">إضافة <i class="ti-plus"></i></asp:LinkButton></li>
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
                                                <asp:GridView ID="gvUsers" CssClass="table Pagination-table" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnPageIndexChanging="gvUser_PageIndexChanged"
                                                    AllowPaging="true" PageSize='<%# ddlPager.Text%>' OnSorting="gv_Sorting">
                                                    <Columns>


                                                        <asp:TemplateField HeaderText=" الإسم " SortExpression="FullName" HeaderStyle-CssClass="upnDownArrow">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFullName" Text='<%# Eval("FullName")%>' runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="إسم المستخدم" SortExpression="UserName" HeaderStyle-CssClass="upnDownArrow">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUserName" Text='<%# Eval("UserName")%>' runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="البريد الإلكتروني" SortExpression="Email" HeaderStyle-CssClass="upnDownArrow">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmail" Text='<%# Eval("Email")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="آخر تحديث" SortExpression="ModifiedDate" HeaderStyle-CssClass="upnDownArrow">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblModifiedDate" Text='<%# Eval("ModifiedDate")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الصلاحيات">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbPermission" CssClass="btni-xxxs btn-blue brd-50" CommandArgument='<%#Eval("UserID")%>' runat="server" ToolTip="Permissions" OnClick="ShowPermission" Visible='<%# IIf(Eval("UserName").ToString = "system", False, True) %>'> <i class="fa-key fa"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="تحديث">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbUpdate" CssClass="btni-xxxs btn-blue brd-50" OnClick="Edit" ToolTip="Edit" runat="server" CommandArgument='<%#Eval("UserID")%>' Visible='<%# IIf(Eval("UserName").ToString = "system", False, True) %>'> <i class="fa-edit fa"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="حذف">
                                                            <ItemTemplate>
                                                                <a href="#" id="hrefDelete" class="btni-xxxs btn-red brd-50" title="Delete"   style='<%# IIf(Eval("UserName").ToString = "system", "display:none;", "display:block;") %>'
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
                                                                        <label>هل تريد حذف هذا المستخدم</label>
                                                                    </div>

                                                                    <div class="footer">
                                                                        <ul class="btn-uls mb0">
                                                                            <li class="btn-lis">
                                                                                <asp:LinkButton ID="lbYesDelete" runat="server" CssClass="btn-main btn-green" CommandArgument='<%# Eval("UserId") %>' OnClick="Delete">نعم<i class="ti-check"></i></asp:LinkButton></li>
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
                                                        لا يوجد مستخدمين
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="pnlPermissionPopup">
                                        <asp:Button ID="btnPerm" runat="server" Style="display: none" />
                                        <asp:ModalPopupExtender ID="mpePermissionPopup" runat="server" PopupControlID="pnlPermissionPopup" ClientIDMode="AutoID" TargetControlID="btnPerm" BackgroundCssClass="modalBackground" CancelControlID="btnClose">
                                        </asp:ModalPopupExtender>

                                        <asp:Panel ID="pnlPermissionPopup" runat="server" CssClass="modal-dialog modal-dialog-scrollable top5" Style="display: none;">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <h4 style="margin-top: 7px !important;">صلاحيات المستخدمين</h4>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="pull-right">
                                                                <asp:LinkButton SkinID="btn-close" runat="server" ID="btnClose"><i class="ti-close icon-close"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-body">
                                                    <div class="table-responsive tableStyle check-head">
                                                        <asp:GridView ID="gvUserPermissionPopup" CssClass="table" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" Style="opacity: 10;">
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
                                                                لا توجد بيانات
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
                                </asp:Panel>
                            </div>

                            <asp:Panel ID="pnlConfirm" CssClass="col-md-12 p0 mb10" runat="server" Visible="false">
                                <ul class="btn-uls pull-right">
                                    <li class="btn-lis">
                                        <span style="position: relative;">
                                            <i class="fa-check fa icon-save"></i>
                                            <asp:Button ID="btnSave" runat="server" SkinID="btn-save" ValidationGroup="Main" OnClick="Save" UseSubmitBehavior="false" OnClientClick="SaveClick(this,'Main');" Text="حفظ" ToolTip="حفظ" />
                                        </span>
                                    </li>
                                    <li class="btn-lis">
                                        <asp:Panel runat="server" ID="pnlCancel">
                                            <a href="#" title="Cancel" class="btn-main btn-red" data-toggle="modal" data-placement="bottom" data-original-title="Cancel"
                                                onclick="ShowConfirmPopup('mpConfirmCancel','pnlConfirmExtenderCancel');return false;">Cancel<i class="ti-close"></i></a>
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

                        <asp:Panel ID="pnlForm" Visible="false" runat="server">
                            <div class="container-fluid">
                                <asp:ValidationSummary ID="ValidationSummary" CssClass="dis-none" ClientIDMode="Static" DisplayMode="BulletList" ValidationGroup="Main" EnableClientScript="true" runat="server" Font-Size="Medium" ForeColor="#CC0000" />

                                <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">

                                    <div class="row">
                                        <div class="user-flex-panel">
                                            <div class="left-700 l-div">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading" role="tab" id="headingOne">
                                                        <h4 class="panel-title">
                                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">التفاصيل</a>
                                                        </h4>
                                                    </div>

                                                    <div id="collapseOne" class="panel-collapse collapse in">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">

                                                                    <div class="col-md-4 mb5">
                                                                        <label class="required">الإسم</label>
                                                                        <asp:TextBox ID="txtFullName" runat="server" MaxLength="50" onkeypress="return isString(event);" onkeyup="ValidateChars(this);"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Main" ControlToValidate="txtFullName"
                                                                            ErrorMessage=" أدخل الإسم " Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-4 mb5">
                                                                        <label class="required">البريد الإلكتروني</label>
                                                                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="200" onkeypress="return isString(event);" onkeyup="ValidateChars(this);"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ValidationGroup="Main" ControlToValidate="txtEmail"
                                                                            ErrorMessage=" أدخل البريد الإلكتورني " Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:CustomValidator ID="cvEmail" runat="server" ValidationGroup="Main" Display="Dynamic" ControlToValidate="txtEmail" ErrorMessage="Email already exist" EnableViewState="false" ValidateEmptyText="true"
                                                                            EnableClientScript="true" Enabled="true" ClientValidationFunction="CheckEmail"></asp:CustomValidator>
                                                                        <asp:RegularExpressionValidator ID="revEmail" ValidationGroup="Main" runat="server" ControlToValidate="txtEmail"
                                                                            ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                    </div>
                                                                    <div class="col-md-4 mb5">
                                                                        <span id="display" style="color: red; font-size: 10px;"></span>
                                                                        <label class="required">إسم المستخدم</label>
                                                                        <asp:TextBox ID="txtUsername" runat="server" MaxLength="20" onkeypress="return isString(event);" onkeyup="ValidateChars(this);"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ValidationGroup="Main" ControlToValidate="txtUsername"
                                                                            ErrorMessage=" أدخل إسم المستخدم " Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:CustomValidator ID="cvUserName" runat="server" ValidationGroup="Main" Display="Dynamic" ControlToValidate="txtUsername" ErrorMessage="Username already exist" EnableViewState="false" ValidateEmptyText="true"
                                                                            EnableClientScript="true" Enabled="true" ClientValidationFunction="CheckUserName"></asp:CustomValidator>
                                                                        <asp:RegularExpressionValidator ID="valtxtUsername" runat="server"
                                                                            ControlToValidate="txtUsername"
                                                                            ErrorMessage="رجاء عدم إدخل أقل من خمسة أحروف"
                                                                            ValidationExpression=".{5}.*" />
                                                                    </div>
                                                                    <div class="col-md-4 mb5">
                                                                        <label class="required">كلمة المرور</label>
                                                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="15" ClientIDMode="Static" onchange="ValidatePass();" autocomplete="new-password"></asp:TextBox>
                                                                        <asp:Label ID="lblPassStatus" runat="server" ClientIDMode="Static"></asp:Label>
                                                                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ValidationGroup="Main" ControlToValidate="txtPassword"
                                                                            ErrorMessage=" أدخل كلمة المرور " Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="Main" CssClass="inp-validate" runat="server" ControlToValidate="txtPassword"
                                                                                ErrorMessage="Password must contain: Minimum 6 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet" ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z\d]{6,}$">*</asp:RegularExpressionValidator>--%>
                                                                        <%-- ^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$  --%>
                                                                        <%--<asp:CustomValidator ID="cfvPassword" CssClass="custmValidator" runat="server" ValidationGroup="Main" Display="Dynamic" ControlToValidate="txtPassword" ErrorMessage="Password Very Week" EnableViewState="false" ValidateEmptyText="true"
                                                                                    EnableClientScript="true" Enabled="true" ClientValidationFunction="ValidatePassword" ForeColor="Red">*</asp:CustomValidator>--%>
                                                                    </div>
                                                                    <div class="col-md-4 mb5">
                                                                        <label class="required">تأكيد كلمة المرور</label>
                                                                        <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" MaxLength="15" autocomplete="new-password"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ValidationGroup="Main" ControlToValidate="txtPasswordConfirm"
                                                                            ErrorMessage="أدخل تاكيد كلمة المرور" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword" ValidationGroup="Main" ForeColor="Red"
                                                                            ControlToValidate="txtPasswordConfirm" ErrorMessage="كلمة المرور غير متطابقة" Text="كلمة المرور غير متطابقة" Style="position: absolute; font-size: 10px;"></asp:CompareValidator>
                                                                    </div>

                                                                    <div class="col-md-4 mb5">
                                                                        <label>مجموعات المستخدمين</label>
                                                                        <div class="clear"></div>
                                                                        <asp:DropDownList ID="ddlUserGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FillPermissions">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-4 mb5">
                                                                        <label class="mb10">تفعيل</label>
                                                                        <div class="cust-rad-btns">
                                                                            <div class="c-check">
                                                                                <asp:CheckBox ID="chkUserActivation" runat="server" Checked="true" Text=" تفعيل " />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4 mb5">
                                                                        <label>نوع المستخدم</label>
                                                                        <div class="clear"></div>
                                                                        <asp:DropDownList ID="ddlUserType" runat="server">
                                                                            <asp:ListItem Value="User">User</asp:ListItem>
                                                                            <asp:ListItem Value="Admin">Admin</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-4 mb5">
                                                                        <label>الهاتف الجوال</label>
                                                                        <asp:TextBox ID="txtMobile" runat="server" MaxLength="15" onkeypress="return isNumber(event);"></asp:TextBox>
                                                                    </div>


                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="right-300 r-217">
                                                <div class="demo-upload-head m0">
                                                    <h3>تحميل الصورة</h3>
                                                </div>
                                                <div class="update-progress-img upload-img">
                                                    <asp:Image ID="imgIconLoader" runat="server" ClientIDMode="Static" ImageUrl="~/images/image-uploader.gif" Style="display: none; width: 100%" />
                                                </div>
                                                <div class="demo-upload-container pb5">
                                                    <div class="custom-file-container" data-upload-id="myFirstImage">
                                                        <div class="custom-file-container__image-preview h-img-pre" title="Logo" runat="server" id="previewDiv" style="text-align: center;">
                                                            <asp:Image ID="imgIcon" ClientIDMode="Static" Style="max-height: 100%; max-width: 100%" runat="server" ImageUrl="~/images/user.jpg" />
                                                        </div>

                                                        <asp:TextBox ID="txtHiddenPassword" runat="server" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                                        <asp:TextBox ID="HiddenIcon" runat="server" ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                                        <asp:Panel ID="pnlfuLogo" runat="server">
                                                            <label class="custom-file-container__custom-file">
                                                                <%--<input type="file" class="custom-file-container__custom-file__custom-file-input" title="Logo" accept="image/*"  runat="server" id="Uploader1" />--%>

                                                                <asp:AsyncFileUpload ID="fuPhoto" ClientIDMode="Static" runat="server" CssClass="inputfile inputfile-1" OnUploadedComplete="PhotoUploaded"
                                                                    OnClientUploadComplete="UploadPhotoCompleted" OnClientUploadError="UploadError" OnClientUploadStarted="UploadStarted" FailedValidation="False" />


                                                                <input type="hidden" name="MAX_FILE_SIZE" value="10485760" />
                                                                <span class="custom-file-container__custom-file__custom-file-control" title="Logo"></span>
                                                            </label>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12 p-lr-10">
                                        <asp:Panel ID="pnlUserDt" runat="server">
                                            <div class="row">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading" role="tab" id="headingTwo">
                                                        <h4 class="panel-title">
                                                            <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">User Permissions</a>
                                                        </h4>
                                                    </div>

                                                    <div id="collapseTwo" class="panel-collapse collapse in">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-12">
                                                                        <div class="table-responsive">
                                                                            <asp:GridView ID="gvUserPermissions" CssClass="table" ClientIDMode="Static" runat="server" AutoGenerateColumns="False">
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
                                                                                    لا توجد بيانات
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </div>
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
                        </asp:Panel>
                    </div>

                </div>

                <script src="JSCode/CPUsers.js"></script>
                <div style="display: none;">
                    <asp:Label ID="lblUserId" runat="server" ClientIDMode="Static"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>

</body>
</html>


