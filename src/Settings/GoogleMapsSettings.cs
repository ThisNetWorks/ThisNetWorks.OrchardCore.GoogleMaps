using System.ComponentModel.DataAnnotations;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Settings
{
    public class GoogleMapsSettings
    {
        public string ApiKey { get; set; }
        public string ApiVersion { get; set; }
        [Required]
        public string DefaultLat { get; set; } = "-41.2443487";
        [Required]
        public string DefaultLng { get; set; } = "174.6916439";
    }
}
