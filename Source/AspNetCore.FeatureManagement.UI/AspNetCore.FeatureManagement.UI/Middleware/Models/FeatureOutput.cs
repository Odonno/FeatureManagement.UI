namespace AspNetCore.FeatureManagement.UI.Middleware.Models
{
    internal interface IFeatureOutput
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal interface IFeatureWithValueOutput<T> : IFeatureOutput
    {
        public T Value { get; set; }
    }

    internal class BoolFeature : IFeatureWithValueOutput<bool?>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Value { get; set; }
    }
    internal class IntFeature : IFeatureWithValueOutput<int?>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Value { get; set; }
    }
    internal class DecimalFeature : IFeatureWithValueOutput<decimal?>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }
    }
    internal class StringFeature : IFeatureWithValueOutput<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
