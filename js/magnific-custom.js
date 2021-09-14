/* ================================================== */
/* Magnific pop-up
================================================== */
/* ---- our work gallery ---- */
$('.mag-pop, .mag-pop1').magnificPopup({
    delegate: 'a.zoom',
    type: 'image',
    fixedContentPos: false,
    removalDelay: 300,
    mainClass: 'mfp-fade',
    gallery: {
        enabled: true,
        preload: [0, 2]
    }
});
/* ---- popup image ---- */
$('.popup-img').magnificPopup({
    type: 'image',
    removalDelay: 300,
    mainClass: 'mfp-fade'
});

$('.file-iframe-popup').magnificPopup({
    type: 'iframe'
});

/* ---- popup video ---- */
$('.popup-youtube, .popup-vimeo, .popup-gmaps, .mag-vid').magnificPopup({
    delegate: 'a.zoom-vid',
    disableOn: 700,
    type: 'iframe',
    mainClass: 'mfp-fade',
    removalDelay: 160,
    preloader: false,
    fixedContentPos: false
});

