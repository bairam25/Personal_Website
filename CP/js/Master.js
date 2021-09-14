/************************************/
// Created By : Mostafa Abdelghffar
// Create Date : 21/5/2015 01:00 PM
// Last Updated By : Mostafa Abdelghffar
// Last Update Date : 26/5/2015 11:30 AM
// Description : This file contains all javaScript functions in Header.Master
/************************************/

$(document).ready(function () {
    JumpLabels();
});

function ChangeMainImage(sender) {
    var value = $(sender).prop('checked')
    $("#gvPhotos #chkMainImage").each(function () {
        $(this).prop('checked', false);
    });
    if (value == true)
        $(sender).closest('#chkMainImage').prop('checked', true);
}

//Shadowbox.init({
//    // a darker overlay looks better on this particular site
//    overlayOpacity: 0.8
//    // setupDemos is defined in assets/demo.js
//}, setupDemos);

function LoadContent() {
    var head = document.getElementsByTagName('head')[0];
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = "../pop_up_files/demo.js";
    head.appendChild(script);
    var script1 = document.createElement('script');
    script1.type = 'text/javascript';
    script1.src = "../pop_up_files/shadowbox.js";
    head.appendChild(script1);
}

window.moveTo(0, 0);
window.resizeTo(screen.width, screen.height);

function LoadLeadRejects(val) {
    window.open('frmRejectLead.aspx' + val, 'Transactions', 'toolbar=no, location=no, menubar=no, width=450px, height=500px, resizable=no, scrollbars=no, top=200, left=250');
}

function IconsMenu() {
    if (document.getElementById('cssmenu').className == 'menu_continer') {
        document.getElementById('right_cont').style.width = '96%';
        document.getElementById('navTopMenu').style.width = '96%';
        document.getElementById('cssmenu').className = '';
        document.getElementById('cssmenu').className += 'menu_continersmall';
        document.getElementById('content_area').style.left = '5%';
        document.getElementById('content_area').style.width = '95%';
       
    }
    else  {
        document.getElementById('right_cont').style.width = '88%';
        document.getElementById('navTopMenu').style.width = '88%';
        document.getElementById('cssmenu').className = '';
        document.getElementById('cssmenu').className += 'menu_continer';
        document.getElementById('content_area').style.left = '12%';
        document.getElementById('navTopMenu').style.width = '88%';
        document.getElementById('content_area').style.width = '88%';
    }
    $('#UlMenu li > ul').hide();
    if (document.getElementById('pnlVMenu').style.display == 'none') {
        document.getElementById('right_cont').style.width = '100%';
        document.getElementById('navTopMenu').style.width = '100%';
    }
}

function menu() {
  
    if (document.getElementById('cssmenu').className == 'menu_continer') {
        document.getElementById('right_cont').style.width = '88%';
        document.getElementById('navTopMenu').style.width = '88%';
        document.getElementById('content_area').style.left = '12%';
        document.getElementById('navTopMenu').style.width = '88%';
        document.getElementById('content_area').style.width = '88%';
        if (document.getElementById('pnlVMenu2').style.display == 'none') {
            document.getElementById('right_cont').style.width = '100%';
            document.getElementById('navTopMenu').style.width = '100%';
        }
    }
    else if (document.getElementById('cssmenu').className == 'menu_continersmall') {
        document.getElementById('cssmenu').className = '';
        document.getElementById('cssmenu').className += 'menu_continer';
        document.getElementById('right_cont').style.width = '88%';
        document.getElementById('navTopMenu').style.width = '88%';
        document.getElementById('content_area').style.left = '12%';
        document.getElementById('navTopMenu').style.width = '88%';
        document.getElementById('content_area').style.width = '88%';
    }
    else if (document.getElementById('pnlVMenu').style.display == 'none') {
      
       
        document.getElementById('content_area').style.width = '100%';
      
    }
}

$(document).ready(function () {
    $('.nav li').click(function () {
        $(this).find('.dd-content').slideToggle('fast');
    });
});

$(document).ready(function () {
    $(".access_btn").click(function () {
        $(".toggle_box").slideToggle();
    });
});

$(document).ready(function () {
    $("#html").mouseup(function (e) {
        var subject = $(".toggle_box, .dd-content");
        if (e.target.id != subject.attr('id') && !subject.has(e.target).length) {
            subject.fadeOut();
        }
    });
});

$("#cssmenu").hover(function () {
    alert('hover');
    $(this).addClass("menu_continer");
    $(this).removeClass("menu_continersmall");
});

//function sizeFrame() {
//    var F = document.getElementById("myFrame");
//    if (F.contentDocument) {
//        F.height = F.contentDocument.documentElement.scrollHeight + 30; //FF 3.0.11, Opera 9.63, and Chrome
//    } else {
//        F.height = F.contentWindow.document.body.scrollHeight + 30; //IE6, IE7 and Chrome
//    }
//}

//window.onload = sizeFrame;

function SetFrame(val) {
    document.getElementById('myFrame').src = val;
    return false;
}

function ChangeForm(sender) {
    WebService.GetFormUrl(sender.value, function (val) {
        if (val != "") {
            window.location.href = val;
        }
    });
}

function SwapMenu(sender) {
    try {
        if (sender == "VMenu") {
          
            $("#pnlVMenu").hide();
            $("#pnlVMenu2").hide();
            $("#pnlHMenu").show();
            WebService.SetMenuType("HMenu", OnUpdatedMenu);
            document.getElementById('right_cont').style.width = '100%';
            document.getElementById('navTopMenu').style.width = '100%';
            document.getElementById('cssmenu').className = '';
            document.getElementById('cssmenu').className= 'full_width';
        }
        else {

            document.getElementById('cssmenu').className = '';
            document.getElementById('cssmenu').className += 'menu_continer';
            $("#pnlVMenu").show();
            $("#pnlVMenu2").show();
            $("#pnlHMenu").hide();
            WebService.SetMenuType("VMenu", OnUpdatedMenu);
            document.getElementById('right_cont').style.width = '88%';
            document.getElementById('navTopMenu').style.width = '88%';
        }
    }
    catch (err) {
        alert(err);
    }
}
function OnUpdatedMenu(res) {
    try {
        if (res == "False") {
            alert("Error");
        }
    }
    catch (err) {
        alert(err);
    }
}
$(document).ready(function () {
    $("#cmd_Notification").click(function () {
        $(".popover").slideToggle();
    });
});