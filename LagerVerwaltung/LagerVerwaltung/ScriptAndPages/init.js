(function (el) {
    var args = arguments;
    document.addEventListener("DOMContentLoaded", function () {
        (function () {
            for (var i = 1; i < args.length; i++) {
                document.getElementsByTagName(el)[0].appendChild((function (src) {
                    return function setAt(elem) {
                        for (var i = 1; i < arguments.length; i += 2) {
                            elem.setAttribute(arguments[i], arguments[i + 1]);
                        }
                        return elem;
                    }(document.createElement("script"), "src", src, "type", "text/javascript");
                })(args[i]));
            }
        })();
    }, false);
})("head", "http://maps.googleapis.com/maps/api/js?sensor=false", "mapInfo.js", "data.js", "map.js");


