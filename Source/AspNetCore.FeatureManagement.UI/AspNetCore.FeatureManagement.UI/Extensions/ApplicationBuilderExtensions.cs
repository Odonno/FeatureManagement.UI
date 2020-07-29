using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Data;
using AspNetCore.FeatureManagement.UI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Apply features in the ASP.NET core application. Apply data migrations.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> now configured to use features.</returns>
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
                var intFeatureChoicesSet = context.IntFeatureChoices;
                var decimalFeatureChoicesSet = context.DecimalFeatureChoices;
                var stringFeatureChoicesSet = context.StringFeatureChoices;

                var existingFeatures = featuresSet
                    .Include(f => f.IntFeatureChoices)
                    .Include(f => f.DecimalFeatureChoices)
                    .Include(f => f.StringFeatureChoices)
                    .ToList();

                var newFeatures = settings.Features
                    .Select(f =>
                    {
                        if (f is IFeatureWithValueSettings<bool> fBool)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                ValueType = FeatureValueTypes.Boolean,
                                Server = f.Type == FeatureTypes.Server
                                    ? new ServerFeatureData { BooleanValue = fBool.Value }
                                    : null,
                                UiPrefix = fBool.UiPrefix,
                                UiSuffix = fBool.UiSuffix
                            };
                        }
                        if (f is IFeatureWithChoicesSettings<int> fIntWithChoices)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                ValueType = FeatureValueTypes.Integer,
                                Server = f.Type == FeatureTypes.Server
                                    ? new ServerFeatureData { IntValue = fIntWithChoices.Value }
                                    : null,
                                IntFeatureChoices = fIntWithChoices.Choices
                                    .Select(c => new IntFeatureChoice { Choice = c })
                                    .ToList(),
                                UiPrefix = fIntWithChoices.UiPrefix,
                                UiSuffix = fIntWithChoices.UiSuffix
                            };
                        }
                        if (f is IFeatureWithValueSettings<int> fInt)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                ValueType = FeatureValueTypes.Integer,
                                Server = f.Type == FeatureTypes.Server
                                    ? new ServerFeatureData { IntValue = fInt.Value }
                                    : null,
                                IntFeatureChoices = new List<IntFeatureChoice>(),
                                UiPrefix = fInt.UiPrefix,
                                UiSuffix = fInt.UiSuffix
                            };
                        }
                        if (f is IFeatureWithChoicesSettings<decimal> fDecimalWithChoices)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                ValueType = FeatureValueTypes.Decimal,
                                Server = f.Type == FeatureTypes.Server
                                    ? new ServerFeatureData { DecimalValue = fDecimalWithChoices.Value }
                                    : null,
                                DecimalFeatureChoices = fDecimalWithChoices.Choices
                                    .Select(c => new DecimalFeatureChoice { Choice = c })
                                    .ToList(),
                                UiPrefix = fDecimalWithChoices.UiPrefix,
                                UiSuffix = fDecimalWithChoices.UiSuffix
                            };
                        }
                        if (f is IFeatureWithValueSettings<decimal> fDecimal)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                ValueType = FeatureValueTypes.Decimal,
                                Server = f.Type == FeatureTypes.Server
                                    ? new ServerFeatureData { DecimalValue = fDecimal.Value }
                                    : null,
                                DecimalFeatureChoices = new List<DecimalFeatureChoice>(),
                                UiPrefix = fDecimal.UiPrefix,
                                UiSuffix = fDecimal.UiSuffix
                            };
                        }
                        if (f is IFeatureWithChoicesSettings<string> fStringWithChoices)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                ValueType = FeatureValueTypes.String,
                                Server = f.Type == FeatureTypes.Server
                                    ? new ServerFeatureData { StringValue = fStringWithChoices.Value }
                                    : null,
                                StringFeatureChoices = fStringWithChoices.Choices
                                    .Select(c => new StringFeatureChoice { Choice = c })
                                    .ToList(),
                                UiPrefix = fStringWithChoices.UiPrefix,
                                UiSuffix = fStringWithChoices.UiSuffix
                            };
                        }
                        if (f is IFeatureWithValueSettings<string> fString)
                        {
                            return new Feature
                            {
                                Name = f.Name,
                                Description = f.Description,
                                ValueType = FeatureValueTypes.String,
                                Server = f.Type == FeatureTypes.Server
                                    ? new ServerFeatureData { StringValue = fString.Value }
                                    : null,
                                StringFeatureChoices = new List<StringFeatureChoice>(),
                                UiPrefix = fString.UiPrefix,
                                UiSuffix = fString.UiSuffix
                            };
                        }

                        return null;
                    })
                    .ToList();

                var featuresToAdd = newFeatures
                    .Where(f => !existingFeatures.Any(sf => sf.Name == f.Name && !FeatureExtensions.HasBreakingChanges(sf, f)));
                var featuresToUpdate = newFeatures
                    .Where(f => existingFeatures.Any(sf => sf.Name == f.Name && !FeatureExtensions.HasBreakingChanges(sf, f)));
                var featuresToDelete = existingFeatures
                    .Where(f => !newFeatures.Any(sf => sf.Name == f.Name && !FeatureExtensions.HasBreakingChanges(sf, f)));

                featuresSet.AddRange(featuresToAdd);

                foreach (var feature in featuresToUpdate)
                {
                    var savedFeature = existingFeatures.Single(f => f.Name == feature.Name);
                    savedFeature.Description = feature.Description;

                    if (feature.ValueType == FeatureValueTypes.Integer)
                    {
                        var existingChoices = savedFeature.IntFeatureChoices;

                        var choicesToAdd = feature.IntFeatureChoices
                            .Where(c => !existingChoices.Any(ec => ec.Choice == c.Choice));
                        var choicesToDelete = existingChoices
                            .Where(c => !feature.IntFeatureChoices.Any(ec => ec.Choice == c.Choice));

                        savedFeature.IntFeatureChoices.AddRange(choicesToAdd);
                        intFeatureChoicesSet.RemoveRange(choicesToDelete);
                    }
                    if (feature.ValueType == FeatureValueTypes.Decimal)
                    {
                        var existingChoices = savedFeature.DecimalFeatureChoices;

                        var choicesToAdd = feature.DecimalFeatureChoices
                            .Where(c => !existingChoices.Any(ec => ec.Choice == c.Choice));
                        var choicesToDelete = existingChoices
                            .Where(c => !feature.DecimalFeatureChoices.Any(ec => ec.Choice == c.Choice));

                        savedFeature.DecimalFeatureChoices.AddRange(choicesToAdd);
                        decimalFeatureChoicesSet.RemoveRange(choicesToDelete);
                    }
                    if (feature.ValueType == FeatureValueTypes.String)
                    {
                        var existingChoices = savedFeature.StringFeatureChoices;

                        var choicesToAdd = feature.StringFeatureChoices
                            .Where(c => !existingChoices.Any(ec => ec.Choice == c.Choice));
                        var choicesToDelete = existingChoices
                            .Where(c => !feature.StringFeatureChoices.Any(ec => ec.Choice == c.Choice));

                        savedFeature.StringFeatureChoices.AddRange(choicesToAdd);
                        stringFeatureChoicesSet.RemoveRange(choicesToDelete);
                    }
                }

                featuresSet.RemoveRange(featuresToDelete);

                context.SaveChanges();
            }

            return app;
        }
    }
}
