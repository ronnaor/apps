function addMarker(results) {
    for (var i = 0; i < results.features.length; i++) {
        var latLng = new google.maps.LatLng(results.Lan, results.Lng);
        var marker = new google.maps.Marker({
            position: latLng,
            map: map
        });
    }
}

function myMap() {
    var mapCanvas = document.getElementById("myMap");
    var myCenter = new google.maps.LatLng(51.508742, -0.120850);
    var mapOptions = {
        center: new google.maps.LatLng(51.508742, -0.120850),
        zoom: 3
    };
    var map = new google.maps.Map(mapCanvas, mapOptions);
    var custoMarker = {
        url: 'images/shauli.png',
        size: new google.maps.Size(55, 55)
    };

    var marker = new google.maps.Marker({
        position: myCenter,
        map: map,
        animation: google.maps.Animation.BOUNCE
    });
    marker.setMap(map);
    var marker2 = new google.maps.Marker({
        position: new google.maps.LatLng(51, 0),
        map: map,
        animation: google.maps.Animation.BOUNCE
    });
    marker2.setMap(map)
    $.getJSON("/Map/Locations", function(data){
        $.each(data, function (lan, lng) {
            var latLng1 = new google.maps.LatLng(lan, lng);
            var marker1 = new google.maps.Marker({
                position: latLng1,
                map: map
            });
            marker1.setMap(map);
        })
        });
};



