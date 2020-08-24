using AspNetCore.FeatureManagement.UI.Core.Configuration;
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
                var groupFeaturesSet = context.GroupFeatures;

                var existingFeatures = featuresSet
                    .Include(f => f.IntFeatureChoices)
                    .Include(f => f.DecimalFeatureChoices)
                    .Include(f => f.StringFeatureChoices)
                    .Include(f => f.GroupFeatures)
                    .ToList();

                var newFeatures = settings.Features
                    .Select(f =>
                    {
                        var feature = new Feature
                        {
                            Name = f.Name,
                            Description = f.Description,
                            UiPrefix = f.UiPrefix,
                            UiSuffix = f.UiSuffix
                        };

                        feature.ApplyConfiguration(f);

                        if (f is IFeatureWithValueSettings<bool> fBool)
                        {
                            feature.ValueType = FeatureValueTypes.Boolean;
                            feature.Server = f.Type == FeatureTypes.Server
                                ? new ServerFeatureData { BooleanValue = fBool.Value }
                                : null;
                        }

                        if (f is IFeatureWithValueSettings<int> fInt)
                        {
                            feature.ValueType = FeatureValueTypes.Integer;
                            feature.Server = f.Type == FeatureTypes.Server
                                ? new ServerFeatureData { IntValue = fInt.Value }
                                : null;

                            if (f is IFeatureWithChoicesSettings<int> fIntWithChoices)
                            {
                                feature.IntFeatureChoices = fIntWithChoices.Choices
                                    .Select(c => new IntFeatureChoice { Choice = c })
                                    .ToList();
                            }
                            else
                            {
                                feature.IntFeatureChoices = new List<IntFeatureChoice>();
                            }
                        }

                        if (f is IFeatureWithValueSettings<decimal> fDecimal)
                        {
                            feature.ValueType = FeatureValueTypes.Decimal;
                            feature.Server = f.Type == FeatureTypes.Server
                                ? new ServerFeatureData { DecimalValue = fDecimal.Value }
                                : null;

                            if (f is IFeatureWithChoicesSettings<decimal> fDecimalWithChoices)
                            {
                                feature.DecimalFeatureChoices = fDecimalWithChoices.Choices
                                    .Select(c => new DecimalFeatureChoice { Choice = c })
                                    .ToList();
                            }
                            else
                            {
                                feature.DecimalFeatureChoices = new List<DecimalFeatureChoice>();
                            }
                        }

                        if (f is IFeatureWithValueSettings<string> fString)
                        {
                            feature.ValueType = FeatureValueTypes.String;
                            feature.Server = f.Type == FeatureTypes.Server
                                ? new ServerFeatureData { StringValue = fString.Value }
                                : null;

                            if (f is IFeatureWithChoicesSettings<string> fStringWithChoices)
                            {
                                feature.StringFeatureChoices = fStringWithChoices.Choices
                                    .Select(c => new StringFeatureChoice { Choice = c })
                                    .ToList();
                            }
                            else
                            {
                                feature.StringFeatureChoices = new List<StringFeatureChoice>();
                            }
                        }

                        return feature;
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

                    var existingGroupFeatures = savedFeature.GroupFeatures;

                    var groupFeaturesToAdd = feature.GroupFeatures
                        .Where(gf => !existingGroupFeatures
                            .Any(egf =>
                                egf.Group == gf.Group &&
                                egf.BooleanValue == gf.BooleanValue &&
                                egf.IntValue == gf.IntValue &&
                                egf.DecimalValue == gf.DecimalValue &&
                                egf.StringValue == gf.StringValue
                            )
                        );
                    var groupFeaturesToDelete = existingGroupFeatures
                        .Where(gf => !feature.GroupFeatures
                            .Any(egf =>
                                egf.Group == gf.Group &&
                                egf.BooleanValue == gf.BooleanValue &&
                                egf.IntValue == gf.IntValue &&
                                egf.DecimalValue == gf.DecimalValue &&
                                egf.StringValue == gf.StringValue
                            )
                        );

                    savedFeature.GroupFeatures.AddRange(groupFeaturesToAdd);
                    groupFeaturesSet.RemoveRange(groupFeaturesToDelete);
                }

                featuresSet.RemoveRange(featuresToDelete);

                context.SaveChanges();
            }

            return app;
        }
    }
}
