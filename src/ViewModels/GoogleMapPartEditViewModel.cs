using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;

namespace ThisNetWorks.OrchardCore.GoogleMaps.ViewModels
{
    public class GoogleMapPartEditViewModel
    {
        public string Location { get; set; }
        public LatLng Marker { get; set; }
        public string Polygons { get; set; }
        public string Json { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GoogleMapPart GoogleMapPart { get; set; }

        [BindNever]
        public GoogleMapsSettings Settings { get; set; }
    }

    public class GoogleMapEditModel
    {
        public LatLng DefaultLocation { get; set; }
        public MarkerModel Marker { get; set; }
        public Polygon[] Polygons { get; set; } = Array.Empty<Polygon>();

    }

    public class MarkerModel
    {

        public string Location { get; set; }    
        public LatLng LatLng { get; set; }
    }
}
