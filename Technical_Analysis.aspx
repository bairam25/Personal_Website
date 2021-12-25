<%@ Page Title="تحليلات الاسواق المالية" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Technical_Analysis.aspx.vb" Inherits="Technical_Analysis" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Start Technical Analysis -->
    <div class="rn-portfolio-area rn-section-gap mt--90" id="portfolio">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title text-center">
                        <span class="subtitle">Analysis Of Financial Markets</span>
                        <h4 class="title">تحليلات الاسواق المالية</h4>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblRes" runat="server"></asp:Label>
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
                                                        <div class="rn-portfolio w-100" data-toggle="modal" data-target="#">
                                                            <div class="inner row">
                                                                <div class="col-md-3">
                                                                    <div class="thumbnail">
                                                                        <a href='<%# "Content_Details.aspx?Id=" + Eval("Id").ToString  %>'>
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
                                                                            <a  href='<%# "Content_Details.aspx?Id=" + Eval("Id").ToString  %>'><%# Eval("Title") %></a>
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
                                    </ItemTemplate>
                                </asp:ListView>
                   
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Technical Analysis -->
</asp:Content>
