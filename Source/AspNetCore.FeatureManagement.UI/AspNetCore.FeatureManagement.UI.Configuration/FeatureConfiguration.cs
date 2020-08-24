using AspNetCore.FeatureManagement.UI.Core.Data;

namespace AspNetCore.FeatureManagement.UI.Configuration
{
    public interface IFeatureConfiguration
    {
    }

    public abstract class BaseFeatureConfiguration : IFeatureConfiguration
    {
        internal abstract void Apply(IFeatureSettings featureSettings, Feature feature);
    }
}
