window.dataLayer = window.dataLayer || [];
function gtag() { dataLayer.push(arguments); }
gtag('js', new Date());

gtag('config', 'UA-126696003-14');


/* Fullscreen */
$('.toggle-fullscreen').click(function () {
    $(document).toggleFullScreen();
});
$('.w-scroll').perfectScrollbar();
//===== Menu-Icon-Change =====//
function animenuFunction(x) {
    x.classList.toggle("ani-menu-change");
}



function LoadFrame(sender) {
    try {
        var url = sender.href;
        document.getElementById('MyFrame').src = url;
        window.scrollTo(0, 0);
    }
    catch (error) {
    }
}


//setInterval(function () {
//    document.getElementById("lbLogOut").click();
//}, 3600000);


function changeIcon() {
    if ($(".navigation li").hasClass("down")) {
        $(".navigation li").removeClass("down");
        $(".navigation li").removeClass("actiive");
    }
    else {
        $(".navigation li").addClass("down");
    }
}