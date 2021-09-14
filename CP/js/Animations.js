function ShowPref() {
    $('#divPref').slideDown('1500', function () { }
     )

}

function ChangeView(sender) {
    if (sender.id == 'afeedback') {
        if (document.getElementById('divfeedback').style.right == '32px') {
            $("#divfeedback").animate(
            { "right": "-350px" },
            "slow");
            return;
        }
        $("#divhelp").animate(
            { "right": "-350px" },
            "slow");

        $("#divfeedback").animate(
            { "right": "32px" },
            "slow");
        //   alert('');
        //        $('#divhelp').slideUp('1500', function () { }
        //     )
    }
    else {
        if (document.getElementById('divhelp').style.right == '32px') {
            $("#divhelp").animate(
            { "right": "-350px" },
            "slow");
            return;
        }
        $("#divfeedback").animate(
            { "right": "-350px" },
            "slow");

        $("#divhelp").animate(
            { "right": "32px" },
            "slow");
        //   alert('');
        //        $('#divhelp').slideUp('1500', function () { }
        //     )
    }
    
   

}
function ShowSignature() {

      $('#tablelogin').slideDown('1500', function () { }
     )
      $('#tablemsgs').slideUp('1500', function () { }
     )
  }
  function ShowMsgDetails() {

      $('#tabledetails').slideDown('1500', function () { }
     )
      $('#tablelogin').slideUp('1500', function () { }
     )
  }
function ShowSettings() {
    
    if (document.getElementById('divSettings').style.display == 'none') {
        $('#divSettings').slideDown('1500', function () { }
     )
       
    }
    else {
        $('#divSettings').slideUp('1500', function () { }
     )
    }
    $('#divPref').slideUp('1500', function () { }
     )
}
function Register() {
    $('#divregisterpane').slideDown('1500', function () { }
     )
    $('#step02').slideDown('1500', function () { }
     )
    $('#divloginpane').slideUp('1500', function () { }
     )
    $('#step01').slideUp('1500', function () { }
     )
    $('#step03').slideUp('1500', function () { }
     )

 }
 function ThanksMsg() {
     $('#divThanks').slideDown('1500', function () { }
     )
     $('#step03').slideDown('1500', function () { }
     )
     $('#divregisterpane').slideUp('1500', function () { }
     )
     $('#step01').slideUp('1500', function () { }
     )
     $('#step02').slideUp('1500', function () { }
     )

 }
function switchview(sender) {
    var id = sender.id;
    if (id == 'lbpassword') {
      //  document.getElementById('changepassword').style.display = 'block';
        sender.className = '';
        sender.className = 'active01';
        //document.getElementById('changemobile').style.display = 'none';
        document.getElementById('lbmobile').className = 'button-inactive';
        $('#changemobile').slideUp('1500', function () { }
     )
        $('#changepassword').slideDown('1500', function () { }
     )
    }
    else {
        //document.getElementById('changemobile').style.display = 'block';
        sender.className = '';
        sender.className = 'active01';
        //document.getElementById('changepassword').style.display = 'none';
        $('#changemobile').slideDown('1500', function () { }
     )
        document.getElementById('lbpassword').className = '';
        document.getElementById('lbpassword').className = 'button-inactive';
        $('#changepassword').slideUp('1500', function () { }
     )
    }
}

function ShowSearch(id) {
      var div = document.getElementById(id);
      var ctrl = '#' + id;
      if (div.style.display == 'none') {
          $(ctrl).slideDown('5500', function () { }
     )
      }
      else {
          $(ctrl).slideUp('5500', function () { }
     )
      }
}
function showinfo(){
	$('#divinfo').fadeIn(500);
}
function hideinfo(){

	 $('#divinfo').fadeOut(1500);
}
function showOne(){
	document.getElementById('divOne').style.display='block';
	
	}

function HideValidator(sender) {
    var ctrl = '#' + sender;
    $(ctrl).slideUp();
     
}
function Validate(sender) {
    
    var ctrl = '#' + sender;
        $(ctrl).slideDown('1500', function () {}
     )
  }
  function divUser() {
    
        $('.validation_message_login').slideDown('1500', function () {}
		
     )
  }

//  function Validate(senderid) {
//      alert('ok');
//      var id = '#' + senderid;
//      $(id).fadeIn(1500);

//  }




function ShowCustom() {
    var txt = document.getElementById('txtCustom');
    var lbl = document.getElementById('lblCustom');
    txt.style.display = 'block';
    lbl.style.display = 'block';
}
function HideCustom() {
    var txt = document.getElementById('txtCustom');
    var lbl = document.getElementById('lblCustom');
    txt.style.display = 'none';
    lbl.style.display = 'none';
}


function ClearField(sender) {
    sender.value = '';

}
function ShowVSC() {
    var VSC = document.getElementById('divVSC');
    var EID = document.getElementById('divEID');
    var VSCLogin = document.getElementById('loginVSC');
    var EIDLogin = document.getElementById('loginEID');
	var Main= document.getElementById('divMain');
	Main.style.display='none';
    VSCLogin.className = '';
    EIDLogin.className = '';
    VSCLogin.className = 'login_menu_selected';
    VSC.style.display = 'block';
    EID.style.display = 'none';
}
function ShowEID() {

    var VSC = document.getElementById('divVSC');
    var EID = document.getElementById('divEID');
    var VSCLogin = document.getElementById('loginVSC');
    var EIDLogin = document.getElementById('loginEID');
	var Main= document.getElementById('divMain');
	Main.style.display='none';
    VSCLogin.className = '';
    EIDLogin.className = 'login_menu_selected';
    VSCLogin.className = '';
    VSC.style.display = 'block';
    EID.style.display = 'none';
    VSC.style.display = 'none';
    EID.style.display = 'block';

}
function ShowVSCAdmin() {
    var VSC = document.getElementById('divVSC');
    var EID = document.getElementById('divEID');
    var VSCLogin = document.getElementById('loginVSC');
    var EIDLogin = document.getElementById('loginEID');
	var Main= document.getElementById('divMain');
	Main.style.display='none';
    VSCLogin.className = '';
    EIDLogin.className = 'login_bt_big';
    VSCLogin.className = 'login_bt_big_selected';
    VSC.style.display = 'block';
    EID.style.display = 'none';
}
function ShowEIDAdmin() {
   
    var VSC = document.getElementById('divVSC');
    var EID = document.getElementById('divEID');
    var VSCLogin = document.getElementById('loginVSC');
    var EIDLogin = document.getElementById('loginEID');
	var Main= document.getElementById('divMain');
	Main.style.display='none';
    VSCLogin.className = '';
    EIDLogin.className = 'login_bt_big_selected';
    VSCLogin.className = 'login_bt_big';
    VSC.style.display = 'none';
    EID.style.display = 'block';

}
//login_bt_big_selected
function ShowConfirm(type, msg) {
    $('#ConfirmDiv').slideDown('1500', function () {

    });
    return false;
}
function CloseMsgDiv() {

    $('.validation-position').hide();
	 $('.validation-position_s').hide();
    return false;
}
function ShowMsg(sender, msg) {
   // var Id = sender.id;
    // var x = document.getElementById(Id).offsetLeft;
    // var y = document.getElementById(Id).offsetTop;
    // y = y + 40;
    var div = document.getElementById('MsgDiv');
   // div.style.left = x + "px";
    // div.style.top = y + "px";

    $('.validation-position').fadeIn(1500);
	 $('.validation-position_s').fadeIn(1500);

}
function ShowConfirmation(sender, msg) {
  
//    var Id = sender.id;
//    var x = document.getElementById(Id).offsetLeft;
//    var y = document.getElementById(Id).offsetTop;
//    y = y + 40;
//    var div = document.getElementById(Id);
//    div.style.left = x + "px";
    //    div.style.top = y + "px";
    var ctrl = '#' + msg;
    
    $(ctrl).fadeIn(1500);

}
function SelectRow(sender, evt) {
    if (sender.className == 'checked')
    { sender.className = 'un_checked'; return false; }
    else { sender.className = 'checked'; return false; }

}