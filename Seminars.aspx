<%@ Page Title="الندوات" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Seminars.aspx.vb" Inherits="Seminars" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Start Technical Analysis -->
    <div class="rn-portfolio-area rn-section-gap mt--90" id="portfolio">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title text-center">
                        <span class="subtitle">Seminars</span>
                        <h4 class="title">الندوات</h4>
                    </div>
                </div>
            </div>

            <div class="row row--25 mt--30 mt_md--30 mt_sm--30">
                <div class="col-lg-12">
                    <div class="tab-area">
                        <div class="row">
                            <asp:Label ID="lblRes" runat="server"></asp:Label>
                            <asp:ListView ID="lvSeminars" runat="server">
                                <ItemTemplate>
                                    <div data-aos="fade-up" data-aos-delay="100" data-aos-once="true" class="col-lg-12 col-xl-12 col-md-12 col-12 mt--30 mt_md--30 mt_sm--30 p-0">
                                        <div class="rn-portfolio w-100" data-toggle="modal" data-target="#">
                                            <div class="inner row">
                                                <div class="col-md-2">
                                                    <div class="thumbnail">
                                                        <a href='<%# "Content_Details.aspx?Id=" + Eval("Id").ToString  %>'>
                                                            <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Photo") %>' ToolTip='<%# Eval("Title") %>' />
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="col-md-10">
                                                    <div class="content pt--0">
                                                        <div class="category-info pb--0">
                                                            <div class="meta">
                                                                <span><i class="far fa-clock"></i><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd MMMM yyyy", "AR") %> <%# PublicFunctions.DateFormat(Eval("CreatedDate").ToString, "hh:mm tt", "AR") %></span>
                                                            </div>
                                                        </div>
                                                        <h4 class="title">
                                                            <a href='<%# "Content_Details.aspx?Id=" + Eval("Id").ToString  %>'><%# Eval("Title") %></a>
                                                        </h4>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- End Section -->
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Technical Analysis -->
</asp:Content>
