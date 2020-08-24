using AspNetCore.FeatureManagement.UI.Core.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SettingsExtensions
    {
        /// <summary>
        /// Configure the Features to be stored in a SQL Server database.
        /// </summary>
        /// <param name="settings">The current <see cref="Settings"/> configuration.</param>
        /// <param name="connectionString">The connection string to access the SQL Server database.</param>
        /// <returns>The <see cref="Settings"/> now configured with a Storage Provider.</returns>
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
