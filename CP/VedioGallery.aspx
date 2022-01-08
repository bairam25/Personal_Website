<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VedioGallery.aspx.vb" Inherits="Gallery" Theme="Theme1" %>
<%@ Register Src="UserControls/HTMLEditor.ascx" TagPrefix="uc1" TagName="HTMLEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>معرض الفيديو</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="keywords" content="Aber, toll gates, Ras Al Khaima" />
    <link rel="shortcut icon" href="../images/logo/favi.png" />
    <!-- Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>

    <script src="JSCode/KeypressValidators.js"></script>
    <script src="JSCode/Popup.js" type="text/javascript"></script>

    <link href="css/themify-icons.css" rel="stylesheet" />
    <link href="css/cpcustom.css" rel="stylesheet" />
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
                                <h4>معرض الفيديو</h4>
                            </div>
                            <div class="col-sm-6 col-xs-7">
                                <ol class="breadcrumb">
                                    <li><a href="../Home.aspx" target="_blank"><i class="ti-home"></i></a></li>
                                    <li>معرض الفيديو</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>

                <!--============================ Page-content =============================-->

                <div class="container-fluid">
                    <div class="acp-tab-layer">

                        <div class="uploader">
                            <asp:UpdateProgress ID="upg" runat="server" AssociatedUpdatePanelID="up">
                                <ProgressTemplate>
                                    <asp:Image class="update-progress" ID="imgLoader" ClientIDMode="Static" runat="server" ImageUrl="images/ajax-loader.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>

                        <div class="row">
                            <div class="col-md-12 p0">
                                <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblAlbumId" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">
                                <asp:Panel runat="server" ID="pnlForm" Visible="false">
                                    <asp:Panel ID="pnlConfirm" CssClass="row" runat="server">
                                        <div class="col-md-12 p0">
                                            <ul class="pull-right p0">
                                                <li class="btn-lis" id="liSave" runat="server">
                                                    <span class="btn-save-wrapper" style="position: relative;">
                                                        <i class="ti-check icon-save"></i>
                                                        <asp:Button ID="cmdSave" ValidationGroup="vGallery" runat="server" SkinID="btn-save" UseSubmitBehavior="false" OnClientClick="SaveClick(this,'vGallery');" ToolTip="حفظ" Text="حفظ" OnClick="Save"></asp:Button>
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

                                    <div class="row">
                                        <div class="col-md-12 p0">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h4 class="panel-title">
                                                        <a class="accordion-toggle" data-toggle="collapse" href="#collapseOne">إضافة ألبوم</a>
                                                    </h4>
                                                </div>

                                                <div id="collapseOne" class="panel-collapse collapse in">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6">
                                                                    <label class="input-label required">عنوان الألبوم</label>
                                                                    <asp:TextBox runat="server" ID="txtTitle" MaxLength="200" autocomplete="off" placeholder="العنوان" ToolTip="العنوان"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ValidationGroup="vGallery" runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtTitle"
                                                                        ErrorMessage="أدخل العنوان" SetFocusOnError="true" />
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="input-label required">الترتيب</label>
                                                                    <asp:TextBox runat="server" ID="txtShowOrder" placeholder="الترتيب" onkeypress="return isNumber(event);" MaxLength="6"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ValidationGroup="vGallery" runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtShowOrder"
                                                                        ErrorMessage="أدخل ترتيب العرض" SetFocusOnError="true" />
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="input-label required">التاريخ</label>
                                                                    <asp:TextBox ID="txtAlbumDate" runat="server" MaxLength="10" onkeypress="return isDate(event);"></asp:TextBox>
                                                                    <asp:CalendarExtender CssClass="custom-calendar" ID="CalendarExtender1" runat="server"
                                                                        Enabled="True" TargetControlID="txtAlbumDate" DaysModeTitleFormat="dd/MM/yyyy"
                                                                        TodaysDateFormat="dd/MM/yyyy" Format="dd/MM/yyyy">
                                                                    </asp:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Text="أدخل تاريخ الألبوم" ValidationGroup="vGallery"
                                                                        ControlToValidate="txtAlbumDate" EnableClientScript="true" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Required Album Date"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="vGallery" runat="server"
                                                                        ErrorMessage="Invalid Date" ControlToValidate="txtAlbumDate" Display="Dynamic"
                                                                        ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
                                                                        CssClass="in-validate" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <label class="input-label">&nbsp;</label>
                                                                    <div class="c-check">
                                                                        <asp:CheckBox runat="server" ID="chkShowInHome" ToolTip="عرض في الرئيسية" Text="عرض في الرئيسية"></asp:CheckBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <label class="input-label">وصف الألبوم</label>
                                                                    <uc1:HTMLEditor ID="txtDescription" runat="server" />
                                                                    <%--<asp:TextBox runat="server" ID="txtDescription" MaxLength="500" TextMode="MultiLine" autocomplete="off" placeholder="الوصف" ToolTip="الوصف"></asp:TextBox>--%>
                                                                </div>
                                                                <%-- <div class="col-md-3">
                                                                        <label runat="server" id="Label1" for="ddlCategory" class="active">التصنيف</label>
                                                                        <asp:DropDownList runat="server" TabIndex="4" ID="ddlCategory" CssClass="add_padding" AutoPostBack="true" OnSelectedIndexChanged="CategoryChanged"></asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <label runat="server" id="Label2" for="ddlSubCategory" class="active">التصنيف الفرعى</label>
                                                                        <asp:DropDownList runat="server" ID="ddlSubCategory" CssClass="add_padding" TabIndex="5">
                                                                            <asp:ListItem Value="0" Text="-- أختر --"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlPhotos" runat="server" ClientIDMode="Static" Style="display: none" CssClass="row">
                                    <div class="col-md-12 p0">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a class="accordion-toggle" data-toggle="collapse" href="#collapse3">الفيديو
           
                                                    </a>
                                                </h4>
                                            </div>
                                            <div id="collapse3" class="panel-collapse collapse in">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-12">
                                                                <asp:RadioButtonList runat="server" ID="rblSRC" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="SelectSRC">
                                                                    <asp:ListItem Text="تحميل" Value="M" Selected="True" />
                                                                    <asp:ListItem Text="رابط يوتيوب" Value="Y" />
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <asp:Panel runat="server" ID="pnlYoutube" Visible="false" CssClass="col-md-12 p0">
                                                                <div class="col-md-6">
                                                                    <label runat="server" class="input-label">رابط الفيديو</label>
                                                                    <asp:TextBox runat="server" ID="txtYoutubeURL" MaxLength="200" />
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <label class="input-label" style="display: block;">&nbsp;</label>
                                                                    <ul class="pull-right p0" style="margin-top: 2px;">
                                                                        <li class="btn-lis">
                                                                            <asp:LinkButton ID="lbSubmitItem" CssClass="btn-main btn-green" runat="server"
                                                                                data-toggle="tooltip" data-original-title="إضافة" OnClick="SubmitItem">إضافة <i class="ti-check"></i></asp:LinkButton>
                                                                        </li>
                                                                        <li class="btn-lis">
                                                                            <asp:LinkButton ID="lbcancelURL" CssClass="btn-main btn-red" runat="server"
                                                                                data-toggle="tooltip" data-original-title="إلغاء" OnClick="CancelURL">إلغاء <i class="ti-close"></i></asp:LinkButton>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 mt20">
                                                        <asp:Panel ID="pnlUpload" runat="server" ClientIDMode="Static">
                                                            <asp:LinkButton SkinID="btn-blue" ID="lbUpload" runat="server">إضافة وسائط <i class="fa fa-plus"></i></asp:LinkButton>

                                                            <asp:ModalPopupExtender ID="mdu" runat="server" BackgroundCssClass="modalBackground" TargetControlID="lbUpload"
                                                                PopupControlID="pnlFileUpload" ClientIDMode="AutoID" CancelControlID="lbClosePopUp" Enabled="True">
                                                            </asp:ModalPopupExtender>

                                                            <asp:Panel ID="pnlFileUpload" runat="server" CssClass="modalPopup-uploader" Style="width: 500px;">
                                                                <div class="modal-header">
                                                                    <asp:LinkButton ID="lbClosePopUp" runat="server" CssClass="pull-right" ToolTip="Close"> X<%--<i class="ti-close close-uploader icon-uploader"></i>--%></asp:LinkButton>
                                                                    <asp:HiddenField ID="FolderName" runat="server" Value="MediaCenter/Album/" />
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="clear"></div>
                                                                    <asp:AjaxFileUpload ID="AjaxFileUpload1" ClientIDMode="static" runat="server" OnClientUploadStart="UploadFileStart" OnClientUploadComplete="uploadFileComplete"
                                                                        MaximumNumberOfFiles="100" MaxFileSize="20480" AllowedFileTypes="jpg,png,jpeg,mp4,webm,wmv" />
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

                                                    <div class="col-md-12 mt20">
                                                        <asp:HiddenField runat="server" ID="hfURLIndex" />
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
                                                                    <asp:TemplateField HeaderText="الفيديو">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblId" runat="server" Visible="false" Text='<%# Eval("Id")%>' />
                                                                            <asp:Label ID="lblShowOrder" runat="server" Text='<%# Eval("ShowOrder")%>' Visible="false" />
                                                                            <asp:Label ID="lblIsURL" runat="server" Text='<%# PublicFunctions.BoolFormat(Eval("IsURl")) %>' Visible="false" />
                                                                            <asp:Label ID="lblType" runat="server" Text='<%# GetMediaType(Eval("Path").ToString)  %>' Visible="false" />
                                                                            <asp:Image ID="lblImg" CssClass="td-img img-thumbnail" runat="server" ImageUrl='<%# Eval("Path")%>' Width="50px" onclick="ImagePreview(this.src,this.alt)" Visible="false" />
                                                                            <asp:Image ID="lblMedia" CssClass="td-img img-thumbnail" runat="server" lang='<%# Eval("Path").ToString.Replace("~/", "../") %>'
                                                                                ImageUrl='<%#IIf(Eval("Path").ToString.Split(".").Last.ToLower = "mp4" OrElse Eval("Path").ToString.Split(".").Last.ToLower = "wmv" OrElse Eval("Path").ToString.Split(".").Last.ToLower = "webm", "images/video.png", Eval("Path")) %>'
                                                                                AlternateText='<%# Eval("Title")%>'
                                                                                Width="50px" onclick="ImagePreview(this.lang,this.alt)" />
                                                                            <%--<img class="td-img img-thumbnail" src='<%# Eval("Path").ToString %>'>--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="الترتيب">
                                                                        <ItemTemplate>
                                                                            <asp:DropDownList ID="ddlShowOrder" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShowOrder_SelectedIndexChanged"></asp:DropDownList>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="عنوان الفيديو">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtTitle" runat="server" MaxLength="200" placeholder="العنوان" Text='<%# Eval("Title")%>'></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="وصف الفيديو" Visible="false">
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
                                                                                    <label>هل تريد حذف الفيديو ؟</label>
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
                                                                    <span class="glyphicon glyphicon-exclamation-sign"></span>&nbsp لا توجد ملفات
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <!-- The Modal -->
                            <asp:Button ID="btnLoadAlbumList" ClientIDMode="Static" runat="server" OnClick="FillGrid" Style="display: none;" />
                            <asp:HiddenField ID="hfShowImages" runat="server" />
                            <asp:ModalPopupExtender ID="mpPopupImgs" runat="server" ClientIDMode="Static" PopupControlID="pnlPopupImgs" TargetControlID="hfShowImages"
                                CancelControlID="lbClosePopupImages" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlPopupImgs" runat="server" ClientIDMode="Static" CssClass="modal-dialog modal-lg top10" align="center" Style="display: none; margin: 30px auto!important;">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title dis-inline-b">الفيديو</h5>
                                        <asp:LinkButton ID="lbClosePopupImages" runat="server" CssClass="close close-popup" OnClientClick="CloseConfirmPopup('mpPopupImgs');triggerLoadMainList();return false;"><span>&times;</span></asp:LinkButton>
                                    </div>
                                    <div class="modal-body body-scroll">
                                        <asp:Label ID="lblResPopupImages" runat="server" Visible="false"></asp:Label>
                                        <asp:ListView ID="lvImages" runat="server" ClientIDMode="AutoID">
                                            <LayoutTemplate>
                                                <table class="tbl-imgs" runat="server" cellspacing="0" rules="all" border="0" id="gvProductImages">
                                                    <tr class="HeaderStyle">
                                                        <th scope="col">م</th>
                                                        <th scope="col">الفيديو </th>
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
                                                        <asp:Label ID="lblAlbumId" runat="server" Text='<%# Eval("AlbumId") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblMediaId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblMain" runat="server" Text='<%# Eval("Main") %>' Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Image ID="lblImg" CssClass="td-img img-thumbnail" runat="server" lang='<%# Eval("Path").ToString.Replace("~/", "../") %>' ImageUrl='<%#IIf(Eval("Path").ToString.Split(".").Last.ToLower = "mp4" OrElse Eval("Path").ToString.Split(".").Last.ToLower = "wmv" OrElse Eval("Path").ToString.Split(".").Last.ToLower = "webm", "images/video.png", Eval("Path")) %>' Width="50px" onclick="ImagePreview(this.lang)" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ShowOrder") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <div class="c-check check-orange">
                                                            <asp:CheckBox ID="rblSelect" runat="server" Text=" " Checked='<%# Eval("Main") %>' Enabled="false" />
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
                                                                <label>هل تريد حذف الفيديو ؟</label>
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
                        <asp:Panel runat="server" ID="pnlList">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-top-panel">
                                        <div class="tbl-top-panel-left">
                                            <div class="row">
                                                <div class="d-flex">
                                                    <div class="input-in">
                                                        <a id="cmdDelete" href="#" title="Cancel" class="btn-main btn-red" data-toggle="modal" data-placement="bottom" data-original-title="Cancel"
                                                            onclick="ShowConfirmPopup('mpeDeleteAll','pnlDeleteAll');return false;">حذف<i class="ti-trash"></i></a>
                                                        <asp:HiddenField ID="hfDeleteAll" runat="server" />
                                                        <asp:ModalPopupExtender ID="mpeDeleteAll" ClientIDMode="Static" runat="server" PopupControlID="pnlDeleteAll" TargetControlID="hfDeleteAll"
                                                            CancelControlID="lbNoDeleteAll" BackgroundCssClass="modalBackground">
                                                        </asp:ModalPopupExtender>
                                                        <asp:Panel ID="pnlDeleteAll" runat="server" ClientIDMode="Static" CssClass="modal-n modalPopup" align="center" Style="display: none">
                                                            <div class="header">
                                                                رسالة تأكيد
                                                            </div>
                                                            <div class="body">
                                                                <label>تأكيد حذف العناصر المحددة ؟</label>
                                                            </div>

                                                            <div class="footer">
                                                                <ul class="btn-uls mb0">
                                                                    <li class="btn-lis">
                                                                        <asp:LinkButton ID="lbYesDeleteAll" runat="server" SkinID="btn-green" OnClick="DeleteAll" CausesValidation="false">نعم<i class="ti-check"></i></asp:LinkButton></li>
                                                                    <li class="btn-lis">
                                                                        <asp:LinkButton ID="lbNoDeleteAll" runat="server" SkinID="btn-red">لا<i class="ti-close"></i></asp:LinkButton></li>
                                                                </ul>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div class="input-in">
                                                        <div class="input-group">
                                                            <asp:DropDownList ID="ddlPager" runat="server" CssClass="form-control" placeholder="Pages" AutoPostBack="true" OnSelectedIndexChanged="FillGrid">
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                                                <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="input-group-addon">سجلات / الصفحة</span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tbl-top-panel-right">
                                            <div class="row">
                                                <asp:Panel runat="server" ID="pnlOps">
                                                    <div class="input-280 input-in searchContiner">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtSearch" ToolTip="بحث" data-placement="bottom" runat="server" type="text" class="form-control" placeholder="بحث بالعنوان" MaxLength="100" AutoPostBack="true" OnTextChanged="FillGrid"></asp:TextBox>
                                                            <asp:LinkButton runat="server" SkinID="clear-search" ID="cmdClear" title="مسح" OnClientClick="$('#txtSearch').val('');">&times;</asp:LinkButton>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lbSearchIcon" runat="server" class="search-bt btn btn-default" type="button" OnClick="FillGrid"> <i class="fa-search fa"></i> </asp:LinkButton>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <asp:LinkButton ID="lbNew" SkinID="btn-new" runat="server" ToolTip="إضافة" OnClick="Add">إضافة<i class="ti-plus"></i></asp:LinkButton>

                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="clearfix"></div>

                                    <div class="table-layer" style="box-shadow: 0px 1px 15px 1px rgba(69,65,78,0.08);">
                                        <div class="pages-table">
                                            <asp:ListView ID="lvGallery" runat="server" ClientIDMode="AutoID"
                                                OnPagePropertiesChanging="OnPagePropertiesChanging" OnSorting="lv_Sorting">
                                                <LayoutTemplate>
                                                    <table id="itemPlaceholderContainer" runat="server" class="table tbl-table">
                                                        <tr class="HeaderStyle">
                                                            <th>
                                                                <asp:CheckBox Text="" runat="server" ID="ckAll" AutoPostBack="true" OnCheckedChanged="CheckAll" /></th>
                                                            <th>م</th>
                                                            <th class="upnDownArrow" id="AlbumDate">
                                                                <asp:LinkButton ID="lbAlbumDate" CommandArgument="AlbumDate" CommandName="Sort" runat="server">التاريخ</asp:LinkButton>
                                                            </th>
                                                            <th>الفيديو</th>
                                                            <th class="upnDownArrow" id="Title">
                                                                <asp:LinkButton ID="lbNewsTitle" CommandArgument="Title" CommandName="Sort" runat="server">العنوان</asp:LinkButton>
                                                            </th>
                                                            <th class="upnDownArrow" id="MediaCount">
                                                                <asp:LinkButton ID="lbNewsMediaCount" CommandArgument="MediaCount" CommandName="Sort" runat="server">عدد الفيديو</asp:LinkButton>
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
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="srialNo" runat="server" Text='<%# Val(Container.DataItemIndex.ToString) + 1 %>'></asp:Label>
                                                            <asp:Label ID="lblAlbumId" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" Text='<%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd/MM/yyyy") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Image ID="ImgbigPhoto" CssClass="td-img img-thumbnail" runat="server" ImageUrl='<%# Eval("MainURL")%>' Visible="false" />
                                                            <asp:LinkButton ID="lbShowImages" runat="server" CommandArgument='<%# Eval("Id")%>' OnClick="ViewPhotos">
                                                                <asp:Image ID="imgPhoto" CssClass="td-img img-thumbnail" runat="server" ImageUrl='<%# Eval("MainURL").ToString  %>' />
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" Text='<%# Eval("MediaCount") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" Text='<%# Eval("ShowOrder") %>'></asp:Label>
                                                        </td>
                                                        <td id="Active">
                                                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Active")%>' AutoPostBack="true" OnCheckedChanged="UpdateActive"></asp:CheckBox>
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
                                                                    <label>هل تريد حذف الألبوم ؟</label>
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
                                                                <div>لا توجد ملفات</div>
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
                                                    Total Albums : <span>
                                                        <asp:Label ID="lblTotalCount" runat="server"></asp:Label></span>
                                                </p>
                                            </div>
                                        </div>

                                        <div class="tbl-bot-panel-right">
                                            <div class="row">
                                                <ul class="pagination">
                                                    <li>
                                                        <asp:DataPager ID="dplvGallery" class="pagination" runat="server" PagedControlID="lvGallery" PageSize='<%# ddlPager.SelectedValue %>' style="width: 100%; display: inline-flex;">
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
                            </div>
                        </asp:Panel>
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
            <video id="videoPreview" width="100%" height="490px" autoplay="autoplay" controls="controls" muted=""></video>

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
