using System;
using Newtonsoft.Json;
using OrchardCore.ContentManagement;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Models
{
    public class Polygon
    {   
        public string Name { get; set; }
        public string StrokeColor { get; set; } = "#FF0000";
        public double StrokeOpacity { get; set; } = 0.8;
        public double StrokeWeight { get; set; } = 2;
        public string FillColor { get; set; } = "#FF0000";
        public double FillOpacity { get; set; } = 0.35;
        public LatLng[] LatLngs { get; set; } = Array.Empty<LatLng>();
    }
}
