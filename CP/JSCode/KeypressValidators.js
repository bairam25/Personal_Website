// On Save Click
function SaveClick(sender, grop) {
    if (grop != '') {
        fnOnUpdateValidators();
        if (Page_ClientValidate(grop)) {
            DisableSaveBtn(sender);
        }
        
    }
    else {
        DisableSaveBtn(sender);
    }
}
function DisableSaveBtn(sender)
    {
    sender.disabled = 'true';
    sender.value = '... رجاء الإنتظار';
    var btn2 = document.getElementById(sender.lang);
    if (btn2) {
        btn2.disabled = 'true';
        btn2.value = '... رجاء الإنتظار';
    }
}
function fnOnUpdateValidators() {
    for (var i = 0; i < Page_Validators.length; i++) {
        var val = Page_Validators[i];
        var ctrl = document.getElementById(val.controltovalidate);
        if (ctrl != null && ctrl.style != null) {
            if (document.getElementById(val.controltovalidate).value == "")
                ctrl.style.borderColor = '#d9534f';
            else
                ctrl.style.borderColor = '';
        }
    }
}
// accept only entering date (numbers with slash)
function isDate(evt) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && charCode != 47 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err);
        return false;
    }
}

function IsValidChar(evt) {
    try {
        //|| charCode == 63 || charCode == 1567
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode == 34 || charCode == 39 || charCode == 47 || charCode == 92) {
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err);
        return false;
    }
}
// accept only entering decimal (numbers with dot)
function isDecimal(evt) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err);
        return false;
    }
}
// accept only entering decimal (numbers with only one dot)
function isDecimal(evt, sender) {
    try {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        evt = (evt) ? evt : window.event;
        if (charCode > 31 && charCode != 46 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        else {
            if (sender.value != "") {
                if (sender.value.indexOf('.') > -1 && charCode == 46) {
                    return false;
                }
            }
        }
        return true;
    }
    catch (err) {
        alert(err);
        return false;
    }
}

// accept only entering numbers
function isNumber(evt) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err);
        return false;
    }
}
// accept only entering numbers + "-"
function isFormatNumber(evt) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && charCode != 45 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    catch (err) {
        alert(err);
        return false;
    }
}
// accept only entering numbers with fixed legnth
function isFixedNumber(evt, sender) {
    try {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        //else {

        //    if (sender.value != "") {
        //        if (sender.value.length > 2) {
        //            return false;
        //        }
        //    }
        //}
        return true;
    }
    catch (err) {
        alert(err);
        return false;
    }
}
function isRating(sender) {
    try {
        if (parseFloat(sender.value) > 5) {
            sender.value = "";
            return false;
        }
    }
    catch (err) {
        alert(err);
        return false;
    }
}
// Accept max value less than or equal 100
function MaxValue(pagerControl) {
    try {
        var pager = pagerControl.value;
      
        if (parseInt(pager) > 100) {
            pagerControl.value = "100";
        }

    }
    catch (err) {
        alert(err);
        return false;
    }
}
// if pager control is empty set it to 1
function resetPager(pagerControl) {
    try {
        var pager = pagerControl.value;
        if (pager == "" || pager.charAt(0) == "0") {
            pagerControl.value = "1";
        }
        if (parseInt(pager) > 100) {
            pagerControl.value = "100";
        }

    }
    catch (err) {
        alert(err);
        return false;
    }
}

// if pager control is empty set it to 10
function resetValue(sender) {
    try {
      
        var val = sender.value;
        if (val == "") {
            sender.value = "0";
        }
    }
    catch (err) {
        alert(err);
        return false;
    }
}

function ValidateChars(sender) {
    var txtValue = sender.value;
    txtValue = txtValue.replace("'", "");
    txtValue = txtValue.replace("/", "");
    txtValue = txtValue.replace("\\", "");
    txtValue = txtValue.replace('"', "");
    sender.value=txtValue
}

function ValidateCount(text, length) {
    var maxlength = length; //set your value here 
    var object = document.getElementById(text.id)
    if (object.value.length > maxlength) {
        object.focus(); //set focus to prevent jumping
        var count1 = object.value.length;
        alert('You have exceeded the comment length of 100 characters , total characters entered are : ' + count1);
        object.value = text.value.substring(0, maxlength); //truncate the value
        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        return false;
    }
    return true;
}

function Loginjs() {
    if ($('#lblFullName', window.parent.document)) {
        var Username = $('#lblFullName', window.parent.document).html();
        if (Username != undefined) {
            window.parent.location.href = 'login.aspx';
        }
    }
}
// search button
$('#txtSearch').keypress(function () {
    if (event.keyCode == 13) {
        $('#btnSearch').click();
    }
});