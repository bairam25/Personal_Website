﻿function UploadStarted(sender, args) {
    var fileName = args.get_fileName();
    var img = document.getElementById('imgLoader');
    img.style.display = 'block';
}
function UploadComplete(sender, args) {
    var fileLength = args.get_length();
    var fileType = args.get_contentType();
    var url = args.get_fileName();
    var img = document.getElementById('imgLoader');
    if (url.split(".").pop().toLowerCase() != "pdf" && url.split(".").pop().toLowerCase() != "jpg" && url.split(".").pop().toLowerCase() != "png" && url.split(".").pop().toLowerCase() != "jpeg" && url.split(".").pop().toLowerCase() != "gif") {
        alert("File Type Error");
        img.style.display = 'none';

        return;
    }
    if (url.split(".").pop().toLowerCase() == "pdf") {
        document.getElementById('hlViewTeamWork').href = '../TeamWorkPhotos/' + args.get_fileName();
        document.getElementById('imgTeamWork').src = '../Images/pdf_icon.jpg';
        document.getElementById('HiddenTeamWorkImg').value = '~/TeamWorkPhotos/' + args.get_fileName();
    }

    else {
        document.getElementById('hlViewTeamWork').href = '../TeamWorkPhotos/' + args.get_fileName();
        document.getElementById('imgTeamWork').src = '../TeamWorkPhotos/' + args.get_fileName();
        document.getElementById('HiddenTeamWorkImg').value = '~/TeamWorkPhotos/' + args.get_fileName();
    }

    img.style.display = 'none';

}
function UploadError() {
}