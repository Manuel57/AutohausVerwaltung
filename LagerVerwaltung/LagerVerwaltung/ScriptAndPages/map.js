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
        //calcRoute(werkstatt.coordinates, lagerCoords[i].coordinates);
    }
    getLongLat(werkstatt);

    this.mapInfo.matrixService.getDistanceMatrix(
      {
          origins: [werkstatt.coordinates],
          destinations: lagerCoords.map(function (el) { return el.coordinates; }),
          travelMode: google.maps.TravelMode.DRIVING,
          unitSystem: google.maps.UnitSystem.METRIC,
          avoidHighways: false,
          avoidTolls: false
      }, callback);

    function callback(response, status) {

        for (var i in response.rows) {
            for (var j in response.rows[i].elements) {
            }
        }
        var shortest = response.rows[0].elements.reduce(function (p, v) {
            return (p.duration.value < v.duration.value ? p : v);
        });

        var cnt = 0;
        for (var i in response.rows) {
            for (var j in response.rows[i].elements) {
                if (shortest == response.rows[i].elements[j]) {
                    cnt = j;
                    break;
                }
            }
        }
        calcRoute(werkstatt.coordinates, response.destinationAddresses[cnt]);

    }

}


function calcRoute(orig, dest) {

    var request = {
        origin: orig,
        destination: dest,
        travelMode: google.maps.TravelMode.DRIVING
    };
    this.mapInfo.directionsService.route(request, function (result, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            this.mapInfo.directionsDisplay.setDirections(result);
        }
    });
}

