



/*================================
  Navigation         
================================= */

//== Wrapping content inside .page-content ==//
$('.page-content').wrapInner('<div class="page-content-inner"></div>');
//== Applying offcanvas class ==//
$(document).on('click', '.offcanvas', function () {
    $('body').toggleClass('offcanvas-active');
});



//===== Default navigation =====//

$('.navigation').find('li.active').parents('li').addClass('active');
$('.navigation').find('li').not('.active').has('ul').children('ul').addClass('hidden-ul');
$('.navigation').find('li').has('ul').children('a').parent('li').addClass('has-ul');


$(document).on('click', '.sidebar-toggle', function (e) {
    e.preventDefault();

    $('body').toggleClass('sidebar-narrow');

    if ($('body').hasClass('sidebar-narrow')) {
        $('.navigation').children('li').children('ul').css('display', '');

        $('.sidebar-content').hide().delay().queue(function () {
            $(this).show().addClass('animated fadeIn').clearQueue();
        });
    }

    else {
        $('.navigation').children('li').children('ul').css('display', 'none');
        $('.navigation').children('li.active').children('ul').css('display', 'block');

        $('.sidebar-content').hide().delay().queue(function () {
            $(this).show().addClass('animated fadeIn').clearQueue();
        });
    }
});


$('.navigation').find('li').has('ul').children('a').on('click', function (e) {
    e.preventDefault();

    if ($('body').hasClass('sidebar-narrow')) {
        $(this).parent('li > ul li').not('.disabled').toggleClass('active').children('ul').slideToggle(250);
        $(this).parent('li > ul li').not('.disabled').siblings().removeClass('active').children('ul').slideUp(250);
    }

    else {
        $(this).parent('li').not('.disabled').toggleClass('active').children('ul').slideToggle(250);
        $(this).parent('li').not('.disabled').siblings().removeClass('active').children('ul').slideUp(250);
    }
});



//===== Panel Options (collapsing, closing) =====//

/* Collapsing */
$('[data-panel=collapse]').click(function (e) {
    e.preventDefault();
    var $target = $(this).parent().parent().next('div');
    if ($target.is(':visible')) {
        $(this).children('i').removeClass('icon-arrow-up9');
        $(this).children('i').addClass('icon-arrow-down9');
    }
    else {
        $(this).children('i').removeClass('icon-arrow-down9');
        $(this).children('i').addClass('icon-arrow-up9');
    }
    $target.slideToggle(200);
});

/* Closing */
$('[data-panel=close]').click(function (e) {
    e.preventDefault();
    var $panelContent = $(this).parent().parent().parent();
    $panelContent.slideUp(200).remove(200);
});


//===== Hiding sidebar =====//

/*$('.sidebar-toggle').click(function () {
  $('.page-container').toggleClass('sidebar-hidden');
});*/
//===== Disabling main navigation links =====//

$('.navigation .disabled a, .navbar-nav > .disabled > a').click(function (e) {
    e.preventDefault();
});

//===== Toggling active class in accordion groups =====//

$('.panel-trigger').click(function (e) {
    e.preventDefault();
    $(this).toggleClass('active');
});


$(".navigation li").on("click", function () {
    $(".navigation li").removeClass("active");
    $(this).addClass("active");
});



//=====================================
//  Responsive Tab
//=====================================

var a = {
    accordionOn: ["xs"]
};
$.fn.responsiveTabs = function (e) {
    var t = $.extend({}, a, e),
        s = "";
    return $.each(t.accordionOn, function (a, e) {
        s += " accordion-" + e
    }), this.each(function () {
        var a = $(this),
            e = a.find("> li > a"),
            t = $(e.first().attr("href")).parent(".tab-content"),
            i = t.children(".tab-pane");
        a.add(t).wrapAll('<div class="responsive-tabs-container" />');
        var n = a.parent(".responsive-tabs-container");
        n.addClass(s), e.each(function (a) {
            var t = $(this),
                s = t.attr("href"),
                i = "",
                n = "",
                r = "";
            t.parent("li").hasClass("active") && (i = " active"), 0 === a && (n = " first"), a === e.length - 1 && (r = " last"), t.clone(!1).addClass("accordion-link" + i + n + r).insertBefore(s)
        });
        var r = t.children(".accordion-link");
        e.on("click", function (a) {
            a.preventDefault();
            var e = $(this),
                s = e.parent("li"),
                n = s.siblings("li"),
                c = e.attr("href"),
                l = t.children('a[href="' + c + '"]');
            s.hasClass("active") || (s.addClass("active"), n.removeClass("active"), i.removeClass("active"), $(c).addClass("active"), r.removeClass("active"), l.addClass("active"))
        }), r.on("click", function (t) {
            t.preventDefault();
            var s = $(this),
                n = s.attr("href"),
                c = a.find('li > a[href="' + n + '"]').parent("li");
            s.hasClass("active") || (r.removeClass("active"), s.addClass("active"), i.removeClass("active"), $(n).addClass("active"), e.parent("li").removeClass("active"), c.addClass("active"))
        })
    })
}

$('.responsive-tabs').responsiveTabs({
    accordionOn: ['xs', 'sm']
});


//=====================================
//  
//=====================================


$("li, a, input").tooltip();
$('.tgl').tooltip();
$('.c-check').tooltip();
$('.custom-file-container__image-preview').tooltip();
$('.custom-file-container__custom-file__custom-file-control').tooltip();
$('textarea').css("resize", "vertical");


