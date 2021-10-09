<%@ Page Title="التفاصيل" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="Content_Details.aspx.vb" Inherits="Content_Details" %>

<asp:Content ID="Content" ContentPlaceHolderID="PageContent" runat="Server">
    <!-- Start News Area -->
    <asp:Label ID="lblRes" runat="server"></asp:Label>
    <asp:Label ID="lblContentId" runat="server" Visible ="false" ></asp:Label>
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
                                                           <asp:Image ID="imgPhoto" runat="server" CssClass="w-100" ImageUrl='<%# Eval("Photo") %>' ToolTip="Photo" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="text-content">
                                                    <h3>
                                                        <span>
                                                            <div class="d-flex justify-content-between">

                                                                <div class="meta">
                                                                    <span><i class="far fa-clock ml-2"></i><%# PublicFunctions.DateFormat(Eval("Date").ToString, "dd MMMM yyyy    hh:mm tt", "AR") %></span>
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
    <!-- End News Area -->
</asp:Content>
