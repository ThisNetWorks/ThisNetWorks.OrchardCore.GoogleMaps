using System.ComponentModel.DataAnnotations;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Settings
{
    public class GoogleMapsSettings
    {
        public const double DefaultLatitude = -41.2443487;
        public const double DefaultLongitude = 174.6916439;

        public string ApiKey { get; set; }
        public string Location { get; set; }

        public LatLng DefaultMarker { get; set; } = new LatLng { Lat = DefaultLatitude, Lng = DefaultLongitude };
    }
}
