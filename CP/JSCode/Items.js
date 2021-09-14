function ImagePreview(src, alt) {
    var modal = document.getElementById('previewImage');
    var modalImg = document.getElementById("img01");

    modal.style.display = "block";
    modalImg.src = src;
}

function closeImgPopup() {
    document.getElementById("previewImage").style.display = "none";
}
function SetMainImage(img) {

    document.getElementById("imgMain").src = img;
}
function SetStatus(pnlID) {
    switch (pnlID) {
        case '1':
            if ($("#Collapse1Status").val() == "") {
                $("#Collapse1Status").val("collapse1");
            }
            else {
                $("#Collapse1Status").val("");
            }
            break;
        case '2':
            if ($("#Collapse2Status").val() == "")
                $("#Collapse2Status").val("collapse2");
            else
                $("#Collapse2Status").val("");
            break;
        case '3':
            if ($("#Collapse3Status").val() == "")
                $("#Collapse3Status").val("collapse3");
            else
                $("#Collapse3Status").val("");
            break;
        case '4':
            if ($("#Collapse4Status").val() == "")
                $("#Collapse4Status").val("collapse4");
            else
                $("#Collapse4Status").val("");
            break;
      
    }

}

function CollapseInDiv(divid) {
    $("#" + divid).addClass('in');
    $("#" + divid).removeClass('collapsed');
}
function ExpandInDiv(divid) {
    $("#" + divid).removeClass('in');
    $("#" + divid).addClass('collapsed');

}