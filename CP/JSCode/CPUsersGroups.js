$(window).scroll(function () {
    var sticky = $('.validationList, .res-label-margin'),
        scroll = $(window).scrollTop();

    if (scroll >= 100) sticky.addClass('validationFixed');
    else sticky.removeClass('validationFixed');
});

function ViewlblMSG() {
    var sticky = $('.validationList, .res-label-margin'),
        scroll = $(window).scrollTop();

    if (scroll >= 100) sticky.addClass('validationFixed');
    else sticky.removeClass('validationFixed');
}


function CheckAll(objRef) {
    try {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        var id = objRef.id;
        id = id.replace("All", '');
        for (var i = 0; i < inputList.length; i++) {
            //Get the Cell To find out ColumnIndex
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i] && inputList[i].id == id) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        }
    }

    catch (err) {
        alert(err);
    }
}
function Select(objRef) {
    try {
        var row = objRef.parentNode.parentNode;
        var GridView = row.parentNode;
      
        //Get all input elements in Gridview
        var inputList = GridView.getElementsByTagName("input");
        var id = objRef.id;
        var headerCheckBox = document.getElementById("chkAll" + id.replace("chk", ""));

        for (var i = 0; i < inputList.length; i++) {
            //The First element is the Header Checkbox
            //Based on all or none checkboxes
            //are checked check/uncheck Header Checkbox
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox && inputList[i].id == id) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;


    }
    catch (err) {
        alert(err);
    }

}
function ClientItemSelected(sender, e) {
    $get("<%=txtSearch.ClientID%>").value = e.get_value();
}