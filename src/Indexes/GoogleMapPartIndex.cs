using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using System;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using YesSql.Indexes;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Indexes
{
    public class GoogleMapPartIndex : MapIndex
    {
        public const int MaxLocationSize = 255;
        public string ContentItemId { get; set; }
        public string ContentType { get; set; }
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

                    var googleMapPartIndex = new GoogleMapPartIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        ContentType = contentItem.ContentType,
                        Location = googleMapPart.Location,
                        Lat = googleMapPart.Lat,
                        Lng = googleMapPart.Lng
                    };

                    if (googleMapPartIndex.ContentType?.Length > ContentItemIndex.MaxContentTypeSize)
                    {
                        googleMapPartIndex.ContentType = googleMapPartIndex.ContentType.Substring(ContentItemIndex.MaxContentTypeSize);
                    }

                    if (googleMapPartIndex.Location?.Length > GoogleMapPartIndex.MaxLocationSize)
                    {
                        googleMapPartIndex.Location = googleMapPartIndex.Location.Substring(GoogleMapPartIndex.MaxLocationSize);
                    }

                    return googleMapPartIndex;
                });
        }
    }
}
