function MapInfo() {

    this._bounds = new google.maps.LatLngBounds();
    this._id = 1;
    this._map = null;


    this.setMarker = function (m) {
        this._geomarker = m;
        this._id++;
    };

    this.getMarker = function () {
        return this._geomarker;
    };

    this.setMap = function (m) {
        this._map = m;
    };
    this.getMap = function () {
        return this._map;
    };

    this.addBound = function () {
        this._bounds.extend(this._lonituteLatitute);
        this._map.fitBounds(this._bounds);
    };

    this.setLonLat = function (ll) {
        this._lonituteLatitute = ll;
    };

    this.getLonlat = function () {
        return this._lonituteLatitute;
    };

    this.existsMap = function () {
        return this._map != null;
    };
    this.createMap = function (ph, o) {
        o.center = this._lonituteLatitute;
        this._map = new google.maps.Map(ph, o);
    };
    this.getId = function () {
        return this._id;
    };
}

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
    google.maps.event.addListener(marker, 'mouseover', function () { infowindow.open(mapInfo.map, marker); });
    google.maps.event.addListener(marker, 'click', function () { infowindow.open(mapInfo.map, marker); });

}


function getLongLat(info) {
    mapInfo.setLonLat(info.coordinates);
    mapInfo.setMarker(info.name);
    draw();
}

