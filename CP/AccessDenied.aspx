<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AccessDenied.aspx.vb" Inherits="AccessDenied" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Access Denied</title>
    <link rel="shortcut icon" href="images/logo/favi.png" />
    <link href="css/style_En.css" rel="stylesheet" />
    <link href="css/homepage.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/themify-icons.css" />
    <link href="css/custome.css" rel="stylesheet" />
    <link href="css/animate.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Libre+Franklin:100,200,300,400,500,600,700,800,900" rel="stylesheet" />
    <style>
       body {
            background: transparent !important;
            overflow-y: auto;
            margin-bottom: 25px !important;
            font-family: 'Libre Franklin', sans-serif !important;
        }
        
        .accessDeniedContiner {
          background: #fff;
    border: 1px solid #f3f3f3;
    width: 100%;
    max-width: 60%;
    margin: auto;
    margin-top: 5%;
    padding: 40px 20px;
    text-align: center;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.05);
    border-radius: 0px;
        }
       .accessDeniedContiner h1 {
                font-size: 35px;
    line-height: inherit;
    margin: 0px;
    color: #015AA2;
    padding-bottom: 5px;
    text-transform: uppercase;
        }
        .infoIcon {
            font-size:100px;
            color:#226bba;
        }
       .accessDeniedContiner p {           
    font-size: 15px;
    margin-top: 8px;
        }
        a {
            color:#105fb5;
        }
        @media screen and (max-width: 480px) {
         .accessDeniedContiner {
          max-width: 80%;
        }
         .accessDeniedContiner h1 {
          font-size: 30px;
        } 
        }
    </style>
</head>
<body>
    

     <form id="form2" runat="server">
    <div class="accessDeniedContiner animated fadeInDown">
        <header class="animated zoomIn" style="animation-delay:.5s;">
            <%--<i class="fa-info-circle fa infoIcon"></i>--%>
        </header>
        <div class="col-md-12 col-xs-12">
            <img src="images/accessDenied.ico" class="mb15" style="width: 215px;" />
        </div> 
         <div class="col-md-12 col-xs-12" style="text-align:center;">
              <h1 class="animated fadeInUp" style="animation-delay:.7s; text-align:center"> Access Denied!</h1>
             <p class="animated fadeInUp"  style="text-align: center; margin-top: 0; margin-bottom: 15px; color: #777;">
                 This URL is valid but you are not authorised for this content
             </p>
            
             </div>
        <a href="../Home.aspx" target="_blank" class="animated fadeInUp btn-main btn-blue" style="animation-delay:1.5s;">Back To Dashboards<i class="ti-back-left"></i></a>
    </div>
    </form>

</body>
</html>
