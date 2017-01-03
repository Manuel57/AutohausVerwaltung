var mapInfo = new MapInfo();
//mapInfo.constructor();


// draws the map to the html
function draw() {

    if (!mapInfo.existsMap()) {
        var options = {
            zoom: 10,
            gestureHandling: 'cooperative'
        }
        mapInfo.createMap(document.getElementById('mapPlaceholder'),
            options);

    }
    var marker = new google.maps.Marker({
        position: new google.maps.LatLng(mapInfo.getLonlat().lat, mapInfo.getLonlat().lng), //mapInfo.getLonlat(),
        map: mapInfo.getMap(),
        title: mapInfo.getMarker(),
        id: mapInfo.getId()
    });
    mapInfo.addBound();
    var infowindow = new google.maps.InfoWindow({ content: '<p>' + mapInfo.getMarker() + '</p>' });
    //google.maps.event.addListener(marker, 'mouseover', function () { infowindow.open(mapInfo.map, marker); });
    //google.maps.event.addListener(marker, 'click', function () { infowindow.open(mapInfo.map, marker); });
    infowindow.open(mapInfo.map, marker);
}


function getLongLat(info) {
    mapInfo.setLonLat(info.coordinates);
    mapInfo.setMarker(info.name);
    draw();
}



function initMap() {
    for (var i in lagerCoords) {
        getLongLat(lagerCoords[i]);
    }
}

