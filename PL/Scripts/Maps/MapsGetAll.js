(g => { var h, a, k, p = "The Google Maps JavaScript API", c = "google", l = "importLibrary", q = "__ib__", m = document, b = window; b = b[c] || (b[c] = {}); var d = b.maps || (b.maps = {}), r = new Set, e = new URLSearchParams, u = () => h || (h = new Promise(async (f, n) => { await (a = m.createElement("script")); e.set("libraries", [...r] + ""); for (k in g) e.set(k.replace(/[A-Z]/g, t => "_" + t[0].toLowerCase()), g[k]); e.set("callback", c + ".maps." + q); a.src = `https://maps.${c}apis.com/maps/api/js?` + e; d[q] = f; a.onerror = () => h = n(Error(p + " could not load.")); a.nonce = m.querySelector("script[nonce]")?.nonce || ""; m.head.append(a) })); d[l] ? console.warn(p + " only loads once. Ignoring:", g) : d[l] = (f, ...n) => r.add(f) && u().then(() => d[l](f, ...n)) })({
    key: "AIzaSyDCt5qUAX_luLPwkcfUOJutLLavQwVFgcA",
    v: "weekly",
});


async function initMap() {
    const cdmx = { lat: 19.4326, lng: -99.1332 };  

    const { Map } = await google.maps.importLibrary("maps");

    const map = new Map(document.getElementById("mapa"), {
        zoom: 10,
        center: cdmx,  
        mapId: "mapa",
    });

    if (empresasData && Array.isArray(empresasData)) {
        empresasData.forEach(empresa => {

            const lat = parseFloat(empresa.Latitud);
            const lng = parseFloat(empresa.Longitud);

            if (!isNaN(lat) && !isNaN(lng)) {
                const latLng = { lat: lat, lng: lng };

                const marker = new google.maps.Marker({
                    map: map,
                    position: latLng,
                    title: empresa.Nombre,
                });

                const infoWindow = new google.maps.InfoWindow({
                    content: `<h6>${empresa.Nombre}</h6>`, 
                });

                marker.addListener("click", () => {
                    infoWindow.open(map, marker);
                });
            } else {
                console.error(`Ubicación inválida para la empresa ${empresa.Nombre}. Latitud: ${empresa.Latitud}, Longitud: ${empresa.Longitud}`);
            }
        });
    } else {
        console.warn('No se han encontrado datos de empresas para mostrar en el mapa.');
    }
}

initMap();

