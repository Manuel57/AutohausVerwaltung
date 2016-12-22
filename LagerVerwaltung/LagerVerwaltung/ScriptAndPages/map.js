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
        position: mapInfo.getLonlat(),
        map: mapInfo.getMap(),
        title: mapInfo.getMarker(),
        id: mapInfo.getId()
    });
    mapInfo.addBound();

}

function initMap() {
    getLongLat("9500 Villach", "Werkstatt A");
    getLongLat("9800 Spittal/Drau", "Lager A");
}

function getLongLat(address, m) {
    var lonnlat = { lat: 0, lng: 0 };
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({
        'address': address
    }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            lonnlat.lat = results[0].geometry.location.lat();
            lonnlat.lng = results[0].geometry.location.lng();
            mapInfo.setLonLat(lonnlat);
            mapInfo.setMarker(m);
            draw();
        } else {
            alert("Request failed.");
        }
    });
}

