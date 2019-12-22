using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Settings
{
    public class GoogleMapsSettingsViewModel
    {
        public string ApiKey { get; set; }
        public string Location { get; set; }
        public string DefaultLat { get; set; }
        public string DefaultLng { get; set; }

        [BindNever]
        public GoogleMapsSettings GoogleMapSettings { get; set; }
    }
}
