<%@ Page Language="VB" AutoEventWireup="false" CodeFile="About.aspx.vb" Inherits="About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!--
		Basic
	-->
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>عنى</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="description" content="vCard & Resume Template" />
    <meta name="keywords" content="vcard, resposnive, resume, personal, card, cv, cards, portfolio" />
    <meta name="author" content="beshleyua" />

    <!--
		Load Fonts
	-->
    <link href="https://fonts.googleapis.com/css?family=Poppins:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

    <!--
		Load CSS
	-->
    <link rel="stylesheet" href="assets/fontawsome-5.4.1/css/all.min.css" />
    <link rel="stylesheet" href="assets/css/basic.css" />
    <link rel="stylesheet" href="assets/css/layout.css" />
    <link rel="stylesheet" href="assets/css/blogs.css" />
    <link rel="stylesheet" href="assets/css/ionicons.css" />
    <link rel="stylesheet" href="assets/css/magnific-popup.css" />
    <link rel="stylesheet" href="assets/css/animate.css" />
    <link rel="stylesheet" href="assets/css/owl.carousel.css" />

    <!--
		Background Gradient
	-->
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

            <!--
			Container
		-->
            <div class="container opened" data-animation-in="fadeInRight" data-animation-out="fadeOutRight">

                <!--
				Header
			-->
                <header class="header">
                    <!-- header profile -->
                    <div class="profile">
                        <div class="title">السيد حسين</div>
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
                            <li>
                                <a href="#contacts-card">
                                    <span class="fas fa-phone"></span>
                                    <span class="link">اتصل</span>
                                </a>
                            </li>
                        </ul>
                    </div>

                </header>

                <!--
				Card - Started
			-->
                <div class="card-started" id="home-card">

                    <!--
					Profile
				-->
                    <div class="profile no-photo">

                        <!-- profile image -->
                        <div class="slide" style="background-image: url(assets/images/slider/banner-01.png);"></div>

                        <!-- profile titles -->
                        <div class="title">السيد حسين</div>
                        <div class="subtitle subtitle-typed">
                            <div class="typing-title">
                                <p>خبير أسواق</p>
                                <p>المال والأعمال</p>
                            </div>
                        </div>

                        <!-- profile socials -->
                        <div class="social">
                            <a target="_blank" href="#"><span class="fab fa-facebook"></span></a>
                            <a target="_blank" href="#"><span class="fab fa-instagram"></span></a>
                            <a target="_blank" href="#"><span class="fab fa-linkedin"></span></a>
                        </div>

                        <!-- profile buttons -->
                        <div class="lnks">
                            <a href="#" class="lnk">
                                <span class="text">تحميل السيرة الذاتية</span>
                                <span class="fas fa-paperclip vertical-middle mr-1"></span>
                            </a>
                            <a href="#" class="lnk discover">
                                <span class="text">اتصل بي</span>
                                <span class="fas fa-envelope vertical-middle mr-1"></span>
                            </a>
                        </div>

                    </div>

                </div>

                <!-- 
				Card - About
			-->
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
                                        <p>
                                            أنا ريان أدلارد ، مصمم ويب من الولايات المتحدة الأمريكية ، كاليفورنيا. لدي خبرة غنية في
تصميم موقع على شبكة الإنترنت وبناء والتخصيص ، كما أنني جيدة في وورد.
أنا أحب أن أتحدث معك عن موقعنا الفريد.
                                        </p>
                                    </div>
                                </div>
                                <div class="col col-d-12 col-t-12 col-m-12 border-line-v">
                                    <div class="info-list">
                                        <ul>
                                            <li><strong>عمر </strong> 30</li>
                                            <li><strong>إقامة </strong> مصر</li>
                                            <li><strong>الحساب الخاص </strong> متاح</li>
                                            <li><strong>عنوان </strong> مصر</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="clear"></div>
                            </div>

                        </div>

                        <!--
						Services
					-->
                        <div class="content services">

                            <!-- title -->
                            <div class="title">خدماتي</div>

                            <!-- content -->
                            <div class="row service-items border-line-v">

                                <!-- service item -->
                                <div class="col col-d-6 col-t-6 col-m-12 border-line-h">
                                    <div class="service-item">
                                        <div class="icon"><span class="fas fa-code"></span></div>
                                        <div class="name">تطوير الشبكة</div>
                                        <p>
                                            موقع إلكتروني حديث وجاهز يساعدك
الوصول إلى كل التسويق الخاص بك.
                                        </p>
                                    </div>
                                </div>

                                <!-- service item -->
                                <div class="col col-d-6 col-t-6 col-m-12 border-line-h">
                                    <div class="service-item">
                                        <div class="icon"><span class="fas fa-music"></span></div>
                                        <div class="name">الكتابة الموسيقية</div>
                                        <p>
                                            نسخ الموسيقى والنسخ والترتيب والتكوين.
                                        </p>
                                    </div>
                                </div>

                                <!-- service item -->
                                <div class="col col-d-6 col-t-6 col-m-12">
                                    <div class="service-item">
                                        <div class="icon"><span class="fas fa-volume-up"></span></div>
                                        <div class="name">إعلان</div>
                                        <p>
                                            وتشمل الخدمات الإعلانية التلفزيون والراديو والطباعة والبريد والويب.
                                        </p>
                                    </div>
                                </div>

                                <!-- service item -->
                                <div class="col col-d-6 col-t-6 col-m-12">
                                    <div class="service-item">
                                        <div class="icon"><span class="fas fa-gamepad"></span></div>
                                        <div class="name">تطوير اللعبة</div>
                                        <p>
                                            تطوير الروبوت المحمول لا تنسى وفريدة من نوعها ، ألعاب ios.
                                        </p>
                                    </div>
                                </div>
                                <div class="clear"></div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--
				Card - Resume
			-->
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
                                <div class="col col-d-6 col-t-6 col-m-12 border-line-v pt-0">
                                    <div class="resume-items">
                                        <div class="resume-item border-line-h active">
                                            <div class="date">2013 - حتى الآن</div>
                                            <div class="name">مدير فني</div>
                                            <div class="company">Facebook Inc.</div>
                                            <p>
                                                التعاون مع فرق التطوير والإبداع في تنفيذ الأفكار.
                                            </p>
                                        </div>
                                        <div class="resume-item border-line-h">
                                            <div class="date">2011 - 2012</div>
                                            <div class="name">المطور الأمامي</div>
                                            <div class="company">Google Inc.</div>
                                            <p>
                                                مراقبة الجوانب الفنية لعملية التسليم الأمامية للعديد من المشاريع.
                                            </p>
                                        </div>
                                        <div class="resume-item">
                                            <div class="date">2009 - 2010</div>
                                            <div class="name">مطور خبير</div>
                                            <div class="company">Abc Inc.</div>
                                            <p>
                                                تحسين أداء موقع الويب باستخدام أحدث التقنيات.
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <!-- education -->
                                <div class="col col-d-6 col-t-6 col-m-12 border-line-v pt-0">
                                    <div class="resume-items">
                                        <div class="resume-item border-line-h">
                                            <div class="date">2006 - 2008</div>
                                            <div class="name">جامعة الفن</div>
                                            <div class="company">نيويورك</div>
                                            <p>
                                                درجة البكالوريوس في علوم الكمبيوتر معهد ABC التقني ، جيفرسون ، ميسوري
                                            </p>
                                        </div>
                                        <div class="resume-item border-line-h">
                                            <div class="date">2005 - 2006</div>
                                            <div class="name">دورة البرمجة</div>
                                            <div class="company">باريس</div>
                                            <p>
                                                الدورات الدراسية - Git و WordPress و Javascript و iOS و Android.
                                            </p>
                                        </div>
                                        <div class="resume-item">
                                            <div class="date">2004 - 2005</div>
                                            <div class="name">دورة تصميم الويب</div>
                                            <div class="company">لندن</div>
                                            <p>
                                                تحويل تصميمات Photoshop إلى صفحات الويب باستخدام HTML و CSS وجافا سكريبت
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="clear"></div>
                            </div>

                        </div>
                    </div>
                </div>

                <!--
				Card - Contacts
			-->
                <div class="card-inner contacts" id="contacts-card">
                    <div class="card-wrap">

                        <!--
						Conacts Info
					-->
                        <div class="content contacts">

                            <!-- title -->
                            <div class="title">ابقى على تواصل</div>

                            <!-- content -->
                            <div class="row">
                                <div class="col col-d-12 col-t-12 col-m-12 border-line-v">
                                    <div class="map">
                                        <img class="w-100" src="assets/images/map.JPG" />
                                    </div>
                                    <div class="info-list">
                                        <ul>
                                            <li><strong>عنوان</strong> القاهرة، مصر</li>
                                            <li><strong>البريد الإلكتروني</strong> admin@example.com</li>
                                            <li><strong>هاتف</strong> 01234567890</li>
                                            <li><strong>حسابهم الخاص</strong> متاح</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="clear"></div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
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
