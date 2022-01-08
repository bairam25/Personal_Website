function UploadStarted(sender, args) {
    var fileName = args.get_fileName();
    var img = document.getElementById('imgLoader');
    img.style.display = 'block';
}
function UploadComplete(sender, args) {
    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    var url = args.get_fileName();
    var ContentDate = document.getElementById('lblDateContent').innerHTML;
    var img = document.getElementById('imgLoader');
    if (url.split(".").pop().toLowerCase() != "pdf" && url.split(".").pop().toLowerCase() != "jpg" && url.split(".").pop().toLowerCase() != "png" && url.split(".").pop().toLowerCase() != "jpeg" && url.split(".").pop().toLowerCase() != "gif") {
        alert("File Type Error");
        img.style.display = 'none';

        return;
    }
    if (url.split(".").pop().toLowerCase() == "pdf") {
        document.getElementById('hlViewContent').href = '../ContentPhotos/' + ContentDate+"_" + args.get_fileName();
        document.getElementById('imgContent').src = '../Images/pdf_icon.jpg';
        document.getElementById('HiddenContentImg').value = '~/ContentPhotos/' + ContentDate + "_"  + args.get_fileName();
    }

    else {
        document.getElementById('hlViewContent').href = '../ContentPhotos/' + ContentDate + "_"  + args.get_fileName();
        document.getElementById('imgContent').src = '../ContentPhotos/' + ContentDate + "_"  + args.get_fileName();
        document.getElementById('HiddenContentImg').value = '~/ContentPhotos/' + ContentDate + "_"  + args.get_fileName();
    }

    img.style.display = 'none';

}
function UploadError() {
}