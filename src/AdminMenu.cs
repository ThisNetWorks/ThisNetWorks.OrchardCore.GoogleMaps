using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;
using ThisNetWorks.OrchardCore.GoogleMaps.Settings;

namespace ThisNetWorks.OrchardCore.GoogleMaps
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer S;
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            S = localizer;
        }


        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!string.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(S["Configuration"], configuration => configuration
                    .Add(S["Settings"], settings => settings
                        .Add(S["Google Maps"], S["Google Maps"], layers => layers
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = GoogleMapsSettingsDisplayDriver.GroupId })
                            .Permission(Permissions.ManageGoogleMaps)
                            .LocalNav()
                        )));

            return Task.CompletedTask;
        }
    }
}