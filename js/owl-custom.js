 jQuery(window).load(function(){
                jQuery("#animation-slide").owlCarousel({
                    items: 1,
                    loop: true,
                    autoplay: true,
                    dots: true,
                    nav: true,
                    autoplayTimeout: 5000,
                    navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
                    animateIn: "zoomIn",
                    animateOut: "fadeOutDown",
                    autoplayHoverPause: false,
                    touchDrag: true,
                    mouseDrag: true
                });
                jQuery("#animation-slide").on("translate.owl.carousel", function () {
                    jQuery(this).find(".owl-item .slide-text > *").removeClass("fadeInUp animated").css("opacity","0");
                    jQuery(this).find(".owl-item .slide-img").removeClass("fadeInRight animated").css("opacity","0");
                });          
                jQuery("#animation-slide").on("translated.owl.carousel", function () {
                    jQuery(this).find(".owl-item.active .slide-text > *").addClass("fadeInUp animated").css("opacity","1");
                    jQuery(this).find(".owl-item.active .slide-img").addClass("fadeInRight animated").css("opacity","1");
                });
            });


$(document).ready(function() {
    $("#products-slider").owlCarousel({
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        nav: true,
        dots: false,
        loop: true,
        autoplay: true,
        items:4,
        responsive : {
        0 : {
            items: 1
        },
        500 : {
            items: 2
        },
        992 : {
            items: 3
        },
        1024 : {
            items: 4
        }
        },
        autoplayTimeout: 3000
    });

     $("#projects-slider").owlCarousel({
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        nav: true,
        dots: false,
        loop: true,
        autoplay: true,
        items:3,
        responsive : {
        0 : {
            items: 1
        },
        500 : {
            items: 2
        },
        992 : {
            items: 3
        },
        1024 : {
            items: 3
        }
        },
        autoplayTimeout: 3000
    });


});






$(document).ready(function () {

    var sync1 = $("#sync1");
    var sync2 = $("#sync2");
    var slidesPerPage = 4;
    var syncedSecondary = true;

    sync1.owlCarousel({
        items: 1,
        slideSpeed: 2000,
        nav: true,
        autoplay: true,
        video: true,
        lazyLoad: true,
        dots: true,
        loop: true,
        responsiveRefreshRate: 200,
        navText: ['<svg width="100%" height="100%" viewBox="0 0 11 20"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M9.554,1.001l-8.607,8.607l8.607,8.606"/></svg>', '<svg width="100%" height="100%" viewBox="0 0 11 20" version="1.1"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M1.054,18.214l8.606,-8.606l-8.606,-8.607"/></svg>'],
    }).on('changed.owl.carousel', syncPosition);

    sync2
        .on('initialized.owl.carousel', function () {
            sync2.find(".owl-item").eq(0).addClass("current");
        })
        .owlCarousel({
            items: slidesPerPage,
            dots: true,
            nav: true,
            smartSpeed: 200,
            slideSpeed: 500,
            slideBy: slidesPerPage,
            responsiveRefreshRate: 100
        }).on('changed.owl.carousel', syncPosition2);

    function syncPosition(el) {
        var count = el.item.count - 1;
        var current = Math.round(el.item.index - (el.item.count / 2) - .5);

        if (current < 0) {
            current = count;
        }
        if (current > count) {
            current = 0;
        }

        //end block

        sync2
            .find(".owl-item")
            .removeClass("current")
            .eq(current)
            .addClass("current");
        var onscreen = sync2.find('.owl-item.active').length - 1;
        var start = sync2.find('.owl-item.active').first().index();
        var end = sync2.find('.owl-item.active').last().index();

        if (current > end) {
            sync2.data('owl.carousel').to(current, 100, true);
        }
        if (current < start) {
            sync2.data('owl.carousel').to(current - onscreen, 100, true);
        }
    }

    function syncPosition2(el) {
        if (syncedSecondary) {
            var number = el.item.index;
            sync1.data('owl.carousel').to(number, 100, true);
        }
    }

    sync2.on("click", ".owl-item", function (e) {
        e.preventDefault();
        var number = $(this).index();
        sync1.data('owl.carousel').to(number, 300, true);
    });
});


