﻿<%@ Page Title="تواصل معى" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Contact.aspx.vb" Inherits="Contact" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <script src="JsCode/KeypressValidators.js"></script>
    <asp:ToolkitScriptManager ID="Toolkitscriptmanager1" runat="server" ScriptMode="Release">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="up" runat="server" ClientIDMode="AutoID">
        <ContentTemplate>
            <div class="page-load">
                <asp:UpdateProgress ID="upgg" runat="server" AssociatedUpdatePanelID="UP">
                    <ProgressTemplate>
                        <div class="spinner">
                            <span></span>
                            <span></span>
                            <span></span>
                            <span></span>
                            <span></span>
                            <span></span>
                            <span></span>
                            <span></span>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <!-- Start Contact section -->
            <div class="rn-contact-area rn-section-gap mt--90" id="contacts">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="section-title text-center">
                                <span class="subtitle">Contact With Me</span>
                                <h2 class="title">تواصل معى</h2>
                            </div>
                        </div>
                    </div>
                    <div class="row mt--50 mt_md--40 mt_sm--40">
                        <div class="col-lg-5"></div>
                        <div class="col-lg-7">
                            <asp:Label ID="lblRes" runat="server" />
                            <asp:ValidationSummary ID="ValidationSummary" CssClass="validation-message" ShowSummary="false"
                                DisplayMode="BulletList" ValidationGroup="vContent" EnableClientScript="true"
                                runat="server" Font-Size="Medium" ForeColor="#CC0000" />
                        </div>
                    </div>
                    <div class="row mt-contact-sm">
                        <div class="col-lg-5">
                            <asp:Repeater runat="server" ID="rpAbout">
                                <ItemTemplate>

                                    <div class="contact-about-area">
                                        <div class="thumbnail">
                                            <img src="assets/images/contact/contact1.png" alt="contact-img">
                                        </div>
                                        <div class="title-area">
                                            <h4 class="title"><%# Eval("Name").ToString  %></h4>
                                            <span><%# Eval("Degree").ToString  %></span>
                                        </div>
                                        <div class="description">
                                            <p>
                                                لطلب المساعدة أو الاستفسار يرجى التواصل معى عبر البريد الاليكترونى أو المراسلة عبر What’s app.
                                            </p>
                                            <span class="phone" style="direction: ltr;">
                                                <a href="<%# "https://wa.me/2" + Eval("Mobile").ToString  %>" target="_blank" style="letter-spacing: 1px;"><%# Eval("Mobile").ToString  %></a>
                                                :What’s app
                                            </span>
                                            <span class="mail">البريد الاليكترونى: <a href="mailto:<%# Eval("Email").ToString  %>" target="_blank" style="letter-spacing: 1px;"><%# Eval("Email").ToString  %></a></span>
                                        </div>
                                        <div class="social-area">
                                            <div class="name">تواصل معى عبر</div>
                                            <div class="social-icone">
                                                <a target="_blank" href="<%# Eval("FacebookURL").ToString  %>"><i data-feather="facebook"></i></a>
                                                <a target="_blank" href="<%# Eval("TwitterURL").ToString  %>"><i data-feather="twitter"></i></a>
                                                <a target="_blank" href="<%# Eval("InstagramURL").ToString  %>"><i data-feather="instagram"></i></a>
                                                <a target="_blank" href="<%# Eval("LinkedInURL").ToString  %>"><i data-feather="linkedin"></i></a>
                                                <a target="_blank" href="<%# Eval("YoutubeURL").ToString  %>"><i data-feather="youtube"></i></a>
                                                <a target="_blank" href="<%# Eval("TelegramURL").ToString  %>"><i class="fab fa-telegram-plane"></i></a>
                                                <a target="_blank" href="<%# Eval("TikTokURL").ToString  %>"><i class="fab fa-tiktok"></i></a>
                                                <a target="_blank" href="<%# "https://wa.me/2" + Eval("Mobile").ToString  %>"><i class="fab fa-whatsapp"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <div data-aos-delay="600" class="col-lg-7 contact-input">
                            <div class="contact-form-wrapper">
                                <div class="introduce">
                                    <div class="rnt-contact-form rwt-dynamic-form row">

                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername">الاسم</asp:Label>
                                                <asp:TextBox ID="txtUsername" runat="server" onkeyup="ValidateChars(this);" CssClass="form-control form-control-lg" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="inp-valid"
                                                    ValidationGroup="vContent" ControlToValidate="txtUsername" ErrorMessage="أدخل الاسم"></asp:RequiredFieldValidator>
                                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters,UppercaseLetters" TargetControlID="txtUsername" />--%>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8"
                                                    ControlToValidate="txtUsername" runat="server" CssClass="inp-valid"
                                                    ErrorMessage="اسم غير صحيح" ValidationGroup="vContent"
                                                    ValidationExpression="^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_ ]*$" />
                                            </div>
                                        </div>

                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblMobile" runat="server" AssociatedControlID="txtMobile">رقم الموبايل</asp:Label>
                                                <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="15" onkeypress="return isNumber(event);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="inp-valid"
                                                    ValidationGroup="vContent" ControlToValidate="txtMobile" ErrorMessage="أدخل الموبايل" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMobile" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    ControlToValidate="txtMobile" runat="server" CssClass="inp-valid"
                                                    ErrorMessage="رقم الموبايل غير صحيح" ValidationGroup="vContent"
                                                    ValidationExpression="^[0-9]{8,15}$" />
                                            </div>
                                        </div>

                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail">البريد الاليكترونى</asp:Label>
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                    ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                    Display="Dynamic" ErrorMessage="بريد الكتروني غير صحيح" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="inp-valid"
                                                    ValidationGroup="vContent" ControlToValidate="txtEmail" ErrorMessage="أدخل البريد الإلكتروني"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblTitle" runat="server" AssociatedControlID="txtTitle">عنوان الرسالة</asp:Label>
                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control form-control-sm" MaxLength="500"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="inp-valid"
                                                    ValidationGroup="vContent" ControlToValidate="txtTitle" ErrorMessage="أدخل العنوان"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblMessage" runat="server" AssociatedControlID="txtMessage">رسالتك</asp:Label>
                                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="inp-valid"
                                                    ValidationGroup="vContent" ControlToValidate="txtMessage" ErrorMessage="أدخل الرسالة"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-lg-12">
                                            <asp:LinkButton ID="lbSend" runat="server" CssClass="rn-btn d-block text-center" ValidationGroup="vContent" OnClick="Send">
                                                <span>ارسال الرسالة</span>
                                                <i data-feather="arrow-left"></i>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Contuct section -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
