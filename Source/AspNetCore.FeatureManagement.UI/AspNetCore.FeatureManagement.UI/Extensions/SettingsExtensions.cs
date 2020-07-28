using System.Collections.Generic;
using AspNetCore.FeatureManagement.UI.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SettingsExtensions
    {
        /// <summary>
        /// Creates a server-side Feature Flag that can be toggled (that stores boolean value).
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings ServerFeature(this Settings settings, string featureName, bool defaultValue = false, string? description = null)
        {
            settings.Features.Add(new BoolServerFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description
            });

            return settings;
        }
        /// <summary>
        /// Creates a server-side Feature Flag that can be updated (that stores integer value).
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings ServerFeature(this Settings settings, string featureName, int defaultValue = 0, string? description = null, IEnumerable<int>? choices = null)
        {
            if (choices != null)
            {
                settings.Features.Add(new IntServerFeatureWithChoicesSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description,
                    Choices = choices
                });
            }
            else
            {
                settings.Features.Add(new IntServerFeatureSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description
                });
            }

            return settings;
        }
        /// <summary>
        /// Creates a server-side Feature Flag that can be updated (that stores decimal value).
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings ServerFeature(this Settings settings, string featureName, decimal defaultValue = 0, string? description = null, IEnumerable<decimal>? choices = null)
        {
            if (choices != null)
            {
                settings.Features.Add(new DecimalServerFeatureWithChoicesSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description,
                    Choices = choices
                });
            }
            else
            {
                settings.Features.Add(new DecimalServerFeatureSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description
                });
            }

            return settings;
        }
        /// <summary>
        /// Creates a server-side Feature Flag that can be updated (that stores string value).
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings ServerFeature(this Settings settings, string featureName, string defaultValue = "", string? description = null, IEnumerable<string>? choices = null)
        {
            if (choices != null)
            {
                settings.Features.Add(new StringServerFeatureWithChoicesSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description,
                    Choices = choices
                });
            }
            else
            {
                settings.Features.Add(new StringServerFeatureSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description
                });
            }

            return settings;
        }

        /// <summary>
        /// Creates a client-side Feature Flag that can be toggled (that stores boolean value).
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings ClientFeature(this Settings settings, string featureName, bool defaultValue = false, string? description = null)
        {
            settings.Features.Add(new BoolClientFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description
            });

            return settings;
        }
        /// <summary>
        /// Creates a client-side Feature Flag that can be updated (that stores integer value).
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings ClientFeature(this Settings settings, string featureName, int defaultValue = 0, string? description = null, IEnumerable<int>? choices = null)
        {
            if (choices != null)
            {
                settings.Features.Add(new IntClientFeatureWithChoicesSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description,
                    Choices = choices
                });
            }
            else
            {
                settings.Features.Add(new IntClientFeatureSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description
                });
            }

            return settings;
        }
        /// <summary>
        /// Creates a client-side Feature Flag that can be updated (that stores decimal value).
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings ClientFeature(this Settings settings, string featureName, decimal defaultValue = 0, string? description = null, IEnumerable<decimal>? choices = null)
        {
            if (choices != null)
            {
                settings.Features.Add(new DecimalClientFeatureWithChoicesSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description,
                    Choices = choices
                });
            }
            else
            {
                settings.Features.Add(new DecimalClientFeatureSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description
                });
            }

            return settings;
        }
        /// <summary>
        /// Creates a client-side Feature Flag that can be updated (that stores string value).
        /// </summary>
        /// <param name="settings">The <see cref="Settings"/>.</param>
        /// <param name="featureName">Name of the feature.</param>
        /// <param name="defaultValue">Default value of the feature.</param>
        /// <param name="description">Description of the feature.</param>
        /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
        /// <returns>The updated <see cref="Settings"/>.</returns>
        public static Settings ClientFeature(this Settings settings, string featureName, string defaultValue = "", string? description = null, IEnumerable<string>? choices = null)
        {
            if (choices != null)
            {
                settings.Features.Add(new StringClientFeatureWithChoicesSettings
                {
                    Name = featureName,
                    Value = defaultValue,
                    Description = description,
                    Choices = choices
                });
            }
            else
            {
                settings.Features.Add(new StringClientFeatureSettings
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
