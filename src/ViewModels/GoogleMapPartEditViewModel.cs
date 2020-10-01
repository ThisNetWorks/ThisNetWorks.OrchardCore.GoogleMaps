using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;

namespace ThisNetWorks.OrchardCore.GoogleMaps.ViewModels
{
    public class GoogleMapPartEditViewModel
    {
        public string Location { get; set; }
        public double Lng { get; set; }
        public double Lat { get; set; }

        [BindNever]
        public ContentItem ContentItem { get; set; }

        [BindNever]
        public GoogleMapPart GoogleMapPart { get; set; }

        [BindNever]
        public GoogleMapsSettings Settings { get; set; }
    }
}
