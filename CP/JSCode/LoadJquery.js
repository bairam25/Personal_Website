function LoadJquery() {
    $(document).ready(function () {
        $('.dropdown-toggle').dropdown()
    });

    $('.table-responsive').on('show.bs.dropdown', function () {
        $('.table-responsive').css("overflow", "inherit");
    });

    $('.table-responsive').on('hide.bs.dropdown', function () {
        $('.table-responsive').css("overflow", "auto");
    })

    // Get the modal
    function ImagePreview(src, alt) {
        var modal = document.getElementById('previewImage');
        var modalImg = document.getElementById("img01");
        var captionText = document.getElementById("caption");
        modal.style.display = "block";
        modalImg.src = src;
        captionText.innerHTML = alt;
    }

    function closeImage() {
        document.getElementById("previewImage").style.display = "none";
    }


    /*RemovePreview*/
    function RemovePreview(_this) {
        if (_this.id == "RemoveLi") {
            $("#fuLogo").val(null);
            //$("#imgIcon").val(null);
            $("#HiddenIcon").val(null);
            $("#imgIcon").attr("src", "../../images/others/img-up.png")
            //$("#lblDateTime").val(null);
            //$("#imgIcon").hide();
            _this.style.display = "none";
        }
        else {
            $("#fuTabIcon").val(null);
            //$("#imgIcon").val(null);
            $("#HiddenIcon2").val(null);
            $("#imgIcon2").attr("src", "../../images/others/img-up.png")
            //$("#lblDateTime").val(null);
            //$("#imgIcon").hide();
            _this.style.display = "none";
        }
    }


    //debugger;
    var head = document.getElementsByTagName('head')[0];
    var body = document.getElementsByTagName('body')[0];

    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = '../js/jquery-min.js';
    document.head.appendChild(script);

    var script5 = document.createElement('script');
    script5.type = 'text/javascript';
    script5.src = '../bootstrap/js/bootstrap.min.js';
    document.head.appendChild(script5);

    $(function () {
        $('[data-toggle="modal"]').tooltip()
    })

    var script2 = document.createElement('script');
    script2.type = 'text/javascript';
    script2.src = 'js/cpcustom.js';
    document.head.appendChild(script2);
}
Sys.Application.add_load(LoadJquery);