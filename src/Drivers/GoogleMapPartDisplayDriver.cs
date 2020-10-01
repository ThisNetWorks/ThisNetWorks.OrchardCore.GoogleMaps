using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Entities;
using OrchardCore.Settings;
using System.Threading.Tasks;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;
using ThisNetWorks.OrchardCore.GoogleMaps.ViewModels;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Drivers
{
    public class GoogleMapPartDisplayDriver : ContentPartDisplayDriver<GoogleMapPart>
    {
        private readonly ISiteService _siteService;

        public GoogleMapPartDisplayDriver(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public override IDisplayResult Display(GoogleMapPart part, BuildPartDisplayContext context)
        {
            return Combine(
                Initialize<GoogleMapPartViewModel>("GoogleMapPart", async m => await BuildViewModel(m, part))
                    .Location("Detail", "Content:20"),
                Initialize<GoogleMapPartViewModel>("GoogleMapPart_Summary", async m => await BuildViewModel(m, part))
                    .Location("Summary", "Meta:5")
            );
        }

        public override IDisplayResult Edit(GoogleMapPart part)
        {
            return Initialize<GoogleMapPartViewModel>("GoogleMapPart_Edit", async m => await BuildViewModel(m, part));
        }

        public override async Task<IDisplayResult> UpdateAsync(GoogleMapPart model, IUpdateModel updater)
        {
            await updater.TryUpdateModelAsync(model, Prefix, t => t.Location, t => t.Marker);

            return Edit(model);
        }

        private async Task BuildViewModel(GoogleMapPartViewModel model, GoogleMapPart part)
        {
            var settings = (await _siteService.GetSiteSettingsAsync()).As<GoogleMapsSettings>();

            model.ContentItem = part.ContentItem;
            model.Location = part.Location;
            model.Marker = part.Marker;
            model.GoogleMapPart = part;
            model.Settings = settings;
        }
    }
}
