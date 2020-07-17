using AspNetCore.FeatureManagement.UI.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SettingsExtensions
    {
        public static Settings Feature(this Settings settings, string featureName, bool enabled = false, string description = null)
        {
            settings.Features.Add(new FeatureSettings
            {
                Name = featureName,
                Enabled = enabled,
                Description = description
            });

            return settings;
        }
    }
}
