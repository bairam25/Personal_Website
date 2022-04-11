<%@ Page Title="التفاصيل" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Content_Details.aspx.vb" Inherits="Content_Details" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <style>
        .vbox-container .vbox-content img.figlio {
            width: 70%;
        }
    </style>

    <!-- Start News Area -->
    <asp:Label ID="lblRes" runat="server"></asp:Label>
    <asp:Label ID="lblContentId" runat="server" Visible="false"></asp:Label>
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
                                                        <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="400" data-aos-once="true">
                                                            <div class="venobox" href='<%# Eval("Photo").ToString.Replace("~", "")  %>' data-gall="venue-gallery" data-title='<%# Eval("Title").ToString   %>'>
                                                                <asp:Image ID="imgPhoto" runat="server" CssClass="w-100" ImageUrl='<%# Eval("Photo") %>' ToolTip='<%# Eval("Title") %>' />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="text-content">
                                                    <h3>
                                                        <span>
                                                            <div class="d-flex justify-content-between">

                                                                <div class="meta">
                                                                    <span><i class="far fa-clock ml-2"></i><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd MMMM yyyy", "AR") %> <%# PublicFunctions.DateFormat(Eval("CreatedDate").ToString, "hh:mm tt", "AR") %></span>
                                                                </div>
                                                            </div>
                                                        </span>
                                                        <%# Eval("Title") %>
                                                    </h3>
                                                    <%# Eval("Description") %>
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
                closeColor: '#fff',
                titleattr: 'data-title'
            });
        });
    </script>
    <!-- End Modal Photo -->

</asp:Content>
