var UIDs = new Array();
var EmailsCount;
var SortOption = "DateSent DESC";
var SelectedFolder = '';
var EmailAccount = '';
var EmailSettingsID = '';


$(document).ready(function () {
    LoadEmails("UserId", "Inbox", SortOption,'','');
       
});
function LoadEmails(UserId, type, SortOption,emailAcc,JobRef) {
    //$("#MailType").html(type);
    UIDs = [];
    $('#upg').show();
    SelectedFolder = type;
    $(".emaillist").remove();
    if (emailAcc != '') {
        $("#lblSelectedEmailAcc").html(emailAcc);
        EmailAccount = emailAcc;
    }
    //Enabling and disabling the move to inbox button
    var btnMove = document.getElementById('btnMove');
    if (type == "Deleted") {

         btnMove.disabled = false;
    }
    else {
        btnMove.disabled = true;
    }
   
    Email.GetEmails(UserId, type, SortOption,emailAcc,JobRef, OnSuccess);
} 
function OnSuccess(res) {

   
    //Checking if the response is null if so no emails have been configured in the system for this user
    if (res) {

        var Emai = res;
        var count = Emai.length;

        for (var i = 0; i < count; i++) {
            var row = Emai[i];
            var strCount = row.split("|").length - 1;
            //Checking if the UIDs has been inserted as a last row in the returned response from GetEmails() or if its newly configured email 
            //PS: If the email is settng up for the first time the count of strCount in every arary elements will be 1
            if ((i == count - 1) && (strCount != 1)) {
                var last = Emai[i];
                var array = Emai[i].split(',');
                var no = array.length;
                

                //This executes for the last row in every responses from GetEmail() ws and it pushes the unique IDs to the UID array
                //ReadNewEmail ws will be called only for these UIDs(emails) from the ReadEmail js fucntion
                for (var j = 0; j < no ; j++) {
                    if (array[j] !== "Removed") {
                        var val = array[j].toString();
                        UIDs.push(val + "|" + j);
                    }
                }
            }

            else {


                //Check if row is not null
                if (row) {

                    //Checking if the email is setting up for the first time, then the StrCount  will be 1
                    if (strCount != 1) {

                        var email = document.createElement("li");
                        row.toString();
                        

                        //Retriveing the values from the response
                        var Id = row.split("|")[0];
                        var MsgId = row.split("|")[1];
                        var Sub = row.split("|")[2];
                        var DateSent = row.split("|")[3];
                        var from = row.split("|")[4];
                        var bodyDetails = row.split("|")[5];
                        var myEmail = row.split("|")[6];
                        var attachments = row.split("|")[9];
                        var TotalScore = row.split("|")[10];
                        var cadidate = row.split("|")[11];
                        var jobRef = row.split("|")[12];
                        var JobTitle = row.split("|")[13];
                        var ApplicationId;
                        
                        if (cadidate) {
                            ApplicationId = cadidate.toString().split(",")[0];
                            var exists = cadidate.toString().split(",")[1];
                            //Checking if the candidate is already saved if so set the saved class
                            if (exists) {
                                if (exists.toLowerCase() == 'true') {
                                    email.setAttribute('class', 'emaillist read fa fa-check');
                                }
                                else {
                                    email.setAttribute('class', 'emaillist read');
                                }
                            }
                            else {
                                email.setAttribute('class', 'emaillist read');
                            }
                        }
                       
                       
                        else {
                            email.setAttribute('class', 'emaillist read');
                        }


                        //Creating the webcontrolls and assigning the values

                        email.id = Id;
                        var a = document.createElement('a');
                        a.setAttribute('href', '#');
                        a.setAttribute('class', 'email');
                         
                        var cb = document.createElement('input');
                        a.setAttribute('onclick', 'ShowBody("' + Id + '");')
                        a.setAttribute('ondblclick', 'ShowEmail("' + Id + '");')
                        cb.type = 'checkbox';
                        cb.value = Id;
                        cb.name = 'email';
                        cb.setAttribute('onclick', 'SelectEmail();')
                        var subSpan = document.createElement('span');
                        subSpan.setAttribute('class', 'subject');
                        subSpan.innerHTML = Sub;
                        var pTime = document.createElement('p');
                        pTime.setAttribute('class', 'time');
                        pTime.innerHTML = DateSent;
                        var pDesc = document.createElement('p');
                        pDesc.setAttribute('class', 'description');
                        pDesc.innerHTML = from;
                        var bodyDiv = document.createElement('div');
                        bodyDiv.setAttribute('class', 'email_body');
                        bodyDiv.innerHTML = bodyDetails;


                        //Creating the details part of the email with reply options

                        var body = document.createElement('li');
                        body.setAttribute('class', 'email_body');
                        body.id = "emailBody" + Id;
                        var divDesc = document.createElement('div');
                        divDesc.setAttribute('class', 'col-md-12 email_open');
                        var hSub = document.createElement('h2');
                        hSub.innerHTML = Sub;
                        divDesc.appendChild(hSub);
                        var pFrom = document.createElement('p');
                        pFrom.innerHTML = "From :" + from;
                        divDesc.appendChild(pFrom);
                        var pSent = document.createElement('p');
                        pSent.innerHTML = "Sent :" + DateSent;
                        divDesc.appendChild(pSent);
                        var pTo = document.createElement('p');
                        pTo.innerHTML = "To :" + myEmail;
                        divDesc.appendChild(pTo);
                        var pBody = document.createElement('p');
                        pBody.setAttribute('class', 'message');
                        pBody.innerHTML = bodyDetails;

                        //Create Matching score linkbutton
                        var score = document.createElement('a');
                        score.text = TotalScore;
                        score.setAttribute('href', 'http://hrms.blueberry.software/Appraisal_Module/AppraisalTemplateView.aspx?type=JobVacancy&JobRef=' + jobRef + '&ApplicantId=' + ApplicationId + '');
                        score.setAttribute('target', '_blank');
                        if (TotalScore != '') {
                            score.setAttribute('class', 'score');
                        }
                        else{
                            score.setAttribute('class', 'no_score');
                        }

                        var JobPosition = document.createElement('p');
                        JobPosition.innerHTML = JobTitle;
                        JobPosition.setAttribute('class', 'job_Title');
                        //Creating Reply and forward buttons

                        var replyUl = document.createElement('ul');
                        replyUl.setAttribute('class', 'replyUL');


                        var replyI = document.createElement('i');
                        replyI.setAttribute('class', 'fa fa-reply');
                        var btnReply = document.createElement('a');
                        btnReply.setAttribute('href', 'SendEmail.aspx?ID=' + Id + '&action=Reply');
                        btnReply.id = "btnReply" + Id;
                        btnReply.innerHTML = "Reply";
                        btnReply.target = '_blank';
                        //  btnReply.setAttribute('onclick','OpenReplyForm();');


                        var replyAllI = document.createElement('i');
                        replyAllI.setAttribute('class', 'fa fa-reply-all');
                        var btnReplyAll = document.createElement('a');
                        btnReplyAll.setAttribute('href', 'SendEmail.aspx?ID=' + Id + '&action=ReplyAll');
                        btnReplyAll.id = "btnReplyAll" + Id;
                        btnReplyAll.innerHTML = "ReplyAll";
                        btnReplyAll.target = '_blank';
                        //   btnReplyAll.setAttribute('onclick', 'OpenReplyForm();');


                        var forwardI = document.createElement('i');
                        forwardI.setAttribute('class', 'fa fa-mail-forward');
                        var btnForward = document.createElement('a');
                        btnForward.setAttribute('href', 'SendEmail.aspx?ID=' + Id + '&action=Forward');
                        btnForward.id = "btnForward" + Id;
                        btnForward.innerHTML = "Forward";
                        btnForward.target = '_blank';
                        // btnForward.setAttribute('onclick', 'OpenReplyForm();');


                        //Attachments Section
                        var attachmentCount = 0;
                        if (attachments) {
                            attachmentCount = attachments.split(";").length - 1;
                        }

                        if (attachmentCount != 0) {
                            for (var j = 0; j < attachmentCount; j++) {

                                var file = document.createElement('a');
                                file.setAttribute('class','attachments');
                                var href = attachments.split(";")[j];
                                file.href = 'http://hrms.blueberry.software/' + href;
                                file.href.replace("\Email_Module", "");
                                var value = (href.split("/")[href.split("/").length - 1]);
                                value = value.toString().replace(/\s/g, '&nbsp;');
                                file.innerHTML = value;
                                file.target = '_blank';
                                body.appendChild(file);
                                a.setAttribute('class', 'email attached'); 
                            }
                        }

                        replyUl.appendChild(replyI);
                        replyUl.appendChild(btnReply);
                        replyUl.appendChild(replyAllI);
                        replyUl.appendChild(btnReplyAll);
                        replyUl.appendChild(forwardI);
                        replyUl.appendChild(btnForward);

                        //Appending the details
                        divDesc.appendChild(pBody);
                        body.appendChild(divDesc);
                        body.appendChild(replyUl);
                        body.style.display = 'none';



                       
                        a.appendChild(subSpan);
                        a.appendChild(pTime);
                        email.appendChild(JobPosition);
                        a.appendChild(pDesc);
                        email.appendChild(cb);
                        email.appendChild(a);
                        email.appendChild(body);
                        email.appendChild(score);
                     
                        //email.appendChild(bodyDiv);
                        $('#emailList').append(email);
                      

                    }
                    else {
                        row.toString();
                        UIDs.push(row);
                    }
                }
            }

        }
        setCounts();
        $('#upg').hide();
        ReadEmail();
        var NoOfEmails = document.getElementById('ddlNoOfEmails');
        var No = NoOfEmails.options[NoOfEmails.selectedIndex].value;
        $('#paging_container11').pajinate({ items_per_page: No });
    }
    else {
        var form = document.getElementById('form1');
        form.style.display = 'none';
        var divConfigure = document.getElementById('divConfigure');
        divConfigure.style.display = 'block';
    }
  
}
function ReadEmail() {
    var ar = new Array();
    var length = UIDs.length;
      //Checking if the UID is empty
    if (length != 0) {

        ar = UIDs[0].split(",");
        //var count = ar.length;
        
        var count = UIDs.length;
       
        //Checking if the Removed is present in the UIDs list if not its a newly configured email
        if (UIDs.indexOf("Removed") == -1) {
            for (var j = 0; j < count; j++) {
                var d = UIDs[j];
                var mesgNo = d.split("|")[1];
                Email.ReadNewEmail(mesgNo, EmailAccount, OnSuccess);
                
            }

        }
        else {
            for (var j = 0; j < count; j++) {
                if (ar[j] !== "Removed") {
                    var msg = ar[j];
                    var msgNo = ar[j].split("|")[1];
                    Email.ReadNewEmail(msgNo, OnSuccess);
                                   }
            }

        }

   
        function OnSuccess(res) {
            //if ($("#MailType").html() = "Inbox") {
            var result = res;
            if (result) {
                res = res.toString();
                var uidcount = UIDs.length;
                var Id = result.split("|")[0];
                var msgId = result.split("|")[1];
                var Sub = result.split("|")[2];
                var DateSent = result.split("|")[3];
                var from = result.split("|")[4];
                var bodyDetails = result.split("|")[5];
                var myEmail = result.split("|")[6];
                var attachments = result.split("|")[8];
                var TotalScore = result.split("|")[9];
                var cadidate = result.split("|")[10];
                var jobRef = result.split("|")[11];
                var JobTitle = result.split("|")[12];
                var ApplicationId;
                   if (cadidate) {
                       ApplicationId = cadidate.toString().split(",")[0];
                       var exists = cadidate.toString().split(",")[1];
                }

               
                //Creating webcontrolls and assigning values



                //Checking if the candidate is already saved if so set the saved class
          


                
                var email = document.createElement('li');
                email.setAttribute('class', 'emaillist unread');
                email.id = Id;
                var a = document.createElement('a');
                a.setAttribute('href', '#');
                a.setAttribute('class', 'email');
                a.setAttribute('onclick', 'ShowBody("' + Id + '");')
                a.setAttribute('ondblclick', 'ShowEmail("' + Id + '");')
                var cb = document.createElement('input');
                cb.type = 'checkbox';
                cb.value = Id;
                cb.name = 'email';
                cb.setAttribute('onchange', 'SelectEmail();')
                var subSpan = document.createElement('span');
                subSpan.setAttribute('class', 'subject');
                subSpan.innerHTML = Sub;
                var pTime = document.createElement('p');
                pTime.setAttribute('class', 'time');
                pTime.innerHTML = DateSent;
                var pDesc = document.createElement('p');
                pDesc.setAttribute('class', 'description');
                pDesc.innerHTML = from;
                var bodyDiv = document.createElement('div');
                bodyDiv.setAttribute('class', 'email_body');

                //Details of the message and creating the body li with buttons for reply and reply all

                var body = document.createElement('li');
                body.setAttribute('class', 'email_body');
                body.id = "emailBody" + Id;
                var divDesc = document.createElement('div');
                divDesc.setAttribute('class', 'col-md-12 email_open');
                var hSub = document.createElement('h2');
                hSub.innerHTML = Sub;
                divDesc.appendChild(hSub);
                var pFrom = document.createElement('p');
                pFrom.innerHTML ="From :"+ from;
                divDesc.appendChild(pFrom);
                var pSent = document.createElement('p');
                pSent.innerHTML = "Sent :" + DateSent;
                divDesc.appendChild(pSent);
                var pTo = document.createElement('p');
                pTo.innerHTML = "To :" + myEmail;
                divDesc.appendChild(pTo);
                var pBody = document.createElement('p');
                pBody.setAttribute('class','message');
                pBody.innerHTML = bodyDetails;


                //Create Matching score linkbutton
                var score = document.createElement('a');
                score.text = TotalScore;
                score.setAttribute('href', 'http://hrms.blueberry.software/Appraisal_Module/AppraisalTemplateView.aspx?type=JobVacancy&JobRef=' + jobRef + '&ApplicantId=' + ApplicationId + '');
                if (TotalScore != '') {
                    score.setAttribute('class', 'score');
                }
                else {
                    score.setAttribute('class', 'no_score');
                }

                var JobPosition = document.createElement('p');
                JobPosition.innerHTML = JobTitle;
                JobPosition.setAttribute('class', 'job_Title');
                //Creating Reply and forward buttons
                var replyUl = document.createElement('ul');


                var replyI = document.createElement('i');
                replyI.setAttribute('class', 'fa fa-reply');
                replyUl.setAttribute('class','replyUL');
                var btnReply = document.createElement('a');
                btnReply.setAttribute('href', 'SendEmail.aspx?ID=' + Id + '&action=Reply');
                btnReply.id = "btnReply" + Id;
                btnReply.innerHTML = "Reply";
                btnReply.target = '_blank';
                //  btnReply.setAttribute('onclick', 'OpenReplyForm();');

                var replyAllI = document.createElement('i');
                replyAllI.setAttribute('class', 'fa fa-reply-all');
                var btnReplyAll = document.createElement('a');
                btnReplyAll.setAttribute('href', 'SendEmail.aspx?ID='+ Id +'&action=ReplyAll');
                btnReplyAll.id = "btnReplyAll" + Id;
                btnReplyAll.innerHTML = "ReplyAll";
                btnReplyAll.target = '_blank';
                //  btnReplyAll.setAttribute('onclick', 'OpenReplyForm();');

                var forwardI = document.createElement('i');
                forwardI.setAttribute('class', 'fa fa-mail-forward');
                var btnForward = document.createElement('a');
                btnForward.setAttribute('href', 'SendEmail.aspx?ID=' + Id + '&action=Forward');
                btnForward.id = "btnForward" + Id;
                btnForward.innerHTML = "Forward";
                btnForward.target = '_blank';
                //   btnForward.setAttribute('onclick', 'OpenReplyForm();');


                //Attachments Section
                var attachmentCount=0;
                if (attachments) {
                    attachmentCount = attachments.split(";").length - 1;
                }

                if (attachmentCount != 0) {
                    for (var j = 0; j < attachmentCount; j++) {

                        var file = document.createElement('a');
                        file.setAttribute('class', 'attachments');
                        var href = attachments.split(";")[j];
                        file.href = file.href = 'http://hrms.blueberry.software/' + href;
                        var value = (href.split("/")[href.split("/").length - 1]);
                        file.innerHTML = value;
                        file.target = '_blank';
                        body.appendChild(file);
                        a.setAttribute('class', 'email attached');
                    }
                }
                replyUl.appendChild(replyI);
                replyUl.appendChild(btnReply);
                replyUl.appendChild(replyAllI);
                replyUl.appendChild(btnReplyAll);
                replyUl.appendChild(forwardI);
                replyUl.appendChild(btnForward);

                divDesc.appendChild(pBody);
             
                body.appendChild(divDesc);
                body.appendChild(replyUl);
                body.style.display = 'none';
                



                bodyDiv.innerHTML = body;
           
                a.appendChild(subSpan);
                a.appendChild(pTime);
                email.appendChild(JobPosition);
                a.appendChild(pDesc);
                email.appendChild(cb);
                email.appendChild(a);
                email.appendChild(body);
                email.appendChild(score);
                
                //email.appendChild(bodyDiv);
                var list = document.getElementById('emailList');
                list.insertBefore(email, list.childNodes[0]);


                //Doing the pagination
                var NoOfEmails = document.getElementById('ddlNoOfEmails');
                var No = NoOfEmails.options[NoOfEmails.selectedIndex].value;
                $('#paging_container11').pajinate({ items_per_page: No });
            }
            setCounts();
           }
      
    }
    
}
function setInterv() {
    Email.GetInterval("", function (interval) {
        if (interval != 0) {
            setInterval(ReadEmail, interval);
        }
    });
}
function setCounts() {
    Email.GetCounts("prifix", EmailAccount, counts);
}
function counts(count) {
    if (count != "") {
        var inboxcount = $(".unread").length;
        $("#Inbox" + EmailSettingsID).html(inboxcount);
        $("#Sent"+EmailSettingsID).html(count[1]);
        $("#Deleted"+EmailSettingsID).html(count[2]);
    }
}
//Function to get the checked checkboxes and enable/Disable delete
var checkedIDs = new Array();
function SelectEmail() {
    checkedIDs = [];
    var AllChecked = true;
    var btnDelete = document.getElementById('btnDelete');
    var btnMove = document.getElementById('btnMove');
    var btnSave = document.getElementById('btnSave');
    if ($("#form1 input[name=email]:checkbox:checked").length > 0) {
        btnDelete.disabled = false;
        btnMove.disabled = false;
        btnSave.disabled = false;
        var checkboxes = $("input[name=email]:checkbox");
     
        for (var i = 0; i < checkboxes.length; i++) {
            // And stick the checked ones onto an array...
            if (checkboxes[i].checked) {
                checkedIDs.push(checkboxes[i].value);
            }
            else {
                if ($(checkboxes[i]).attr('id') != 'chkAll') {
                    AllChecked = false;
                }
               
            }
        }
        if (AllChecked == true) {
            $("#chkAll").attr('checked', 'checked');
        }
        else {
            $("#chkAll").removeAttr('checked');
        }
       }
    else {
        btnDelete.disabled = true;
        btnMove.disabled = true;
        btnSave.disabled = true;
        $("#chkAll").prop('checked', false);
    }
  
}

//Function To delete Emails on Delete button click
function DeleteEmails()
{
    var len = checkedIDs.length;
    
    if (len != 0) {
        Email.DeleteEmails(checkedIDs, ShowSuccessMessage);
        function ShowSuccessMessage(res) {
            var lblRes = document.getElementById('lblRes');
            var len = checkedIDs.length;
            for (var i = 0; i < len; i++) {
                var Id = checkedIDs[i];
                var list = document.getElementById(Id);
                list.remove();
            }
            var btnDelete = document.getElementById('btnDelete');
            btnDelete.disabled = true;
            setCounts();
            lblRes.innerHTML = 'The selected emails have been deleted';
            lblRes.style.display = 'block';
            FadeOut();
            var NoOfEmails = document.getElementById('ddlNoOfEmails');
            var No = NoOfEmails.options[NoOfEmails.selectedIndex].value;
            $('#paging_container11').pajinate({ items_per_page: No });
            $("#chkAll").removeAttr('checked');
        }
        
        
    }
    else {
        var lblRes = document.getElementById('lblRes');
        lblRes.innerHTML = 'Please select an email';
        lblRes.style.display = 'block';
    }

}

//Function to move the Deleted Emails back to Inbox

function MoveEmailstoInbox() {
    $('#divChooseFolder').show();
    $('.opaque').show();
}

//Function to Move Email to Inbox or Sent Items
function MoveEmails() {
    
    var len = checkedIDs.length;
    
    var folder = $('input[name=choosefolder]:checked').val();
    
    if (SelectedFolder.toLowerCase() == folder.toLowerCase()) {
        $('#divChooseFolder').hide();
        $('.opaque').hide();
        return;
    }
    var lblRes = document.getElementById('lblRes');
   
        if (len != 0) {
            Email.MoveEmailsToInbox(checkedIDs, folder, ShowSuccessMessage);
            function ShowSuccessMessage(res) {
                var len = checkedIDs.length;
                for (var i = 0; i < len; i++) {
                    var Id = checkedIDs[i];
                        var list = document.getElementById(Id);
                        list.remove();
                 
                }
                setCounts();
                $('#divChooseFolder').hide();
                $('.opaque').hide();
                lblRes.innerHTML = 'The selected emails have been moved ';
                lblRes.style.display = 'block';
                FadeOut();
                var NoOfEmails = document.getElementById('ddlNoOfEmails');
                var No = NoOfEmails.options[NoOfEmails.selectedIndex].value;
                $('#paging_container11').pajinate({ items_per_page: No });
            }


        }
        else {
            lblRes.innerHTML = 'Please select an email';
            lblRes.style.display = 'block';
            $('#divChooseFolder').hide();
            $('.opaque').hide();
            FadeOut();
            var NoOfEmails = document.getElementById('ddlNoOfEmails');
            var No = NoOfEmails.options[NoOfEmails.selectedIndex].value;
            $('#paging_container11').pajinate({ items_per_page: No });
        }
        $("#chkAll").removeAttr('checked');
    }
    
    


//Function to show the details of the email

function ShowBody(Id)
{
    
    if ($('#emailBody' + Id).css('display') == 'none') {
     
        document.getElementById('emailBody' + Id).style.display = 'block';
        $('#' + Id).removeClass('unread').addClass('read');
       
    }
    else if ($('#emailBody' + Id).css('display') == 'block') {
     
        document.getElementById('emailBody' + Id).style.display = 'none';
    }
}


//function to show email on double click
function ShowEmail(Id) {
    $('#showEmailDiv').children(':not(input[type="button"])').remove();
    var email = document.getElementById(Id);
    var body = document.getElementById('emailBody' + Id);
    //body.style.display = 'block';
    var btnClose = document.createElement('input');
    btnClose.setAttribute('type', 'button');
    btnClose.setAttribute('class', 'close');
    btnClose.setAttribute('onclick', 'CloseDiv();');
    btnClose.setAttribute('value', 'X');
    var contentDiv = document.getElementById('showEmailDiv');
    contentDiv.innerHTML = body.innerHTML;
    contentDiv.appendChild(btnClose);
    contentDiv.style.display = 'block';
}

//Function to close the EmailBody Div
function CloseDiv() {
    document.getElementById('showEmailDiv').style.display = 'none';
    $('#showEmailDiv').children(':not(input[type="button"])').remove();
}


//function to change the number of emails per page

function CalcNoOfEmails() {
    var NoOfEmails = document.getElementById('ddlNoOfEmails');
    var No = NoOfEmails.options[NoOfEmails.selectedIndex].value;
    $('#paging_container11').pajinate({ items_per_page: No });

}

function FadeOut() {
   $('#lblRes').fadeOut(4000,"linear");
   
}

function CloseDivFolder() {
    $('#divChooseFolder').hide();
    $('.opaque').hide();

}

//function to Save candiadates

function SaveCandidates() {
    var JobRefNos = new Array();
    JobRefNos = [];
    var len = checkedIDs.length;
    var lblRes = document.getElementById('lblRes');
    for (var i = 0; i < len; i++) {
        var Id = checkedIDs[i];
        var list = document.getElementById(Id);
        if (list.className.toString().indexOf('fa fa-check') > -1) {
            lblRes.innerHTML = 'The selected emails/candidates have already been saved,please select only unsaved ones';
            lblRes.style.display = 'block';
            return;
        }
    }
    debugger;
    var jobReferneces = $('input[name=job]:checked', '#divJobs');
    for (i = 0; i < jobReferneces.length; i++) {
        JobRefNos.push(jobReferneces[i].value);
    }
   if (JobRefNos.length == 0) {
        lblRes.innerHTML = 'Please select a job ';
        lblRes.style.display = 'block';
        return;
    }
   
    if (len != 0) {
        Email.SaveCandidate(checkedIDs, JobRefNos, ShowSuccessMessage);
        function ShowSuccessMessage(res) {
            var len = checkedIDs.length;
            for (var i = 0; i < len; i++) {
                var Id = checkedIDs[i];
                var list = document.getElementById(Id);
                var checkboxes = $("input:checkbox");
                for (var i = 0; i < checkboxes.length; i++) {
                    // And stick the checked ones onto an array...
                    checkboxes[i].checked = false;
                }
                $(list).addClass('fa fa-check');

            }
            lblRes.innerHTML = 'The selected candidates have been saved ';
            lblRes.style.display = 'block';
            FadeOut();

        }
    }
}


//Function to  bind the jobs

function ShowJobs() {
    try{
       $("#divJobs").empty();
        $("#lblMsg").hide();
        $("#lblRes").hide();
        $("#lblTitle").html('');
        var len = checkedIDs.length;
        var lblRes = document.getElementById('lblMsg');
        for (var i = 0; i < len; i++) {
            var Id = checkedIDs[i];
            var list = document.getElementById(Id);
            if (list.className.toString().indexOf('fa fa-check') > -1) {
                lblRes.innerHTML = 'The selected emails/candidates have already been saved,please select only unsaved ones';
                lblRes.style.display = 'block';
                return;
            }
        }

        Email.GetJobs(function (jobs) {
            if (jobs) {
                if (jobs.length > 0) {
                    $("#lblTitle").html('Select Jobs');
                    $("#divJobs").empty();
                    var count = jobs.length;
                    for (i = 0; i < count; i++) {
                        var res = jobs[i];
                        var jobref = res.toString().split("|")[0];
                        var postion = res.toString().split("|")[1];
                        var chkbox = document.createElement('input');
                        chkbox.type = 'checkbox';
                        chkbox.value = jobref;
                        chkbox.name = "job";
                        $("#divJobs").append(chkbox);
                        $("#divJobs").append('<span>' + postion + '</span>');
                    }
                }
            }
            });
        }
    catch (err) {
        alert(err);
    }

}



//Function to Sort the emails

function SortEmails(option) {
    try{
     
        switch (option) {
            case "DateSent":
                if ((SortOption == "DateSent DESC")) {
                    SortOption = "DateSent ASC";
                    LoadEmails('UserId', SelectedFolder, SortOption,EmailAccount,'');
                    break;
                }
                else {
                    SortOption = "DateSent DESC";
                    LoadEmails('UserId', SelectedFolder, SortOption, EmailAccount,'');
                    break;
                }
            case "Subject":
                if ((SortOption == "Subject DESC")) {
                    SortOption = "Subject ASC";
                    LoadEmails('UserId', SelectedFolder, SortOption, EmailAccount,'');
                    break;
                }
                else {
                    SortOption = "Subject DESC";
                    LoadEmails('UserId', SelectedFolder, SortOption, EmailAccount,'');
                    break;
                }
            case "EmailAddress":
                if ((SortOption == "EmailAddress DESC")) {
                    SortOption = "EmailAddress ASC";
                    LoadEmails('UserId', SelectedFolder, SortOption, EmailAccount,'');
                    break;
                }
                else {
                    SortOption = "EmailAddress DESC";
                    LoadEmails('UserId', SelectedFolder, SortOption, EmailAccount,'');
                    break;
                }

        }

    }
    catch (err) {

        alert(err)
    }

}

//function to select All
function SelectAll(sender) {
    try {
        checkedIDs = [];
        var checkboxes = $("input[name=email]:checkbox");
        if (sender.checked) {
            for (var i = 0; i < checkboxes.length; i++) {
                var li = $(checkboxes[i]).parent();
                if ($(li).css('display') != 'none') {
                    checkboxes[i].checked = true;
                    checkedIDs.push(checkboxes[i].value);
                  }
            }
            var btnDelete = document.getElementById('btnDelete');
            var btnMove = document.getElementById('btnMove');
            var btnSave = document.getElementById('btnSave');
            btnDelete.disabled = false;
            btnMove.disabled = false;
        }
        else {
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = false;
            }
        }
    }
    catch (err) {
        alert(err);
    }

}


//Function to select a different email account

function SelectEmailAccount(sender) {
    try {
     
           var email = $(sender).html();
            EmailAccount = $(sender).html();
            EmailSettingsID = $(sender).attr('id');
          
            if (email != '') {
                // LoadEmails('userId', 'Inbox', 'DateSent DESC', EmailAccount);
            }
      
       
    }
    catch (err) {

    }
}


//Function to select a default/first email account and set its values to variables


function SetDefaultEmailValues(emailaccount, Id) {
    try {
        debugger;
        if (emailaccount != '') {
            EmailAccount = emailaccount;
            $("#lblSelectedEmailAcc").html(emailaccount);
        }
        if (Id != '') {
            EmailSettingsID = Id;
        }
    }
    catch (err) {
        alert(err);
    }

}


//Function to load the sub folder emails
function LoadFolderEmails(UserId, type, SortOption, emailAcc, JobRef) {
    try{
        $('#lblSelectedEmailAcc').html(emailAcc);
        LoadEmails(UserId, type, SortOption, emailAcc, JobRef);
    }
    catch (err) {
        alert(err);
    }

}