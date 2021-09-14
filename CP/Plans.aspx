<%@ Page Theme="Theme1" Language="VB" AutoEventWireup="false" CodeFile="Plans.aspx.vb" Inherits="Projects" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>الخطط</title>
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
    <script src="JSCode/UplloadPlanLogo.js"></script>
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
                        <h4>الخطط</h4>
                    </div>
                    <div class="col-sm-6 col-xs-7">
                        <ol class="breadcrumb">
                            <li><a href="Dashboards.aspx"><i class="ti-home"></i></a></li>
                            <li>الخطط</li>
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
                                                        <asp:TextBox ID="txtSearch" runat="server" type="text" placeholder="بحث بالإسم " AutoPostBack="true" OnTextChanged="FillGrid" onkeypress="return isString(event);" onkeyup="ValidateChars(this);ShowHideClearSearch(this.value);" ToolTip="بحث بالإسم "></asp:TextBox>

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

                            <asp:Panel ID="pnlList" runat="server">
                                <div class="col-md-12">
                                    <div class="table-layer" style="box-shadow: 0px 1px 15px 1px rgba(69,65,78,0.08);">
                                        <div class="table-responsive mt5">
                                            <asp:HiddenField ID="SortExpression" runat="server" />
                                            <asp:ListView ID="lvProjects" runat="server" ClientIDMode="AutoID"
                                                OnPagePropertiesChanging="OnPagePropertiesChanging" OnSorting="lv_Sorting">
                                                <LayoutTemplate>
                                                    <table id="itemPlaceholderContainer" runat="server" class="table tbl-table">
                                                        <tr class="HeaderStyle">
                                                            <th>م</th>
                                                            <th class="upnDownArrow" id="_WorkArea">
                                                                <asp:LinkButton ID="lbName" CommandArgument="_WorkArea" CommandName="Sort" runat="server">نطاق العمل</asp:LinkButton>
                                                            </th>
                                                             <th>الشعار</th>
                                                            <th class="upnDownArrow" id="PlanType">
                                                                <asp:LinkButton ID="lbType" CommandArgument="PlanType" CommandName="Sort" runat="server">النوع</asp:LinkButton>
                                                            </th>
                                                           
                                                            <th class="upnDownArrow" id="ActiveHeader">
                                                                <asp:LinkButton ID="lbActve" CommandArgument="Active" CommandName="Sort" runat="server">تفعيل</asp:LinkButton>
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
                                                            <asp:Label ID="lblPlanID" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbName" runat="server" Text='<%# Eval("_WorkArea").ToString %>'></asp:Label>
                                                        </td>
                                                         <td>
                                                            <img runat="server" class="td-img img-thumbnail" alt='<%# Eval("_WorkArea")%>' src='<%# IIf(Eval("Photo").ToString <> String.Empty, Eval("Photo"), "~/CP/images/noimage.jpg")%>'  onclick="ImagePreview(this.src,this.alt)" />
                                                         </td>
                                                        <td>
                                                            <asp:Label runat="server" Text='<%# Eval("_PlanType").ToString %>'></asp:Label>
                                                        </td>
                                                       
                                                        <td id="Active">
                                                            <asp:CheckBox runat="server" ID="chkActive" Checked='<%# Eval("Active").ToString %>' AutoPostBack="true" OnCheckedChanged="UpdateActive" />
                                                        </td>
                                                        <td id="Edit">
                                                            <asp:LinkButton ID="lbUpdate" CssClass="btni-xxxs btn-blue brd-50" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="Edit" ToolTip="Edit">
                                                                    <i class="fa-edit fa"></i>
                                                            </asp:LinkButton>
                                                            <%--<asp:LinkButton runat="server" ID="lbEdit" CauseValidation="false" CssClass="btni-xxxs btn-blue brd-50" data-placement="bottom" data-original-title="Edit" CommandArgument='<%# Eval("Id") %>' OnClick="Edit" data-toggle="modal"><i class="fa fa-edit"></i></asp:LinkButton>--%>
                                                        </td>
                                                        <td id="Delete">
                                                            <asp:Panel runat="server" ID="pnlDelete">
                                                                <a class="btni-xxxs btn-red brd-50" href="#" title="Delete" data-toggle="modal" data-placement="bottom" data-original-title="Delete"
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
                                                                    <label>تأكيد حذف السجل ?</label>
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
                                                                <div>لا توجد خطط</div>
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
                                                <p class="mb0 table-counts" style ="display:none ">
                                                    Total Plans : <span>
                                                        <asp:Label ID="lblTotalCount" runat="server"></asp:Label></span>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="tbl-bot-panel-right">
                                            <div class="row">
                                                <ul class="pagination">
                                                    <li>
                                                        <asp:DataPager ID="dplvProjects" class="pagination" runat="server" PagedControlID="lvProjects" PageSize='<%# ddlPager.SelectedValue %>' style="width: 100%; display: inline-flex;">
                                                            <Fields>
                                                                <asp:NextPreviousPagerField ButtonType="Link"
                                                                    ShowFirstPageButton="true" FirstPageText="<i class='ti-angle-double-left'></i>"
                                                                    ShowPreviousPageButton="true" PreviousPageText="<i class='ti-angle-left'></i>"
                                                                    ShowLastPageButton="false" ShowNextPageButton="false" />

                                                                <asp:NumericPagerField ButtonType="link" RenderNonBreakingSpacesBetweenControls="false" NextPreviousButtonCssClass="hidedots" />

                                                                <asp:NextPreviousPagerField ButtonType="Link"
                                                                    ShowNextPageButton="true" NextPageText="<i class='ti-angle-right'></i>"
                                                                    ShowLastPageButton="true" LastPageText="<i class='ti-angle-double-right'></i>"
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

                        </div>

                        <asp:Panel ID="pnlConfirm" CssClass="col-md-12 p0 mb10" runat="server" Visible="false">
                            <ul class="btn-uls pull-right">
                                <li class="btn-lis">
                                    <span style="position: relative;">
                                        <i class="fa-check fa icon-save"></i>
                                        <asp:Button ID="cmdSave" SkinID="btn-save" ValidationGroup="vProjects" OnClick="Save" runat="server" Text="حفظ" ToolTip="حفظ" />
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

                    <asp:Panel ID="pnlForm" runat="server" Style="display: none;">
                        <div class="container-fluid">
                            <div class="row">
                                <asp:ValidationSummary ID="ValidationSummary" CssClass="validation-message" DisplayMode="BulletList" ValidationGroup="vProjects" EnableClientScript="true" runat="server" Font-Size="Medium" ForeColor="#CC0000" />
                                <asp:Label ID="lblPlanID" runat="server" Visible="false"></asp:Label>

                                <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">
                                    <div class="user-flex-panel">
                                        <div class="left-700">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="heading1">
                                                    <h4 class="panel-title">
                                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse1"> خطة نطاق العمل</a>
                                                    </h4>
                                                </div>

                                                <div id="collapse1" class="panel-collapse collapse in">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6">
                                                                    <label>نطاق عمل الخطة</label>
                                                                   <asp:DropDownList ID="ddlWorkArea" runat="server">
                                                                        <asp:ListItem Value="0" Text="--اختر--"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="displaynone"
                                                                        ValidationGroup="vProjects" ControlToValidate="ddlWorkArea" InitialValue="0" ErrorMessage="اختر نطاق العمل"></asp:RequiredFieldValidator>
                                                                </div>
                                                              
                                                                <div class="col-md-6">
                                                                    <label>نوع الخطة</label>
                                                                    <asp:DropDownList ID="ddlType" runat="server">
                                                                        <asp:ListItem Value="0" Text="--اختر--"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="displaynone"
                                                                        ValidationGroup="vProjects" ControlToValidate="ddlType" InitialValue="0" ErrorMessage="اختر الخطة"></asp:RequiredFieldValidator>
                                                           
                                                                </div>
                                                                  <div class="col-md-6">
                                                                    <label>الوصف</label>
                                                                    <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                               
                                                                 
                                                            </div>
                                                        </div>
                                                         
                                                         
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                       <div class="right-300 pos-relavtive">
                                                <div class="demo-upload-head m0">
                                                    <h3>الشعار</h3>
                                                </div>
                                                <div class="update-progress-img">
                                                    <asp:Image ID="imgLoader" runat="server" ClientIDMode="Static" ImageUrl="~/images/image-uploader.gif" Style="display: none;" />
                                                </div>
                                                <fieldset>

                                                    <asp:Panel ID="pnlTLCopy" runat="server" CssClass="demo-upload-container pb5">
                                                        <div class="custom-file-container">

                                                            <asp:HyperLink ID="hlViewSection" CssClass="custom-file-container__image-preview h-img-pre" runat="server" ClientIDMode="Static" Target="_blank">
                                                                <asp:Image ID="imgSection" ClientIDMode="Static" runat="server" Style="max-height: 100%; max-width: 100%" ImageUrl="~/images/img-up.png" />
                                                            </asp:HyperLink>
                                                            <asp:TextBox ID="HiddenWorkAreaImg" runat="server" ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="displaynone"
                                                            ValidationGroup="vProjects" ControlToValidate="HiddenWorkAreaImg" ErrorMessage="تحميل صورة الشعار"></asp:RequiredFieldValidator>--%>

                                                            <asp:Panel ID="pnlfuPhoto" runat="server" CssClass="photo-upload-box_inactive">
                                                                <label class="custom-file-container__custom-file">
                                                                    <asp:AsyncFileUpload ID="fuPhoto" CssClass="inputfile inputfile-1" runat="server" OnUploadedComplete="SectionPhotoUploaded"
                                                                        OnClientUploadComplete="UploadComplete" OnClientUploadError="UploadError" OnClientUploadStarted="UploadStarted"
                                                                        FailedValidation="False" />

                                                                    <input type="hidden" name="MAX_FILE_SIZE" value="10485760" />
                                                                    <span class="custom-file-container__custom-file__custom-file-control" title="Logo"></span>
                                                                </label>
                                                            </asp:Panel>
                                                        </div>

                                                    </asp:Panel>
                                                </fieldset>
                                            </div>
                                    </div>
                                </div>


                            </div>

                            <div class="row">
                                <div class="mb0" id="accordionFiles" role="tablist" aria-multiselectable="false">
                                    <div>
                                        <div>
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab">
                                                    <h4 class="panel-title">
                                                        <a role="button" data-toggle="collapse" data-parent="#accordionFiles" href="#collapseFiles">ملفات الخطة</a>
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
                                                                         <asp:HiddenField ID="FolderName" runat ="server" Value ="PlanFiles/" />
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
                <%--<script src="JSCode/Section.js"></script>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script src="JSCode/jsImagePreview.js"></script>
    <script src="JSCode/jsWorkArea.js"></script>
    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAsUO5F3TK85dMEdldKHne0nulD-6YsY-g&callback=PointOfInterestMap"></script>

</body>
</html>
