function ShowConfirmPopup(popup, pnl) {
    try {
       
        var modal = $find(popup);
        //var modal = document.getElementById(popup);
        modal.show();
        $("#" + pnl + "").css('top', '');
        $("#" + pnl + "").css({
            position: 'fixed',
            top: $(window.parent.document).scrollTop() + 20,
            left: 44.5

        });
        topFunction();
    }
    catch (err) {
        alert(err);
    }

}
function ShowConfirmPopupNoTop(popup, pnl) {
    try {

        var modal = $find(popup);
        //var modal = document.getElementById(popup);
        modal.show();
        $("#" + pnl + "").css('top', '');
        $("#" + pnl + "").css({
            position: 'fixed',
            top: $(window.parent.document).scrollTop() + 20,
            left: 44.5

        });
    }
    catch (err) {
        alert(err);
    }

}
function CloseConfirmPopup(popup) {
    var modal = $find(popup);
    modal.hide();

}
function CloseConfirmPopupParent(popup) {
    $('#' + popup, window.parent.document).click();

}

function topFunction() {
    $('html, body').animate({ scrollTop: '0px' }, 300);
    window.parent.parent.scrollTo(0, 0);
}

function performValidation() {
    if (Page_ClientValidate()) {
    }
    else
    {
        topFunction();
    }

}

function OpenPopUp(arg) {
    try {

        document.getElementById('frmFrame').src = '';
        document.getElementById('frmFrame').src = arg;
    }
    catch (err) {
        alert(err);
    }
}


function OpenPopUpFromGv(arg) {
    try {
        //document.getElementById('imgLoader').style.display = 'block';

        document.getElementById('frmFrame').src = '';
        document.getElementById('frmFrame').src = arg;
        $('#myModal').modal('show');
        //document.getElementById('imgLoader').style.display = 'none';
    }
    catch (err) {
        alert(err);
    }
}

function OpenImagePopUp(arg) {
    try {
        document.getElementById('imgPopup').src = '';
        document.getElementById('imgPopup').src = arg;
        $('#myModalPhoto').modal('show');
    }
    catch (err) {
        alert(err);
    }
}


function UnloadForm() {
    try {
        document.getElementById('frmFrame').src = '';
        $('#myModal').modal('hide');
    }
    catch (err) {
        alert(err);
    }
}


function CloseWindow() {
    try {

        window.parent.UnloadForm();
    } catch (error) {
        alert(error);
    }
}