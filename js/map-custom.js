 var locations = [
            [' <br/> <h4 class="mp-h3">Head Office</h4> <img src="images/icons/m1.svg" class="svg-ico" width="30px">&nbsp;&nbsp;&nbsp; : &nbsp;&nbsp; Marrakech St - Dubai <br/> <img src="images/icons/m2.svg" class="svg-ico" width="30">&nbsp;&nbsp;&nbsp; : &nbsp;&nbsp; <a href="http://aht-pirelli.com/" target="blank">aht-pirelli.com</a> <br/> <img src="images/icons/m5.svg" class="svg-ico" width="30">&nbsp;&nbsp;&nbsp; : &nbsp;&nbsp; <a href="mailto:ali@aht.ae">ali@aht.ae</a> <br/>  <img src="images/icons/m3.svg" class="svg-ico" width="30">&nbsp;&nbsp;&nbsp; : &nbsp;&nbsp; <a href="tel:042853515"> 04 - 2853515</a> <br/>   <img src="images/icons/m4.svg" class="svg-ico" width="30">&nbsp;&nbsp;&nbsp; : &nbsp;&nbsp; 042861121 <br/> '
                , 25.23696, 55.3635, 1],
        ];
        var styles = [
            {
                featureType: "administrative",
                elementType: "all",
                stylers: [
                    { saturation: -30 }
                ]
            }, {
                featureType: "landscape",
                elementType: "all",
                stylers: [
                    { saturation: -30 }
                ]
            }, {
                featureType: "poi",
                elementType: "all",
                stylers: [
                    { saturation: -30 }
                ]
            }, {
                featureType: "road",
                elementType: "all",
                stylers: [
                    { saturation: -30 }
                ]
            }, {
                featureType: "transit",
                elementType: "all",
                stylers: [
                    { saturation: -30 }
                ]
            }, {
                featureType: "water",
                elementType: "all",
                stylers: [
                    { saturation: -30 }
                ]
            }
        ];
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 7,
            closeBoxMargin: "17px 20px 2px 2px",
            center: new google.maps.LatLng(26.000368190858015, 55.22101459765622),
            mapTypeId: google.maps.MapTypeId.MONOCITY,
            styles: styles
        });
        var infowindow = new google.maps.InfoWindow();
        var marker, i;
        for (i = 0; i < locations.length; i++) {
            marker = new google.maps.Marker({
                position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                map: map
            });

            google.maps.event.addListener(marker, 'click', (function (marker, i) {
                return function () {
                    infowindow.setContent(locations[i][0]);
                    infowindow.open(map, marker);
                }
            })(marker, i));
        }