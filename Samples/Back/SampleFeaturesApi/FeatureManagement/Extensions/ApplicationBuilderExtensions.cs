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
            var serviceProvider = app.ApplicationServices.GetRequiredService<IServiceProvider>();

            using (var scope = serviceProvider.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<FeatureManagementDb>())
            {
                var settings = scope.ServiceProvider.GetRequiredService<Settings>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<FeatureManagementDb>>();

                // Apply migrations
                bool isInMemory = context.Database.IsInMemory();
                bool hasPendingMigrations = !isInMemory && context.Database.GetPendingMigrations().Any();
                bool shouldMigrate = !isInMemory && hasPendingMigrations;

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
