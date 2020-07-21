using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SettingsExtensions
    {
        /// <summary>
        /// Configure the Features to be stored in-memory.
        /// </summary>
        /// <param name="settings">The current <see cref="Settings"/> configuration.</param>
        /// <returns>The <see cref="Settings"/> now configured with a Storage Provider.</returns>
        public static Settings AddInMemoryStorage(this Settings settings)
        {
            settings.Services.AddDbContext<FeatureManagementDb>(options =>
            {
                options.UseInMemoryDatabase("FeatureManagement");
            });

            return settings;
        }
    }
}
