﻿<%@ Page Title="البوم الفيديوهات" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Album_Videos.aspx.vb" Inherits="Album_Videos" %>

<asp:Content ID="ContentCSS" ContentPlaceHolderID="StyleSheet" runat="Server">
    <style>
        #divYoutube iframe {
            width: 100%;
            height: 490px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Start Album Videos Area -->
    <div class="rn-blog-area rn-section-gap mt--90" id="Videos">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div data-aos="fade-up" data-aos-duration="500" data-aos-delay="500" data-aos-once="true" class="section-title text-center">
                       <%-- <span class="subtitle">Videos</span>--%>
                        <h4 class="title"> الفيديو</h4>
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
                                        <a data-toggle="modal"  
                                            lang='<%# Eval("MainURL").ToString.Replace("~", "")  %>'
                                            data-count='<%# Eval("MediaCount").ToString %>'
                                            data-id='<%# Eval("ID").ToString %>'
                                            data-title='<%# Eval("Title").ToString %>'
                                            onclick="OpenAlbum(this);">                                         
                                            <img src='<%# Eval("MainURL").ToString.Replace("~", "")  %>' alt='<%# Eval("Title").ToString  %>'>
                                        </a>
                                    </div>
                                    <div class="content">
                                        <div class="category-info">
                                            <div class="category-list">
                                                <a href="javascript:void(0)"><i class="fas fa-video ml-1"></i><%# Eval("MediaCount").ToString  %></a>
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
            </div>
        </div>
    </div>
    <!-- ENd Album Videos Area -->

    <!-- Modal Video Start -->
    <div class="modal fade" id="previewImage" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick='closeImgPopup();'>
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
                        <div class="col-lg-12" id="divYoutube">
                            <%--<img id="imgModal" class="img-fluid modal-feat-img">--%>
                            <!-- Modal Content (The Image) -->
                            <img class="previewImage-content" id="img01" />
                            <video id="videoPreview" width="100%" height="490px" autoplay="autoplay" controls="controls" muted=""></video>
                        </div>
                    </div>
                    <!-- End of .row Body-->
                </div>
            </div>
        </div>
    </div>
    <!-- End Modal Video -->

    <script>     
        //function OpenAlbum(sender) {
        //    const VedioID = $(sender).data("id");
        //    const MediaCount = $(sender).data("count");
        //    const VedioTitle = $(sender).data("title");
        //    if (MediaCount > 1) {
        //        document.location.href = "Album_Videos_Details.aspx?ID=" + VedioID;
        //    } else {
        //        $('#previewImage').modal('show');
        //        ImagePreview(sender.lang, VedioTitle);
        //    }         
        //}
    </script>
</asp:Content>
