using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.Data.Migration;
using OrchardCore.ContentManagement.Records;
using ThisNetWorks.OrchardCore.GoogleMaps.Indexes;
using YesSql.Sql;

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
            SchemaBuilder.CreateMapIndexTable<GoogleMapPartIndex>(table => table
                .Column<string>("ContentType", column => column.WithLength(ContentItemIndex.MaxContentTypeSize))
            );

            // Index on content type as that is most likely to be used for retrieving data from index
            // without having to query document table as well.
            SchemaBuilder.AlterTable(nameof(GoogleMapPartIndex), table => table
                .CreateIndex("IDX_GoogleMapPartIndex_ContentType", "DocumentId", "ContentType")
            );

            return 2;
        }
    }
}