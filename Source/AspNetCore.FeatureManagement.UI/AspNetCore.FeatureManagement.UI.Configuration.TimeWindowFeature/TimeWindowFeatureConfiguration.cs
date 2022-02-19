using DatabaseTimeWindowFeature = AspNetCore.FeatureManagement.UI.Core.Data.TimeWindowFeature;

namespace AspNetCore.FeatureManagement.UI.Configuration.TimeWindowFeature;

public interface ITimeWindowFeatureConfiguration<T> : IFeatureConfiguration
{
    List<TimeWindowFeature<T>> TimeWindows { get; }
}

public class TimeWindowFeatureConfiguration<T> : BaseFeatureConfiguration, ITimeWindowFeatureConfiguration<T>
{
    public List<TimeWindowFeature<T>> TimeWindows { get; set; } = new List<TimeWindowFeature<T>>();

    internal override void Apply(IFeatureSettings featureSettings, Feature feature)
    {
        if (featureSettings is IFeatureWithValueSettings<bool> fBool)
        {
            if (featureSettings.Configuration is ITimeWindowFeatureConfiguration<bool> gfcBool)
            {
                feature.ConfigurationType = ConfigurationTypes.TimeWindow;
                feature.TimeWindowFeatures = gfcBool.TimeWindows
                    .Select(g => new DatabaseTimeWindowFeature
                    {
                        StartDate = g.StartDate,
                        EndDate = g.EndDate,
                        BooleanValue = g.Value
                    })
                    .Concat(new[] { new DatabaseTimeWindowFeature { StartDate = null, EndDate = null, BooleanValue = fBool.Value } })
                    .ToList();
            }
        }

        if (featureSettings is IFeatureWithValueSettings<int> fInt)
        {
            if (featureSettings.Configuration is ITimeWindowFeatureConfiguration<int> gfcInt)
            {
                feature.ConfigurationType = ConfigurationTypes.TimeWindow;
                feature.TimeWindowFeatures = gfcInt.TimeWindows
                    .Select(g => new DatabaseTimeWindowFeature
                    {
                        StartDate = g.StartDate,
                        EndDate = g.EndDate,
                        IntValue = g.Value
                    })
                    .Concat(new[] { new DatabaseTimeWindowFeature { StartDate = null, EndDate = null, IntValue = fInt.Value } })
                    .ToList();
            }
        }

        if (featureSettings is IFeatureWithValueSettings<decimal> fDecimal)
        {
            if (featureSettings.Configuration is ITimeWindowFeatureConfiguration<decimal> gfcDecimal)
            {
                feature.ConfigurationType = ConfigurationTypes.TimeWindow;
                feature.TimeWindowFeatures = gfcDecimal.TimeWindows
                    .Select(g => new DatabaseTimeWindowFeature
                    {
                        StartDate = g.StartDate,
                        EndDate = g.EndDate,
                        DecimalValue = g.Value
                    })
                    .Concat(new[] { new DatabaseTimeWindowFeature { StartDate = null, EndDate = null, DecimalValue = fDecimal.Value } })
                    .ToList();
            }
        }

        if (featureSettings is IFeatureWithValueSettings<string> fString)
        {
            if (featureSettings.Configuration is ITimeWindowFeatureConfiguration<string> gfcString)
            {
                feature.ConfigurationType = ConfigurationTypes.TimeWindow;
                feature.TimeWindowFeatures = gfcString.TimeWindows
                    .Select(g => new DatabaseTimeWindowFeature
                    {
                        StartDate = g.StartDate,
                        EndDate = g.EndDate,
                        StringValue = g.Value
                    })
                    .Concat(new[] { new DatabaseTimeWindowFeature { StartDate = null, EndDate = null, StringValue = fString.Value } })
                    .ToList();
            }
        }
    }
}
