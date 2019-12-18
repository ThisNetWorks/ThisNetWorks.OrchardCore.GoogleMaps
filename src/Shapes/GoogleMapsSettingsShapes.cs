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
            builder.Describe("GoogleMapPart_Summary")
                .OnDisplaying(displaying =>
                {
                    IShape editor = displaying.Shape;

                    if (editor.Metadata.Type == "GoogleMapPart_Summary")
                    {
                        editor.Metadata.Wrappers.Add("DisplayMap_Wrapper__Settings");
                    }
                });
        }
    }
}
