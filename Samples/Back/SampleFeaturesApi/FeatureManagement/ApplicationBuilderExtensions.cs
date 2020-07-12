using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleFeaturesApi.FeatureManagement.Data;
using System;
using System.Linq;

namespace SampleFeaturesApi.FeatureManagement
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseFeatures(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceProvider>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<FeatureManagementDb>();
                var settings = scope.ServiceProvider.GetRequiredService<Settings>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<FeatureManagementDb>>();

                // Apply migrations
                bool hasPendingMigrations = context.Database.GetPendingMigrations().Any();
                bool shouldMigrate = hasPendingMigrations;

                if (shouldMigrate)
                {
                    logger.LogInformation("Executing database migrations");
                    context.Database.Migrate();
                }

                // Update Features table
                var savedFeatures = context.Features;
                var savedFeatureNames = context.Features.Select(f => f.Name);

                var newFeatures = settings.Features
                    .Select(f => new Feature
                    {
                        Name = f.Name,
                        Enabled = f.Enabled,
                        Description = f.Description
                    });
                var newFeatureNames = context.Features.Select(f => f.Name);

                var featuresToAdd = newFeatures
                    .Where(f => !savedFeatureNames.Contains(f.Name));
                var featuresToUpdate = newFeatures
                    .Where(f => savedFeatureNames.Contains(f.Name));
                var featuresToDelete = savedFeatures
                    .Where(f => !newFeatureNames.Contains(f.Name));

                savedFeatures.AddRange(featuresToAdd);

                foreach (var feature in featuresToUpdate)
                {
                    var savedFeature = savedFeatures.Single(f => f.Name == feature.Name);
                    savedFeature.Description = feature.Description;
                }

                savedFeatures.RemoveRange(featuresToDelete);

                context.SaveChanges();
            }

            return app;
        }
    }
}
