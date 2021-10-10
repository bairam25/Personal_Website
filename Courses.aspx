<%@ Page Title="الدورات" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Courses.aspx.vb" Inherits="Courses" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Start Technical Analysis -->
    <div class="rn-portfolio-area rn-section-gap mt--90" id="portfolio">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title text-center">
                        <span class="subtitle">Courses</span>
                        <h2 class="title">الدورات</h2>
                    </div>
                </div>
            </div>

            <div class="row row--25 mt--30 mt_md--30 mt_sm--30">
                <div class="col-lg-12">
                    <div class="tab-area">
                        <div class="row">
                            <asp:Label ID="lblRes" runat="server"></asp:Label>
                            <!-- Start Single Seminars -->
                            <asp:ListView ID="lvCourses" runat="server">
                                <ItemTemplate>
                                    <div class="testimonial mt--50 mt_md--40 mt_sm--40 col-md-12 p-0">
                                        <div class="inner d-rtl">
                                            <div class="card-info">
                                                <div class="card-thumbnail">
                                                    <asp:Image ID="imgPhoto" runat="server" ImageUrl='<%# Eval("Photo") %>' ToolTip="Person Photo" />
                                                </div>
                                                <div class="card-content">
                                                    <h3 class="title"><%# Eval("Name") %></h3>
                                                    <span class="designation"><%# Eval("Category") %></span>
                                                </div>
                                            </div>
                                            <div class="card-description w-100">
                                                <div class="title-area">
                                                    <div class="title-info text-right">
                                                        <h3 class="title"><%# Eval("Title") %></h3>
                                                        <span class="date"><i class="far fa-clock ml-2"></i><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd MMMM yyyy    hh:mm tt", "AR") %></span>
                                                    </div>
                                                    <asp:LinkButton Visible="false" ID="lbMoreDetails" runat="server" CssClass="rn-btn" href='<%# "Content_Details.aspx?Id=" + Eval("Id").ToString  %>'>
                                                        <span>قراءة المزيد</span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="seperator"></div>
                                                <p class="discription">
                                                    <%# Eval("Description") %>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                            <!--End Single Seminars -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End Technical Analysis -->
</asp:Content>
