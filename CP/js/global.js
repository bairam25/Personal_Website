///// <reference path="jquery-1.7.1-vsdoc.js" />
$('html').addClass('js'); //http://www.learningjquery.com/2008/10/1-way-to-avoid-the-flash-of-unstyled-content



function loadQuickSearch() {
    $(document).ready(function () {
        $(".searchinput").focus(function () {
            if ($(this).val() == $(this).attr('title'))
                $(this).val('');
        }).autocomplete({
            source: function (request, response) {
                var lang = $("meta[name='content-account']").attr('content');
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "/Services/PredictiveSearch.asmx/FetchResult",
                    data: "{'keyword':'" + request.term.replace(/'/gi, "") + "','lang':'" + lang + "','pageId':'" + currentPageId + "'}",
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (result) {
                        alert("Due to unexpected errors we were unable to load data: " + result.responseText);
                    }
                });
            },
            minLength: 2
        });
    });
}

function loadAccountDropdown() {
    $('.header .account').click(function (e) {
        $('.header .account ul').fadeToggle(400, function () {
            var $ul = $(this);
            if ($ul.css('display') == 'block') {
                var hideEvent = function (e) {
                    if ($(e.target).parents().index($ul) <= 0) {
                        $ul.hide();
                        $('body').unbind('click', hideEvent);
                    }
                };
                $('body').bind('click', hideEvent);
            }
        });
    });
}



function loadPagePartNavigation() {
    var $container = $('#multipage-container');
    if ($container.length > 0) {
        var $slides = $container.children('div');
        var $pagers = $container.siblings('.page-nav');
        $slides.each(function (i) {
            var $slide = $(this);
            var $navItem = $('<a href="#">' + $slide.attr("data-page-name") + '</a>')
            .bind('click', function (e) {
                e.preventDefault();
                $slide.show().siblings().hide();
                $pagers.children().removeClass('activeSlide');
                $pagers.each(function () {
                    $(this).children().eq(i).addClass('activeSlide');
                });
                window.scroll(0, findPos($container[0]));
            });
            if (i == 0)
                $navItem.addClass('activeSlide');
            $navItem.appendTo($pagers);
        });
        $slides.hide().first().show();
    }
}




//Do stuff when document is ready (not all image properties might have loaded into DOM yet in FF...)
$().ready(function () {
    loadPagePartNavigation();
    loadQuickSearch();
    loadAccountDropdown();
    loadManagement();
    loadImageSlideDown();
    loadLightBox();
    loadVideoPlayer();
    loadReportStartSlideShow();
    fixFinnishDashBreaks();
    loadHighcharts();
});

jQuery(document).ready(function ($) {
    var filetypes = /\.(zip|exe|pdf\?epsaccount\=en|pdf\?epsaccount\=fi|pdf\?epsaccount\=sv|doc*|xls*|ppt*|mp3|pdf)$/i;
    var baseHref = '';
    if (jQuery('base').attr('href') != undefined)
        baseHref = jQuery('base').attr('href');
    jQuery('a').each(function () {
        var href = jQuery(this).attr('href');
        if (href && (href.match(/^https?\:/i)) && (!href.match(document.domain))) {
            jQuery(this).click(function () {
                var extLink = href.replace(/^https?\:\/\//i, '');
                _gaq.push(['_trackEvent', 'External', 'Click', extLink]);
                if (jQuery(this).attr('target') != undefined && jQuery(this).attr('target').toLowerCase() != '_blank') {
                    setTimeout(function () { location.href = href; }, 200);
                    return false;
                }
            });
        }
        else if (href && href.match(/^mailto\:/i)) {
            jQuery(this).click(function () {
                var mailLink = href.replace(/^mailto\:/i, '');
                _gaq.push(['_trackEvent', 'Email', 'Click', mailLink]);
            });
        }
        else if (href && href.match(filetypes)) {
            jQuery(this).click(function () {
                var extension = (/[.]/.exec(href)) ? /[^.]+$/.exec(href) : undefined;
                var filePath = href;
                _gaq.push(['_trackEvent', 'Download', 'Click-' + extension, filePath]);
                if (jQuery(this).attr('target') != undefined && jQuery(this).attr('target').toLowerCase() != '_blank') {
                    setTimeout(function () { location.href = baseHref + href; }, 200);
                    return false;
                }
            });
        }
    });
});


//Do stuff when DOM tree is ready occurs later
$(window).load(function () {

    //fade in slideshow
    $('#stage .introimageslide .image').fadeIn(2000);

    //or fade in single introimage
    $('#stage .introimage').fadeIn(2000);

});