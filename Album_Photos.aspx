<%@ Page Title="البوم الصور" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Album_Photos.aspx.vb" Inherits="Album_Photos" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Start Album Photos Area -->
    <div class="rn-blog-area rn-section-gap mt--90" id="Photos">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="500" data-aos-once="true" class="section-title text-center">
                        <span class="subtitle">Photos</span>
                        <h2 class="title">البوم الصور</h2>
                    </div>
                </div>
            </div>
            <div class="row row--25 mt--30 mt_md--10 mt_sm--10">
                <asp:Label Text="" ID="lblRes" runat="server" />
                <asp:ListView runat="server" ID="lvGallery">
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
                                            <div class="category-list">
                                                <a href="javascript:void(0)"><i class="fas fa-images ml-1"></i><%# Eval("MediaCount").ToString  %></a>
                                            </div>
                                            <div class="meta">
                                                <span><i class="far fa-clock"></i><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd/MM/yyyy")  %></span>
                                            </div>
                                        </div>
                                        <h4 class="title">
                                            <a href="javascript:void(0)"><%# Eval("Title").ToString  %>
                                                <i class="fas fa-external-link-alt"></i>
                                            </a>
                                        </h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End Photo -->
                    </ItemTemplate>
                </asp:ListView>

                <%--   <!-- Start Photo -->
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
                                    <div class="category-list">
                                        <a href="javascript:void(0)"><i class="fas fa-images ml-1"></i>9</a>
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
                                    <div class="category-list">
                                        <a href="javascript:void(0)"><i class="fas fa-images ml-1"></i>7</a>
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
                                    <div class="category-list">
                                        <a href="javascript:void(0)"><i class="fas fa-images ml-1"></i>7</a>
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
                                    <div class="category-list">
                                        <a href="javascript:void(0)"><i class="fas fa-images ml-1"></i>7</a>
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
                <!-- End Photo -->--%>
            </div>

            <div class="tbl-bot-panel-right">
                <div class="row">
                    <ul class="pagination">
                        <li>
                            <asp:DataPager ID="dplvGallery" class="pagination" runat="server" PagedControlID="lvGallery" PageSize="6" style="width: 100%; display: inline-flex;">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Link"
                                        ShowFirstPageButton="true" FirstPageText="<i class='ti-angle-double-left'></i>"
                                        ShowPreviousPageButton="true" PreviousPageText="<i class='ti-angle-left'></i>"
                                        ShowLastPageButton="false" ShowNextPageButton="false" />

                                    <asp:NumericPagerField ButtonType="link" RenderNonBreakingSpacesBetweenControls="false" NextPreviousButtonCssClass="hidedots" />

                                    <asp:NextPreviousPagerField ButtonType="Link"
                                        ShowNextPageButton="true" NextPageText="<i class='ti-angle-right'></i>"
                                        ShowLastPageButton="true" LastPageText="<i class='ti-angle-double-right'></i>"
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
    <!-- ENd Album Photos Area -->

    <%--    <!-- Modal Photo Start -->
    <div class="modal fade" id="ModalPhoto" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true"><i data-feather="x"></i></span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="text-content">
                                <h3 class="text-center">عنوان الصورة</h3>
                            </div>
                            <!-- End of .text-content -->
                        </div>
                        <div class="col-lg-12">
                            <img src="assets/images/blog/blog-big-01.jpg" alt="news modal" class="img-fluid modal-feat-img">
                        </div>
                    </div>
                    <!-- End of .row Body-->
                </div>
            </div>
        </div>
    </div>
    <!-- End Modal Photo -->--%>
</asp:Content>
