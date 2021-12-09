<%@ Page Theme="Theme1" Language="VB" AutoEventWireup="false" CodeFile="About.aspx.vb" Inherits="Profile" %>

<%@ Register Src="UserControls/HTMLEditor.ascx" TagPrefix="uc1" TagName="HTMLEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>السيرة الذاتية</title>
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
     <script src="JSCode/UploadPersonalPhoto.js"></script>
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
                        <h4>السيرة الذاتية</h4>
                    </div>
                    <div class="col-sm-6 col-xs-7">
                        <ol class="breadcrumb">
                            <li><a href="../Home.aspx"  target="_blank"><i class="ti-home"></i></a></li>
                            <li>السيرة الذاتية</li>
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

                         

                        <asp:Panel ID="pnlConfirm" CssClass="col-md-12 p0 mb10" runat="server"  >
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

                    <asp:Panel ID="pnlForm" runat="server"  >
                        <div class="container-fluid">
                            <div class="row">
                                <asp:ValidationSummary ID="ValidationSummary" CssClass="validation-message" DisplayMode="BulletList" ValidationGroup="vContent" EnableClientScript="true" runat="server" Font-Size="Medium" ForeColor="#CC0000" />
                                <asp:Label ID="lblRes" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblContentId" runat="server" Visible="false"></asp:Label>

                                <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">
                                    <div class="user-flex-panel">
                                        <div class="left-750">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="heading1">
                                                    <h4 class="panel-title">
                                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse1">السيرة الذاتية</a>
                                                    </h4>
                                                </div>

                                                <div id="collapse1" class="panel-collapse collapse in">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-md-12 form-group">
                                                                <div class="col-md-4">
                                                                    <label class="input-label required">الاسم</label>
                                                                    <asp:TextBox runat="server" ID="txtName" MaxLength="100"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="displaynone"
                                                                        ValidationGroup="vContent" ControlToValidate="txtName" ErrorMessage="أدخل الاسم"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label required">البريد الالكتروني</label>
                                                                    <asp:TextBox runat="server" ID="txtEmail" MaxLength="100"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="displaynone"
                                                                        ValidationGroup="vContent" ControlToValidate="txtEmail" ErrorMessage="أدخل البريد الالكتروني"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label required">التليفون</label>
                                                                    <asp:TextBox runat="server" ID="txtPhone" MaxLength="100" onkeypress="return isNumber(event);"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="displaynone"
                                                                        ValidationGroup="vContent" ControlToValidate="txtPhone" ErrorMessage="أدخل التليفون"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label required">الموبايل</label>
                                                                    <asp:TextBox runat="server" ID="txtMobile" MaxLength="100" onkeypress="return isNumber(event);"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="displaynone"
                                                                        ValidationGroup="vContent" ControlToValidate="txtMobile" ErrorMessage="أدخل الموبايل"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label">السن</label>
                                                                    <asp:TextBox runat="server" ID="txtAge" MaxLength="3" onkeypress="return isNumber(event);"></asp:TextBox>
                                                                </div>
                                                                 <div class="col-md-4 form-group">
                                                                    <label class="input-label">الدولة</label>
                                                                    <asp:TextBox runat="server" ID="txtCountry" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                                 <div class="col-md-4 form-group">
                                                                    <label class="input-label">المدينة</label>
                                                                    <asp:TextBox runat="server" ID="txtCity" MaxLength="50"></asp:TextBox>
                                                                </div>
                                                                  <div class="col-md-8 form-group">
                                                                    <label class="input-label">العنوان</label>
                                                                    <asp:TextBox runat="server" ID="txtAddress" MaxLength="500"></asp:TextBox>
                                                                </div>
                                                                  <div class="col-md-12 form-group">
                                                                    <label class="input-label required">السيرة الذاتية</label>
                                                                      <uc1:HTMLEditor ID="txtBio" runat="server" />
                                                                 </div>
                                                                  <div class="col-md-12 form-group">
                                                                    <label class="input-label required">الدرجة العلمية</label>
                                                                      <uc1:HTMLEditor ID="txtDegree" runat="server" />
                                                                 </div>
                                                                <div class="col-md-12 form-group">
                                                                    <label class="input-label">الخبرات</label>
                                                                     <uc1:HTMLEditor ID="txtExperience" runat="server" />
                                                                </div>
                                                                 <div class="col-md-12 form-group">
                                                                    <label class="input-label">الشهادات</label>
                                                                      <uc1:HTMLEditor ID="txtCertificates" runat="server" />
                                                                </div>
                                                                <div class="col-md-12 form-group">
                                                                    <label class="input-label">الأهداف</label>
                                                                     <uc1:HTMLEditor ID="txtSkills" runat="server" />
                                                                </div>
                                                                  <div class="col-md-12 form-group">
                                                                    <label class="input-label">الرؤية</label>
                                                                       <uc1:HTMLEditor ID="txtVision" runat="server" />
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label">الفيسبوك</label>
                                                                    <asp:TextBox runat="server" ID="txtFacebook" MaxLength="8000"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label">تويتر</label>
                                                                    <asp:TextBox runat="server" ID="txtTwitter" MaxLength="8000"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label">انستجرام</label>
                                                                    <asp:TextBox runat="server" ID="txtInstagram" MaxLength="8000"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label">لينكد ان</label>
                                                                    <asp:TextBox runat="server" ID="txtLinkedIn" MaxLength="8000"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-8 form-group">
                                                                    <label class="input-label">قناة اليوتيوب</label>
                                                                    <asp:TextBox runat="server" ID="txtYouTube" MaxLength="8000"></asp:TextBox>
                                                                </div>
                                                                
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label">تليجرام</label>
                                                                    <asp:TextBox runat="server" ID="txtTelegram" MaxLength="8000"></asp:TextBox>
                                                                </div>
                                                                 <div class="col-md-4 form-group">
                                                                    <label class="input-label">تيك توك</label>
                                                                    <asp:TextBox runat="server" ID="txtTikTok" MaxLength="8000"></asp:TextBox>
                                                                </div>
                                                                <div class="col-md-4 form-group">
                                                                    <label class="input-label">رابط السيرة الذاتية</label>
                                                                    <asp:TextBox runat="server" ID="txtCVURL" MaxLength="8000"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="right-250 pos-relavtive">
                                            <div class="demo-upload-head m0">
                                                <h3 class="required">الصورة الشخصية</h3>
                                            </div>
                                            <div class="update-progress-img">
                                                <asp:Image ID="imgLoader" runat="server" ClientIDMode="Static" ImageUrl="~/images/image-uploader.gif" Style="display: none;" />
                                            </div>
                                            <fieldset>

                                                <asp:Panel ID="pnlTLCopy" runat="server" CssClass="demo-upload-container pb5">
                                                    <div class="custom-file-container">

                                                        <asp:HyperLink ID="hlViewContent" CssClass="custom-file-container__image-preview" runat="server" ClientIDMode="Static" Target="_blank">
                                                            <asp:Image ID="imgContent" ClientIDMode="Static" runat="server" Style="max-height: 100%; max-width: 100%" ImageUrl="~/images/img-up.png" />
                                                        </asp:HyperLink>
                                                        <asp:TextBox ID="HiddenContentImg" runat="server" ClientIDMode="Static" Style="display: none"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="displaynone"
                                                            ValidationGroup="vContent" ControlToValidate="HiddenContentImg" ErrorMessage="تحميل الصورة"></asp:RequiredFieldValidator>

                                                        <asp:Panel ID="pnlfuPhoto" runat="server" CssClass="photo-upload-box_inactive">
                                                            <label class="custom-file-container__custom-file">
                                                                <asp:AsyncFileUpload ID="fuPhoto" CssClass="inputfile inputfile-1" runat="server" OnUploadedComplete="UploadFile"
                                                                    OnClientUploadComplete="UploadComplete" OnClientUploadError="UploadError" OnClientUploadStarted="UploadStarted"
                                                                    FailedValidation="False" />

                                                                <input type="hidden" name="MAX_FILE_SIZE" value="10485760" />
                                                                <span class="custom-file-container__custom-file__custom-file-control" title="الصورة الشخصية"></span>
                                                            </label>
                                                        </asp:Panel>
                                                    </div>

                                                </asp:Panel>
                                            </fieldset>
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
