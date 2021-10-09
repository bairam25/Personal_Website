<%@ Page Title="التفاصيل" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Content_Details.aspx.vb" Inherits="News_Details" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Start News Area -->
    <asp:Label ID="lblRes" runat="server"></asp:Label>
    <asp:Label ID="lblContentId" runat="server" Visible ="false" ></asp:Label>
    <asp:ListView ID="lvContentDetails" runat="server">
        <ItemTemplate>
            <div class="rn-portfolio-area rn-section-gap mt--90" id="portfolio">
                <div class="container">

                    <div class="row">
                        <div class="modal modal-page">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-body">
                                        <div class="row pt--50">
                                            <div class="col-lg-6">
                                                <div class="portfolio-popup-thumbnail">
                                                    <div class="image">
                                                        <img class="w-100" src="assets/images/news/news-1.jpg" alt="slide" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="text-content">
                                                    <h3>
                                                        <span>
                                                            <div class="d-flex justify-content-between">

                                                                <div class="meta">
                                                                    <span><i class="far fa-clock ml-2"></i>Aug 30, 2021 - 10:45 PM</span>
                                                                </div>
                                                            </div>
                                                        </span>
                                                        عنوان الخبر
                                                    </h3>
                                                    <p class="mb--30">
                                                        لوريم ايبسوم هو نموذج افتراضي يوضع في التصاميم لتعرض على العميل ليتصور طريقه وضع النصوص بالتصاميم سواء كانت تصاميم مطبوعه ... بروشور او فلاير على سبيل المثال ... او نماذج مواقع انترنت ...
                                                    </p>
                                                    <p class="mb--30">
                                                        وخلافاَ للاعتقاد السائد فإن لوريم إيبسوم ليس نصاَ عشوائياً، بل إن له جذور في الأدب اللاتيني الكلاسيكي منذ العام 45 قبل الميلاد. من كتاب "حول أقاصي الخير والشر"
                                                    </p>
                                                    <p>
                                                        لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور
                                                        أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد
                                                        أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات . ديواس
                                                        أيوتي أريري دولار إن ريبريهينديرأيت فوليوبتاتي فيلايت أيسسي كايلليوم دولار أيو فيجايت
                                                        نيولا باراياتيور. أيكسسيبتيور ساينت أوككايكات كيوبايداتات نون بروايدينت ,سيونت ان كيولبا
                                                        كيو أوفيسيا ديسيريونتموليت انيم أيدي ايست لابوريوم.
                                                    </p>
                                                </div>
                                                <!-- End of .text-content -->
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
    <!-- End News Area -->
</asp:Content>
