$(document).ready(function () {
    $(".Modern-Slider").slick({
        autoplay: true,
        autoplaySpeed: 10000,
        speed: 600,
        slidesToShow: 1,
        slidesToScroll: 1,
        pauseOnHover: false,
        dots: true,
        pauseOnDotsHover: true,
        cssEase: 'linear',
        // fade:true,
        draggable: true,
        prevArrow: '<button class="PrevArrow" onclick="return false;"><i class="fas fa-chevron-left"></i></button>',
        nextArrow: '<button class="NextArrow" onclick="return false;"><i class="fas fa-chevron-right"></i></button>',
    });
});