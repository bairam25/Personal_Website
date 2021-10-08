<%@ Page Language="VB" AutoEventWireup="false" CodeFile="About.aspx.vb" Inherits="About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Basic -->
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>عنى</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="description" content="vCard & Resume Template" />
    <meta name="keywords" content="vcard, resposnive, resume, personal, card, cv, cards, portfolio" />
    <meta name="author" content="beshleyua" />

    <!-- Load Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Poppins:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!-- Load CSS -->
    <link rel="stylesheet" href="assets/fontawsome-5.4.1/css/all.min.css" />
    <link rel="stylesheet" href="assets/css/basic.css" />
    <link rel="stylesheet" href="assets/css/layout.css" />
    <link rel="stylesheet" href="assets/css/blogs.css" />
    <link rel="stylesheet" href="assets/css/ionicons.css" />
    <link rel="stylesheet" href="assets/css/magnific-popup.css" />
    <link rel="stylesheet" href="assets/css/animate.css" />
    <link rel="stylesheet" href="assets/css/owl.carousel.css" />

    <!-- Background Gradient -->
    <link rel="stylesheet" href="assets/css/gradient.css" />

    <!--
		Template New-Skin
	-->
    <link rel="stylesheet" href="assets/css/new-skin/new-skin.css" />

    <!--
		Template RTL
	-->
    <link rel="stylesheet" href="assets/css/rtl-new.css" />

    <!--
		Template Dark
	-->
    <link rel="stylesheet" href="assets/css/template-dark/dark.css" />


    <!--[if lt IE 9]>
	<script src="http://css3-mediaqueries-js.googlecode.com/svn/trunk/css3-mediaqueries.js"></script>
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->

    <!--
		Favicons
	-->
    <link rel="shortcut icon" type="image/x-icon" href="assets/images/favicon.ico" />

    <script>
        window.dataLayer = window.dataLayer || [];

        function gtag() {
            dataLayer.push(arguments);
        }
        gtag('js', new Date());

        gtag('config', 'UA-125314689-11');
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="page new-skin">

            <!-- preloader -->
            <div class="preloader">
                <div class="centrize full-width">
                    <div class="vertical-center">
                        <div class="spinner">
                            <div class="double-bounce1"></div>
                            <div class="double-bounce2"></div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- background -->
            <div class="background gradient">
                <ul class="bg-bubbles">
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
            </div>
            <asp:Label Text="" ID="lblRes" runat="server" />
            <asp:Repeater runat="server" ID="rpAbout">
                <ItemTemplate>
                    <!-- 	Container 		-->
                    <div class="container opened" data-animation-in="fadeInRight" data-animation-out="fadeOutRight">

                        <!--				Header			-->
                        <header class="header">
                            <!-- header profile -->
                            <div class="profile">
                                <div class="title"><%# Eval("Name").ToString  %></div>
                                <div class="subtitle subtitle-typed">
                                    <div class="typing-title">
                                        <p>خبير أسواق</p>
                                        <p>المال والأعمال</p>
                                    </div>
                                </div>
                            </div>

                            <!-- menu btn -->
                            <a href="Home.aspx" class="menu-btn"><i class="fas fa-home"></i></a>

                            <!-- menu -->
                            <div class="top-menu">
                                <ul>
                                    <li class="active">
                                        <a href="#about-card">
                                            <span class="fas fa-user"></span>
                                            <span class="link">عني</span>
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#resume-card">
                                            <span class="fas fa-address-card"></span>
                                            <span class="link">السيرة الذاتية</span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </header>

                        <!--				Card - Started			-->
                        <div class="card-started" id="home-card">

                            <!--
					Profile
				-->
                            <div class="profile no-photo">

                                <!-- profile image -->
                                <div class="slide" style='background-image: url(<%# Eval("Photo").ToString.replace("~/","")  %>);'></div>
 
                                <!-- profile titles -->
                                <div class="title"><%# Eval("Name").ToString  %></div>
                                <div class="subtitle subtitle-typed">
                                    <div class="typing-title">
                                        <p>خبير أسواق</p>
                                        <p>المال والأعمال</p>
                                    </div>
                                </div>

                                <!-- profile socials -->
                                <div class="social">
                                    <a target="_blank" href="<%# Eval("FacebookURL").ToString  %>"><span class="fab fa-facebook"></span></a>
                                    <a target="_blank" href="<%# Eval("InstagramURL").ToString  %>"><span class="fab fa-instagram"></span></a>
                                    <a target="_blank" href="<%# Eval("LinkedInURL").ToString  %>"><span class="fab fa-linkedin"></span></a>
                                </div>

                                <!-- profile buttons -->
                                <div class="lnks">
                                    <a href="<%# Eval("CVURL").ToString  %>" class="lnk" download>
                                        <span class="text">تحميل السيرة الذاتية</span>
                                        <span class="fas fa-paperclip vertical-middle mr-1"></span>
                                    </a>
                                    <a href="Contact.aspx" class="lnk discover">
                                        <span class="text">اتصل بي</span>
                                        <span class="fas fa-envelope vertical-middle mr-1"></span>
                                    </a>
                                </div>

                            </div>

                        </div>

                        <!-- 				Card - About			-->
                        <div class="card-inner animated active" id="about-card">
                            <div class="card-wrap">

                                <!-- 
						About 
					-->
                                <div class="content about">

                                    <!-- title -->
                                    <div class="title">عني</div>

                                    <!-- content -->
                                    <div class="row">
                                        <div class="col col-d-12 col-t-12 col-m-12 border-line-v">
                                            <div class="text-box">
                                                <p><%# Eval("Bio").ToString  %></p>
                                            </div>
                                        </div>
                                        <div class="col col-d-12 col-t-12 col-m-12 border-line-v">
                                            <div class="info-list">
                                                <ul>
                                                    <li>
                                                        <asp:Panel runat="server" ID="pnlAge" Visible='<%# Not String.IsNullOrEmpty(Eval("Age").ToString)  %>'><strong>عمر </strong><%# Eval("Age").ToString  %></asp:Panel>
                                                    </li>
                                                    <li>
                                                        <asp:Panel runat="server" ID="pnlCountry" Visible='<%# Not String.IsNullOrEmpty(Eval("Country").ToString)  %>'><strong>إقامة </strong><%# Eval("Country").ToString  %></asp:Panel>
                                                    </li>
                                                    <li>
                                                        <asp:Panel runat="server" ID="pnlEmail" Visible='<%# Not String.IsNullOrEmpty(Eval("Email").ToString)  %>'><strong>الحساب الخاص </strong><%# Eval("Email").ToString  %></asp:Panel>
                                                    </li>
                                                    <li>
                                                        <asp:Panel runat="server" ID="pnlAddress" Visible='<%# Not String.IsNullOrEmpty(Eval("Address").ToString)  %>'><strong>عنوان </strong><%# Eval("Address").ToString  %></asp:Panel>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="clear"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!--				Card - Resume			-->
                        <div class="card-inner" id="resume-card">
                            <div class="card-wrap">

                                <!--
						Resume
					-->
                                <div class="content resume">

                                    <!-- title -->
                                    <div class="title">السيرة الذاتية</div>

                                    <!-- content -->
                                    <div class="row">

                                        <!-- experience -->
                                        <div class="col col-d-12 col-t-12 col-m-12 border-line-v pt-0">
                                            <div class="resume-items">
                                               <%# Eval("experience").ToString  %>
                                            </div>
                                        </div>
                                        <div class="clear"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
    <!--
		jQuery Scripts
	-->
    <script src="assets/js/jquery.min.js "></script>
    <script src="assets/js/jquery.validate.js "></script>
    <script src="assets/js/jquery.magnific-popup.js "></script>
    <script src="assets/js/imagesloaded.pkgd.js "></script>
    <script src="assets/js/isotope.pkgd.js "></script>
    <script src="assets/js/jquery.slimscroll.js "></script>
    <script src="assets/js/owl.carousel.js "></script>
    <script src="assets/js/typed.js "></script>
    <script src="https://use.fontawesome.com/8da76d029b.js "></script>

    <!--
		Main Scripts
	-->
    <script src="assets/js/scripts.js "></script>
</body>
</html>
