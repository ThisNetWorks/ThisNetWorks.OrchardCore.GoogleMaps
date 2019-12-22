using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Security.Permissions;

namespace ThisNetWorks.OrchardCore.GoogleMaps
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageGoogleMaps = new Permission("ManageGoogleMaps", "Manage Google Maps");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageGoogleMaps }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageGoogleMaps }
                }
            };
        }

    }
}