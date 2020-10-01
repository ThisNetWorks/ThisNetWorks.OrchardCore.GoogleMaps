using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Shapes
{
    public class GoogleMapsSettingsShapes : IShapeTableProvider
    {
        public void Discover(ShapeTableBuilder builder)
        {
            // builder.Describe("GoogleMapPart_Summary")
            //     .OnDisplaying(displaying =>
            //     {
            //         IShape display = displaying.Shape;

            //         if (display.Metadata.Type == "GoogleMapPart_Summary")
            //         {
            //             display.Metadata.Wrappers.Add("DisplayMap_Wrapper__Settings");
            //         }
            //     });

            // builder.Describe("QueryGoogleMaps")
            //     .OnDisplaying(displaying =>
            //     {
            //         IShape display = displaying.Shape;

            //         if (display.Metadata.Type == "QueryGoogleMaps")
            //         {
            //             display.Metadata.Wrappers.Add("DisplayMap_Wrapper__Settings");
            //         }
            //     });
        }
    }
}
