<%@ Page Language="VB" AutoEventWireup="false" EnableSessionState="False" EnableViewState="false" CodeFile="login.aspx.vb" Inherits="cp_login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--first commit--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">

    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" type="image/png" sizes="16x16" href="#" />

    <title>تسجيل الدخول</title>

    <!-- Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Resource style -->
    <link href="css/themify-icons.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/cpcustom.css" />

    <!-- Resource script -->
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="JSCode/KeypressValidators.js"></script>

    <style>
        body {
            padding: 0 !important;
            overflow: hidden;
            margin: 0 !important;
        }
    </style>
</head>
<body onload="Loginjs();">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ToolkitScriptManager ID="TKSM" runat="server" ScriptMode="Release">
            <Services>
                <asp:ServiceReference Path="~/WebService.asmx" />
            </Services>
        </asp:ToolkitScriptManager>

        <asp:UpdatePanel ID="UP_MAIN" runat="server">
            <ContentTemplate>
                <div class="login-page">
                    <div class="uploader">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UP_MAIN">
                            <ProgressTemplate>
                                <div class="top-loader">
                                    <asp:Image ID="Image7" runat="server" ImageUrl="Images/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>

                    <div class="login-form-50per">
                        <div class="area-layer" id="login-area">

                            <div class="mb10">
                                <asp:ValidationSummary ID="ValidationSummary1" DisplayMode="BulletList" CssClass="validationSummary" runat="server" ValidationGroup="Main" />
                                <asp:Label ID="lblRes" CssClass="res-label-info " runat="server"></asp:Label>
                            </div>

                            <asp:MultiView ID="mvLogin" runat="server">
                                <asp:View ID="vwLogin" runat="server">
                                    <div class="login-area">
                                        <div class="log-area-head">
                                            <h3>تسجيل الدخول</h3>
                                        </div>

                                        <div class="log-area-body">
                                            <div class="row">

                                                <div class="col-sm-12 input-field-i">
                                                    <label class="input-label username-lbl">إسم المستخدم</label>
                                                    <asp:TextBox ID="txtUserName" ClientIDMode="Static" MaxLength="50" CssClass="form-control input-h" runat="server" placeholder="إسم المستخدم" onkeypress="return IsValidChar(event);" onkeyup="ValidateChars(this);"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator CssClass="reqValidator" ID="rfvEID" ValidationGroup="Main" runat="server" ControlToValidate="txtUserName" Text="*" ErrorMessage="Enter Username"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="col-sm-12 input-field-i">
                                                    <label class="input-label password-lbl">كملة المرور</label>
                                                    <asp:TextBox ID="txtPassword"  ClientIDMode="Static" MaxLength="50" CssClass="form-control input-h" runat="server" placeholder="كلمة المرور" TextMode="Password" onkeypress="return IsValidChar(event);" onkeyup="ValidateChars(this);"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator CssClass="reqValidator" ID="rfvPassword" ValidationGroup="Main" runat="server" ControlToValidate="txtPassword" Text="*" ErrorMessage="Enter Password"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="log-area-footer">
                                            <div class="log-footer-layer1">
                                                <div class="log-remember c-check check-green">
                                                    <asp:CheckBox runat="server" ID="chklogin" Text=" &nbsp; الدخول تلقائيا في المرة القادمة &nbsp;" />
                                                </div>
                                               
                                            </div>
                                            <div class="log-footer-layer2">
                                                <asp:Button ID="lblLogin" class="btn-block btn btn-green brd-30 sdw-green" runat="server" ValidationGroup="Main" Text="تسجيل الدخول" OnClick="CheckLogin" />
                                            </div>

                                        </div>
                                    </div>
                                </asp:View>

                           
                            </asp:MultiView>
                            <div class="log-footer-layer3">
                                <p class="">2022 جميع الحقوق محفوظة   </p>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

</body>
</html>



