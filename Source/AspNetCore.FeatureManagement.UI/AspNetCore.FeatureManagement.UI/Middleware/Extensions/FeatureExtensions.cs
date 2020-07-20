using AspNetCore.FeatureManagement.UI.Core.Data;
using AspNetCore.FeatureManagement.UI.Middleware.Models;
using System.Linq;

namespace AspNetCore.FeatureManagement.UI.Middleware.Extensions
{
    internal static class FeatureExtensions
    {
        internal static IFeatureOutput ToOutput(this Feature feature)
        {
            if (feature.Type == FeatureTypes.Boolean)
            {
                return new BoolFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.BooleanValue
                };
            }
            if (feature.Type == FeatureTypes.Integer)
            {
                bool hasChoices = feature.IntFeatureChoices?.Any() ?? false;

                return new IntFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.IntValue,
                    Choices = hasChoices 
                        ? feature.IntFeatureChoices.Select(c => c.Choice).ToList() 
                        : null
                };
            }
            if (feature.Type == FeatureTypes.Decimal)
            {
                bool hasChoices = feature.DecimalFeatureChoices?.Any() ?? false;

                return new DecimalFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.DecimalValue,
                    Choices = hasChoices
                        ? feature.DecimalFeatureChoices.Select(c => c.Choice).ToList()
                        : null
                };
            }
            if (feature.Type == FeatureTypes.String)
            {
                bool hasChoices = feature.StringFeatureChoices?.Any() ?? false;

                return new StringFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.StringValue,
                    Choices = hasChoices
                        ? feature.StringFeatureChoices.Select(c => c.Choice).ToList()
                        : null
                };
            }

            return null;
        }
    }
}
