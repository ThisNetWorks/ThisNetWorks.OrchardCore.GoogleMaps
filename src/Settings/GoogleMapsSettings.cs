using ThisNetWorks.OrchardCore.GoogleMaps.Models;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Settings
{
    public class GoogleMapsSettings
    {
        public const double DefaultLatitude = -40.84855059999999;
        public const double DefaultLongitude = 172.8079347;

        public string ApiKey { get; set; }
        public string Location { get; set; }

        public LatLng DefaultMarker { get; set; } = new LatLng { Lat = DefaultLatitude, Lng = DefaultLongitude };
    }
}
