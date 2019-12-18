using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.ContentManagement.Records;
using ThisNetWorks.OrchardCore.GoogleMaps.Indexes;

namespace ThisNetWorks.OrchardCore.GoogleMaps
{
    public class Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public int Create()
        {
            _contentDefinitionManager.AlterPartDefinition("GoogleMapPart", builder => builder
                .Attachable()
                .WithDescription("Provides a Google Map part for your content item."));

            return 1;
        }

        public int UpdateFrom1()
        {
            SchemaBuilder.CreateMapIndexTable(nameof(GoogleMapPartIndex), table => table
                .Column<string>("Lat", col => col.WithLength(25))
                .Column<string>("Lng", col => col.WithLength(25))
                .Column<string>("ContentItemId", c => c.WithLength(26))
                .Column<string>("ContentType", column => column.WithLength(ContentItemIndex.MaxContentTypeSize))
                .Column<string>("Location", column => column.WithLength(GoogleMapPartIndex.MaxLocationSize))
            );

            // Index on content type as that is most likely to be used for retrieving data from index
            // without having to query document table as well.
            SchemaBuilder.AlterTable(nameof(GoogleMapPartIndex), table => table
                .CreateIndex("IDX_GoogleMapPartIndex_ContentType", "ContentType")
            );

            return 2;
        }
    }
}