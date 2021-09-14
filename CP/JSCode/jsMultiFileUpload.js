function UploadFileStart(sender, args) {
}
function uploadError() {
    alert("Error")
}
/* upload file on the server */
function uploadFileComplete(sender, args) {
    try {
        var fileLength = args.get_fileSize();
        //Only 10MB max size 
        var MaxFileSize = 10 * 1024 * 1024;
        if (fileLength > MaxFileSize) {
            $('#AjaxFileUpload1_FileInfoContainer_' + sender.getCurrentFileItem()._id).css('color', 'red');
            $('#AjaxFileUpload1_FileItemContainer_' + sender.getCurrentFileItem()._id).css('height', 'unset');
            $('#AjaxFileUpload1_FileItemStatus_' + sender.getCurrentFileItem()._id).text('(Size Denied, Only files smaller than 10MB can be uploaded.)');
            return;
        }
        var folderName = $('#FolderName').val();
        args["_fileName"] = folderName + "|" + args["_fileName"];

        wsMultiFileUpload.UploadFile(args, onSuccessFileUploaded);

    }
    catch (error) {

        alert(error);
        return false;
    }
}
function uploadFileComplete1(sender, args) {
    try {
        var folderName = "NewsPhotos/" + $('form').attr('action').split('.')[0];
        args["_fileName"] = folderName + "|" + args["_fileName"];
        wsMultiFileUpload.UploadFile(args, onSuccessFileUploaded);
    }
    catch (error) {
        alert(error);
        return false;
    }
}
/* after file uploaded successfully, show a preview with delete option */
function onSuccessFileUploaded(arr) {
    var val = arr[1];
    // create cmdDelete to enable delete the uploaded file
    var cmdDelete = document.createElement('input');
    cmdDelete.id = 'cmd' + val;
    cmdDelete.type = 'button';
    cmdDelete.value = 'x';
    cmdDelete.className += 'delete-photo';
    cmdDelete.addEventListener('click', function () {
        deleteUploadedFile(val, arr[2]);
    });

    // create image to show file uploaded
    var img = document.createElement('img');
    img.src = val;
    img.id = val;
    img.alt = arr[2];
    img.title = arr[0];
    img.className += 'uploadedPhoto';
    img.width = '40';
    // create div to show file uploaded in it
    var container = document.createElement('div');
    container.id = 'divPhotos' + val;
    container.className += 'photoWrap';

    // append image and button created into div
    $(container).append(cmdDelete).append(img);

    // append container into "okDiv"
    $('#okDiv').append(container);

    setIcon(img, '../' + val);
}

// show suitable icon to file uploaded//
// if file uploaded is image, then show it.//
function setIcon(img, val) {
    var type = img.src.split(".").pop();
    switch (type) {
        case "doc": case "docx":
            img.src = 'images/word.png'; break;
        case "xls": case "xlsx":
            img.src = 'images/icon-xlsx.png'; break;
        case "pdf":
            img.src = 'images/pdf_icon.jpg'; break;
        case "ppt":
            img.src = 'images/powepoint.png'; break;
        case "pptx":
            img.src = 'images/powepoint.png'; break;
        case "txt":
            img.src = 'images/icon-text.gif'; break;
        case "dwg":
            img.src = 'images/dwg.jpg'; break;
        case "dxf":
            img.src = 'images/dxf.jpg'; break;
        case "rar":
            img.src = 'images/rar.jpg'; break;
        case "zip":
            img.src = 'images/zip.jpg'; break;
        case "jpg": case "jpeg": case "png":
            img.src = val; break;
        case "mp4": case "wmv": case "webm":
            img.src = 'images/video.png'; break;
        case "":
            img.src = 'images/noimage.jpg'; break;
        default:
            img.src = '../' + val;
        //alert("Error at document type"); break;
    }
}

/* delete file from the server */
function deleteUploadedFile(val, filePath) {
    try {
        wsMultiFileUpload.DeleteFile(val, onSuccessFileDeleted);
        $('#AjaxFileUpload1_FileItemContainer_' + filePath).remove();
    }
    catch (error) {
        alert(error);
        return false;
    }
}

/* after file deleted successfully, remove its preview */
function onSuccessFileDeleted(val) {
    try {
        if (val != '') {
            $("div[id='divPhotos" + val + "']").remove();
        }
    }
    catch (error) {
        alert(error);
        return false;
    }
}

/* show Uploaded files in html table after click ok */
function showUploadedFilesTable() {
    if ($('.photoWrap img').length > 0) {
        if ($('#tblUploadedFiles').length == 0) {
            createTblUploadedFile();
        }

        $('.photoWrap img').each(function () {
            var tr = document.createElement('tr');

            var val = this.id;
            var filePath = this.alt;

            $(tr).append('<td><span id="lblFileId" style="display:none;"></span><img id="' + this.id + '"  src="' + this.src + '" title="' + this.title + '" width="40"></img></td>');

            $(tr).append('<td><span>' + this.title.split('.')[1] + '</span></td>');

            $(tr).append('<td><input id="txtTitle" value="' + this.title.split('.')[0] + '"></input></td>');

            $(tr).append('<td><input id="txtDescription" TextMode="MultiLine"></input></td>');

            $(tr).append('<td><input id="txtDrawingNo"></input></td>');
            attachChangeEventDrawingNo($(tr).find('#txtDrawingNo'));

            $(tr).append('<td><input type="button" value="Delete" class="glyphicon glyphicon-trash btn btn-primary"></input></td>');
            $(tr).find('td:last input').click(function () {
                wsMultiFileUpload.DeleteFile(val);
                $('#AjaxFileUpload1_FileItemContainer_' + filePath).remove();
                if ($(this).closest('table').find('tr').length == 2) {
                    $(this).closest('table').remove();
                } else {
                    $(this).closest('tr').remove();
                }

                $('#mainDiv tbody tr #ddlDrawingNO').each(function () {
                    setDrawingNoOptions($(this));
                });
            });

            $('#tblUploadedFiles').append(tr);
        });

        // remove all .photoWrap div that contain files preview
        $('.photoWrap').remove();
    }
    ResizeParentIframe();
}

/* append uploaded files details to label and fire submit link button click event */
function bindUploadedFilesLabel() {
    if ($('.photoWrap img').length > 0) {
        var FilesDetails = "";
        $('.photoWrap img').each(function () {
            FilesDetails = FilesDetails + "|" + "0?" + this.id + "?" + this.title;;
        });

        $('#lblUploadedFilesDetails').val($('#lblUploadedFilesDetails').val() + FilesDetails);
        document.getElementById('lbSubmit').click();

        // remove all .photoWrap div that contain files preview
        $('.photoWrap').remove();
    }
    else {
        CloseConfirmPopup('mdu');
    }
}

/* change change event to txtDrawingNo in uploadeFiles table */
function attachChangeEventDrawingNo(sender) {
    $(sender).change(function () {
        var count = 0;
        var drawingNo = $(this).val();
        $('#tblUploadedFiles tr #txtDrawingNo').each(function () {
            if ($(this).val() == drawingNo) {
                count = count + 1;
            }
        });

        if (count > 1) {
            alert("Drawing No Already Exist");
            $(this).val('');
        }

        $('#mainDiv tbody tr #ddlDrawingNO').each(function () {
            setDrawingNoOptions($(this));
        });
    });
}

/* create table to show uploaded files details */
function createTblUploadedFile() {
    try {
        var tbl = document.createElement('table');
        tbl.border = 3;
        tbl.id = 'tblUploadedFiles';
        tbl.className = 'table table-bordered table-condensed table-fixed-head';
        var txt = ["File", "Type", "Title", "Description", "Drawing No", "Delete"];
        var header = document.createElement('tr');
        for (i = 0; i < txt.length; i++) {
            var th = document.createElement('th');
            th.innerHTML = txt[i];
            header.appendChild(th);
        }
        tbl.appendChild(header);
        var div = document.getElementById('divUploadedFiles');
        div.appendChild(tbl);
    }
    catch (error) {
        alert(error);
        return false;
    }
}


// show Uploaded files in html table after click ok */
function showUploadedFilesTableManpower() {
    if ($('.photoWrap img').length > 0) {
        if ($('#tblUploadedFiles').length == 0) {
            createTblUploadedFile();
        }

        $('.photoWrap img').each(function () {
            var tr = document.createElement('tr');

            var val = this.id;
            var filePath = this.alt;

            $(tr).append('<td><span id="lblFileId" style="display:none;"></span><img id="' + this.id + '"  src="' + this.src + '" title="' + this.title + '" width="40"></img></td>');

            $(tr).append('<td><span>' + this.title.split('.')[1] + '</span></td>');

            $(tr).append('<td><input id="txtTitle" value="' + this.title.split('.')[0] + '"></input></td>');

            $(tr).append('<td><input id="txtDescription" TextMode="MultiLine"></input></td>');

            $(tr).append('<td><input id="txtDrawingNo"></input></td>');
            attachChangeEventDrawingNo($(tr).find('#txtDrawingNo'));

            $(tr).append('<td><input type="button" value="Delete" class="glyphicon glyphicon-trash btn btn-primary"></input></td>');
            $(tr).find('td:last input').click(function () {
                wsMultiFileUpload.DeleteFile(val);
                $('#AjaxFileUpload1_FileItemContainer_' + filePath).remove();
                if ($(this).closest('table').find('tr').length == 2) {
                    $(this).closest('table').remove();
                } else {
                    $(this).closest('tr').remove();
                }

                $('#mainDiv tbody tr #ddlDrawingNO').each(function () {
                    setDrawingNoOptions($(this));
                });
            });

            $('#tblUploadedFiles').append(tr);
        });

        // remove all .photoWrap div that contain files preview
        $('.photoWrap').remove();
    }
    ResizeParentIframe();
}