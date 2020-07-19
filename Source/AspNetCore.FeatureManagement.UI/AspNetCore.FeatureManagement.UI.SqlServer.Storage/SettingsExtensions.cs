using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SettingsExtensions
    {
        public static Settings AddSqlServerStorage(this Settings settings, string connectionString)
        {
            settings.Services.AddDbContext<FeatureManagementDb>(options =>
            {
                options.UseSqlServer(
                    connectionString,
                    s => s.MigrationsAssembly(typeof(SettingsExtensions).Assembly.FullName)
                        .MigrationsHistoryTable("__EFMigrationsHistory", "FeatureManagement")
                );
            });

            return settings;
        }
    }
}
