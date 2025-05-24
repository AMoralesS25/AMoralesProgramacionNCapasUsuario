(g => { var h, a, k, p = "The Google Maps JavaScript API", c = "google", l = "importLibrary", q = "__ib__", m = document, b = window; b = b[c] || (b[c] = {}); var d = b.maps || (b.maps = {}), r = new Set, e = new URLSearchParams, u = () => h || (h = new Promise(async (f, n) => { await (a = m.createElement("script")); e.set("libraries", [...r] + ""); for (k in g) e.set(k.replace(/[A-Z]/g, t => "_" + t[0].toLowerCase()), g[k]); e.set("callback", c + ".maps." + q); a.src = `https://maps.${c}apis.com/maps/api/js?` + e; d[q] = f; a.onerror = () => h = n(Error(p + " could not load.")); a.nonce = m.querySelector("script[nonce]")?.nonce || ""; m.head.append(a) })); d[l] ? console.warn(p + " only loads once. Ignoring:", g) : d[l] = (f, ...n) => r.add(f) && u().then(() => d[l](f, ...n)) })({
    key: "AIzaSyDCt5qUAX_luLPwkcfUOJutLLavQwVFgcA",
    v: "weekly",
});


let map;
let marker = null;

async function initMap() {
    const cdmx = { lat: 19.4326, lng: -99.1332 };

    const { Map } = await google.maps.importLibrary("maps");

    map = new Map(document.getElementById("mapa"), {
        zoom: 12,
        center: cdmx,
        mapId: "mapa",
    });

    const lat = parseFloat(document.getElementById("lat").value);
    const lng = parseFloat(document.getElementById("lng").value);

    if (!isNaN(lat) && !isNaN(lng)) {
        placeMarker(lat, lng);
    }

    map.addListener("click", (event) => {
        const latLng = event.latLng;

        if (!marker) {
            placeMarker(latLng.lat(), latLng.lng());
        } else {
            marker.setPosition(latLng);
        }

        document.getElementById("lat").value = latLng.lat();
        document.getElementById("lng").value = latLng.lng();
    });
}

function placeMarker(lat, lng) {
    const latLng = { lat: lat, lng: lng };

    if (marker) {
        marker.setPosition(latLng);
    } else {
        marker = new google.maps.Marker({
            map: map,
            position: latLng,
            title: "Ubicación",
            draggable: true,
        });

        marker.addListener("dragend", () => {
            const position = marker.getPosition();
            document.getElementById("lat").value = position.lat();
            document.getElementById("lng").value = position.lng();
        });
    }

    map.setCenter(latLng);
}

async function fetchLocationData(id) {
    const response = await fetch(`/api/locations/${id}`);
    if (response.ok) {
        const data = await response.json();
        return data; 
    } else {
        console.error("Error");
    }
}

initMap();
