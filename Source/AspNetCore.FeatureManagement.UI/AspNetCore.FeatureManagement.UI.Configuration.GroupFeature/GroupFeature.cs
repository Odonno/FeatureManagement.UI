namespace AspNetCore.FeatureManagement.UI.Configuration.GroupFeature
{
    public class GroupFeature<T>
    {
        public string Group { get; set; }
        public T Value { get; set; }
    }
}
