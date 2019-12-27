using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Extensions;
using OrchardCore.Modules;
using OrchardCore.Recipes.Models;
using OrchardCore.Recipes.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ThisNetWorks.OrchardCore.GoogleMaps.Sample
{
    /// <summary>
    /// Restricts the setup recipes to only TheBlogTheme.
    /// </summary>
    public class RestrictedRecipeHarvestor : RecipeHarvester
    {
        private readonly IExtensionManager _extensionManager;

        public RestrictedRecipeHarvestor(
            IRecipeReader recipeReader,
            IExtensionManager extensionManager,
            IHostEnvironment hostingEnvironment,
            ILogger<RecipeHarvester> logger) : base(recipeReader, extensionManager, hostingEnvironment, logger)
        {
            _extensionManager = extensionManager;
        }

        public override Task<IEnumerable<RecipeDescriptor>> HarvestRecipesAsync()
        {
            return _extensionManager.GetExtensions().Where(x => x.Id == "TheBlogTheme").InvokeAsync(HarvestRecipes, Logger);
        }

        private Task<IEnumerable<RecipeDescriptor>> HarvestRecipes(IExtensionInfo extension)
        {
            var folderSubPath = PathExtensions.Combine(extension.SubPath, "Recipes");
            return HarvestRecipesAsync(folderSubPath);
        }
    }
}
