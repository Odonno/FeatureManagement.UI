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
        public static Settings Feature(this Settings settings, string featureName, int defaultValue = 0, string description = null)
        {
            settings.Features.Add(new IntFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description
            });

            return settings;
        }
        public static Settings Feature(this Settings settings, string featureName, decimal defaultValue = 0, string description = null)
        {
            settings.Features.Add(new DecimalFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description
            });

            return settings;
        }
        public static Settings Feature(this Settings settings, string featureName, string defaultValue = "", string description = null)
        {
            settings.Features.Add(new StringFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description
            });

            return settings;
        }
    }
}
