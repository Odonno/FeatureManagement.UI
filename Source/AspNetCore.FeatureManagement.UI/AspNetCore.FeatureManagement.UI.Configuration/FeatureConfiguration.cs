using System.Collections.Generic;

namespace AspNetCore.FeatureManagement.UI.Configuration
{
    public interface IFeatureConfiguration
    {
    }

    public interface IGroupFeatureConfiguration<T> : IFeatureConfiguration
    {
        List<GroupFeature<T>> Groups { get; }
    }
}
