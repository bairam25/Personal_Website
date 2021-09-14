// JavaScript Document
(function($) {
    "use strict";

/*================================
   Preloader
================================= */
   $(window).load(function () {
        $(".green-preloader").delay(1000).fadeOut(600);
    });

/*================================
   Scroll-to-top
================================= */
    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scroll-to-top').fadeIn();
        } else {
            $('.scroll-to-top').fadeOut();
        }
    });

    $('.scroll-to-top').on('click', function (e) {
        e.preventDefault();
        $('html, body').animate({ scrollTop: 0 }, 800);
    });

/*================================
   Content-Starting
================================= */

$(document).ready(function () {

// Smooth-Mouse-scroll
    $("html").easeScroll();  
// Wow-Animation-scroll
    new WOW().init();

// Text-area-Resize
    $('textarea').css("resize", "vertical");

//=====================================
 //    Service-Filter
 //=====================================
        $(function () {
            $('ul.filter-ul li').on('click', function () {
                $(this).parent().find('li.active').removeClass('active');
                $(this).addClass('active');
            });
        });

        $(".filter-button").click(function () {
            var value = $(this).attr('data-filter');
            if (value == "all") {
                $('.filter').show('1000');
            }
            else {
                $(".filter").not('.' + value).hide('3000');
                $('.filter').filter('.' + value).show('3000');
            }
        });
        if ($(".filter-button").removeClass("active")) {
            $(this).removeClass("active");
        }
        $(this).addClass("active");

//=====================================
 // 
 //=====================================



 });

})(jQuery);