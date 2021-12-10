<%@ Page Title="عرض الالبوم" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Album_Photos_Details.aspx.vb" Inherits="Album_Photos_Details" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <asp:Label Text="" ID="lblRes" runat="server" />

    <!-- Start Album Photos Area -->
    <div class="rn-blog-area rn-section-gap mt--90" id="Photos">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="500" data-aos-once="true" class="section-title text-center">
                        <%--<span class="subtitle">Photos</span>--%>
                        <h2 class="title">
                            <asp:Label Text="text" runat="server" ID="lblAlbumTitle" /></h2>
                    </div>
                </div>
            </div>
            <div class="row row--25 mt--30 mt_md--10 mt_sm--10">
                <asp:ListView runat="server" ID="lvGallery">
                    <ItemTemplate>
                        <!-- Start Photo -->
                        <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="400" data-aos-once="true" class="col-lg-6 col-xl-4 mt--30 col-md-6 col-sm-12 col-12 mt--30">
                            <div class="rn-blog venobox" href='<%# Eval("Path").ToString.Replace("~", "")  %>' data-gall="venue-gallery">
                                <div class="inner">
                                    <div class="thumbnail">
                                        <a href="javascript:void(0)">
                                            <img src='<%# Eval("Path").ToString.Replace("~", "")  %>' alt='<%# Eval("Description").ToString   %>'>
                                        </a>
                                    </div>
                                    <div class="content">
                                        <h4 class="title">
                                            <a href="javascript:void(0)"><%# Eval("Title").ToString   %>
                                                <i class="fas fa-external-link-alt"></i>
                                            </a>
                                        </h4>
                                          <%# Eval("Description").ToString  %>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End Photo -->
                        
                    </ItemTemplate>
                </asp:ListView>

                 
            </div>
        </div>
    </div>
    <!-- ENd Album Photos Area -->

    <!-- Modal Photo Start -->
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
                                <h3 class="text-center"><span id="lblImgTitle"></span></h3>
                            </div>
                            <!-- End of .text-content -->
                        </div>
                        <div class="col-lg-12">
                            <img id="imgModal" alt="news modal" class="img-fluid modal-feat-img">
                        </div>
                    </div>
                    <!-- End of .row Body-->
                </div>
            </div>
        </div>
    </div>
    <script src="assets/js/vendor/jquery.js"></script>
    <script src="lib/venobox/venobox.min.js"></script>
    <script>    
        //function show(s, t) {
        //    let img = document.getElementById("imgModal")
        //    let lblImgTitle = document.getElementById("lblImgTitle")
        //    img.src = s;
        //    lblImgTitle.innerText = t;
        //}

        // Initialize Venobox
        jQuery(document).ready(function ($) {
            $('.venobox').venobox({
                bgcolor: '',
                overlayColor: 'rgba(6, 12, 34, 0.85)',
                closeBackground: '',
                closeColor: '#fff'
            });
        });
    </script>
    <!-- End Modal Photo -->
</asp:Content>
