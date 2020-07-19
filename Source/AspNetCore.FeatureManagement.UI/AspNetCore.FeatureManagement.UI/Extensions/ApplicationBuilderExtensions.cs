using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
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
                var featuresSet = context.Features;

                var existingFeatures = featuresSet.ToList();

                var newFeatures = settings.Features
                    .Select(f =>
                    {
                        if (f is IFeatureWithValueSettings<bool> fBool)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                Type = FeatureTypes.Boolean,
                                BooleanValue = fBool.Value
                            };
                        }
                        if (f is IFeatureWithValueSettings<int> fInt)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                Type = FeatureTypes.Integer,
                                IntValue = fInt.Value
                            };
                        }
                        if (f is IFeatureWithValueSettings<decimal> fDecimal)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                Type = FeatureTypes.Decimal,
                                DecimalValue = fDecimal.Value
                            };
                        }
                        if (f is IFeatureWithValueSettings<string> fString)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                Type = FeatureTypes.String,
                                StringValue = fString.Value
                            };
                        }

                        return null;
                    });

                var featuresToAdd = newFeatures
                    .Where(f => !featuresSet.Any(sf => sf.Name == f.Name && sf.Type == f.Type));
                var featuresToUpdate = newFeatures
                    .Where(f => featuresSet.Any(sf => sf.Name == f.Name && sf.Type == f.Type));
                var featuresToDelete = existingFeatures
                    .Where(f => !newFeatures.Any(sf => sf.Name == f.Name && sf.Type == f.Type));

                featuresSet.AddRange(featuresToAdd);

                foreach (var feature in featuresToUpdate)
                {
                    var savedFeature = existingFeatures.Single(f => f.Name == feature.Name);
                    savedFeature.Description = feature.Description;
                }

                featuresSet.RemoveRange(featuresToDelete);

                context.SaveChanges();
            }

            return app;
        }
    }
}
