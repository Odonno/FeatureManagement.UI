using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SettingsExtensions
    {
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
