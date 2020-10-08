using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using System;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using YesSql.Indexes;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Indexes
{
    public class GoogleMapPartIndex : MapIndex
    {
        public string ContentType { get; set; }
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

                    if (googleMapPart == null)
                    {
                        return null;
                    }

                    var googleMapPartIndex = new GoogleMapPartIndex
                    {
                        ContentType = contentItem.ContentType
                    };

                    if (googleMapPartIndex.ContentType?.Length > ContentItemIndex.MaxContentTypeSize)
                    {
                        googleMapPartIndex.ContentType = googleMapPartIndex.ContentType.Substring(ContentItemIndex.MaxContentTypeSize);
                    }

                    return googleMapPartIndex;
                });
        }
    }
}
