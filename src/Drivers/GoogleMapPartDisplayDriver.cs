using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Entities;
using OrchardCore.Settings;
using System;
using System.Threading.Tasks;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;
using ThisNetWorks.OrchardCore.GoogleMaps.ViewModels;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Drivers
{
    public class GoogleMapPartDisplayDriver : ContentPartDisplayDriver<GoogleMapPart>
    {
        private readonly ISiteService _siteService;
        private readonly IStringLocalizer S;

        public GoogleMapPartDisplayDriver(
            ISiteService siteService,
            IStringLocalizer<GoogleMapPartDisplayDriver> stringLocalizer)
        {
            _siteService = siteService;
            S = stringLocalizer;
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
            return Initialize<GoogleMapPartEditViewModel>("GoogleMapPart_Edit", async m => await BuildEditViewModel(m, part));
        }

        public override async Task<IDisplayResult> UpdateAsync(GoogleMapPart part, IUpdateModel updater)
        {
            var model = new GoogleMapPartEditViewModel();

            await updater.TryUpdateModelAsync(model, Prefix);
            try
            {
                var editModel = JsonConvert.DeserializeObject<GoogleMapEditModel>(model.Json, CamelCaseJsonSerializer.Settings);
                part.Location = editModel.Marker.Location;
                part.Marker  = editModel.Marker.LatLng;
                part.Polygons = editModel.Polygons;
            }
            catch
            {
                updater.ModelState.AddModelError(Prefix, S["The JSON is written in an incorrect format."]);
            }

            return Edit(part);
        }

        private async Task BuildViewModel(GoogleMapPartViewModel model, GoogleMapPart part)
        {
            var settings = (await _siteService.GetSiteSettingsAsync()).As<GoogleMapsSettings>();

            model.ContentItem = part.ContentItem;
            model.Location = part.Location;
            model.Marker = part.Marker;
            model.Polygons = part.Polygons;
            model.GoogleMapPart = part;
            model.Settings = settings;
        }

        private async Task BuildEditViewModel(GoogleMapPartEditViewModel model, GoogleMapPart part)
        {
            var settings = (await _siteService.GetSiteSettingsAsync()).As<GoogleMapsSettings>();

            var editModel = new GoogleMapEditModel 
            {
                DefaultLocation = new LatLng 
                {
                    Lat = settings.DefaultMarker.Lat,
                    Lng = settings.DefaultMarker.Lng
                },
                Marker = new MarkerModel 
                {
                    Location = String.IsNullOrEmpty(part.Location) ? String.Empty : part.Location,
                    LatLng = part.Marker,
                },
                Polygons = part.Polygons
            };

            model.ContentItem = part.ContentItem;
            model.Location = part.Location;
            model.Json = JsonConvert.SerializeObject(editModel, CamelCaseJsonSerializer.Settings);
            model.GoogleMapPart = part;
            model.Settings = settings;
        }        
    }
}
