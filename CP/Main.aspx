<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Main.aspx.vb" Inherits="c_panel_Main" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="keywords" content="aldealer.com, aldealer">
    <meta name="description" content="your company is an...">

    <title>وحدة التحكم</title>
    <link rel="shortcut icon" href="../images/logo/favi.png">
    <!-- Bootstrap -->
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />

    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <!-- Resource style -->
    <link href="css/themify-icons.css" rel="stylesheet" />
    <link href="fontawsome-5.4.1/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/cpmenu.css" />
    <link rel="stylesheet" href="css/m-scroll.css" />
    <link rel="stylesheet" href="css/main-c-scroll.css" />
    <link rel="stylesheet" href="css/cpcustom.css" />

    <!-- Google Analytics -->
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-126696003-14"></script>
    <!-- End Google Analytics -->

    <style>
        html, body, form {
            height: 100%;
        }

        body {
            padding: 0 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:Label ID="lblRes" CssClass="res-label-info " runat="server"></asp:Label>
        <!--============================= Navigation =============================-->
        <div class="navbar navbar-inverse" role="navigation">
            <div class="navbar-header">
                <a class="sidebar-toggle" onclick="animenuFunction(this)">
                    <div class="ani-menu">
                        <div class="bar3"></div>
                        <div class="bar2"></div>
                        <div class="bar1"></div>
                    </div>
                </a>
                <a class="nav-link toggle-fullscreen" href="#">
                    <i class="ti-fullscreen"></i>
                </a>
                <div class="logo-title">
                    <h1>
                        <a href="#">السيد حسين</a>
                        <a href="#">
                            <span>وحدة التحكم في بيانات الموقع</span>
                        </a>
                    </h1>
                </div>
               
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-icons">
                    <span class="sr-only">Toggle navbar</span>
                    <i class="ti-menu"></i>
                </button>
                <button type="button" class="navbar-toggle offcanvas">
                    <span class="sr-only">Toggle navigation</span>
                    <i class="ti-menu"></i>
                </button>
            </div>
            <div class="">
            </div>

            <ul class="nav navbar-nav navbar-right collapse" id="navbar-icons">
                <li>
                    <span style="position: relative;">
                        <i class="ti-power-off icon-logout"></i>
                        <asp:Button ID="lbLogOut" runat="server" OnClick="Logout" CssClass="mod-icon" Text="تسجيل الخروج"></asp:Button>
                    </span>
                </li>

                <li class="user dropdown">
                    <a class="dropdown-toggle" data-toggle="dropdown">
                        
                        <asp:Label ID="lblUserGroup" runat="server" Visible="false"></asp:Label>
                        <span>
                            <abbr>مرحبا</abbr>
                            <asp:Label ID="lblFullName" runat="server">Admin</asp:Label>

                        </span>
                    </a>
                </li>
            </ul>
        </div>
        <!-- /navbar -->
        <!-- Page container -->
        <div class="page-container">
            <!--============================ Sidebar =============================-->

            <div class="sidebar w-scroll" id="content-3">
                <div class="sidebar-content">
                    <ul class="navigation" id="UlMenu" runat="server">
                        <li class="active">
                            <a href="News.aspx" onclick="LoadFrame(this);return false;" ><i class="fa fa-newspaper-o"></i><span>الأخبار</span></a>
                        </li>
                         <li>
                            <a href="Seminars.aspx" onclick="LoadFrame(this);return false;" ><i class="fa fa-chalkboard-teacher"></i><span>الندوات</span></a>
                        </li>
                        <li>
                            <a href="Courses.aspx" onclick="LoadFrame(this);return false;" ><i class="fa fa-book"></i><span>الدورات</span></a>
                        </li>
                         <li>
                            <a href="Conferences.aspx" onclick="LoadFrame(this);return false;" ><i class="fa fa-solar-panel"></i><span>المؤتمرات</span></a>
                        </li>
                        <li>
                            <a href="Categories.aspx" onclick="LoadFrame(this);return false;" ><i class="fa fa-chart-pie"></i><span>تصنيف التحليلات</span></a>
                        </li>
                         <li>
                            <a href="Analytics.aspx" onclick="LoadFrame(this);return false;" ><i class="fa fa-chart-bar"></i><span>تحليلات الاسواق المالية</span></a>
                        </li>
                        <li>
                            <a href="Gallery.aspx" onclick="LoadFrame(this);return false;" ><i class="fa fa-images"></i><span>معرض الصور</span></a>
                        </li>
                         <li>
                            <a href="VedioGallery.aspx" onclick="LoadFrame(this);return false;" ><i class="fa fa-video"></i><span>معرض الفيديو</span></a>
                        </li>
                          <li>
                            <a href="About.aspx" onclick="LoadFrame(this);return false;" ><i class="fa fa-address-card"></i><span>السيرة الذاتية</span></a>
                        </li>
                    </ul>

                </div>
            </div>


            <!-- /sidebar -->
            <!--============================ Page-Content =============================-->
            <!-- Page content Start-->
            <div class="page-content" style="overflow-x: hidden;">
                <div class="container-fluid p0" style="height: 100%;">

                    <div class="row" style="height: 100%;">
                        <div class="col-md-12" style="height: 100%;">
                            <div class="widget" style="height: 100%;">
                                <div class="w-body w-no-hf" style="height: 100%;">
                                    <iframe id="MyFrame"  name="myiframe" class="iframe1-f" src="News.aspx"></iframe>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
            <!-- Page content End-->
            <!--============================ Site-Footer =============================-->
            <div class="clearfix"></div>
            <footer class="site-footer">
                <div class="row">
                    <div class="col-md-12 col-xs-12 text-center">
                        <span class="site-footer-legal">2022 <span>©</span> <a href="#">جميع الحقوق محفوظة</a></span>
                    </div>
                </div>
            </footer>

        </div>
    </form>

    <!--============================ js files =============================-->
    <script src="../js/jquery-min.js"></script>
    <script src="bootstrap4/js/bootstrap.min.js"></script>
    <script src="js/m-scroll.js"></script>
      <script src="JSCode/CPMain.js" type="text/javascript"></script>
    <script src="js/fullscreen.js"></script>
    <script src="js/cpcustom.js"></script>
  
</body>
</html>



