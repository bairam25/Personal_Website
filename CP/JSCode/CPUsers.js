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


function SetSalesmanUsername(username) {
    if ($('#txtUsername', window.parent.document).length) {
        $('#txtUsername', window.parent.document).val(username);
    }
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

function CheckUserName(source, arguments) {
    try {

        if (arguments.Value != '') {
            var exists;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../WebService.asmx/CheckUserNameUnique",
                data: "{'prefixText': '" + arguments.Value + "','ID':'" + $('#lblUserId').html() + "'}",
                dataType: "json",
                async: false,
                success: function (res) {
                    exists = res.d;
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
            arguments.IsValid = exists;

        }
    }
    catch (err) {
        alert(err);
    }

}

function CheckEmail(source, arguments) {
    try {

        if (arguments.Value != '') {
            var exists;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../WebService.asmx/CheckEmailUnique",
                data: "{'prefixText': '" + arguments.Value + "','ID':'" + $('#lblUserId').html() + "'}",
                dataType: "json",
                async: false,
                success: function (res) {
                    exists = res.d;
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
            arguments.IsValid = exists;
        }
    }
    catch (err) {
        alert(err);
    }

}

function ValidatePassword(source, arguments) {
    try {

        if (arguments.Value.length < 6 && arguments.Value.length > 0) {
            arguments.IsValid = false;
        }
        else {
            arguments.IsValid = true;
        }
    }
    catch (err) {
        alert(err);
    }
}


function ValidatePass() {
    try {
        var txtValue = document.getElementById("txtPassword").value;
        if (txtValue.length < 6) { $('#lblPassStatus').html(''); return false }
    }
    catch (err) {
        alert(err);
    }
}
function SetPassword() {
    var txtValue = document.getElementById("txtPassword").value;
    if (txtValue == '') { $('#lblPassStatus').html(''); return false }
    var regEx = /^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z\d]{6,}$/;
    if (regEx.test(txtValue)) {
        $('#lblPassStatus').html("Password must contain: Minimum 6 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabetr")
        $("#lblPassStatus").css("color", "red");
        return false;
    }
}
function CheckPassword() {
    try {

        var txtValue = document.getElementById("txtPassword").value;
        document.getElementById("txtPasswordConfirm").disabled = false;
        var regEx = /^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z\d]{6,}$/;
        if (txtValue == '') { $('#lblPassStatus').html(''); return false }
        else if (!regEx.test(txtValue)) {
            $('#lblPassStatus').html("Password must contain: Minimum 6 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabetr")
            $("#lblPassStatus").css("color", "red");
            document.getElementById("txtPasswordConfirm").disabled = true;
        }
        else if (txtValue.length < 6) {
            $('#lblPassStatus').html('Password Week')
            $("#lblPassStatus").css("color", "red");
        }
        else if (txtValue.length >= 6) {

            var i = 0;
            var character = '';
            var isNum = false
            var isUper = false
            var isLower = false
            while (i <= txtValue.length) {
                character = txtValue.charAt(i);
                if (character != '') {
                    if (!isNaN(character * 1)) {
                        isNum = true
                    }
                    else {
                        if (character == character.toUpperCase()) {
                            isUper = true
                        }
                        if (character == character.toLowerCase()) {
                            isLower = true
                        }
                    }
                }
                i++;
            }
            if (isNum && isLower && isUper) {
                $('#lblPassStatus').html('Password Strong')
                $("#lblPassStatus").css("color", "green");
            }
            else if (isLower && isUper) {
                $('#lblPassStatus').html('Password Medium')
                $("#lblPassStatus").css("color", "green");
            }
            else if (isNum && isLower) {
                $('#lblPassStatus').html('Password Medium')
                $("#lblPassStatus").css("color", "green");
            }
            else if (isUper && isNum) {
                $('#lblPassStatus').html('Password Medium')
                $("#lblPassStatus").css("color", "green");
            }
            else {
                $('#lblPassStatus').html('Password Medium')
                $("#lblPassStatus").css("color", "orange");
            }
        }

    }
    catch (err) {
        alert(err);
    }

}
