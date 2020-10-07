var map;

var googleMapsEditor;

var mapInit = function () {
    var defaultLat = $('#googleMapSettings').data('lat');
    var defaultLng = $('#googleMapSettings').data('lng');
    map = new google.maps.Map(document.getElementById('google-map'), {
        center: { lat: defaultLat, lng: defaultLng },
        zoom: 8
    });

    if (googleMapsEditor) {
        googleMapsEditor.setAllMapShapes();
        googleMapsEditor.setAutocomplete();
    }

    map.addListener('click', function(e) {
        if (!googleMapsEditor) {
            console.log('error in vue app');
            return;
        }

        googleMapsEditor.mapClicked(e.latLng);

    }); 
};

function initializeGoogleMapsEditor(elem, data, modalBodyId) {
    var initialPolygon;
    var initialPolygonIndex;
    var polygons = [];
    if (data.polygons.length > 0) {    
        polygons = data.polygons.map((polygon) => {
            return {
                name: polygon.name,
                strokeColor: polygon.strokeColor,
                strokeOpacity: polygon.strokeOpacity,
                strokeWeight: polygon.strokeWeight,
                fillColor: polygon.fillColor,
                fillOpacity: polygon.fillOpacity,
                latLngs: polygon.latLngs,
                selectedIndex: polygon.latLngs.length > 0 ? polygon.latLngs.length -1 : 0           
            }
        });
        initialPolygon = polygons[0];  
        initialPolygonIndex = 0;
    }
    var polygonShapes = [];
    var marker;
    var autocomplete;

    var store = {
        debug: false,
        state: {
            defaultLocation: data.defaultLocation,
            marker: data.marker,
            polygons: polygons,
            selectedPolygon: initialPolygon,
            selectedPolygonIndex: initialPolygonIndex
        },
        addPolygon: function () {
            this.state.polygons.push({ 
                name: 'enter an identifier here',
                strokeColor: '#FF0000',
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: '#FF0000',
                fillOpacity: 0.35,
                latLngs: [],
                selectedIndex: 0
            });
        },
        removePolygon: function (index) {
            this.state.polygons.splice(index, 1);
            this.setAllMapShapes();
        },
        selectPolygon: function (index) {
            this.state.selectedPolygon = this.state.polygons[index];
            this.state.selectedPolygonIndex = index;
            this.setAllMapShapes();
        },
        selectPolygonLatLngIndex: function (index) {
            this.state.selectedPolygon.selectedIndex = index;
            this.setPolygons();
        },     
        addPolygonLatLng: function () {
            var polygon = this.state.polygons.splice(this.state.selectedPolygonIndex, 1)[0];
            var newIndex = 0;
            if (polygon.latLngs.length != 0) {
                newIndex = polygon.selectedIndex + 1;
            }            
            polygon.selectedIndex = newIndex;
            polygon.latLngs.push({ lat: '', lng: '' });
            this.state.polygons.splice(this.state.selectedPolygonIndex, 0, polygon);
            this.setAllMapShapes();

        },
        addPolygonLatLngs: function (latLngs) {
            var polygon = this.state.polygons.splice(this.state.selectedPolygonIndex, 1)[0];
            var newIndex = 0;
            if (polygon.latLngs.length != 0) {
                newIndex = polygon.selectedIndex + 1;
            }
            polygon.latLngs.splice(newIndex, 0, { lat: latLngs.lat(), lng: latLngs.lng() });
            polygon.selectedIndex = newIndex;
            this.state.polygons.splice(this.state.selectedPolygonIndex, 0, polygon);
            this.setAllMapShapes();
        },
        removePolygonLatLng: function (index) {
            var polygon = this.state.polygons.splice(this.state.selectedPolygonIndex, 1)[0];
            polygon.latLngs.splice(index, 1);
            var newIndex = 0;
            if (polygon.latLngs.length != 0) {
                newIndex = polygon.selectedIndex - 1;
            }        
            polygon.selectedIndex = newIndex;    
            this.state.polygons.splice(this.state.selectedPolygonIndex, 0, polygon);
            this.setAllMapShapes();
        },
        getJson: function () {
            return JSON.stringify({ 
                marker: this.state.marker,
                polygons: this.state.polygons.map((polygon) => {
                    return {
                        name: polygon.name,
                        strokeColor: polygon.strokeColor,
                        strokeOpacity: polygon.strokeOpacity,
                        strokeWeight: polygon.strokeWeight,
                        fillColor: polygon.fillColor,
                        fillOpacity: polygon.fillOpacity,
                        latLngs: polygon.latLngs.filter(latLng => {
                            return latLng.lat && latLng.lng;
                        })                   
                    }
                })
            });
        },
        setAllMapShapes: function () {
            if (!map) {
                return;
            }            
            this.setPolygons();
            this.setMarker();
            this.setBounds();
        },
        setBounds: function () {
            var bounds = new google.maps.LatLngBounds();
            var hasLocation = false;
            polygonShapes.forEach((shape, i) => {
                shape.markers.forEach((marker) => {
                    bounds.extend(marker.position);
                    hasLocation = true;
                });
            });

            if (marker) {
                bounds.extend(marker.position);
                hasLocation = true;
            }

            if (!hasLocation) {
                map.setCenter(new google.maps.LatLng(this.state.defaultLocation.lat, this.state.defaultLocation.lng));
            } else {
                map.fitBounds(bounds);
            }
        },
        setMarker: function (latLng) {
            if (latLng) {
                this.state.marker.latLng.lat = latLng.lat();
                this.state.marker.latLng.lng = latLng.lng();
            }       
            if (this.state.marker && this.state.marker.latLng && this.state.marker.latLng.lat !== 0 && this.state.marker.latLng.lng !== 0) {
                if (marker) {
                    marker.setMap(null);
                }
     
                var position = new google.maps.LatLng(this.state.marker.latLng.lat, this.state.marker.latLng.lng);
                marker = new google.maps.Marker({
                    position: position,
                    map: map
                });
   
                // probably don't need this because we want to do a bounds everytime?
                map.panTo(position);
            }
        },
        clearMarker: function () {
            this.state.marker.location = '';
            this.state.marker.latLng.lat = 0;
            this.state.marker.latLng.lng = 0;
            if (marker) {
                marker.setMap(null);
            }     
            marker = null;
        },
        setPolygons: function () {
            var self = this;

            // for each polygon set the map
            this.state.polygons.forEach((polygon, polygonI) => {
                var points = [];
                var markers = [];
                var listeners = [];
                polygon.latLngs.forEach((point, pointI) => {
                    if (point.lat && point.lng) {
                        var latLng = new google.maps.LatLng(point.lat, point.lng);
                        points.push(latLng);
                        var strokeColor = 'black';
                        if (self.state.selectedPolygonIndex == polygonI) {
                            if (polygon.selectedIndex == pointI) {
                                strokeColor = '#001bff';
                            }
                            // if it's the last one in the index it goes to the first
                            // otherwise it goes to the next
                            else if (polygon.selectedIndex == polygon.latLngs.length -1 && pointI == 0) {
                                strokeColor = '#007bff';
                            } else if (polygon.selectedIndex != polygon.latLngs.length -1 && pointI == polygon.selectedIndex + 1) {
                                strokeColor = '#007bff';
                            }
                        }

                        var marker = new google.maps.Marker({
                            position: latLng,
                            icon: {
                              path: google.maps.SymbolPath.CIRCLE,
                              scale: 5,
                              strokeColor: strokeColor
                            },
                            draggable: true,
                            map: map,
                        });
                        markers.push(marker);
                        var listener = google.maps.event.addListener(marker, 'dragend', function(e){
                            var draggedPolygon = self.state.polygons.splice(polygonI, 1)[0];
                            var latLng = draggedPolygon.latLngs[pointI];
                            latLng.lat = e.latLng.lat();
                            latLng.lng = e.latLng.lng();
                            draggedPolygon.selectedIndex = pointI;

                            self.state.polygons.splice(polygonI, 0, draggedPolygon);
                            self.setPolygons();                            
                        });    
                        listeners.push(listener);                    
                    }
                });
                
                if (polygonShapes[polygonI] && polygonShapes[polygonI].shape) {
                    polygonShapes[polygonI].listeners.forEach((listener) => {
                        google.maps.event.removeListener(listener);
                        listener = null;
                    });                      
                    polygonShapes[polygonI].markers.forEach((marker, index) => {
                        marker.setMap(null);
                        marker = null;
                    }); 
                    polygonShapes[polygonI].shape.setMap(null);                 
                }

                polygonShapes[polygonI] = {
                    shape: new google.maps.Polygon({
                        paths: points,
                        strokeColor: polygon.strokeColor,
                        strokeOpacity: polygon.strokeOpacity,
                        strokeWeight: polygon.strokeWeight,
                        fillColor: polygon.fillColor,
                        fillOpacity: polygon.fillOpacity
                    }),
                    markers: markers,
                    listeners: listeners
                };
                polygonShapes[polygonI].shape.setMap(map);
            });
        }
    }

    var markerEditor = {
        template: '#marker-editor',
        props: ['data'],
        name: 'marker-editor',

        methods: {
            clear: function () {
                store.clearMarker();
            },
            setAutocomplete: function() {
                autocomplete = new google.maps.places.Autocomplete(this.$refs.location);
                places = new google.maps.places.PlacesService(map);
            
                autocomplete.addListener('place_changed', function () {
                    var place = autocomplete.getPlace();
                    if (place.geometry) {
                        store.setMarker(place.geometry.location);
                    } else {
                        store.clearMarker();
                    }
                });   
            }
        }
    };

    var polygonMetadataEditor = {
        template: '#polygon-metadata-editor',
        props: ['data', 'index', 'jsonid'],
        name: 'polygon-metadata-editor',
        methods: {
            remove: function () {
                store.removePolygon(index);
            }
        }
    };

    var polygonLatlngsTable = {
        template: '#polygon-latlngs-table',
        props: ['data', 'index'],
        name: 'polygon-latlngs-table',
        methods: {
            add: function () {
                store.addPolygonLatLng();
            },
            remove: function (index) {
                store.removePolygonLatLng(index);
            },
            dragEnd: function (e) {
                store.selectPolygonLatLngIndex(e.newIndex);
                store.setPolygons();
            },
            selectIndex: function (index) {
                store.selectPolygonLatLngIndex(index);
            },
            getSelectedColor: function(index) {
                if (this.data.selectedIndex == index) {
                    return { 'background-color': '#001bff'}
                } else if (this.data.selectedIndex == this.data.latLngs.length -1 && index == 0) {
                    return { 'background-color': '#007bff'}
                } else if (this.data.selectedIndex != this.data.latLngs.length -1 && index == this.data.selectedIndex + 1) {
                    return { 'background-color': '#007bff'}
                }
            }
        }
    };

    var latlngsModal = {
        template: '#latlngs-modal',
        props: ['data'],
        name: 'latlngs-modal',
        methods: {
            showModal: function () {
                $('#' + modalBodyId).modal();
            },
            closeModal: function () {
                var modal = $('#' + modalBodyId).modal();
                modal.modal('hide');
                store.setPolygons();
            }

        }
    };

    googleMapsEditor = new Vue({
        components: {
            markerEditor: markerEditor,
            polygonMetadataEditor: polygonMetadataEditor,
            polygonLatlngsTable: polygonLatlngsTable,
            latlngsModal: latlngsModal
        },
        data: {
            sharedState: store.state,
            activeTabId: 'tabMarker'
        },
        el: elem,
        mounted: function () {
            var self = this;
            this.setAllMapShapes();
            $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                self.activateTab(e.target);
            });
        },
        methods: {
            activateTab: function (e) {
                this.activeTabId = e.id;
            },
            showModal: function () {
                latlngsModal.methods.showModal();
            },
            addPolygon: function () {
                store.addPolygon();
            },
            removePolygon: function (index) {
                store.removePolygon(index);
            },
            getJson: function () {
                return store.getJson();
            },
            selectPolygon: function (index) {
                store.selectPolygon(index);
            },
            addPolygonLatLngs: function (latLngs) {
                store.addPolygonLatLngs(latLngs)
            },
            setMarker: function (latLng) {
                store.setMarker(latLng);
            },
            setAllMapShapes: function () {
                store.setAllMapShapes()
            },
            mapClicked: function (latLng) {
                switch (this.activeTabId) {
                    case 'tabMarker' :
                        this.setMarker(latLng);
                        break;
                    case 'tabPolygons' :
                        this.addPolygonLatLngs(latLng);
                        break;
                    default :
                        console.log('error on map click');
                    
                }
            },
            setAutocomplete: function () {
                this.$refs.markerEditor.setAutocomplete();
            }
        }
    });
}
