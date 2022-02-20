namespace FeatureManagement.UI.Core.Configuration;

public interface IFeatureConfiguration
{
}

public abstract class BaseFeatureConfiguration : IFeatureConfiguration
{
    internal abstract void Apply(IFeatureSettings featureSettings, Feature feature);
}
