using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleFeaturesApi.FeatureManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleFeaturesApi.FeatureManagement
{
    public static class SettingsExtensions
    {
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
