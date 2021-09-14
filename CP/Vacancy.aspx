<%@ Page Theme="Theme1" Language="VB" AutoEventWireup="false" CodeFile="Vacancy.aspx.vb" Inherits="Vacancy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>فرص العمل</title>
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
    <script src="JSCode/UploadPhotoVacancy.js"></script>

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
                        <h4>فرص العمل</h4>
                    </div>
                    <div class="col-sm-6 col-xs-7">
                        <ol class="breadcrumb">
                            <li><a href="Dashboards.aspx"><i class="ti-home"></i></a></li>
                            <li>فرص العمل</li>
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
                                            <asp:GridView ID="gvVacancy" CssClass="table Pagination-table" runat="server" AutoGenerateColumns="false" AllowSorting="true" OnPageIndexChanging="gvVacancy_PageIndexChanging"
                                                AllowPaging="true" PageSize='<%# ddlPager.SelectedValue  %>' OnSorting="gv_Sorting">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="الوظيفة" SortExpression="Name" HeaderStyle-CssClass="upnDownArrow">
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
                                                    <span class="glyphicon glyphicon-exclamation-sign"></span>&nbsp لا توجد فرص عمل
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
                                        <asp:Button ID="cmdSave" SkinID="btn-save" ValidationGroup="vVacancy" OnClick="Save" runat="server" Text="حفظ" ToolTip="حفظ" />
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
                                    <asp:ValidationSummary ID="ValidationSummary" CssClass="validation-message" DisplayMode="BulletList" ValidationGroup="vVacancy" EnableClientScript="true" runat="server" Font-Size="Medium" ForeColor="#CC0000" />
                                    <asp:Label ID="lblVacancyId" runat="server" Visible="false"></asp:Label>
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
                                                                <label class="required">إسم الوظيفة</label>
                                                                <asp:TextBox runat="server" ID="txtTitle" MaxLength="100"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="displaynone"
                                                                    ValidationGroup="vVacancy" ControlToValidate="txtTitle" ErrorMessage="أدخل إسم الوظيفة"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label>تاريخ التقديم</label>
                                                                <asp:TextBox ID="txtIssueDate" runat="server" MaxLength="10" onkeypress="return isDate(event);"></asp:TextBox>
                                                                <asp:CalendarExtender CssClass="custom-calendar" ID="CalendarExtender1" runat="server"
                                                                    Enabled="True" TargetControlID="txtIssueDate" DaysModeTitleFormat="dd/MM/yyyy"
                                                                    TodaysDateFormat="dd/MM/yyyy" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>

                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="vVacancy" runat="server"
                                                                    ErrorMessage="خطأ في تاريخ التقديم" ControlToValidate="txtIssueDate" Display="Dynamic"
                                                                    ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                                    CssClass="in-validate" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <label>تاريخ الإنتهاء</label>
                                                                <asp:TextBox ID="txtExpiryDate" runat="server" MaxLength="10" onkeypress="return isDate(event);"></asp:TextBox>
                                                                <asp:CalendarExtender CssClass="custom-calendar" ID="CalendarExtender2" runat="server"
                                                                    Enabled="True" TargetControlID="txtExpiryDate" DaysModeTitleFormat="dd/MM/yyyy"
                                                                    TodaysDateFormat="dd/MM/yyyy" Format="dd/MM/yyyy">
                                                                </asp:CalendarExtender>

                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="vVacancy" runat="server"
                                                                    ErrorMessage="خطأ في تاريخ الإنتهاء" ControlToValidate="txtExpiryDate" Display="Dynamic"
                                                                    ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                                    CssClass="in-validate" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                                <asp:CompareValidator ValidationGroup="vVacancy" ErrorMessage="تاريخ الإنتهاء يجب ان يكون اكبر من تاريخ التقديم" CssClass="res-label-info" Style="display: inline;"
                                                                    ControlToValidate="txtExpiryDate" ControlToCompare="txtIssueDate" Display="Dynamic" Type="Date" Operator="GreaterThanEqual" runat="server" />
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label class="required">الترتيب</label>
                                                                <asp:TextBox runat="server" ID="txtOrderNo" MaxLength="2" onkeypress="return isNumber(event);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="displaynone"
                                                                    ValidationGroup="vVacancy" ControlToValidate="txtOrderNo" ErrorMessage="أدخل ترتيب المؤشر"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <label>الوصف</label>
                                                                <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                                            </div>
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
