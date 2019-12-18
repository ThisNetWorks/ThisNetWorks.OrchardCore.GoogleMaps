using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Text;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using YesSql.Indexes;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Indexes
{
    public class GoogleMapPartIndex : MapIndex
    {
        public string ContentItemId { get; set; }
        public string Location { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
    }


    public class GoogleMapPartIndexProvider : IndexProvider<ContentItem>
    {
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<GoogleMapPartIndex>()
                .Map(contentItem =>
                {
                    if (!contentItem.IsPublished())
                    {
                        return null;
                    }

                    var googleMapPart = contentItem.As<GoogleMapPart>();

                    if (googleMapPart == null || String.IsNullOrEmpty(googleMapPart.Lat) || String.IsNullOrEmpty(googleMapPart.Lng))
                    {
                        return null;
                    }

                    return new GoogleMapPartIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        Location = googleMapPart.Location,
                        Lat = googleMapPart.Lat,
                        Lng = googleMapPart.Lng
                    };
                });
        }
    }
}
