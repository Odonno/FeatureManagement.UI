using AspNetCore.FeatureManagement.UI.Core.Data;
using AspNetCore.FeatureManagement.UI.Core.Models;
using System;
using System.Linq;

namespace AspNetCore.FeatureManagement.UI.Extensions
{
    internal static class FeatureExtensions
    {
        internal static bool HasBreakingChanges(this Feature f1, Feature f2)
        {
            if (f1.Name != f2.Name)
            {
                throw new Exception("The two features does not have the same name...");
            }

            return 
                f1.Type != f2.Type ||
                f1.ValueType != f2.ValueType;
        }

        internal static IFeature ToOutput(this Feature feature)
        {
            if (feature.ValueType == FeatureValueTypes.Boolean)
            {
                return new BoolFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.Server.BooleanValue // TODO : Client vs. Server feature
                };
            }
            if (feature.ValueType == FeatureValueTypes.Integer)
            {
                bool hasChoices = feature.IntFeatureChoices?.Any() ?? false;

                return new IntFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.Server.IntValue, // TODO : Client vs. Server feature
                    Choices = hasChoices
                        ? feature.IntFeatureChoices.Select(c => c.Choice).ToList()
                        : null
                };
            }
            if (feature.ValueType == FeatureValueTypes.Decimal)
            {
                bool hasChoices = feature.DecimalFeatureChoices?.Any() ?? false;

                return new DecimalFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.Server.DecimalValue, // TODO : Client vs. Server feature
                    Choices = hasChoices
                        ? feature.DecimalFeatureChoices.Select(c => c.Choice).ToList()
                        : null
                };
            }

            {
                bool hasChoices = feature.StringFeatureChoices?.Any() ?? false;

                return new StringFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.Server.StringValue, // TODO : Client vs. Server feature
                    Choices = hasChoices
                        ? feature.StringFeatureChoices.Select(c => c.Choice).ToList()
                        : null
                };
            }
        }
    }
}
