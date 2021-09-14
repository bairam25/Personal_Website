<%@ Page Language="VB" AutoEventWireup="false" CodeFile="About.aspx.vb" Inherits="News" Theme="Theme1" %>

<%@ Register Src="UserControls/HTMLEditor.ascx" TagPrefix="uc1" TagName="HTMLEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>من نحن</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="keywords" content="Aber, toll gates, Ras Al Khaima" />
    <link rel="shortcut icon" href="../images/logo/favi.png" />
    <!-- Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Resource style -->
    <link rel="stylesheet" href="css/c-scroll.css" />
    <link rel="stylesheet" href="css/themify-icons.css" />
    <link rel="stylesheet" href="css/cpcustom.css" />
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>

    <script src="JSCode/KeypressValidators.js"></script>
    <script src="JSCode/Popup.js" type="text/javascript"></script>
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

        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <!--============================ Page-header =============================-->

                <div class="container-fluid">
                    <div class="page-header">
                        <div class="row">
                            <div class="col-sm-6 col-xs-5 text-left">
                                <h4>من نحن</h4>
                            </div>
                            <div class="col-sm-6 col-xs-7">
                                <ol class="breadcrumb">
                                    <li><i class="ti-home"></i></li>
                                    <li>من نحن</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>

                <!--============================ Page-content =============================-->
                <div class="container-fluid">
                    <div class="acp-tab-layer">
                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div class="row">
                                <div class="tab-pane active" id="edit">
                                    <div class="uploader">
                                        <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                                            <ProgressTemplate>
                                                <asp:Image class="update-progress" ID="imgLoader" ClientIDMode="Static" runat="server" ImageUrl="images/ajax-loader.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                    <div class="col-md-12 p0">
                                        <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblNewsId" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <div class="col-md-12 p0">
                                        <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">
                                            <div class="container-fluid">
                                                <asp:Panel ID="pnlConfirm" CssClass="row" runat="server">
                                                    <div class="col-md-12 p0">
                                                        <ul class="pull-right p0">
                                                            <li class="btn-lis" id="liSave" runat="server">
                                                                <span class="btn-save-wrapper" style="position: relative;">
                                                                    <i class="ti-check icon-save"></i>
                                                                    <asp:Button ID="cmdSave" ValidationGroup="vNews" runat="server" SkinID="btn-save" UseSubmitBehavior="false" OnClientClick="SaveClick(this,'');" ToolTip="حفظ" Text="حفظ" OnClick="Save"></asp:Button>
                                                                </span>
                                                            </li>

                                                            <li class="btn-lis">
                                                                <asp:Panel runat="server" ID="pnlCancel">
                                                                    <a href="#" title="إلغاء" class="btn-main btn-red" data-toggle="modal" data-placement="bottom" data-original-title="Cancel"
                                                                        onclick="ShowConfirmPopup('mpConfirmCancel','pnlConfirmExtenderCancel');return false;">إلغاء<i class="ti-close"></i></a>
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
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <asp:Panel ID="pnlPhotos" runat="server" ClientIDMode="Static" CssClass="col-md-12 p0">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" href="#collapse3">عن البرنامج
           
                                                            </a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse3" class="panel-collapse collapse in">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <label class="input-label required">عن البرنامج</label>
                                                                    <uc1:HTMLEditor ID="txtAboutProg" runat="server" />
                                                                </div>
                                                            </div>

                                                            <div class="header_area">
                                                                <asp:Panel ID="pnlUpload" runat="server" ClientIDMode="Static">
                                                                    <asp:LinkButton SkinID="btn-blue" ID="lbUpload" runat="server">إضافة صور<i class="fa fa-plus"></i></asp:LinkButton>

                                                                    <asp:ModalPopupExtender ID="mdu" runat="server" BackgroundCssClass="modalBackground" TargetControlID="lbUpload"
                                                                        PopupControlID="pnlFileUpload" ClientIDMode="AutoID" CancelControlID="lbClosePopUp" Enabled="True">
                                                                    </asp:ModalPopupExtender>

                                                                    <asp:Panel ID="pnlFileUpload" runat="server" CssClass="modalPopup-uploader" Style="width: 500px;">
                                                                        <div class="modal-header">
                                                                            <asp:LinkButton ID="lbClosePopUp" runat="server" CssClass="pull-right" ToolTip="Close"> X<%--<i class="ti-close close-uploader icon-uploader"></i>--%></asp:LinkButton>

                                                                        </div>
                                                                        <asp:HiddenField ID="FolderName" runat="server" Value="MediaCenter/About/" />
                                                                        <div class="modal-body">
                                                                            <div class="clear"></div>
                                                                            <asp:AjaxFileUpload ID="AjaxFileUpload1" ClientIDMode="static" runat="server" OnClientUploadComplete="uploadFileComplete"
                                                                                MaximumNumberOfFiles="100" AllowedFileTypes="jpeg,jpg,png,gif" />
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
                                                                    <asp:GridView ID="gvItemsImgs" CssClass="tbl-imgs tbl-imgs-custom" runat="server" AutoGenerateColumns="False" AllowSorting="true">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="م">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSerialNo" runat="server" Text='<%# Container.DataItemIndex + 1  %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="الرئيسية">

                                                                                <ItemTemplate>
                                                                                    <asp:RadioButton ID="rblSelect" runat="server" Text=' ' Checked='<%# Eval("Main")%>' OnCheckedChanged="SelectRBL" AutoPostBack="true" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="الصور">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Eval("Id")%>' />
                                                                                    <asp:Label ID="lblShowOrder" runat="server" Text='<%# Eval("ShowOrder")%>' Visible="false" />
                                                                                    <asp:Image ID="lblImg" CssClass="td-img img-thumbnail" runat="server" ImageUrl='<%# Eval("MediaPath")%>' Width="50px" onclick="ImagePreview(this.src,this.alt)" />

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="الترتيب">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlShowOrder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShowOrder_SelectedIndexChanged"></asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="العنوان">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtTitle" runat="server" MaxLength="200" placeholder="العنوان" Text='<%# Eval("Title")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="الوصف">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDescription" runat="server" MaxLength="500" TextMode="MultiLine" placeholder="الوصف" Text='<%# Eval("Description")%>'></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="حذف">
                                                                                <ItemTemplate>
                                                                                    <a href="#" class="btni-xxxs btn-red brd-50" id="hrefDeleteImg" title="حذف"
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
                                                                                            <label>هل تريد حذف الصورة ؟</label>
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
                                                                            <span class="glyphicon glyphicon-exclamation-sign"></span>&nbsp لا توجد صور
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="pnlForm">
                                                <div class="container-fluid">

                                                    <div class="row">
                                                        <div class="col-md-12 p0">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a class="accordion-toggle" data-toggle="collapse" href="#collapseOne">بيانات البرنامج</a>
                                                                    </h4>
                                                                </div>

                                                                <div id="collapseOne" class="panel-collapse collapse in">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label required">مراحل البرنامج</label>
                                                                                    <uc1:HTMLEditor ID="txtProgramSteps" runat="server" />
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="input-label required">مكونات البرنامج</label>
                                                                                    <uc1:HTMLEditor ID="txtProgramComponent" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">الهيكل التنظيمي والمؤسسي للبرنامج</label>
                                                                                    <uc1:HTMLEditor ID="txtProgramOrganisationalChart" runat="server" />
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

                                        <!-- The Modal -->
                                        <asp:HiddenField ID="hfShowImages" runat="server" />
                                        <asp:ModalPopupExtender ID="mpPopupImgs" runat="server" ClientIDMode="Static" PopupControlID="pnlPopupImgs" TargetControlID="hfShowImages"
                                            CancelControlID="lbClosePopupImages" BackgroundCssClass="modalBackground">
                                        </asp:ModalPopupExtender>
                                        <asp:Panel ID="pnlPopupImgs" runat="server" ClientIDMode="Static" CssClass="modal-dialog modal-lg top10" align="center" Style="display: none; margin: 30px auto!important;">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title dis-inline-b">الصور</h5>
                                                    <asp:LinkButton ID="lbClosePopupImages" runat="server" CssClass="close close-popup" OnClientClick="CloseConfirmPopup('mpPopupImgs');triggerLoadMainList();return false;"><span>&times;</span></asp:LinkButton>
                                                </div>
                                                <div class="modal-body body-scroll">
                                                    <asp:Label ID="lblResPopupImages" runat="server" Visible="false"></asp:Label>
                                                    <asp:ListView ID="lvImages" runat="server" ClientIDMode="AutoID">
                                                        <LayoutTemplate>
                                                            <table class="tbl-imgs" runat="server" cellspacing="0" rules="all" border="0" id="gvProductImages">
                                                                <tr class="HeaderStyle">
                                                                    <th scope="col">م</th>
                                                                    <th scope="col">الصور</th>
                                                                    <th scope="col">الترتيب</th>
                                                                    <th scope="col">الرئيسية</th>
                                                                    <th scope="col">حذف</th>
                                                                </tr>
                                                                <tr id="itemPlaceholder">
                                                                </tr>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr id="lvItemRow" runat="server">
                                                                <td>
                                                                    <asp:Label ID="srialNo" runat="server" Text='<%# Val(Container.DataItemIndex.ToString) + 1 %>'></asp:Label>
                                                                    <asp:Label ID="lblNewsId" runat="server" Text='<%# Eval("SourceId") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblMediaId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblMain" runat="server" Text='<%# Eval("Main") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Image ID="imgNews" onclick="ImagePreview(this.src,this.alt)" CssClass="img-thumbnail" runat="server" ImageUrl='<%# Eval("MediaThumbPath")%>' Width="50px" />

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ShowOrder") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <div class="c-check check-orange">
                                                                        <asp:CheckBox runat="server" Text=" " Checked='<%# Eval("Main") %>' Enabled="false" />
                                                                    </div>
                                                                </td>
                                                                <td>
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
                                                                            <label>هل تريد حذف الصورة ؟</label>
                                                                        </div>

                                                                        <div class="footer">
                                                                            <ul class="btn-uls mb0">
                                                                                <li class="btn-lis">
                                                                                    <asp:LinkButton ID="lbYesDelete" runat="server" SkinID="btn-green" CommandArgument='<%# Eval("Id") %>' OnClick="DeletePhoto" CausesValidation="false">نعم<i class="ti-check"></i></asp:LinkButton></li>
                                                                                <li class="btn-lis">
                                                                                    <asp:LinkButton ID="lbNoDelete" runat="server" SkinID="btn-red" OnClientClick="CloseConfirmPopup('mpConfirmDelete');return false;">لا<i class="ti-close"></i></asp:LinkButton></li>
                                                                            </ul>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                                <div class="modal-footer">
                                                    <ul class="btn-uls mb0"></ul>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- The Modal -->
        <div id="previewImage" class="previewImage">

            <!-- The Close Button -->
            <a class="Myclose" onclick='closeImgPopup();'>&times;</a>

            <!-- Modal Content (The Image) -->
            <img class="previewImage-content" id="img01" />
        </div>
    </form>
    <!--============================ js files =============================-->
    <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/cpcustom.js"></script>
    <script src="JSCode/jsImagePreview.js"></script>
    <script>
        function triggerLoadMainList() {
            $("#btnLoadAlbumList").click();
        }
    </script>
</body>
</html>
