using Microsoft.AspNetCore.Mvc.ModelBinding;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Settings
{
    public class GoogleMapsSettingsViewModel
    {
        public string ApiKey { get; set; }
        public string Location { get; set; }
        public LatLng DefaultMarker { get; set; }

        [BindNever]
        public GoogleMapsSettings GoogleMapSettings { get; set; }
    }
}
