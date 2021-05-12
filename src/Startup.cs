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
using ThisNetWorks.OrchardCore.GoogleMaps.ViewModels;
using YesSql.Indexes;

namespace ThisNetWorks.OrchardCore.GoogleMaps
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TemplateOptions>(o =>
            {
                o.MemberAccessStrategy.Register<GoogleMapPartViewModel>();
                o.MemberAccessStrategy.Register<GoogleMapsSettingsViewModel>();
                o.MemberAccessStrategy.Register<DisplayMapViewModel>();
            });

            services.AddSingleton<IIndexProvider, GoogleMapPartIndexProvider>();
            services.AddScoped<IDataMigration, Migrations>();

            services.AddContentPart<GoogleMapPart>()
                .UseDisplayDriver<GoogleMapPartDisplayDriver>();

            services.AddScoped<IDataMigration, Migrations>();

            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();

            services.AddScoped<IDisplayDriver<ISite>, GoogleMapsSettingsDisplayDriver>();
        }
    }
}
