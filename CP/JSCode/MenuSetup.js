$(document).ready(function () {
    $("input:text").focus(function () { $(this).select(); });
});

function SetContextKey(sender) {
    var x = document.getElementById('aceNationality');
    x.setAttribute('contextKey', 'g');
}

function ShowMsg(type, msg) {
    $('#MsgDiv').slideDown('slow', function () {
    });
    return false;
}

function UploadStartedMenu(sender, args) {
    var fileName = args.get_fileName();
    var img = document.getElementById('imgLoaderMenu');
    img.style.display = 'block';
}

function UploadCompleteMenu(sender, args) {
    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    //   alert('ok');
    document.getElementById('imgMenu').src = '../Menues/' + args.get_fileName();
    var img = document.getElementById('imgLoaderMenu');
    img.style.display = 'none';
    //    document.getElementById('lblFileName').innerHTML = args.get_fileName();
    switch (true) {
        case (fileLength > 1000000):

            fileLength = fileLength / 1000000 + 'MB';
            break;

        case (fileLength < 1000000):

            fileLength = fileLength / 1000000 + 'KB';
            break;

        default:
            fileLength = '1 MB';
            break;
    }
}


function UploadStartedForm(sender, args) {
    var fileName = args.get_fileName();
    var img = document.getElementById('imgLoader');
    img.style.display = 'block';
}

function UploadCompleteForm(sender, args) {
    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    document.getElementById('imgForm').src = '../Forms/' + args.get_fileName();
    var img = document.getElementById('imgLoader');
    img.style.display = 'none';
    switch (true) {
        case (fileLength > 1000000):
            fileLength = fileLength / 1000000 + 'MB';
            break;

        case (fileLength < 1000000):
            fileLength = fileLength / 1000000 + 'KB';
            break;

        default:
            fileLength = '1 MB';
            break;
    }
}


function UploadError() {
    //  var lbl = document.getElementById('lblStatus');
    // lbl.innerHTML = 'Upload Error';
}
function ResizeParentIframe() {
    try {
        var iframeWin = parent.document.getElementById("MyFrame");
        var newheight = iframeWin.contentWindow.document.body.scrollHeight;
        iframeWin.height = newheight;
    }
    catch (err) {

    }
}



//var prm = Sys.WebForms.PageRequestManager.getInstance();
//prm.add_pageLoaded(setupSB);

function setupSB() {
    Shadowbox.init({ skipSetup: true });
    Shadowbox.clearCache();
    Shadowbox.setup();
}

function ClearMe(sender) {
    sender.value = '';
}
function ResizeParentIframe() {
    try {
        var iframeWin = parent.document.getElementById("MyFrame");
        var newheight = iframeWin.contentWindow.document.body.scrollHeight;
        iframeWin.height = newheight;
    }
    catch (err) {

    }
}
function ShowEdit(sender) {
    try {
        var pnlEdit = '#pnlEdit' + sender.className;
        var icon = '#icon' + sender.className;
        var btnShowValue = sender.value;

        if (btnShowValue == "Show" || $(sender).val() == "") {
            $(pnlEdit).show();
            //$(sender).hide();
            $(sender).val('Hide');
            $(icon).removeClass('fa-eye fa').addClass('fa-eye-slash fa');

        }
        else {
            $(pnlEdit).hide();
            //$(sender).hide();
            $(sender).val('Show');
            $(icon).removeClass('fa-eye-slash fa').addClass('fa-eye fa');


        }
    }
    catch (err) {
        alert(err);
    }
}