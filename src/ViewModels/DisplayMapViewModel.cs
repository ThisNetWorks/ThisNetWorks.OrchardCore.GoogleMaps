using ThisNetWorks.OrchardCore.GoogleMaps.Models;

namespace ThisNetWorks.OrchardCore.GoogleMaps.ViewModels
{
    public class DisplayMapViewModel
    {
        public LatLng Marker { get; set; }
        public Polygon[] Polygons { get; set; }
        public string Path { get; set; }
    }
}
