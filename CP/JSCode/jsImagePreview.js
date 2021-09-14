function ImagePreview(Mediarsrc) {
    var extention = Mediarsrc.split(".").pop();

    var modal = document.getElementById('previewImage');
    var modalImg = document.getElementById("img01");
    var video = document.getElementById('videoPreview');
    modalImg.style.display = "none";
    if (video !== null) { video.style.display = "none";} 
    if (extention === "mp4" || extention === "wmv" || extention === "webm")
    {
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
      
    }
    else
    {
        modalImg.src = Mediarsrc;    
        modalImg.style.display = "block";
    }

    modal.style.display = "block";

}

function closeImgPopup() {
    document.getElementById("previewImage").style.display = "none";
}