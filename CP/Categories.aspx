﻿<%@ Page Theme="Theme1" Language="VB" AutoEventWireup="false" CodeFile="Categories.aspx.vb" Inherits="Categories" %>

<%@ Register Src="UserControls/HTMLEditor.ascx" TagPrefix="uc1" TagName="HTMLEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>تصنيف التحليللات</title>
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
    <script src="JSCode/UploadPhotoContent.js"></script>

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
                        <h4>تصنيف التحليللات</h4>
                    </div>
                    <div class="col-sm-6 col-xs-7">
                        <ol class="breadcrumb">
                            <li><a href="../Home.aspx" target="_blank"><i class="ti-home"></i></a></li>
                            <li>تصنيف التحليللات</li>
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
                                                <asp:Panel ID="pgPanel" CssClass="d-flex" runat="server">

                                                    <div class="input-in">
                                                        <div class="input-group">
                                                            <asp:DropDownList runat="server" CssClass="form-control ltr" ID="ddlPager" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed">
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="input-group-addon" id="basic-addon1">سجلات / الصفحة</span>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <div class="tbl-top-panel-right">
                                            <div class="row">
                                                <div class="input-280 input-in searchContiner pull-right">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtSearch" runat="server" type="text" placeholder="بحث باسم التصنيف " AutoPostBack="true" OnTextChanged="FillGrid" onkeypress="return isString(event);" onkeyup="ValidateChars(this);ShowHideClearSearch(this.value);" ToolTip="بحث بالعنوان "></asp:TextBox>

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
                                <asp:Label ID="lblDateContent" runat="server" Style="display: none"></asp:Label>
                            </div>

                            <asp:Panel ID="pnlGV" runat="server">
                                <div class="col-md-12">
                                    <div class="table-layer" style="box-shadow: 0px 1px 15px 1px rgba(69,65,78,0.08);">
                                        <div class="pages-table">
                                            <asp:ListView ID="lvContent" runat="server" ClientIDMode="AutoID"
                                                OnPagePropertiesChanging="OnPagePropertiesChanging" OnSorting="lv_Sorting">
                                                <LayoutTemplate>
                                                    <table id="itemPlaceholderContainer" runat="server" class="table tbl-table">
                                                        <tr class="HeaderStyle">
                                                            <th>م</th>

                                                            <th class="upnDownArrow" id="Name">
                                                                <asp:LinkButton ID="lbCategory" CommandArgument="Category" CommandName="Sort" runat="server">التصنيف</asp:LinkButton>
                                                            </th>

                                                            <th class="upnDownArrow" id="ShowOrder">
                                                                <asp:LinkButton ID="lbShowOrder" CommandArgument="ShowOrder" CommandName="Sort" runat="server">الترتيب</asp:LinkButton>
                                                            </th>
                                                            <th class="upnDownArrow" id="Active">
                                                                <asp:LinkButton ID="lbActive" CommandArgument="Active" CommandName="Sort" runat="server">تفعيل</asp:LinkButton>
                                                            </th>
                                                            <th class="upnDownArrow" id="ShowInHome">
                                                                <asp:LinkButton ID="lbShowInHome" CommandArgument="ShowInHome" CommandName="Sort" runat="server">عرض الرئيسية</asp:LinkButton>
                                                            </th>
                                                            <th id="EditHeader">تعديل</th>
                                                            <th id="DeleteHeader">حذف</th>

                                                        </tr>
                                                        <tr id="itemPlaceholder">
                                                        </tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="lvItemRow" runat="server">

                                                        <td>
                                                            <asp:Label ID="srialNo" runat="server" Text='<%# Val(Container.DataItemIndex.ToString) + 1 %>'></asp:Label>
                                                            <asp:Label ID="lblContentId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label runat="server" Text='<%# Eval("ShowOrder") %>'></asp:Label>
                                                        </td>
                                                        <td id="Active">
                                                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# PublicFunctions.BoolFormat(Eval("Active"))%>' AutoPostBack="true" OnCheckedChanged="UpdateActive"></asp:CheckBox>
                                                        </td>
                                                        <td id="Home">
                                                            <asp:CheckBox ID="chkHome" runat="server" Checked='<%# PublicFunctions.BoolFormat(Eval("ShowInHome"))%>' AutoPostBack="true" OnCheckedChanged="UpdateShowHome"></asp:CheckBox>
                                                        </td>
                                                        <td id="Edit">
                                                            <asp:LinkButton ID="lbUpdate" CssClass="btni-xxxs btn-blue brd-50" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="Edit" ToolTip="تحديث">
                                                                    <i class="fa-edit fa"></i>
                                                            </asp:LinkButton>
                                                            <%--<asp:LinkButton runat="server" ID="lbEdit" CauseValidation="false" CssClass="btni-xxxs btn-blue brd-50" data-placement="bottom" data-original-title="Edit" CommandArgument='<%# Eval("Id") %>' OnClick="Edit" data-toggle="modal"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                        </td>
                                                        <td id="Delete">
                                                            <asp:Panel runat="server" ID="pnlDelete">
                                                                <a class="btni-xxxs btn-red brd-50" href="#" title="حذف" data-toggle="modal" data-placement="bottom" data-original-title="حذف"
                                                                    onclick="ShowConfirmPopup('<%# CType(Container, ListViewItem).FindControl("mpConfirmDelete").ClientID.ToString%>','<%# CType(Container, ListViewItem).FindControl("pnlConfirmExtenderDelete").ClientID.ToString%>');return false;">
                                                                    <i class="fa fa-trash"></i></a>
                                                            </asp:Panel>
                                                            <asp:HiddenField ID="hfDelete" runat="server" />
                                                            <asp:ModalPopupExtender ID="mpConfirmDelete" runat="server" PopupControlID="pnlConfirmExtenderDelete" TargetControlID="hfDelete"
                                                                CancelControlID="lbNoDelete" BackgroundCssClass="modalBackground">
                                                            </asp:ModalPopupExtender>
                                                            <asp:Panel ID="pnlConfirmExtenderDelete" runat="server" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                                <div class="header">
                                                                    رسالة تأكيد
                                                                </div>
                                                                <div class="body">
                                                                    <label>هل تريد الحذف ؟</label>
                                                                </div>

                                                                <div class="footer">
                                                                    <ul class="btn-uls mb0">
                                                                        <li class="btn-lis">
                                                                            <asp:LinkButton ID="lbYesDelete" runat="server" SkinID="btn-green" CommandArgument='<%# Eval("Id") %>' OnClick="Delete" CausesValidation="false">نعم<i class="ti-check"></i></asp:LinkButton></li>
                                                                        <li class="btn-lis">
                                                                            <asp:LinkButton ID="lbNoDelete" runat="server" SkinID="btn-red" OnClientClick="CloseConfirmPopup('mpConfirmDelete');return false;">لا<i class="ti-close"></i></asp:LinkButton></li>
                                                                    </ul>
                                                                </div>
                                                            </asp:Panel>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                                <EmptyDataTemplate>
                                                    <table style="width: 100%;">
                                                        <tr class="EmptyRowStyle">
                                                            <td>
                                                                <div>لا توجد تصنيفات</div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>

                                    <div class="table-bot-panel">
                                        <div class="tbl-bot-panel-left">
                                            <div class="row">
                                                <p class="mb0 table-counts" style="display: none">
                                                    Total News : <span>
                                                        <asp:Label ID="lblTotalCount" runat="server"></asp:Label></span>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="tbl-bot-panel-right">
                                            <div class="row">
                                                <ul class="pagination">
                                                    <li>
                                                        <asp:DataPager ID="dplvContent" class="pagination" runat="server" PagedControlID="lvContent" PageSize='<%# ddlPager.SelectedValue %>' style="width: 100%; display: inline-flex;">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonType="Link"
                                                                    ShowFirstPageButton="true" FirstPageText="<i class='ti-angle-double-right'></i>"
                                                                    ShowPreviousPageButton="true" PreviousPageText="<i class='ti-angle-right'></i>"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />

                                                                <asp:NumericPagerField ButtonType="link" RenderNonBreakingSpacesBetweenControls="false" NextPreviousButtonCssClass="hidedots" />

                                                                <asp:NextPreviousPagerField ButtonType="Link"
                                                                    ShowNextPageButton="true" NextPageText="<i class='ti-angle-left'></i>"
                                                                    ShowLastPageButton="true" LastPageText="<i class='ti-angle-double-left'></i>"
                                                                    ShowFirstPageButton="false"
                                                                    ShowPreviousPageButton="false" />
                                                            </Fields>
                                                        </asp:DataPager>
                                                    </li>
                                                </ul>
                                            </div>
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
                                        <asp:Button ID="cmdSave" SkinID="btn-save" ValidationGroup="vContent" OnClick="Save" runat="server" Text="حفظ" ToolTip="حفظ" />
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
                                <asp:ValidationSummary ID="ValidationSummary" CssClass="validation-message" DisplayMode="BulletList" ValidationGroup="vContent" EnableClientScript="true" runat="server" Font-Size="Medium" ForeColor="#CC0000" />
                                <asp:Label ID="lblContentId" runat="server" Visible="false"></asp:Label>

                                <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">
                                    <div class="left-700">
                                        <div class="panel panel-default">
                                            <div class="panel-heading" role="tab" id="heading1">
                                                <h4 class="panel-title">
                                                    <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse1">بيانات التصنيف</a>
                                                </h4>
                                            </div>

                                            <div id="collapse1" class="panel-collapse collapse in">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12">

                                                            <div class="col-md-4 form-group" id="divNewCategory" runat="server">
                                                                <label class="input-label required">التصنيف</label>
                                                                <asp:TextBox runat="server" ID="txtCategory" MaxLength="200"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="displaynone"
                                                                    ValidationGroup="vContent" ControlToValidate="txtCategory" ErrorMessage="أدخل التصنيف"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="col-md-2 form-group">
                                                                <label class="input-label required">الترتيب</label>
                                                                <asp:TextBox runat="server" ID="txtOrderNo" MaxLength="6" onkeypress="return isNumber(event);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="displaynone"
                                                                    ValidationGroup="vContent" ControlToValidate="txtOrderNo" ErrorMessage="أدخل ترتيب المؤشر"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label class="input-label">&nbsp;</label>
                                                                <div class="c-check">
                                                                    <asp:CheckBox runat="server" ID="chkActive" ToolTip="تفعيل" Text="تفعيل"></asp:CheckBox>
                                                                </div>
                                                            </div>
                                                           <%-- <div class="col-md-12 form-group">
                                                                <label class="input-label">الوصف</label>
                                                                <uc1:HTMLEditor ID="txtDescription" runat="server" />
                                                            </div>--%>
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

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
