using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;

namespace ThisNetWorks.OrchardCore.GoogleMaps.ViewModels
{
    public class GoogleMapPartViewModel
    {
        public string Location { get; set; }
        public LatLng Marker { get; set; }
        public Polygon[] Polygons { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GoogleMapPart GoogleMapPart { get; set; }

        [BindNever]
        public GoogleMapsSettings Settings { get; set; }
    }
}
