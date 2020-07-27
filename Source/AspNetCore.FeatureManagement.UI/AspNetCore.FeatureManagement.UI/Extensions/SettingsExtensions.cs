using System.Collections.Generic;
using AspNetCore.FeatureManagement.UI.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SettingsExtensions
    {
        /// <summary>
        /// Creates a boolean Feature Flag that can be toggled.
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings Feature(this Settings settings, string featureName, bool defaultValue = false, string? description = null)
        {
            settings.Features.Add(new BoolFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description
            });

            return settings;
        }
        /// <summary>
        /// Creates an integer Feature Flag that can be updated.
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings Feature(this Settings settings, string featureName, int defaultValue = 0, string? description = null, IEnumerable<int>? choices = null)
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
        /// <summary>
        /// Creates a decimal Feature Flag that can be updated.
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings Feature(this Settings settings, string featureName, decimal defaultValue = 0, string? description = null, IEnumerable<decimal>? choices = null)
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
        /// <summary>
        /// Creates a string Feature Flag that can be updated.
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings Feature(this Settings settings, string featureName, string defaultValue = "", string? description = null, IEnumerable<string>? choices = null)
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
