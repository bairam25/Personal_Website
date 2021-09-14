var AR;
function SetAR(val) {
    AR = val;
}

function setIframe(sender) {
    var url = sender;
    var array;
    array = AR.split(";");

    //Search For Current URL in Array 'array' and Get Index
    var cindex = array.indexOf(url.replace("%20", " "));

    var nindex = cindex + 1;
    var pindex = cindex - 1;
    //Make second Array
    var nextURL = array[nindex];
    var previousURL = array[pindex];

    document.getElementById('lbNext').href = nextURL;
    document.getElementById('lbPrev').href = previousURL;

    if (cindex == 0) {
        document.getElementById('lbPrev').style.display = 'none';
    }
    else {
        document.getElementById('lbPrev').style.display = 'block';
    }

    if (nindex == array.length) {
        document.getElementById('lbNext').style.display = 'none';
    }
    else {
        document.getElementById('lbNext').style.display = 'block';
    }

    document.getElementById('ItemFrame').src = url;
}
function closeIframe() {
    document.getElementById('ItemFrame').src = "";
}