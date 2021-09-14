
    $("[id*=lnkView]").live("click", function () {
        var subject = $(this).text();
        var row = $(this).closest("tr");
        $("#body").html($(".body", row).html());
        $("#attachments").html($(".Attachments", row).html());
        $("#dialog").dialog({
            title: subject,
            buttons: {
                Ok: function () {
                    $(this).dialog('close');
                }
            }
        });
        return false;
    });
