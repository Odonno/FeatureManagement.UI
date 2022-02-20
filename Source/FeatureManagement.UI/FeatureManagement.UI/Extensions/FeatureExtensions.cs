namespace FeatureManagement.UI.Extensions;

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

    internal static async Task<IFeature> ToOutput(
        this Feature feature,
        IFeaturesService featuresService,
        bool @readonly,
        string? clientId,
        IEnumerable<string>? clientGroups
    )
    {
        if (feature.ValueType == FeatureValueTypes.Boolean)
        {
            var value = await featuresService.GetValue<bool>(feature.Name, clientId, clientGroups);

            return new BoolFeature
            {
                Name = feature.Name,
                Description = feature.Description,
                Value = value,
                Readonly = @readonly,
                UiPrefix = feature.UiPrefix,
                UiSuffix = feature.UiSuffix
            };
        }
        if (feature.ValueType == FeatureValueTypes.Integer)
        {
            bool hasChoices = feature.IntFeatureChoices?.Any() ?? false;
            var value = await featuresService.GetValue<int>(feature.Name, clientId, clientGroups);

            return new IntFeature
            {
                Name = feature.Name,
                Description = feature.Description,
                Value = value,
                Choices = hasChoices
                    ? feature.IntFeatureChoices.Select(c => c.Choice).ToList()
                    : null,
                Readonly = @readonly,
                UiPrefix = feature.UiPrefix,
                UiSuffix = feature.UiSuffix
            };
        }
        if (feature.ValueType == FeatureValueTypes.Decimal)
        {
            bool hasChoices = feature.DecimalFeatureChoices?.Any() ?? false;
            var value = await featuresService.GetValue<decimal>(feature.Name, clientId, clientGroups);

            return new DecimalFeature
            {
                Name = feature.Name,
                Description = feature.Description,
                Value = value,
                Choices = hasChoices
                    ? feature.DecimalFeatureChoices.Select(c => c.Choice).ToList()
                    : null,
                Readonly = @readonly,
                UiPrefix = feature.UiPrefix,
                UiSuffix = feature.UiSuffix
            };
        }

        {
            bool hasChoices = feature.StringFeatureChoices?.Any() ?? false;
            var value = await featuresService.GetValue<string>(feature.Name, clientId, clientGroups);

            return new StringFeature
            {
                Name = feature.Name,
                Description = feature.Description,
                Value = value,
                Choices = hasChoices
                    ? feature.StringFeatureChoices.Select(c => c.Choice).ToList()
                    : null,
                Readonly = @readonly,
                UiPrefix = feature.UiPrefix,
                UiSuffix = feature.UiSuffix
            };
        }
    }

    internal static void ApplyConfiguration(this Feature feature, IFeatureSettings featureSettings)
    {
        if (featureSettings.Configuration is BaseFeatureConfiguration baseFeatureConfiguration)
        {
            baseFeatureConfiguration?.Apply(featureSettings, feature);
        }
    }
}
