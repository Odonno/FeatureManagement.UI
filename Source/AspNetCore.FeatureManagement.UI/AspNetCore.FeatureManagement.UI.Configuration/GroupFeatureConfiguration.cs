using System.Collections.Generic;

namespace AspNetCore.FeatureManagement.UI.Configuration
{
    public class GroupFeatureConfiguration<T> : IGroupFeatureConfiguration<T>
    {
        public List<GroupFeature<T>> Groups { get; set; } = new List<GroupFeature<T>>();
    }

    public class GroupFeature<T>
    {
        public string Group { get; set; }
        public T Value { get; set; }
    }
}
