function MapInfo() {


    this._id = 1;
    this._map = null;

    this.directionsService = null;// new google.maps.DirectionsService();
    this.directionsDisplay = null;//new google.maps.DirectionsRenderer();
    this.matrixService = null;


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
        this.directionsService = new google.maps.DirectionsService();
        this.directionsDisplay = new google.maps.DirectionsRenderer();
        this.matrixService = new google.maps.DistanceMatrixService();
        this._map = new google.maps.Map(ph, o);
        this.directionsDisplay.setMap(this._map);
        this._bounds = new google.maps.LatLngBounds();
    };
    this.getId = function () {
        return this._id;
    };
}
