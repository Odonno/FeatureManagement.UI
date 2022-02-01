namespace Microsoft.Extensions.DependencyInjection;

public static class SettingsExtensions
{
    /// <summary>
    /// Creates a server-side Feature Flag that can be toggled (that stores boolean value).
    /// </summary>
    /// <param name="settings">The <see cref="Settings"/>.</param>
    /// <param name="featureName">Name of the feature.</param>
    /// <param name="configuration">Time window feature configuration (assign a single value for a specified date range).</param>
    /// <param name="defaultValue">Default value of the feature.</param>
    /// <param name="description">Description of the feature.</param>
    /// <returns>The updated <see cref="Settings"/>.</returns>
    public static Settings ServerFeature(this Settings settings, string featureName, TimeWindowFeatureConfiguration<bool> configuration, bool defaultValue = false, string? description = null)
    {
        FeatureConfigurationExtensions.EnsuresCorrectConfiguration(configuration);

        settings.Features.Add(new BoolFeatureSettings
        {
            Name = featureName,
            Value = defaultValue,
            Description = description,
            Type = FeatureTypes.Server,
            Configuration = configuration
        });

        return settings;
    }
    /// <summary>
    /// Creates a server-side Feature Flag that can be updated (that stores integer value).
    /// </summary>
    /// <param name="settings">The <see cref="Settings"/>.</param>
    /// <param name="featureName">Name of the feature.</param>
    /// <param name="configuration">Time window feature configuration (assign a single value for a specified date range).</param>
    /// <param name="defaultValue">Default value of the feature.</param>
    /// <param name="description">Description of the feature.</param>
    /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
    /// <returns>The updated <see cref="Settings"/>.</returns>
    public static Settings ServerFeature(this Settings settings, string featureName, TimeWindowFeatureConfiguration<int> configuration, int defaultValue = 0, string? description = null, IEnumerable<int>? choices = null, string? uiPrefix = null, string? uiSuffix = null)
    {
        FeatureConfigurationExtensions.EnsuresCorrectConfiguration(configuration);

        if (choices != null)
        {
            settings.Features.Add(new IntFeatureWithChoicesSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description,
                Type = FeatureTypes.Server,
                Choices = choices,
                UiPrefix = uiPrefix,
                UiSuffix = uiSuffix,
                Configuration = configuration
            });
        }
        else
        {
            settings.Features.Add(new IntFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description,
                Type = FeatureTypes.Server,
                UiPrefix = uiPrefix,
                UiSuffix = uiSuffix,
                Configuration = configuration
            });
        }

        return settings;
    }
    /// <summary>
    /// Creates a server-side Feature Flag that can be updated (that stores decimal value).
    /// </summary>
    /// <param name="settings">The <see cref="Settings"/>.</param>
    /// <param name="featureName">Name of the feature.</param>
    /// <param name="configuration">Time window feature configuration (assign a single value for a specified date range).</param>
    /// <param name="defaultValue">Default value of the feature.</param>
    /// <param name="description">Description of the feature.</param>
    /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
    /// <returns>The updated <see cref="Settings"/>.</returns>
    public static Settings ServerFeature(this Settings settings, string featureName, TimeWindowFeatureConfiguration<decimal> configuration, decimal defaultValue = 0, string? description = null, IEnumerable<decimal>? choices = null, string? uiPrefix = null, string? uiSuffix = null)
    {
        FeatureConfigurationExtensions.EnsuresCorrectConfiguration(configuration);

        if (choices != null)
        {
            settings.Features.Add(new DecimalFeatureWithChoicesSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description,
                Type = FeatureTypes.Server,
                Choices = choices,
                UiPrefix = uiPrefix,
                UiSuffix = uiSuffix,
                Configuration = configuration
            });
        }
        else
        {
            settings.Features.Add(new DecimalFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description,
                Type = FeatureTypes.Server,
                UiPrefix = uiPrefix,
                UiSuffix = uiSuffix,
                Configuration = configuration
            });
        }

        return settings;
    }
    /// <summary>
    /// Creates a server-side Feature Flag that can be updated (that stores string value).
    /// </summary>
    /// <param name="settings">The <see cref="Settings"/>.</param>
    /// <param name="featureName">Name of the feature.</param>
    /// <param name="configuration">Time window feature configuration (assign a single value for a specified date range).</param>
    /// <param name="defaultValue">Default value of the feature.</param>
    /// <param name="description">Description of the feature.</param>
    /// <param name="choices">A list of choices to limit the feature to a small set of values.</param>
    /// <returns>The updated <see cref="Settings"/>.</returns>
    public static Settings ServerFeature(this Settings settings, string featureName, TimeWindowFeatureConfiguration<string> configuration, string defaultValue = "", string? description = null, IEnumerable<string>? choices = null, string? uiPrefix = null, string? uiSuffix = null)
    {
        FeatureConfigurationExtensions.EnsuresCorrectConfiguration(configuration);

        if (choices != null)
        {
            settings.Features.Add(new StringFeatureWithChoicesSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description,
                Type = FeatureTypes.Server,
                Choices = choices,
                UiPrefix = uiPrefix,
                UiSuffix = uiSuffix,
                Configuration = configuration
            });
        }
        else
        {
            settings.Features.Add(new StringFeatureSettings
            {
                Name = featureName,
                Value = defaultValue,
                Description = description,
                Type = FeatureTypes.Server,
                UiPrefix = uiPrefix,
                UiSuffix = uiSuffix,
                Configuration = configuration
            });
        }

        return settings;
    }
}
