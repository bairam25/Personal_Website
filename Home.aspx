<%@ Page Title="الرئيسية" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:Label ID="lblRes" runat="server"></asp:Label>
    <!-- Start Slider Area -->
    <div id="home" class="rn-slider-area">
        <div class="slide slider-style-1">
            <div class="container">
                <asp:ListView runat="server" ID="lvMaster">
                    <ItemTemplate>
                        <div class="row row--30 align-items-center">
                            <div class="order-2 order-lg-1 col-lg-7 mt_md--50 mt_sm--50 mt_lg--30">
                                <div class="content">
                                    <div class="inner">
                                        <span class="subtitle">مرحبا بكم ...</span>
                                        <h1 class="title">مرحباً ,أنا <span><%# Eval("Name").ToString  %></span><br>
                                            <span class="header-caption" id="page-top">
                                                <!-- type headline start-->
                                                <span class="cd-headline clip is-full-width">
                                                    <!-- ROTATING TEXT -->
                                                    <span class="cd-words-wrapper">
                                                        <b class="is-visible">خبير أسواق.</b>
                                                        <b class="is-hidden">المال والأعمال.</b>
                                                    </span>
                                                </span>
                                                <!-- type headline end -->
                                            </span>
                                        </h1>

                                        <div>
                                            <p class="description">
                                                <%# Eval("Bio").ToString  %>
                                             </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-xl-12 col-md-12 col-sm-12 col-12">
                                            <div class="social-share-inner-left">
                                                <span class="title">تواصل معى عبر</span>
                                                <ul class="social-share d-flex liststyle">                            
                                                    <li class="facebook">
                                                        <a target="_blank" href="<%# Eval("FacebookURL").ToString  %>"><i data-feather="facebook"></i></a>
                                                    </li>
                                                    <li class="instagram">
                                                        <a target="_blank" href="<%# Eval("InstagramURL").ToString  %>"><i data-feather="instagram"></i></a>
                                                    </li>
                                                    <li class="linkedin">
                                                        <a target="_blank" href="<%# Eval("LinkedInURL").ToString  %>"><i data-feather="linkedin"></i></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="order-1 order-lg-2 col-lg-5">
                                <div class="thumbnail">
                                    <div class="inner">
                                        <img src='<%# Eval("Photo").ToString.replace("~/","")  %>' alt='<%# Eval("Name").ToString   %>'>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
    <!-- End Slider Area -->
    <!-- Start News Area -->
    <div class="rn-portfolio-area rn-section-gap section-separator" id="News">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title text-center">
                        <span class="subtitle">Top News</span>
                        <h2 class="title">أهم الأخبار</h2>
                    </div>
                </div>
            </div>

            <div class="row row--25 mt--10 mt_md--10 mt_sm--10">
                <asp:ListView ID="lvNews" runat="server">
                    <ItemTemplate>
                        <!-- Start News -->
                        <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-6 col-xl-4 col-md-6 col-12 mt--50 mt_md--30 mt_sm--30">
                            <div class="rn-portfolio" onclick="location.href='<%# "News_Details.aspx?Id=" + Eval("Id").ToString  %>'">
                                <div class="inner">
                                    <div class="thumbnail">
                                        <a href="javascript:void(0)">
                                            <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Photo") %>' ToolTip="News Photo" />
                                        </a>
                                    </div>
                                    <div class="content">
                                        <div class="category-info">
                                            <%--<div class="category-list">
                                        <a href="javascript:void(0)">تصنيف الخبر</a>
                                    </div>--%>
                                            <div class="meta">
                                                <span><i class="far fa-clock"></i><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd MMMM yyyy    hh:mm tt", "AR") %></span>
                                            </div>
                                        </div>
                                        <h4 class="title">
                                            <a href="javascript:void(0)"><%# Eval("Title") %></a>
                                        </h4>
                                        <p class="description"><%# Eval("Description") %></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End News -->
                    </ItemTemplate>
                </asp:ListView>


            </div>

            <a id="lbShowMoreNews" class="rn-btn d-block text-center mt--60 btn-more" href="News.aspx">
                <span>مشاهدة المزيد</span>
            </a>
        </div>
    </div>
    <!-- End News Area -->
    <!-- Start Conferences and Courses Area -->
    <div class="rn-resume-area rn-section-gap section-separator" id="resume">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title text-center">
                        <span class="subtitle">Courses, Seminars And Conferences</span>
                        <h2 class="title">الدورات والندوات والمؤتمرات</h2>
                    </div>
                </div>
            </div>
            <div class="row mt--45">
                <div class="col-lg-12">
                    <ul class="rn-nav-list nav nav-tabs" id="myTabs" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" id="Courses-tab" data-toggle="tab" href="#Courses" role="tab" aria-controls="Courses" aria-selected="false">الدورات</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="Seminars-tab" data-toggle="tab" href="#Seminars" role="tab" aria-controls="Seminars" aria-selected="true">الندوات</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="Conferences-tab" data-toggle="tab" href="#Conferences" role="tab" aria-controls="Conferences" aria-selected="false">المؤتمرات</a>
                        </li>
                    </ul>
                    <!-- Start Tab Content Wrapper  -->
                    <div class="rn-nav-content tab-content" id="myTabContents">
                        <!-- Start Single Tab  -->
                        <div class="tab-pane show active fade single-tab-area" id="Courses" role="tabpanel" aria-labelledby="Courses-tab">
                            <div class="col-lg-12 p-0">
                                <div class="testimonial-activation seminars-slider testimonial-pb mb--30 d-ltr">
                                    <!-- Start Single Seminars -->
                                    <div class="testimonial mt--50 mt_md--40 mt_sm--40">
                                        <div class="inner d-rtl">
                                            <div class="card-info">
                                                <div class="card-thumbnail">
                                                    <img src="assets/images/testimonial/final-home--1st.png" alt="Testimonial-image">
                                                </div>
                                                <div class="card-content">
                                                    <h3 class="title">الاسم</h3>
                                                    <span class="designation">المسمى الوظيفى</span>
                                                </div>
                                            </div>
                                            <div class="card-description">
                                                <div class="title-area">
                                                    <div class="title-info text-right">
                                                        <h3 class="title">عنوان الدورة</h3>
                                                        <span class="date"><i class="far fa-clock ml-2"></i>Aug 30, 2021</span>
                                                    </div>
                                                    <asp:LinkButton ID="lbMoreCourses" runat="server" CssClass="rn-btn">
                                                        <span>قراءة المزيد</span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="seperator"></div>
                                                <p class="discription">
                                                    لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <!--End Single Seminars -->
                                    <!-- Start Single Seminars -->
                                    <div class="testimonial mt--50 mt_md--40 mt_sm--40">
                                        <div class="inner d-rtl">
                                            <div class="card-info">
                                                <div class="card-thumbnail">
                                                    <img src="assets/images/testimonial/final-home--2nd.png" alt="Testimonial-image">
                                                </div>
                                                <div class="card-content">
                                                    <h3 class="title">الاسم</h3>
                                                    <span class="designation">المسمى الوظيفى</span>
                                                </div>
                                            </div>
                                            <div class="card-description">
                                                <div class="title-area">
                                                    <div class="title-info text-right">
                                                        <h3 class="title">عنوان الدورة</h3>
                                                        <span class="date"><i class="far fa-clock ml-2"></i>Aug 30, 2021</span>
                                                    </div>
                                                </div>
                                                <div class="seperator"></div>
                                                <p class="discription">
                                                    لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <!--End Single Seminars -->
                                    <!-- Start Single Seminars -->
                                    <div class="testimonial mt--50 mt_md--40 mt_sm--40">
                                        <div class="inner d-rtl">
                                            <div class="card-info">
                                                <div class="card-thumbnail">
                                                    <img src="assets/images/testimonial/final-home--3rd.png" alt="Testimonial-image">
                                                </div>
                                                <div class="card-content">
                                                    <h3 class="title">الاسم</h3>
                                                    <span class="designation">المسمى الوظيفى</span>
                                                </div>
                                            </div>
                                            <div class="card-description">
                                                <div class="title-area">
                                                    <div class="title-info text-right">
                                                        <h3 class="title">عنوان الدورة</h3>
                                                        <span class="date"><i class="far fa-clock ml-2"></i>Aug 30, 2021</span>
                                                    </div>
                                                </div>
                                                <div class="seperator"></div>
                                                <p class="discription">
                                                    لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <!--End Single Seminars -->
                                    <!-- Start Single Seminars -->
                                    <div class="testimonial mt--50 mt_md--40 mt_sm--40">
                                        <div class="inner d-rtl">
                                            <div class="card-info">
                                                <div class="card-thumbnail">
                                                    <img src="assets/images/testimonial/final-home--4th.png" alt="Testimonial-image">
                                                </div>
                                                <div class="card-content">
                                                    <h3 class="title">الاسم</h3>
                                                    <span class="designation">المسمى الوظيفى</span>
                                                </div>
                                            </div>
                                            <div class="card-description">
                                                <div class="title-area">
                                                    <div class="title-info text-right">
                                                        <h3 class="title">عنوان الدورة</h3>
                                                        <span class="date"><i class="far fa-clock ml-2"></i>Aug 30, 2021</span>
                                                    </div>
                                                </div>
                                                <div class="seperator"></div>
                                                <p class="discription">
                                                    لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <!--End Single Seminars -->
                                    <!-- Start Single Seminars -->
                                    <div class="testimonial mt--50 mt_md--40 mt_sm--40">
                                        <div class="inner d-rtl">
                                            <div class="card-info">
                                                <div class="card-thumbnail">
                                                    <img src="assets/images/testimonial/final-home--5th.png" alt="Testimonial-image">
                                                </div>
                                                <div class="card-content">
                                                    <h3 class="title">الاسم</h3>
                                                    <span class="designation">المسمى الوظيفى</span>
                                                </div>
                                            </div>
                                            <div class="card-description">
                                                <div class="title-area">
                                                    <div class="title-info text-right">
                                                        <h3 class="title">عنوان الدورة</h3>
                                                        <span class="date"><i class="far fa-clock ml-2"></i>Aug 30, 2021</span>
                                                    </div>
                                                </div>
                                                <div class="seperator"></div>
                                                <p class="discription">
                                                    لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <!--End Single Seminars -->
                                </div>
                            </div>

                            <a id="LinkButton2" class="rn-btn d-block text-center mt--60 btn-more" href="Courses.aspx">
                                <span>مشاهدة المزيد</span>
                            </a>
                        </div>
                        <!-- End Single Tab  -->
                        <!-- Start Single Tab  -->
                        <div class="tab-pane fade" id="Seminars" role="tabpanel" aria-labelledby="Seminars-tab">
                            <!-- Start Section -->
                            <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30 p-0">
                                <div class="rn-portfolio" data-toggle="modal" data-target="#">
                                    <div class="inner row">
                                        <div class="col-md-2">
                                            <div class="thumbnail">
                                                <a href="javascript:void(0)">
                                                    <img src="assets/images/technical_analysis/technical_analysis-3.jpg" alt="Technical Analysis Photo">
                                                </a>
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="content pt--0">
                                                <div class="category-info pb--0">
                                                    <div class="meta">
                                                        <span><i class="far fa-clock"></i>Aug 30, 2021 - 10:45 PM</span>
                                                    </div>
                                                </div>
                                                <h4 class="title">
                                                    <a href="javascript:void(0)">عنوان الندوة</a>
                                                </h4>
                                                <p class="description">
                                                    لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات .
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End Section -->
                            <!-- Start Section -->
                            <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30 p-0">
                                <div class="rn-portfolio" data-toggle="modal" data-target="#">
                                    <div class="inner row">
                                        <div class="col-md-2">
                                            <div class="thumbnail">
                                                <a href="javascript:void(0)">
                                                    <img src="assets/images/technical_analysis/technical_analysis-3.jpg" alt="Technical Analysis Photo">
                                                </a>
                                            </div>
                                        </div>
                                        <div class="col-md-10">
                                            <div class="content pt--0">
                                                <div class="category-info pb--0">
                                                    <div class="meta">
                                                        <span><i class="far fa-clock"></i>Aug 30, 2021 - 10:45 PM</span>
                                                    </div>
                                                </div>
                                                <h4 class="title">
                                                    <a href="javascript:void(0)">عنوان الندوة</a>
                                                </h4>
                                                <p class="description">
                                                    لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات .
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End Section -->

                            <a id="LinkButton2" class="rn-btn d-block text-center mt--60 btn-more" href="Seminars.aspx">
                                <span>مشاهدة المزيد</span>
                            </a>
                        </div>
                        <!-- End Single Tab  -->
                        <!-- Start Single Tab  -->
                        <div class="tab-pane fade " id="Conferences" role="tabpanel" aria-labelledby="Conferences-tab">
                            <div class="row row--25 mt--10 mt_md--10 mt_sm--10">
                                <asp:ListView ID="lvConferences" runat="server">
                                    <ItemTemplate>
                                        <!-- Start News -->
                                        <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-6 col-xl-4 col-md-6 col-12 mt--50 mt_md--30 mt_sm--30">
                                            <div class="rn-portfolio" onclick="location.href='<%# "News_Details.aspx?Id=" + Eval("Id").ToString  %>'">
                                                <div class="inner">
                                                    <div class="thumbnail">
                                                        <a href="javascript:void(0)">
                                                            <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Photo") %>' ToolTip="News Photo" />
                                                        </a>
                                                    </div>
                                                    <div class="content">
                                                        <div class="category-info">
                                                            <%--<div class="category-list">
                                        <a href="javascript:void(0)">تصنيف الخبر</a>
                                    </div>--%>
                                                            <div class="meta">
                                                                <span><i class="far fa-clock"></i><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd MMMM yyyy    hh:mm tt", "AR") %></span>
                                                            </div>
                                                        </div>
                                                        <h4 class="title">
                                                            <a href="javascript:void(0)"><%# Eval("Title") %></a>
                                                        </h4>
                                                        <p class="description"><%# Eval("Description") %></p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- End News -->
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>

                            <asp:LinkButton ID="LinkButton5" runat="server" CssClass="rn-btn d-block text-center mt--60 btn-more">
                                <span>مشاهدة المزيد</span>
                            </asp:LinkButton>
                        </div>
                        <!-- End Single Tab  -->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Conferences and Courses Area -->
    <!-- Start Technical Analysis -->
    <div class="rn-portfolio-area rn-section-gap section-separator" id="portfolio">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title text-center">
                        <span class="subtitle">Analysis Of Financial Markets</span>
                        <h2 class="title">تحليلات الاسواق المالية</h2>
                    </div>
                </div>
            </div>
            <div class="row row--25 mt--30 mt_md--30 mt_sm--30">
                <div class="col-lg-4">
                    <div class="d-flex flex-wrap align-content-start h-100">
                        <div class="position-sticky clients-wrapper sticky-top rbt-sticky-top-adjust">
                            <ul class="nav tab-navigation-button flex-column nav-pills me-3 mt--30 mt_md--30 mt_sm--30" id="v-tab" role="tablist">

                                <asp:ListView ID="lvAnlyticsCategories" runat="server">
                                    <ItemTemplate>
                                        <li class="nav-item">
                                            <a class='<%#IIf(Val(Container.DataItemIndex.ToString) = 0, "nav-link acive active", "nav-link") %>' id='<%# "v-tab-" + Container.DataItemIndex.ToString %>' data-toggle="tab" href='<%# "#v-pills-" + Container.DataItemIndex.ToString %>' role="tab" aria-selected="true"><%# Eval("Category") %></a>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>

                                <%--<li class="nav-item">
                                    <a class="nav-link" id="v-tab-1" data-toggle="tab" href="#v-pills-1" role="tab" aria-selected="true">تحليلات الاسواق العالمية</a>
                                </li>

                                <li class="nav-item">
                                    <a class="nav-link active" id="v-tab-2" data-toggle="tab" href="#v-pills-2" role="tab" aria-selected="true">تحليلات السوق المصرى</a>
                                </li>

                                <li class="nav-item">
                                    <a class="nav-link" id="v-tab-3" data-toggle="tab" href="#v-pills-3" role="tab" aria-selected="true">تحليلات السوق السعودى</a>
                                </li>--%>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="col-lg-8">
                    <div class="tab-area">
                        <div class="d-flex align-items-start">
                            <div class="tab-content w-100" id="v-tabContent">
                                <asp:ListView ID="lvCategories" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>' Visible="false"></asp:Label>
                                        <div class='<%#IIf(Val(Container.DataItemIndex.ToString) = 0, "tab-pane fade show active", "tab-pane fade") %>' id='<%# "v-pills-" + Container.DataItemIndex.ToString %>' role="tabpanel" aria-labelledby='<%#"v-tab-" + Container.DataItemIndex.ToString %>'>
                                            <asp:ListView ID="lvAnalytics" runat="server">
                                                <ItemTemplate>
                                                    <!-- Start Section -->
                                                    <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30">
                                                        <div class="rn-portfolio" data-toggle="modal" data-target="#">
                                                            <div class="inner row">
                                                                <div class="col-md-3">
                                                                    <div class="thumbnail">
                                                                        <a href="javascript:void(0)">
                                                                            <asp:Image ID="imgAnalyticsPhoto" runat="server" ImageUrl='<%# Eval("Photo") %>' ToolTip='<%# Eval("Category") %>' />
                                                                        </a>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-9">
                                                                    <div class="content pt--0">
                                                                        <div class="category-info pb--0">
                                                                            <div class="meta">
                                                                                <span><i class="far fa-clock"></i><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd MMMM yyyy    hh:mm tt", "AR") %></span>
                                                                            </div>
                                                                        </div>
                                                                        <h4 class="title">
                                                                            <a href="javascript:void(0)"><%# Eval("Title") %></a>
                                                                        </h4>
                                                                        <p class="description">
                                                                            <%# Eval("Description") %>
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- End Section -->
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                <%--  <div class="tab-pane fade" id="v-pills-1" role="tabpanel" aria-labelledby="v-tab-1">
                                    <!-- Start Section -->
                                    <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30">
                                        <div class="rn-portfolio" data-toggle="modal" data-target="#">
                                            <div class="inner row">
                                                <div class="col-md-3">
                                                    <div class="thumbnail">
                                                        <a href="javascript:void(0)">
                                                            <img src="assets/images/technical_analysis/technical_analysis-3.jpg" alt="Technical Analysis Photo">
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="content">
                                                        <div class="category-info pb--0">
                                                            <div class="meta">
                                                                <span><i class="far fa-clock"></i>Aug 30, 2021 - 10:45 PM</span>
                                                            </div>
                                                        </div>
                                                        <h4 class="title">
                                                            <a href="javascript:void(0)">عنوان التحليل الفنى</a>
                                                        </h4>
                                                        <p class="description">
                                                            لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات .
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Section -->
                                    <!-- Start Section -->
                                    <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30">
                                        <div class="rn-portfolio" data-toggle="modal" data-target="#">
                                            <div class="inner row">
                                                <div class="col-md-3">
                                                    <div class="thumbnail">
                                                        <a href="javascript:void(0)">
                                                            <img src="assets/images/technical_analysis/technical_analysis-4.jpg" alt="Technical Analysis Photo">
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="content">
                                                        <div class="category-info pb--0">
                                                            <div class="meta">
                                                                <span><i class="far fa-clock"></i>Aug 10, 2021 - 5:30 PM</span>
                                                            </div>
                                                        </div>
                                                        <h4 class="title">
                                                            <a href="javascript:void(0)">عنوان التحليل الفنى</a>
                                                        </h4>
                                                        <p class="description">
                                                            لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات .
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Section -->
                                </div>

                                <div class="tab-pane fade show active" id="v-pills-2" role="tabpanel" aria-labelledby="v-tab-2">
                                    <!-- Start Section -->
                                    <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30">
                                        <div class="rn-portfolio" onclick="location.href='Technical_Analysis_Details.aspx'">
                                            <div class="inner row">
                                                <div class="col-md-3">
                                                    <div class="thumbnail">
                                                        <a href="javascript:void(0)">
                                                            <img src="assets/images/technical_analysis/technical_analysis-1.jpg" alt="Technical Analysis Photo">
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="content">
                                                        <div class="category-info pb--0">
                                                            <div class="meta">
                                                                <span><i class="far fa-clock"></i>Aug 30, 2021 - 10:45 PM</span>
                                                            </div>
                                                        </div>
                                                        <h4 class="title">
                                                            <a href="javascript:void(0)">عنوان التحليل الفنى</a>
                                                        </h4>
                                                        <p class="description">
                                                            لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات .
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Section -->
                                    <!-- Start Section -->
                                    <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30">
                                        <div class="rn-portfolio" data-toggle="modal" data-target="#">
                                            <div class="inner row">
                                                <div class="col-md-3">
                                                    <div class="thumbnail">
                                                        <a href="javascript:void(0)">
                                                            <img src="assets/images/technical_analysis/technical_analysis-2.jpg" alt="Technical Analysis Photo">
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="content">
                                                        <div class="category-info pb--0">
                                                            <div class="meta">
                                                                <span><i class="far fa-clock"></i>Aug 10, 2021 - 5:30 PM</span>
                                                            </div>
                                                        </div>
                                                        <h4 class="title">
                                                            <a href="javascript:void(0)">عنوان التحليل الفنى</a>
                                                        </h4>
                                                        <p class="description">
                                                            لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات .
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Section -->
                                </div>

                                <div class="tab-pane fade" id="v-pills-3" role="tabpanel" aria-labelledby="v-tab-3">
                                    <!-- Start Section -->
                                    <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30">
                                        <div class="rn-portfolio" data-toggle="modal" data-target="#">
                                            <div class="inner row">
                                                <div class="col-md-3">
                                                    <div class="thumbnail">
                                                        <a href="javascript:void(0)">
                                                            <img src="assets/images/technical_analysis/technical_analysis-5.jpg" alt="Technical Analysis Photo">
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="content">
                                                        <div class="category-info pb--0">
                                                            <div class="meta">
                                                                <span><i class="far fa-clock"></i>Aug 30, 2021 - 10:45 PM</span>
                                                            </div>
                                                        </div>
                                                        <h4 class="title">
                                                            <a href="javascript:void(0)">عنوان التحليل الفنى</a>
                                                        </h4>
                                                        <p class="description">
                                                            لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات .
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Section -->
                                    <!-- Start Section -->
                                    <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30">
                                        <div class="rn-portfolio" data-toggle="modal" data-target="#">
                                            <div class="inner row">
                                                <div class="col-md-3">
                                                    <div class="thumbnail">
                                                        <a href="javascript:void(0)">
                                                            <img src="assets/images/technical_analysis/technical_analysis-6.jpg" alt="Technical Analysis Photo">
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-md-9">
                                                    <div class="content">
                                                        <div class="category-info pb--0">
                                                            <div class="meta">
                                                                <span><i class="far fa-clock"></i>Aug 10, 2021 - 5:30 PM</span>
                                                            </div>
                                                        </div>
                                                        <h4 class="title">
                                                            <a href="javascript:void(0)">عنوان التحليل الفنى</a>
                                                        </h4>
                                                        <p class="description">
                                                            لوريم ايبسوم دولار سيت أميت ,كونسيكتيتور أدايبا يسكينج أليايت,سيت دو أيوسمود تيمبور أنكايديديونتيوت لابوري ات دولار ماجنا أليكيوا . يوت انيم أد مينيم فينايم,كيواس نوستريد أكسير سيتاشن يللأمكو لابورأس نيسي يت أليكيوب أكس أيا كوممودو كونسيكيوات .
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Section -->
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <a id="lbShowMoreTechAnalysis" class="rn-btn d-block text-center mt--60 btn-more" href="Technical_Analysis.aspx">
                <span>مشاهدة المزيد</span>
            </a>
        </div>
    </div>
    <!-- End Technical Analysis -->
    <!-- Start Album Videos Area -->
    <div class="rn-blog-area rn-section-gap section-separator" id="blog" style="direction: ltr;">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="500" data-aos-once="true" class="section-title text-center">
                        <span class="subtitle">Videos</span>
                        <h2 class="title">البوم الفيديوهات</h2>
                    </div>
                </div>
            </div>
            <div class="testimonial-activation testimonial-activation-3 row row--25 mt--30 mt_md--10 mt_sm--10">
                <asp:ListView runat="server" ID="lvGallery">
                    <ItemTemplate>
                        <!-- Start Video -->
                        <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="400" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                            <div class="rn-blog" onclick="location.href='Album_Videos_Details.aspx?ID=<%# Eval("ID").ToString  %>'">
                                <div class="inner">
                                    <div class="thumbnail">
                                        <a href="javascript:void(0)">
                                            <img src='<%# Eval("MainURL").ToString.Replace("~", "")  %>' alt='<%# Eval("Description").ToString  %>'>
                                            <%--<video>
                                                <source src="assets/videos/video-1.mp4" type="video/mp4" />
                                                Your browser does not support the video tag.
                                            </video>--%>
                                        </a>
                                    </div>
                                    <div class="content">
                                        <div class="category-info">
                                            <div class="meta">
                                                <span><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd/MM/yyyy")  %><i class="far fa-clock"></i></span>
                                            </div>
                                            <div class="category-list">
                                                <a href="javascript:void(0)"><%# Eval("MediaCount").ToString  %><i class="fas fa-video ml-1"></i></a>
                                            </div>
                                        </div>
                                        <h4 class="title">
                                            <a href="javascript:void(0)"><%# Eval("Title").ToString  %>
                                            </a>
                                        </h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End Video -->
                    </ItemTemplate>
                </asp:ListView>

            </div>

            <a id="lbShowMoreVideos" class="rn-btn d-block text-center mt--60 btn-more" href="Album_Videos.aspx">
                <span>مشاهدة المزيد</span>
            </a>
        </div>
    </div>
    <!-- ENd Album Videos Area -->
    <!-- Start Album Photos Area -->
    <div class="rn-blog-area rn-section-gap section-separator" id="blog" style="direction: ltr;">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="500" data-aos-once="true" class="section-title text-center">
                        <span class="subtitle">Photos</span>
                        <h2 class="title">البوم الصور</h2>
                    </div>
                </div>
            </div>
            <div class="testimonial-activation testimonial-activation-3 row row--25 mt--30 mt_md--10 mt_sm--10">
                <asp:ListView runat="server" ID="lvPhotos">
                    <ItemTemplate>
                        <!-- Start Photo -->
                        <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="400" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                            <div class="rn-blog" onclick="location.href='Album_Photos_Details.aspx?ID=<%# Eval("ID").ToString  %>'">
                                <div class="inner">
                                    <div class="thumbnail">
                                        <a href="javascript:void(0)">
                                            <img src='<%# Eval("MainURL").ToString.Replace("~", "")  %>' alt='<%# Eval("Description").ToString  %>'>
                                        </a>
                                    </div>
                                    <div class="content">
                                        <div class="category-info">
                                            <div class="meta">
                                                <span><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd/MM/yyyy")  %><i class="far fa-clock"></i></span>
                                            </div>
                                            <div class="category-list">
                                                <a href="javascript:void(0)"><%# Eval("MediaCount").ToString  %><i class="fas fa-images ml-1"></i></a>
                                            </div>
                                        </div>
                                        <h4 class="title">
                                            <a href="javascript:void(0)"><%# Eval("Title").ToString  %>
                                            </a>
                                        </h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End Photo -->
                    </ItemTemplate>
                </asp:ListView>

                <%-- <!-- Start Photo -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="400" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" onclick="location.href='Album_Photos_Details.aspx'">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <img src="assets/images/blog/blog-01.jpg" alt="Personal Portfolio Images">
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="meta">
                                        <span>Aug 30, 2021 - 10:45 PM<i class="far fa-clock"></i></span>
                                    </div>
                                    <div class="category-list">
                                        <a href="javascript:void(0)">4<i class="fas fa-images ml-1"></i></a>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Photo -->
                <!-- Start Photo -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="600" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" data-toggle="modal" data-target="#">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <img src="assets/images/blog/blog-02.jpg" alt="Personal Portfolio Images">
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="meta">
                                        <span>Aug 5, 2021 - 2:15 PM<i class="far fa-clock"></i></span>
                                    </div>
                                    <div class="category-list">
                                        <a href="javascript:void(0)">9<i class="fas fa-images ml-1"></i></a>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Photo -->
                <!-- Start Photo -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="800" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" data-toggle="modal" data-target="#">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <img src="assets/images/blog/blog-03.jpg" alt="Personal Portfolio Images">
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="meta">
                                        <span>May 17, 2021 - 3:00 PM<i class="far fa-clock"></i></span>
                                    </div>
                                    <div class="category-list">
                                        <a href="javascript:void(0)">7<i class="fas fa-images ml-1"></i></a>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Photo -->
                <!-- Start Photo -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="800" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" data-toggle="modal" data-target="#">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <img src="assets/images/blog/blog-03.jpg" alt="Personal Portfolio Images">
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="meta">
                                        <span>May 17, 2021 - 3:00 PM<i class="far fa-clock"></i></span>
                                    </div>
                                    <div class="category-list">
                                        <a href="javascript:void(0)">7<i class="fas fa-images ml-1"></i></a>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Photo -->
                <!-- Start Photo -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="800" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" data-toggle="modal" data-target="#">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <img src="assets/images/blog/blog-03.jpg" alt="Personal Portfolio Images">
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="meta">
                                        <span>May 17, 2021 - 3:00 PM<i class="far fa-clock"></i></span>
                                    </div>
                                    <div class="category-list">
                                        <a href="javascript:void(0)">7<i class="fas fa-images ml-1"></i></a>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Photo -->--%>
            </div>

            <a id="LinkButton4" class="rn-btn d-block text-center mt--60 btn-more" href="Album_Photos.aspx">
                <span>مشاهدة المزيد</span>
            </a>
        </div>
    </div>
    <!-- ENd Album Photos Area -->
</asp:Content>
