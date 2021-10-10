<%@ Page Title="البوم الفيديوهات" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Album_Videos.aspx.vb" Inherits="Album_Videos" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Start Album Videos Area -->
    <div class="rn-blog-area rn-section-gap mt--90" id="Videos">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="500" data-aos-once="true" class="section-title text-center">
                        <span class="subtitle">Videos</span>
                        <h2 class="title">البوم الفيديوهات</h2>
                    </div>
                </div>
            </div>
            <div class="row row--25 mt--30 mt_md--10 mt_sm--10">
                <asp:Label Text="" ID="lblRes" runat="server" />
                <asp:ListView runat="server" ID="lvGallery">
                    <ItemTemplate>
                        <!-- Start Photo -->
                        <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="400" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                            <div class="rn-blog">
                                <div class="inner">
                                    <div class="thumbnail">
                                        <a href='Album_Videos_Details.aspx?ID=<%# Eval("ID").ToString  %>'>
                                            <img src='<%# Eval("MainURL").ToString.Replace("~", "")  %>' alt='<%# Eval("Description").ToString  %>'>
                                        </a>
                                    </div>
                                    <div class="content">
                                        <div class="category-info">
                                            <div class="category-list">
                                                <a href='Album_Videos_Details.aspx?ID=<%# Eval("ID").ToString  %>'><i class="fas fa-video ml-1"></i><%# Eval("MediaCount").ToString  %></a>
                                            </div>
                                            <div class="meta">
                                                <span><i class="far fa-clock"></i><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd/MM/yyyy")  %></span>
                                            </div>
                                        </div>
                                        <h4 class="title">
                                            <a href='Album_Videos_Details.aspx?ID=<%# Eval("ID").ToString  %>'><%# Eval("Title").ToString  %></a>
                                        </h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End Photo -->
                    </ItemTemplate>
                </asp:ListView>
                <%--<!-- Start Video -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="400" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" onclick="location.href='Album_Videos_Details.aspx'">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <video>
                                        <source src="assets/videos/video-1.mp4" type="video/mp4" />
                                        Your browser does not support the video tag.
                                    </video>
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="category-list">
                                        <a href="javascript:void(0)"><i class="fas fa-video ml-1"></i>6</a>
                                    </div>
                                    <div class="meta">
                                        <span><i class="far fa-clock"></i>Aug 30, 2021 - 10:45 PM</span>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                                <i class="fas fa-external-link-alt"></i>
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Video -->
                <!-- Start Video -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="600" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" data-toggle="modal" data-target="#">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <video>
                                        <source src="assets/videos/video-2.mp4" type="video/mp4" />
                                        Your browser does not support the video tag.
                                    </video>
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="category-list">
                                        <a href="javascript:void(0)"><i class="fas fa-video ml-1"></i>12</a>
                                    </div>
                                    <div class="meta">
                                        <span><i class="far fa-clock"></i>Aug 5, 2021 - 2:15 PM</span>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                                <i class="fas fa-external-link-alt"></i>
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Video -->
                <!-- Start Video -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="800" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" data-toggle="modal" data-target="#">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <video>
                                        <source src="assets/videos/video-3.mp4" type="video/mp4" />
                                        Your browser does not support the video tag.
                                    </video>
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="category-list">
                                        <a href="javascript:void(0)"><i class="fas fa-video ml-1"></i>4</a>
                                    </div>
                                    <div class="meta">
                                        <span><i class="far fa-clock"></i>May 17, 2021 - 3:00 PM</span>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                                <i class="fas fa-external-link-alt"></i>
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Video -->
                <!-- Start Video -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="800" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" data-toggle="modal" data-target="#">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <video>
                                        <source src="assets/videos/video-3.mp4" type="video/mp4" />
                                        Your browser does not support the video tag.
                                    </video>
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="category-list">
                                        <a href="javascript:void(0)"><i class="fas fa-video ml-1"></i>4</a>
                                    </div>
                                    <div class="meta">
                                        <span><i class="far fa-clock"></i>May 17, 2021 - 3:00 PM</span>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                                <i class="fas fa-external-link-alt"></i>
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Video -->
                <!-- Start Video -->
                <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="800" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                    <div class="rn-blog" data-toggle="modal" data-target="#">
                        <div class="inner">
                            <div class="thumbnail">
                                <a href="javascript:void(0)">
                                    <video>
                                        <source src="assets/videos/video-3.mp4" type="video/mp4" />
                                        Your browser does not support the video tag.
                                    </video>
                                </a>
                            </div>
                            <div class="content">
                                <div class="category-info">
                                    <div class="category-list">
                                        <a href="javascript:void(0)"><i class="fas fa-video ml-1"></i>4</a>
                                    </div>
                                    <div class="meta">
                                        <span><i class="far fa-clock"></i>May 17, 2021 - 3:00 PM</span>
                                    </div>
                                </div>
                                <h4 class="title">
                                    <a href="javascript:void(0)">عنوان الالبوم
                                                <i class="fas fa-external-link-alt"></i>
                                    </a>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Video -->--%>
            </div>
        </div>
    </div>
    <!-- ENd Album Videos Area -->
</asp:Content>
