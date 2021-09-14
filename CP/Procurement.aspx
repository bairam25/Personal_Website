<%@ Page Theme="Theme1" Language="VB" AutoEventWireup="false" CodeFile="Procurement.aspx.vb" Inherits="Procurement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>المناقصات</title>
    <link rel="shortcut icon" href="../images/logo/favi.png" />
    <!-- Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Resource style -->
    <link rel="stylesheet" href="css/c-scroll.css" />
    <link rel="stylesheet" href="css/themify-icons.css" />
    <link rel="stylesheet" href="css/multifile-up.css" />
    <link rel="stylesheet" href="css/cpcustom.css" />
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700,800|Poppins:300,400,500,600,700,800|Roboto:300,400,500,700,900" rel="stylesheet" />


    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js" type="text/javascript"></script>

    <script src="js/respond.js" type="text/javascript"></script>
    <script src="js/matchmedia.polyfill.js" type="text/javascript"></script>

    <script src="js/sidebar-nav.min.js" type="text/javascript"></script>
    <script src="JSCode/KeypressValidators.js"></script>
    <script src="JSCode/Popup.js"></script>
    <script src="JSCode/jsMultiFileUpload.js"></script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" autocomplete="off">
        <asp:ToolkitScriptManager ID="Toolkitscriptmanager1" runat="server" ScriptMode="Release">
            <Services>
                <asp:ServiceReference Path="~/WebService.asmx" />
                <asp:ServiceReference Path="~/wsMultiFileUpload.asmx" />
            </Services>
        </asp:ToolkitScriptManager>

        <!--============================ Page-header =============================-->
        <div class="container-fluid">
            <div class="page-header">
                <div class="row">
                    <div class="col-sm-6 col-xs-5 text-left">
                        <h4>المناقصات</h4>
                    </div>
                    <div class="col-sm-6 col-xs-7">
                        <ol class="breadcrumb">
                            <li><a href="Dashboards.aspx"><i class="ti-home"></i></a></li>
                            <li>المناقصات</li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>

                <!--============================ Page-content =============================-->
                <div id="page-wrapper" style="min-height: 684px;">
                    <div class="container-fluid">
                        <div class="uploader">
                            <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                                <ProgressTemplate>
                                    <asp:Image ID="Image7" runat="server" ImageUrl="images/ajax-loader.gif" />
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
                                                        <asp:DropDownList runat="server" CssClass="form-control ltr" ID="ddlPager" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed">
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
                                                        <asp:TextBox ID="txtSearch" runat="server" type="text" placeholder="بحث بالعنوان " AutoPostBack="true" OnTextChanged="FillGrid" onkeypress="return isString(event);" onkeyup="ValidateChars(this);ShowHideClearSearch(this.value);" ToolTip="بحث بالعنوان "></asp:TextBox>

                                                        <asp:LinkButton runat="server" SkinID="clear-search" ID="cmdClear" title="مسح" OnClientClick="$('#txtSearch').val('');">&times;</asp:LinkButton>
                                                        <span class="input-group-btn">
                                                            <asp:Button ID="btnSearch" runat="server" Style="display: none" ClientIDMode="Static" OnClick="FillGrid" />
                                                            <asp:LinkButton ID="cmdSearch" runat="server" SkinID="btn-search" type="button" OnClick="FillGrid"> <i class="fa-search fa"></i> </asp:LinkButton>
                                                        </span>
                                                    </div>
                                                    <!-- /input-group -->
                                                </div>
                                                <asp:LinkButton ID="cmdNew" SkinID="btn-new" runat="server" OnClick="Add" ToolTip="إضافة">إضافة <i class="ti-plus"></i></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="col-md-12">
                                <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                            </div>

                            <asp:Panel ID="pnlGV" runat="server">
                                <div class="col-md-12">
                                    <div class="table-layer" style="box-shadow: 0px 1px 15px 1px rgba(69,65,78,0.08);">
                                        <div class="table-responsive mt5">
                                            <asp:HiddenField ID="SortExpression" runat="server" />
                                            <asp:GridView ID="gvProcurement" CssClass="table Pagination-table" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnPageIndexChanging="gvProcurement_PageIndexChanging"
                                                AllowPaging="true" PageSize='<%# ddlPager.SelectedValue  %>' OnSorting="gv_Sorting">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="الأسم" SortExpression="Name" HeaderStyle-CssClass="upnDownArrow">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Eval("Id")%>'></asp:Label>
                                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ التقديم" SortExpression="IssueDate" HeaderStyle-CssClass="upnDownArrow">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%# PublicFunctions.DateFormat(Eval("IssueDate").ToString, "dd/MM/yyyy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ الإنتهاء" SortExpression="ExpiryDate" HeaderStyle-CssClass="upnDownArrow">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExpiryDate" runat="server" Text='<%# PublicFunctions.DateFormat(Eval("ExpiryDate").ToString, "dd/MM/yyyy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="آخر تحديث" SortExpression="ModifiedDate" HeaderStyle-CssClass="upnDownArrow">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModifiedDate" runat="server" Text='<%# Eval("ModifiedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تفعيل" SortExpression="Active" HeaderStyle-CssClass="upnDownArrow">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Active")%>' AutoPostBack="true" OnCheckedChanged="UpdateActive"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تحديث">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbEdit" CssClass="btni-xxxs btn-info brd-50" runat="server" CommandArgument='<%# Eval("ID")%>' OnClick="Edit"><i class="fa-edit fa"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="حذف">
                                                        <ItemTemplate>
                                                            <a href="#" id="hrefDelete" class="btni-xxxs btn-red brd-50" title="Delete"
                                                                onclick="ShowConfirmPopup('<%# CType(Container, GridViewRow).FindControl("mpConfirmDelete").ClientID.ToString%>','<%# CType(Container, GridViewRow).FindControl("pnlConfirmExtenderDelete").ClientID.ToString%>');return false;"><i class="fa fa-trash"></i></a>
                                                            <asp:HiddenField ID="hfDelete" runat="server" />
                                                            <asp:ModalPopupExtender ID="mpConfirmDelete" runat="server" PopupControlID="pnlConfirmExtenderDelete" TargetControlID="hfDelete"
                                                                CancelControlID="lbNoDelete" BackgroundCssClass="modalBackground">
                                                            </asp:ModalPopupExtender>
                                                            <asp:Panel ID="pnlConfirmExtenderDelete" runat="server" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                                <div class="header">
                                                                    رسالة تأكيد
                                                                </div>
                                                                <div class="body">
                                                                    <label>هل تريد حذف هذا السجل؟</label>
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
                                                    <span class="glyphicon glyphicon-exclamation-sign"></span>&nbsp لا توجد مناقصات
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnlLastupdate">
                            </asp:Panel>
                        </div>

                        <asp:Panel ID="pnlConfirm" CssClass="col-md-12 p0 mb10" runat="server" Visible="false">
                            <ul class="btn-uls pull-right">
                                <li class="btn-lis">
                                    <span style="position: relative;">
                                        <i class="fa-check fa icon-save"></i>
                                        <asp:Button ID="cmdSave" SkinID="btn-save" ValidationGroup="vProcurement" OnClick="Save" runat="server" Text="حفظ" ToolTip="حفظ" />
                                    </span>
                                </li>
                                <li class="btn-lis">
                                    <%--<asp:LinkButton ID="cmdCancel" CausesValidation="false" runat="server" OnClick="Cancel"><i class="fa-close fa"></i> Cancel</asp:LinkButton>--%>

                                    <asp:Panel runat="server" ID="pnlCancel">
                                        <a id="cmdCancel" href="#" title="Cancel" class="btn-main btn-red" data-toggle="modal" data-placement="bottom" data-original-title="Cancel"
                                            onclick="ShowConfirmPopup('mpConfirmCancel','pnlConfirmExtenderCancel');return false;">إالغاء<i class="ti-close"></i></a>
                                        <asp:HiddenField ID="hfCancel" runat="server" />
                                        <asp:ModalPopupExtender ID="mpConfirmCancel" ClientIDMode="Static" runat="server" PopupControlID="pnlConfirmExtenderCancel" TargetControlID="hfCancel"
                                            CancelControlID="lbNoCancel" BackgroundCssClass="modalBackground">
                                        </asp:ModalPopupExtender>

                                    </asp:Panel>
                                    <asp:Panel ID="pnlConfirmExtenderCancel" runat="server" ClientIDMode="Static" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                        <div class="header">
                                            رسالة تأكيد
                                        </div>
                                        <div class="body">
                                            <label>تأكيد الإلغاء ؟</label>
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

                    <asp:Panel ID="pnlForm" runat="server" Visible="false">
                        <div class="container-fluid">
                            <div class="row">


                                <div class="col-md-12 p-lr-10">
                                    <asp:ValidationSummary ID="ValidationSummary" CssClass="validation-message" DisplayMode="BulletList" ValidationGroup="vProcurement" EnableClientScript="true" runat="server" Font-Size="Medium" ForeColor="#CC0000" />
                                    <asp:Label ID="lblProcurementId" runat="server" Visible="false"></asp:Label>
                                </div>

                                <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">
                                    <div class="col-md-12">
                                        <div class="panel panel-default">
                                            <div class="panel-heading" role="tab" id="heading1">
                                                <h4 class="panel-title">
                                                    <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse1">بيانات القسم</a>
                                                </h4>
                                            </div>

                                            <div id="collapse1" class="panel-collapse collapse in">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-6">
                                                                <label class="required">نوع المناقصة</label>
                                                                <asp:RadioButtonList ID="rblProcurementType" runat="server" RepeatDirection="Horizontal"></asp:RadioButtonList>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label class="required">إسم المناقصة</label>
                                                                <asp:TextBox runat="server" ID="txtTitle" MaxLength="100"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="displaynone"
                                                                    ValidationGroup="vProcurement" ControlToValidate="txtTitle" ErrorMessage="أدخل إسم المناقصة"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label class="required">تاريخ التقديم</label>
                                                                <asp:TextBox ID="txtIssueDate" runat="server" MaxLength="10" onkeypress="return isDate(event);"></asp:TextBox>
                                                                <asp:CalendarExtender CssClass="custom-calendar" ID="CalendarExtender1" runat="server"
                                                                    Enabled="True" TargetControlID="txtIssueDate" DaysModeTitleFormat="dd/MM/yyyy"
                                                                    TodaysDateFormat="dd/MM/yyyy" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="displaynone"
                                                                    ValidationGroup="vProcurement" ControlToValidate="txtIssueDate" ErrorMessage="أدخل تاريخ التقديم"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="vProcurement" runat="server"
                                                                    ErrorMessage="خطأ في تاريخ التقديم" ControlToValidate="txtIssueDate" Display="Dynamic"
                                                                    ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                                    CssClass="in-validate" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label class="required">تاريخ الإنتهاء</label>
                                                                <asp:TextBox ID="txtExpiryDate" runat="server" MaxLength="10" onkeypress="return isDate(event);"></asp:TextBox>
                                                                <asp:CalendarExtender CssClass="custom-calendar" ID="CalendarExtender2" runat="server"
                                                                    Enabled="True" TargetControlID="txtExpiryDate" DaysModeTitleFormat="dd/MM/yyyy"
                                                                    TodaysDateFormat="dd/MM/yyyy" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="displaynone"
                                                                    ValidationGroup="vProcurement" ControlToValidate="txtExpiryDate" ErrorMessage="أدخل تاريخ الإنتهاء"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="vProcurement" runat="server"
                                                                    ErrorMessage="خطأ في تاريخ الإنتهاء" ControlToValidate="txtExpiryDate" Display="Dynamic"
                                                                    ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                                    CssClass="in-validate" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                                <asp:CompareValidator ValidationGroup="vProcurement" ErrorMessage="تاريخ الإنتهاء يجب ان يكون اكبر من تاريخ التقديم" CssClass="res-label-info" Style="display: inline;"
                                                                    ControlToValidate="txtExpiryDate" ControlToCompare="txtIssueDate" Display="Dynamic" Type="Date" Operator="GreaterThanEqual" runat="server" />
                                                            </div>
                                                            <div class="col-md-9" id="divWorkArea" runat ="server" visible ="false" >
                                                                <label>منطقة العمل</label>
                                                                <asp:RadioButtonList ID="rblAreas" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblAreas_SelectedIndexChanged">
                                                                    <asp:ListItem Value="M" Text="الإقليم"></asp:ListItem>
                                                                    <asp:ListItem Value="T" Text="المحافظة"></asp:ListItem>
                                                                    <asp:ListItem Value="C" Text="المركز"></asp:ListItem>
                                                                    <asp:ListItem Value="U" Text="الوحدة المحلية"></asp:ListItem>
                                                                    <asp:ListItem Value="V" Text="القرية"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="displaynone"
                                                                    ValidationGroup="vProcurement" ControlToValidate="rblAreas" ErrorMessage="اختر التقسيم الإداري" InitialValue=""></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-8" id="divLoc" runat ="server" visible ="false">
                                                                <asp:Panel ID="pnlDDLs" runat="server" Visible="false">

                                                                    <div class="col-md-3">
                                                                        <asp:Panel ID="pnlMainDistribution" runat="server">
                                                                            <label>الإقليم الإداري</label>
                                                                            <asp:DropDownList ID="ddlMainDistribution" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMainDistribution_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0" Text="--اختر--"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="displaynone"
                                                                                ValidationGroup="vProcurement" ControlToValidate="ddlMainDistribution" ErrorMessage="اختر الإقليم الإداري" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </asp:Panel>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Panel ID="pnlGov" runat="server">
                                                                            <label>المحافظة</label>
                                                                            <asp:DropDownList ID="ddlgov" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgov_TextChanged">
                                                                                <asp:ListItem Value="0" Text="--اختر--"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="displaynone"
                                                                                ValidationGroup="vProcurement" ControlToValidate="ddlgov" ErrorMessage="اختر المحافظة" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </asp:Panel>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Panel ID="pnlCenter" runat="server">
                                                                            <label>المركز</label>
                                                                            <asp:DropDownList ID="ddlCenter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCenter_TextChanged">
                                                                                <asp:ListItem Value="0" Text="--اختر--"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="displaynone"
                                                                                ValidationGroup="vProcurement" ControlToValidate="ddlCenter" ErrorMessage="اختر المركز" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </asp:Panel>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Panel ID="pnlUnits" runat="server">
                                                                            <label>الوحدة المحلية</label>
                                                                            <asp:DropDownList ID="ddlUnits" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnits_TextChanged">
                                                                                <asp:ListItem Value="0" Text="--اختر--"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="displaynone"
                                                                                ValidationGroup="vProcurement" ControlToValidate="ddlUnits" ErrorMessage="اختر الوحدة المحلية" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </asp:Panel>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Panel ID="pnlVillage" runat="server">
                                                                            <label>القرية</label>
                                                                            <asp:DropDownList ID="ddlVillage" runat="server">
                                                                                <asp:ListItem Value="0" Text="--اختر--"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" CssClass="displaynone"
                                                                                ValidationGroup="vProcurement" ControlToValidate="ddlVillage" ErrorMessage="اختر القرية" InitialValue="0"></asp:RequiredFieldValidator>
                                                                        </asp:Panel>
                                                                    </div>

                                                                </asp:Panel>
                                                            </div>
                                                            <div class="clearfix"></div>
                                                            <div class="col-md-6">
                                                                <label>نوع العملية</label>
                                                                <asp:TextBox runat="server" ID="txtOfficeName" MaxLength="200"></asp:TextBox>

                                                            </div>

                                                            <div class="col-md-6">
                                                                <label> العملية</label>
                                                                <asp:TextBox runat="server" ID="txtDescription" MaxLength="200"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-6" id="divVacancy" runat ="server" visible ="false">
                                                                <label>مرتبط ب فرصة عمل</label>
                                                                <asp:CheckBox ID="chkIsVacancy" runat="server" />
                                                                <asp:Label ID="lblIsVacancy" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label>تم الترسية</label>
                                                                <asp:CheckBox ID="chkWin" runat="server" AutoPostBack="true" OnCheckedChanged="chkWin_CheckedChanged" />
                                                            </div>
                                                            <asp:Panel ID="pnlWinner" runat="server" Visible="false">
                                                                <div class="col-md-3">
                                                                    <label>إسم الفائز</label>
                                                                    <asp:TextBox runat="server" ID="txtWinnerName" MaxLength="200"></asp:TextBox>
                                                                </div>
                                                            </asp:Panel>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </asp:Panel>
                    <asp:Panel ID="pnlFiles" runat="server" Style="display: none">
                        <div class="row">
                            <div class="mb0" id="accordionFiles" role="tablist" aria-multiselectable="false">
                                <div>
                                    <div>
                                        <div class="panel panel-default">
                                            <div class="panel-heading" role="tab">
                                                <h4 class="panel-title">
                                                    <a role="button" data-toggle="collapse" data-parent="#accordionFiles" href="#collapseFiles">ملفات المناقصة</a>
                                                </h4>
                                            </div>

                                            <div id="collapseFiles" class="panel-collapse collapse in">
                                                <div class="panel-body">

                                                    <div class="header_area">
                                                        <asp:Panel ID="pnlUpload" runat="server" ClientIDMode="Static">
                                                            <asp:LinkButton SkinID="btn-blue" ID="lbUpload" runat="server">إضافة ملف <i class="fa fa-plus"></i></asp:LinkButton>

                                                            <asp:ModalPopupExtender ID="mdu" runat="server" BackgroundCssClass="modalBackground" TargetControlID="lbUpload"
                                                                PopupControlID="pnlFileUpload" ClientIDMode="AutoID" CancelControlID="lbClosePopUp" Enabled="True">
                                                            </asp:ModalPopupExtender>

                                                            <asp:Panel ID="pnlFileUpload" runat="server" CssClass="modalPopup-uploader" Style="width: 500px;">
                                                                <div class="modal-header">
                                                                    <asp:LinkButton ID="lbClosePopUp" runat="server" CssClass="pull-right" ToolTip="Close"> X<%--<i class="ti-close close-uploader icon-uploader"></i>--%></asp:LinkButton>
                                                                    <asp:HiddenField ID="FolderName" runat="server" Value="ProcurementPhotos/" />
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="clear"></div>
                                                                    <asp:AjaxFileUpload ID="AjaxFileUpload1" ClientIDMode="static" runat="server" OnClientUploadStart="UploadFileStart" OnClientUploadComplete="uploadFileComplete"
                                                                        MaximumNumberOfFiles="100" MaxFileSize="20480" AllowedFileTypes="jpeg,jpg,png,gif,doc,docx,pdf,xls,xlsx,ppt,pptx" />
                                                                    <div class="clear"></div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <div id="okDiv" runat="server">
                                                                        <div class="clear"></div>
                                                                        <div class="col-md-12 zero">
                                                                            <ul class="btn-uls mb0">
                                                                                <li class="btn-lis mb0 mt2">
                                                                                    <asp:LinkButton ID="lbOK" runat="server" CausesValidation="false" ClientIDMode="AutoID" OnClientClick="bindUploadedFilesLabel(); return false;" SkinID="btn-green">Ok<i class="ti-check"></i></asp:LinkButton>
                                                                                </li>
                                                                                <li id="divSubmit" class="btn-lis mb0 mt2" style="display: none">
                                                                                    <asp:LinkButton ID="lbSubmit" runat="server" CausesValidation="false" Text="Submit" OnClick="AddFiles"></asp:LinkButton>
                                                                                    <asp:HiddenField ID="lblUploadedFilesDetails" runat="server" ClientIDMode="Static" />
                                                                                </li>
                                                                            </ul>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-md-12 p0 mt20">
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gvItemsImgs" CssClass="tbl-imgs tbl-imgs-custom" runat="server"
                                                                AutoGenerateColumns="False" AllowSorting="true" OnDataBound="gvItemsImgs_DataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSerialNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Main" Visible="false">

                                                                        <ItemTemplate>
                                                                            <asp:RadioButton ID="rblSelect" runat="server" Text=' ' Checked='<%# Eval("Main")%>' OnCheckedChanged="SelectRBL" AutoPostBack="true" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الملف">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Eval("Id")%>' />
                                                                            <asp:Label ID="lblShowOrder" runat="server" Text='<%# Eval("ShowOrder")%>' Visible="false" />
                                                                            <asp:Image ID="lblImg" CssClass="td-img img-thumbnail" runat="server" ImageUrl='<%# Eval("MediaPath")%>' Width="50px" onclick="ImagePreview(this.src,this.alt)" Visible="false" />
                                                                            <a href='<%# Eval("MediaPath").ToString.Replace("~/", "../") %>' target="_blank">
                                                                                <asp:Image ID="lblMedia" CssClass="td-img img-thumbnail" runat="server" lang='<%# Eval("MediaPath").ToString.Replace("~/", "../") %>' Width="50px" /></a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ترتيب العرض" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlShowOrder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShowOrder_SelectedIndexChanged"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="العنوان">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtTitle" SkinID="txt-imgs" runat="server" MaxLength="200" placeholder="Title" Text='<%# Eval("Title")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الوصف" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="500" TextMode="MultiLine" placeholder="Description" Text='<%# Eval("Description")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="حذف">
                                                                        <ItemTemplate>
                                                                            <a href="#" class="btni-xxxs btn-red brd-50" id="hrefDeleteImg" title="Delete Img"
                                                                                onclick="ShowConfirmPopup('<%# CType(Container, GridViewRow).FindControl("mpConfirmDeleteImg").ClientID.ToString%>','<%# CType(Container, GridViewRow).FindControl("pnlConfirmExtenderDeleteImg").ClientID.ToString%>');return false;"><i class="fa fa-trash"></i></a>
                                                                            <asp:HiddenField ID="hfDeleteImg" runat="server" />
                                                                            <asp:ModalPopupExtender ID="mpConfirmDeleteImg" runat="server" PopupControlID="pnlConfirmExtenderDeleteImg" TargetControlID="hfDeleteImg"
                                                                                CancelControlID="lbNoDeleteImg" BackgroundCssClass="modalBackground">
                                                                            </asp:ModalPopupExtender>
                                                                            <asp:Panel ID="pnlConfirmExtenderDeleteImg" runat="server" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                                                <div class="header">
                                                                                    رسالة تأكيد
                                                                                </div>
                                                                                <div class="body">
                                                                                    <label>هل تريد حذف هذا الملف ?</label>
                                                                                </div>

                                                                                <div class="footer">
                                                                                    <ul class="btn-uls mb0">
                                                                                        <li class="btn-lis">
                                                                                            <asp:LinkButton ID="lbYesDeleteImg" runat="server" CssClass="btn-main btn-green" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteImg">نعم<i class="ti-check"></i></asp:LinkButton></li>
                                                                                        <%--OnClick="Delete"--%>
                                                                                        <li class="btn-lis">
                                                                                            <a id="lbNoDeleteImg" class="btn-main btn-red" onclick="CloseConfirmPopup('<%# CType(Container, GridViewRow).FindControl("mpConfirmDeleteImg").ClientID.ToString%>');return false;">لا<i class="ti-close"></i></a>
                                                                                        </li>
                                                                                    </ul>
                                                                                </div>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <span class="glyphicon glyphicon-exclamation-sign"></span>&nbsp لا يوجد ملفات
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
                        </div>
                    </asp:Panel>
                    <!-- The Modal -->
                    <div id="previewImage" class="previewImage">

                        <!-- The Close Button -->
                        <a class="Myclose" onclick='closeImgPopup();'>&times;</a>

                        <!-- Modal Content (The Image) -->
                        <img class="previewImage-content" id="img01" style="max-height: 515px;" />
                    </div>
                </div>
                <script src="JSCode/imgPreview.js"></script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
