using System.ComponentModel.DataAnnotations;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Settings
{
    public class GoogleMapsSettings
    {
        public const string DefaultLatitude = "-41.2443487";
        public const string DefaultLongitude = "174.6916439";

        public string ApiKey { get; set; }
        public string Location { get; set; }
        [Required]
        public string DefaultLat { get; set; } = DefaultLatitude;
        [Required]
        public string DefaultLng { get; set; } = DefaultLongitude;
    }
}
