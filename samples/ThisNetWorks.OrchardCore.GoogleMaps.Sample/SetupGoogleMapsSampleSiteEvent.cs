using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Environment.Extensions;
using OrchardCore.Environment.Shell;
using OrchardCore.Setup.Events;
using System;
using System.Linq;
using System.Threading.Tasks;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;
using YesSql;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Sample
{
    /// <summary>
    /// This setup event configures the GoogleMaps module, and BlogPost content items.
    /// </summary>
    public class SetupGoogleMapsSampleSiteEvent : ISetupEventHandler
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly ISession _session;

        private readonly IExtensionManager _extensionManager;
        private readonly IShellFeaturesManager _shellFeatureManager;
        public SetupGoogleMapsSampleSiteEvent(
            IContentDefinitionManager contentDefinitionManager,
            ISession session,
            IExtensionManager extensionManager,
            IShellFeaturesManager shellFeatureManager
            )
        {
            _contentDefinitionManager = contentDefinitionManager;
            _session = session;
            _extensionManager = extensionManager;
            _shellFeatureManager = shellFeatureManager;
        }

        public async Task Setup(string siteName, string userName, string email, string password, string dbProvider, string dbConnectionString, string dbTablePrefix, string siteTimeZone, Action<string, string> reportError)
        {
            var features = _extensionManager.GetFeatures();

            var featuresToEnable = features.Where(x => x.Id == "ThisNetWorks.OrchardCore.GoogleMaps");

            await _shellFeatureManager.EnableFeaturesAsync(featuresToEnable, true);

            var ctds = _contentDefinitionManager.ListPartDefinitions();
            if (ctds.FirstOrDefault(x => x.Name == "BlogPost") != null)
            {
                _contentDefinitionManager.AlterTypeDefinition("BlogPost", builder => builder
                    .WithPart("GoogleMapPart"));

                var query = _session.Query<ContentItem>()
                    .With<ContentItemIndex>(x => x.ContentType == "BlogPost" && x.Published);

                var blogPosts = await query.ListAsync();
                foreach (var blogPost in blogPosts)
                {
                    blogPost.Alter<GoogleMapPart>(part =>
                    {
                        part.Marker = new LatLng { Lat = GoogleMapsSettings.DefaultLatitude, Lng = GoogleMapsSettings.DefaultLongitude };
                    });

                    _session.Save(blogPost);
                }
            }
        }
    }
}
