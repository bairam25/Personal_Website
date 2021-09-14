function UploadStartedCV(sender, args) {
    var fileName = args.get_fileName();
    var img = document.getElementById('imgLoaderCV');
    img.style.display = 'block';
}
function UploadCompleteCV(sender, args) {
    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    var url = args.get_fileName();
    var img = document.getElementById('imgLoaderCV');
    if (url.split(".").pop().toLowerCase() != "pdf" && url.split(".").pop().toLowerCase() != "jpg" && url.split(".").pop().toLowerCase() != "png" && url.split(".").pop().toLowerCase() != "jpeg" && url.split(".").pop().toLowerCase() != "gif" && url.split(".").pop().toLowerCase() != "doc" && url.split(".").pop().toLowerCase() != "docx") {
        alert("File Type Error");
        img.style.display = 'none';

        return;
    }
    if (url.split(".").pop().toLowerCase() == "pdf") {
        document.getElementById('hlViewCV').href = '../TeamWorkCVs/' + args.get_fileName();
        document.getElementById('imgCV').src = '../Images/pdf_icon.jpg';
        document.getElementById('HiddenCV').value = '~/TeamWorkCVs/' + args.get_fileName();
    }else
        if (url.split(".").pop().toLowerCase() == "doc" || url.split(".").pop().toLowerCase() == "docx") {
        document.getElementById('hlViewCV').href = '../TeamWorkCVs/' + args.get_fileName();
            document.getElementById('imgCV').src = '../Images/word.png';
        document.getElementById('HiddenCV').value = '~/TeamWorkCVs/' + args.get_fileName();
    }
    else {
        document.getElementById('hlViewCV').href = '../TeamWorkCVs/' + args.get_fileName();
        document.getElementById('imgCV').src = '../TeamWorkCVs/' + args.get_fileName();
        document.getElementById('HiddenCV').value = '~/TeamWorkCVs/' + args.get_fileName();
    }

    img.style.display = 'none';

}
function UploadErrorCV() {
}