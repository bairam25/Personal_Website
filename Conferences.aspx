<%@ Page Title="المؤتمرات" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Conferences.aspx.vb" Inherits="News" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Start News Area -->
    <div class="rn-portfolio-area rn-section-gap mt--90" id="News">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title text-center">
                        <span class="subtitle">Conferences</span>
                        <h2 class="title">المؤتمرات</h2>
                    </div>
                </div>
            </div>

            <div class="row row--25 mt--10 mt_md--10 mt_sm--10">
                <asp:Label ID="lblRes" runat="server"></asp:Label>
                <asp:ListView ID="lvConferences" runat="server">
                    <ItemTemplate>
                        <!-- Start News -->
                        <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-6 col-xl-4 col-md-6 col-12 mt--50 mt_md--30 mt_sm--30">
                            <div class="rn-portfolio" onclick="location.href='<%# "Content_Details.aspx?Id=" + Eval("Id").ToString  %>'">
                                <div class="inner">
                                    <div class="thumbnail">
                                        <a href='<%# "Content_Details.aspx?Id=" + Eval("Id").ToString  %>'>
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
                                            <a href='<%# "Content_Details.aspx?Id=" + Eval("Id").ToString  %>'><%# Eval("Title") %></a>
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
        </div>
    </div>
    <!-- End News Area -->
</asp:Content>
