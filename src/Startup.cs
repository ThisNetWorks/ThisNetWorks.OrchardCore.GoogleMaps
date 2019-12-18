using Fluid;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using ThisNetWorks.OrchardCore.GoogleMaps.Drivers;
using ThisNetWorks.OrchardCore.GoogleMaps.Indexes;
using ThisNetWorks.OrchardCore.GoogleMaps.Models;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;
using ThisNetWorks.OrchardCore.GoogleMaps.Shapes;
using ThisNetWorks.OrchardCore.GoogleMaps.ViewModels;
using YesSql.Indexes;

namespace ThisNetWorks.OrchardCore.GoogleMaps
{
    public class Startup : StartupBase
    {
        static Startup()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<GoogleMapPartViewModel>();
            TemplateContext.GlobalMemberAccessStrategy.Register<GoogleMapsSettingsViewModel>();
            TemplateContext.GlobalMemberAccessStrategy.Register<DisplayMapViewModel>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIndexProvider, GoogleMapPartIndexProvider>();
            services.AddScoped<IDataMigration, Migrations>();

            services.AddContentPart<GoogleMapPart>();
            services.AddScoped<IContentPartDisplayDriver, GoogleMapPartDisplayDriver>();
            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();

            services.AddScoped<IDisplayDriver<ISite>, GoogleMapsSettingsDisplayDriver>();

            services.AddScoped<IShapeTableProvider, GoogleMapsSettingsShapes>();
        }

    }
}