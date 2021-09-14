<%@ Page Title="تواصل معى" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Contact.aspx.vb" Inherits="Contact" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
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
            <div class="row mt--50 mt_md--40 mt_sm--40 mt-contact-sm">
                <div class="col-lg-5">
                    <div class="contact-about-area">
                        <div class="thumbnail">
                            <img src="assets/images/contact/contact1.png" alt="contact-img">
                        </div>
                        <div class="title-area">
                            <h4 class="title">السيد حسين</h4>
                            <span>خبير أسواق المال والأعمال</span>
                        </div>
                        <div class="description">
                            <p>
                                لطلب المساعدة أو الاستفسار يرجى التواصل معى عبر البريد الاليكترونى أو الاتصال بى.
                            </p>
                            <span class="phone">الهاتف: <a href="tel:01941043264" style="letter-spacing: 1px;">01234567890</a></span>
                            <span class="mail">البريد الاليكترونى: <a href="mailto:admin@example.com" style="letter-spacing: 1px;">admin@example.com</a></span>
                        </div>
                        <div class="social-area">
                            <div class="name">تواصل معى عبر</div>
                            <div class="social-icone">
                                <a href="#"><i data-feather="facebook"></i></a>
                                <a href="#"><i data-feather="instagram"></i></a>
                                <a href="#"><i data-feather="linkedin"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
                <div data-aos-delay="600" class="col-lg-7 contact-input">
                    <div class="contact-form-wrapper">
                        <div class="introduce">
                            <div class="rnt-contact-form rwt-dynamic-form row">

                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername">الاسم</asp:Label>
                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control form-control-lg"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblMobile" runat="server" AssociatedControlID="txtMobile">رقم الهاتف</asp:Label>
                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail">البريد الاليكترونى</asp:Label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblSubject" runat="server" AssociatedControlID="txtSubject">عنوان الرسالة</asp:Label>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Label ID="lblMessage" runat="server" AssociatedControlID="txtMessage">رسالتك</asp:Label>
                                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-12">
                                    <asp:LinkButton ID="lbSend" runat="server" CssClass="rn-btn d-block text-center">
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
</asp:Content>
