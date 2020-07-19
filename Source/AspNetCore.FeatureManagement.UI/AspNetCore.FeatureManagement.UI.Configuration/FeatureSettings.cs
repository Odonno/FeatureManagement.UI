namespace AspNetCore.FeatureManagement.UI.Configuration
{
    internal interface IFeatureSettings
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    internal interface IFeatureWithValueSettings<T> : IFeatureSettings
    {
        T Value { get; set; }
    }

    internal class BoolFeatureSettings : IFeatureWithValueSettings<bool>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Value { get; set; }
    }
    internal class IntFeatureSettings : IFeatureWithValueSettings<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
    }
    internal class DecimalFeatureSettings : IFeatureWithValueSettings<decimal>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
    internal class StringFeatureSettings : IFeatureWithValueSettings<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
