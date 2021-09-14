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
        document.getElementById('imgLoader').style.display = 'block';

            document.getElementById('frmFrame').src = '';
            document.getElementById('frmFrame').src = arg;
            $('#myModal').modal('show');
            document.getElementById('imgLoader').style.display = 'none';
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