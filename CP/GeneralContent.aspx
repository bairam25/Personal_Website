<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GeneralContent.aspx.vb" Inherits="News" Theme="Theme1" %>

<%@ Register Src="UserControls/HTMLEditor.ascx" TagPrefix="uc1" TagName="HTMLEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>المحتوى العام</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="keywords" content="" />
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
    <%--<script src="JSCode/jsMultiFileUpload.js"></script>--%>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" autocomplete="off">
        <asp:ToolkitScriptManager ID="Toolkitscriptmanager1" runat="server" ScriptMode="Release">
            <%--  <Services>
                <asp:ServiceReference Path="~/WebService.asmx" />
                <asp:ServiceReference Path="~/wsMultiFileUpload.asmx" />
            </Services>--%>
        </asp:ToolkitScriptManager>

        <asp:UpdatePanel ID="up" runat="server">
            <ContentTemplate>
                <!--============================ Page-header =============================-->

                <div class="container-fluid">
                    <div class="page-header">
                        <div class="row">
                            <div class="col-sm-6 col-xs-5 text-left">
                                <h4>المحتوى العام</h4>
                            </div>
                            <div class="col-sm-6 col-xs-7">
                                <ol class="breadcrumb">
                                    <li><i class="ti-home"></i></li>
                                    <li>المحتوى العام</li>
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
                                        <%--<asp:Label ID="lblNewsId" runat="server" Visible="false"></asp:Label>--%>
                                    </div>
                                    <div class="col-md-12 p0">
                                        <div class="mb0" id="accordion" role="tablist" aria-multiselectable="false">
                                            <asp:Panel runat="server" ID="pnlForm">
                                                <div class="container-fluid">
                                                    <asp:Panel ID="pnlConfirm" CssClass="row" runat="server">
                                                        <div class="col-md-12 p0">
                                                            <ul class="pull-right p0">
                                                                <li class="btn-lis" id="liSave" runat="server">
                                                                    <span class="btn-save-wrapper" style="position: relative;">
                                                                        <i class="ti-check icon-save"></i>
                                                                        <asp:Button ID="cmdSave" runat="server" SkinID="btn-save" UseSubmitBehavior="false" OnClientClick="SaveClick(this,'');" ToolTip="حفظ" Text="حفظ" OnClick="Save"></asp:Button>
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
                                                                        <a class="accordion-toggle" data-toggle="collapse" href="#collapseOne">بيانات الإتصال</a>
                                                                    </h4>
                                                                </div>

                                                                <div id="collapseOne" class="panel-collapse collapse in">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">التليفون</label>
                                                                                    <asp:TextBox runat="server" ID="txtPhone" placeholder="التليفون" onkeypress="return isNumber(event);" MaxLength="16"></asp:TextBox>
                                                                                    <%--  <asp:RequiredFieldValidator ValidationGroup="vNews" runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtShowOrder"
                                                                                        ErrorMessage="أدخل ترتيب الخبر" SetFocusOnError="true" />--%>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">الفاكس</label>
                                                                                    <asp:TextBox runat="server" ID="txtFax" MaxLength="50" autocomplete="off" onkeypress="return isNumber(event);" placeholder="الفاكس" ToolTip="الفاكس"></asp:TextBox>
                                                                                    <%--   <asp:RequiredFieldValidator ValidationGroup="vNews" runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtTitle"
                                                                                        ErrorMessage="أدخل العنوان" SetFocusOnError="true" />--%>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">البريد الإلكتروني</label>
                                                                                    <asp:TextBox runat="server" ID="txtEmail" MaxLength="50" autocomplete="off" placeholder="البريد الإلكتروني" ToolTip="البريد الإلكتروني"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">الموقع الإلكتروني</label>
                                                                                    <asp:TextBox runat="server" ID="txtWebSite" MaxLength="50" autocomplete="off" placeholder="الموقع الإلكتروني" ToolTip="الموقع الإلكتروني"></asp:TextBox>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">Facebook</label>
                                                                                    <asp:TextBox runat="server" ID="txtFacebook" placeholder="Facebook"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">Youtube</label>
                                                                                    <asp:TextBox runat="server" ID="txtYoutube" placeholder="Youtube"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">LinkedIn</label>
                                                                                    <asp:TextBox runat="server" ID="txtLinkedIn" placeholder="LinkedIn"></asp:TextBox>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">Twitter</label>
                                                                                    <asp:TextBox runat="server" ID="txtTwitter" placeholder="Twitter"></asp:TextBox>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">رمز البريد</label>
                                                                                    <asp:TextBox runat="server" ID="txtPOBox" placeholder="رمز البريد" onkeypress="return isNumber(event);" MaxLength="16"></asp:TextBox>
                                                                                    <%--  <asp:RequiredFieldValidator ValidationGroup="vNews" runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtShowOrder"
                                                                                        ErrorMessage="أدخل ترتيب الخبر" SetFocusOnError="true" />--%>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">العنوان</label>
                                                                                    <uc1:HTMLEditor ID="txtAddress" runat="server" />

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="row" style="display: none;">
                                                        <div class="col-md-12 p0">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a class="accordion-toggle" data-toggle="collapse" href="#collapse2">من نحن</a>
                                                                    </h4>
                                                                </div>

                                                                <div id="collapse2" class="panel-collapse collapse in">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">من نحن</label>
                                                                                <uc1:HTMLEditor ID="txtAboutUs" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="display: none;">
                                                        <div class="col-md-12 p0">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a class="accordion-toggle" data-toggle="collapse" href="#collapse3">الرؤية</a>
                                                                    </h4>
                                                                </div>

                                                                <div id="collapse3" class="panel-collapse collapse in">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">الرؤية</label>
                                                                                <uc1:HTMLEditor ID="txtVission" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="display: none;">
                                                        <div class="col-md-12 p0">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a class="accordion-toggle" data-toggle="collapse" href="#collapse4">الرسالة</a>
                                                                    </h4>
                                                                </div>

                                                                <div id="collapse4" class="panel-collapse collapse in">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">الرسالة</label>
                                                                                <uc1:HTMLEditor ID="txtMission" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12 p0">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a class="accordion-toggle" data-toggle="collapse" href="#collapse5">مجالات العمل</a>
                                                                    </h4>
                                                                </div>

                                                                <div id="collapse5" class="panel-collapse collapse in">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">التنمية الصناعية</label>
                                                                                <uc1:HTMLEditor ID="txtWRKIndustrialDev" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">التكتلات الإقتصادية</label>
                                                                                <uc1:HTMLEditor ID="txtWRKEconomicBlocs" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">تحسين الخدمات الإقتصادية</label>
                                                                                <uc1:HTMLEditor ID="txtWRKImproveServices" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">البنية الأساسية والخدمات</label>
                                                                                <uc1:HTMLEditor ID="txtWRKInfraServices" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">التطوير المؤسسي</label>
                                                                                <uc1:HTMLEditor ID="txtWRKInstitutionalDev" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">بناء القدرات</label>
                                                                                <uc1:HTMLEditor ID="txtWRKBuildingAbilities" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">الاعتبارات الإجتماعية</label>
                                                                                <uc1:HTMLEditor ID="txtWRKSocialCons" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">الاعتبارات البيئية</label>
                                                                                <uc1:HTMLEditor ID="txtWRKEnviromentalCons" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3" style ="display:none ">
                                                                                <label class="input-label">السلامة والصحة المهنية</label>
                                                                                <uc1:HTMLEditor ID="txtWRKSafetyHealth" runat="server" />
                                                                            </div>
                                                                          
                                                                        </div>
                                                                          <div class="row">
                                                                               <div class="col-md-3">
                                                                                <h4>إشراك المواطنين</h4>
                                                                                   </div>
                                                                            </div>

                                                                            <div class="row">
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">التخطيط التشاركي</label>
                                                                                    <uc1:HTMLEditor ID="txtCitizenPlan" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">تصميم المشروعات</label>
                                                                                    <uc1:HTMLEditor ID="txtCitizenDesign" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">متابعة التنفيذ</label>
                                                                                    <uc1:HTMLEditor ID="txtCitizenFollow" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">تقييم ردود فعل المستفيد</label>
                                                                                    <uc1:HTMLEditor ID="txtCitizenEvaluation" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">الشكاوى والتظلمات</label>
                                                                                    <uc1:HTMLEditor ID="txtCitizenComplains" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">منتديات التنمية المحلية</label>
                                                                                    <uc1:HTMLEditor ID="txtCitizenLocalEvents" runat="server" />
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <label class="input-label">منتديات المناطق الصناعية</label>
                                                                                    <uc1:HTMLEditor ID="txtCitizenIndustrialEvents" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12 p0">
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading">
                                                                    <h4 class="panel-title">
                                                                        <a class="accordion-toggle" data-toggle="collapse" href="#collapse6">الإطار المؤسسي</a>
                                                                    </h4>
                                                                </div>

                                                                <div id="collapse6" class="panel-collapse collapse in">
                                                                    <div class="panel-body">
                                                                        <div class="row">
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">لجنة التسيير</label>
                                                                                <uc1:HTMLEditor ID="txtORGSteering" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">المكتب التنسيقي</label>
                                                                                <uc1:HTMLEditor ID="txtORGCoordinationOffice" runat="server" />
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <label class="input-label">وحدة التنفيذ المحلية</label>
                                                                                <uc1:HTMLEditor ID="txtORGLocalImplement" runat="server" />
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
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


    </form>
    <!--============================ js files =============================-->
    <script src="js/jquery-1.11.2.min.js"></script>
    <script src="js/jquery-ui.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/cpcustom.js"></script>

</body>
</html>
