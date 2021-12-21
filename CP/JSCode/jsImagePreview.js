function ImagePreview(Mediarsrc, caption) {
    //debugger;
    var extention = Mediarsrc.split(".").pop();
    var youtubeIfram = $('#YouTubeIfram');
    if (youtubeIfram) {
        $(youtubeIfram).remove();
    }
    var modal = document.getElementById('previewImage');
    var modalImg = document.getElementById("img01");
    var video = document.getElementById('videoPreview');
    if (document.getElementById('lblImgTitle')) {
        document.getElementById('lblImgTitle').innerText = caption;
    }

    modalImg.style.display = "none";
    if (video !== null) { video.style.display = "none"; }
    if (extention === "mp4" || extention === "wmv" || extention === "webm") {
        //Remove Previous <scource> tag
        var element = document.getElementsByTagName("source"), index;
        for (index = element.length - 1; index >= 0; index--) {
            element[index].parentNode.removeChild(element[index]);
        }
        //Append new <source>
        var source = document.createElement('source');
        source.setAttribute('src', Mediarsrc);
        source.setAttribute('type', "video/mp4");
        video.appendChild(source);
        video.play();
        video.style.display = "block";

    } else if (Mediarsrc.includes("img.youtube.com")) {
        const pathname = new URL(Mediarsrc).pathname;
        const paths = pathname.split("/");
        const video_id = paths[2];
        var divYoutube = document.getElementById('divYoutube');
        if (divYoutube) {
            modal = divYoutube;
        }
        const embedURL = "https://www.youtube.com/embed/" + video_id;
        $('<iframe />', {
            name: 'YouTubeIfram',
            id: 'YouTubeIfram',
            frameborder: "0",
            allowfullscreen: true,
            src: embedURL
        }).appendTo(modal);
    }
    else {
        modalImg.src = Mediarsrc;
        modalImg.style.display = "block";
    }

    modal.style.display = "block";

}

function closeImgPopup() {
    document.getElementById("previewImage").style.display = "none";
    var youtubeIfram = $('#YouTubeIfram');
    if (youtubeIfram) {
        $(youtubeIfram).remove();
    }
}

function OpenAlbum(sender) {
    const VedioID = $(sender).data("id");
    const MediaCount = $(sender).data("count");
    const VedioTitle = $(sender).data("title");
    if (MediaCount > 1) {
        document.location.href = "Album_Videos_Details.aspx?ID=" + VedioID;
    } else {
        $('#previewImage').modal('show');
        ImagePreview(sender.lang, VedioTitle);
    }
}