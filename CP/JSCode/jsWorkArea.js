function PointOfInterestMap() {
    let DefaultLat = 26.8206;
    let DefaultLng = 30.8025;
    let txtLat = parseFloat($("#txtLatitude").val()) || '';
    let txtLng = parseFloat($("#txtLogitude").val()) || '';
    if (txtLat !== '' && txtLng !== '') {
        DefaultLat = txtLat;
        DefaultLng = txtLng;
    }
    var mapProp = {
        center: new google.maps.LatLng(DefaultLat, DefaultLng),
        mapTypeId: 'roadmap',
        zoomControl: true,
        mapTypeControl: false,
        scaleControl: false,
        streetViewControl: false,
        rotateControl: false,
        fullscreenControl: true,
        zoom: 7,
    };
    var mapDiv = document.getElementById("googleMap");
    //If map div not ready then return
    if (mapDiv === null) { return; }

    var map = new google.maps.Map(mapDiv, mapProp);
   var marker = new google.maps.Marker({
        map: map,
        draggable: true,
        animation: google.maps.Animation.DROP,
        position: { lat: DefaultLat, lng: DefaultLng }
    });
    //On click on map set Marker on lat / lng and in their related fields
    google.maps.event.addListener(map, 'click', function (event) {
        marker.setPosition(event.latLng);
        $("#txtLatitude").val(event.latLng.lat().toFixed(5));
        $("#txtLogitude").val(event.latLng.lng().toFixed(5));
    });

    marker.addListener('click', toggleBounce);
    //On Drag Marker set lat / lng in their related fields
    google.maps.event.addListener(marker, 'drag', function (evt) {
        $("#txtLatitude").val(evt.latLng.lat().toFixed(5));
        $("#txtLogitude").val(evt.latLng.lng().toFixed(5));
    });
    if (txtLat !== '' && txtLng !== '') {
         let myLatlng = new google.maps.LatLng(parseFloat(txtLat), parseFloat(txtLng));
        marker.setPosition(myLatlng);
    }
    function toggleBounce(e) {
        if (marker.getAnimation() !== null) {
            marker.setAnimation(null);
        } else {
            marker.setAnimation(google.maps.Animation.BOUNCE);
        }
    }
}
