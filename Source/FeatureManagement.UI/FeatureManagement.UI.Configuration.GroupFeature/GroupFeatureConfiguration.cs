using DatabaseGroupFeature = FeatureManagement.UI.Core.Data.GroupFeature;

namespace FeatureManagement.UI.Configuration.GroupFeature;

public interface IGroupFeatureConfiguration<T> : IFeatureConfiguration
{
    List<GroupFeature<T>> Groups { get; }
}

public class GroupFeatureConfiguration<T> : BaseFeatureConfiguration, IGroupFeatureConfiguration<T>
{
    public List<GroupFeature<T>> Groups { get; set; } = new List<GroupFeature<T>>();

    internal override void Apply(IFeatureSettings featureSettings, Feature feature)
    {
        if (featureSettings is IFeatureWithValueSettings<bool> fBool)
        {
            if (featureSettings.Configuration is IGroupFeatureConfiguration<bool> gfcBool)
            {
                feature.ConfigurationType = ConfigurationTypes.Group;
                feature.GroupFeatures = gfcBool.Groups
                    .Select(g => new DatabaseGroupFeature
                    {
                        Group = g.Group,
                        BooleanValue = g.Value
                    })
                    .Concat(new[] { new DatabaseGroupFeature { Group = null, BooleanValue = fBool.Value } })
                    .ToList();
            }
        }

        if (featureSettings is IFeatureWithValueSettings<int> fInt)
        {
            if (featureSettings.Configuration is IGroupFeatureConfiguration<int> gfcInt)
            {
                feature.ConfigurationType = ConfigurationTypes.Group;
                feature.GroupFeatures = gfcInt.Groups
                    .Select(g => new DatabaseGroupFeature
                    {
                        Group = g.Group,
                        IntValue = g.Value
                    })
                    .Concat(new[] { new DatabaseGroupFeature { Group = null, IntValue = fInt.Value } })
                    .ToList();
            }
        }

        if (featureSettings is IFeatureWithValueSettings<decimal> fDecimal)
        {
            if (featureSettings.Configuration is IGroupFeatureConfiguration<decimal> gfcDecimal)
            {
                feature.ConfigurationType = ConfigurationTypes.Group;
                feature.GroupFeatures = gfcDecimal.Groups
                    .Select(g => new DatabaseGroupFeature
                    {
                        Group = g.Group,
                        DecimalValue = g.Value
                    })
                    .Concat(new[] { new DatabaseGroupFeature { Group = null, DecimalValue = fDecimal.Value } })
                    .ToList();
            }
        }

        if (featureSettings is IFeatureWithValueSettings<string> fString)
        {
            if (featureSettings.Configuration is IGroupFeatureConfiguration<string> gfcString)
            {
                feature.ConfigurationType = ConfigurationTypes.Group;
                feature.GroupFeatures = gfcString.Groups
                    .Select(g => new DatabaseGroupFeature
                    {
                        Group = g.Group,
                        StringValue = g.Value
                    })
                    .Concat(new[] { new DatabaseGroupFeature { Group = null, StringValue = fString.Value } })
                    .ToList();
            }
        }
    }
}
