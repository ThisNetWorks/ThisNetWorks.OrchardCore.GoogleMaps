﻿using OrchardCore.ContentManagement;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Models
{
    public class GoogleMapPart : ContentPart
    {
        /// <summary>
        /// Location returned from google places, if places has been used to select location.
        /// </summary>
        public string Location { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
