//1) Read the location string from hfMapLocations 
//2) Convert String to Array by Separtor <>
//3) If locations string is empty then set array=[] to not loop throuth empty array and if not empty loop and compine a new array of arrays
var MapLocations = document.getElementById("hfMapLocations").value;
var array = MapLocations.split('<>');
if (array[0] == "") {
    array = [];
}
var locations = [];
for (var i = 0; i < array.length; i++) {
    let location = array[i].split(",");
    let info = location[0];
    let lat = parseFloat(location[1]) || 0;
    let lng = parseFloat(location[2]) || 0;
    let index = parseInt(location[3]) || 0;
    //Push Array to array to compine group of array 
    let locationArr = [info, lat, lng, index];
    locations.push(locationArr);
}
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
    zoom: 12,
    closeBoxMargin: "17px 20px 2px 2px",
    center: new google.maps.LatLng(25.7809021218639, 56.057677350097705),
    mapTypeId: google.maps.MapTypeId.MONOCITY,
    styles: styles
});

var icons = {
    gate: {
        icon: 'img/gate-mark.png'
    }
};
var infowindow = new google.maps.InfoWindow();

var marker, i;
var bounds = new google.maps.LatLngBounds();
for (i = 0; i < locations.length; i++) {
    marker = new google.maps.Marker({
        position: new google.maps.LatLng(locations[i][1], locations[i][2]),
        map: map,
        icon: 'img/gate-mark.png'
    });
    google.maps.event.addListener(marker, 'click', (function (marker, i) {
        return function () {
            infowindow.setContent(locations[i][0]);
            infowindow.open(map, marker);
        }
    })(marker, i));


}
function ShowOnMap(lat, lng) {
    map.setCenter({
        lat: parseFloat(lat.replace(",", ".")),
        lng: parseFloat(lng.replace(",", "."))
    });
    map.setZoom(17);
}