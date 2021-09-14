// Get the modal
function ImagePreview(src) {
    var modal = document.getElementById('previewImage');
    var modalImg = document.getElementById("img01");
    modal.style.display = "block";
    modalImg.src = src;
}
var modal = document.getElementById('previewImage');
// Get the <span> element that closes the modal
var span = document.getElementsByClassName("Myclose")[0];
// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}
function closeImgPopup() {
    document.getElementById("previewImage").style.display = "none";
}