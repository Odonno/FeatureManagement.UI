using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleFeaturesApi.FeatureManagement.Data;

namespace SampleFeaturesApi.FeatureManagement
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

        public static Settings AddSqlServerStorage(this Settings settings, string connectionString)
        {
            settings.Services.AddDbContext<FeatureManagementDb>(options =>
            {
                options.UseSqlServer(
                    connectionString, 
                    s => s.MigrationsAssembly(typeof(FeatureManagementDb).Assembly.FullName)
                        .MigrationsHistoryTable("__EFMigrationsHistory", "FeatureManagement")
                );
            });

            return settings;
        }

        public static Settings Feature(this Settings settings, string featureName, bool enabled = false, string description = null)
        {
            settings.Features.Add(new FeatureSettings
            {
                Name = featureName,
                Enabled = enabled,
                Description = description
            });

            return settings;
        }
    }
}
