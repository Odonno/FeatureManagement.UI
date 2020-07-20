using System.Collections.Generic;
using AspNetCore.FeatureManagement.UI.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SettingsExtensions
    {
        public static Settings Feature(this Settings settings, string featureName, bool defaultValue = false, string description = null)
        {
            settings.Features.Add(new BoolFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description
            });

            return settings;
        }
        public static Settings Feature(this Settings settings, string featureName, int defaultValue = 0, string description = null, IEnumerable<int> choices = null)
        {
            if (choices != null)
            {
                settings.Features.Add(new IntFeatureWithChoicesSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description,
                    Choices = choices
                });
            }
            else
            {
                settings.Features.Add(new IntFeatureSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description
                });
            }

            return settings;
        }
        public static Settings Feature(this Settings settings, string featureName, decimal defaultValue = 0, string description = null, IEnumerable<decimal> choices = null)
        {
            if (choices != null)
            {
                settings.Features.Add(new DecimalFeatureWithChoicesSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description,
                    Choices = choices
                });
            }
            else
            {
                settings.Features.Add(new DecimalFeatureSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description
                });
            }

            return settings;
        }
        public static Settings Feature(this Settings settings, string featureName, string defaultValue = "", string description = null, IEnumerable<string> choices = null)
        {
            if (choices != null)
            {
                settings.Features.Add(new StringFeatureWithChoicesSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description,
                    Choices = choices
                });
            }
            else
            {
                settings.Features.Add(new StringFeatureSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description
                });
            }

            return settings;
        }
    }
}
