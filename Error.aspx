<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Error.aspx.vb" Inherits="ErrorPage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
    <!-- Favicon -->
    <link rel="shortcut icon" type="image/x-icon" href="assets/images/favicon.png" />

    <style>
        @import url("https://fonts.googleapis.com/css2?family=Open+Sans:wght@800&family=Roboto:wght@100;300&display=swap");

        :root {
            --button: #b3b3b3;
            --button-color: #0a0a0a;
            --shadow: #000;
            --bg: #737373;
            --header: #7a7a7a;
            --color: #fafafa;
            --lit-header: #e6e6e6;
            --speed: 2s;
        }

        * {
            box-sizing: border-box;
            transform-style: preserve-3d;
        }

        @property --swing-x {
            initial-value: 0;
            inherits: false;
            syntax: '<integer>';
        }

        @property --swing-y {
            initial-value: 0;
            inherits: false;
            syntax: '<integer>';
        }

        body {
            min-height: 100vh;
            display: flex;
            font-family: 'Roboto', sans-serif;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            background: var(--bg);
            color: var(--color);
            perspective: 1200px;
            margin: 0;
            direction: rtl;
        }

        a {
            text-transform: uppercase;
            text-decoration: none;
            background: #ff014f;
            color: #fff;
            padding: 1rem 4rem;
            border-radius: 4rem;
            font-size: 1rem;
            letter-spacing: 0.05rem;
            font-weight: 600;
        }

        p {
            font-weight: 100;
        }

        h1 {
            -webkit-animation: swing var(--speed) infinite alternate ease-in-out;
            animation: swing var(--speed) infinite alternate ease-in-out;
            font-size: clamp(5rem, 40vmin, 20rem);
            font-family: 'Open Sans', sans-serif;
            margin: 0;
            margin-bottom: 1rem;
            letter-spacing: 1rem;
            transform: translate3d(0, 0, 0vmin);
            --x: calc(50% + (var(--swing-x) * 0.5) * 1%);
            background: radial-gradient(#ff014f, #ff014f 45%) var(--x) 100%/200% 200%;
            -webkit-background-clip: text;
            color: transparent;
        }

            h1:after {
                -webkit-animation: swing var(--speed) infinite alternate ease-in-out;
                animation: swing var(--speed) infinite alternate ease-in-out;
                content: "404";
                position: absolute;
                top: 0;
                left: 0;
                color: var(--shadow);
                filter: blur(1.5vmin);
                transform: scale(1.05) translate3d(0, 12%, -10vmin) translate(calc((var(--swing-x, 0) * 0.05) * 1%), calc((var(--swing-y) * 0.05) * 1%));
            }

        .cloak {
            animation: swing var(--speed) infinite alternate-reverse ease-in-out;
            height: 100%;
            width: 100%;
            transform-origin: 50% 30%;
            transform: rotate(calc(var(--swing-x) * -0.25deg));
            background: radial-gradient(40% 40% at 50% 42%, transparent, #000 35%);
        }

        .cloak__wrapper {
            position: fixed;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
            overflow: hidden;
        }

        .cloak__container {
            height: 250vmax;
            width: 250vmax;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
        }

        .info {
            text-align: center;
            line-height: 1.5;
            max-width: clamp(300rem, 90vmin, 25rem);
            margin: 0 1rem;
        }

            .info > p {
                margin-bottom: 3rem;
            }

        @-webkit-keyframes swing {
            0% {
                --swing-x: -100;
                --swing-y: -100;
            }

            50% {
                --swing-y: 0;
            }

            100% {
                --swing-y: -100;
                --swing-x: 100;
            }
        }

        @keyframes swing {
            0% {
                --swing-x: -100;
                --swing-y: -100;
            }

            50% {
                --swing-y: 0;
            }

            100% {
                --swing-y: -100;
                --swing-x: 100;
            }
        }
    </style>
</head>
<body>
    <h1>404</h1>
    <div class="cloak__wrapper">
        <div class="cloak__container">
            <div class="cloak"></div>
        </div>
    </div>
    <div class="info">
       <%-- <h2>لا يمكننا العثور على تلك الصفحة</h2>--%>
         <h2>خطأ</h2>
         <p style="margin-bottom: 1rem;">
           رجاء التوصل مع الدعم الفني .
        </p>
        <%--<p style="margin-bottom: 1rem;">
            للأسف الصفحة التي تبحث عنها لا يمكن العثور عليها. قد يكون غير متاح مؤقتًا أو تم نقله أو لم يعد موجودًا.
        </p>
        <p>
            تحقق من عنوان URL الذي أدخلته بحثًا عن أي أخطاء وحاول مرة أخرى. بدلاً من ذلك ، ابحث عن كل ما هو مفقود أو ألق نظرة على بقية موقعنا.
        </p>--%>
        <a href="../Home.aspx" target="_blank">الرئيسية</a>
    </div>
</body>
</html>
