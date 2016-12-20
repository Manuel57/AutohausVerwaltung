

class MapInfo {
    constructor() {
        this.bounds = new google.maps.LatLngBounds();
    }
    
    set marker(m) {
        this._geomarker = m;
        this._id++;
    }

    get marker() {
        return this._geomarker;
    }

    set geomap(m) {
        this.map = m;
    }

    addBound() {
        this.bounds.extend(this.lonlat);
        this.map.fitBounds(this.bounds);
    }

    set lonlat(ll) {
        this._lonituteLatitute = ll;
    }

    get lonlat() {
        return this._lonituteLatitute;
    }

    existsMap() {
        return this.map != null;
    }
    createMap(ph, o) {
        o.center = this.lonlat;
        this.geomap = new google.maps.Map(ph, o);
    }
    get Id() {
        return this._id;
    }
}
var mapInfo = new MapInfo();

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
        position: mapInfo.lonlat,
        map: mapInfo.map,
        title: mapInfo.marker,
        id: mapInfo.Id
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
            mapInfo.lonlat = lonnlat;
            mapInfo.marker = m;
            draw();
        } else {
            alert("Request failed.");
        }
    });
}

