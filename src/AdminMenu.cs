using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;

namespace ThisNetWorks.OrchardCore.GoogleMaps
{
    public class AdminMenu : INavigationProvider
    {
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        public IStringLocalizer T { get; set; }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(T["Configuration"], configuration => configuration
                    .Add(T["Settings"], settings => settings
                        .Add(T["Google Maps"], T["Google Maps"], layers => layers
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = GoogleMapsSettingsDisplayDriver.GroupId })
                            .Permission(Permissions.ManageGoogleMaps)
                            .LocalNav()
                        )));

            return Task.CompletedTask;
        }
    }
}