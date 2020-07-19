using AspNetCore.FeatureManagement.UI.Core.Data;
using AspNetCore.FeatureManagement.UI.Middleware.Models;

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
                return new IntFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.IntValue
                };
            }
            if (feature.Type == FeatureTypes.Decimal)
            {
                return new DecimalFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.DecimalValue
                };
            }
            if (feature.Type == FeatureTypes.String)
            {
                return new StringFeature
                {
                    Name = feature.Name,
                    Description = feature.Description,
                    Value = feature.StringValue
                };
            }

            return null;
        }
    }
}
