using System.ComponentModel.DataAnnotations;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Settings
{
    public class GoogleMapsSettings
    {
        public const double DefaultLatitude = -41.2443487;
        public const double DefaultLongitude = 174.6916439;

        public string ApiKey { get; set; }
        public string Location { get; set; }
        [Required]
        public double DefaultLat { get; set; } = DefaultLatitude;
        [Required]
        public double DefaultLng { get; set; } = DefaultLongitude;
    }
}
