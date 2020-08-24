using System.Collections.Generic;

namespace AspNetCore.FeatureManagement.UI.Core.Configuration
{
    internal interface IFeatureSettings
    {
        string Name { get; set; }
        string? Description { get; set; }
        string Type { get; set; }
        string? UiPrefix { get; set; }
        string? UiSuffix { get; set; }
        IFeatureConfiguration? Configuration { get; set; }
    }

    internal interface IFeatureWithValueSettings<T> : IFeatureSettings
    {
        T Value { get; set; }
    }

    internal interface IFeatureWithChoicesSettings<T> : IFeatureWithValueSettings<T>
    {
        IEnumerable<T> Choices { get; set; }
    }

    internal class BoolFeatureSettings : IFeatureWithValueSettings<bool>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public bool Value { get; set; }
        public string? UiPrefix { get; set; }
        public string? UiSuffix { get; set; }
        public IFeatureConfiguration? Configuration { get; set; }
    }
    internal class IntFeatureSettings : IFeatureWithValueSettings<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
        public string? UiPrefix { get; set; }
        public string? UiSuffix { get; set; }
        public IFeatureConfiguration? Configuration { get; set; }
    }
    internal class IntFeatureWithChoicesSettings : IFeatureWithChoicesSettings<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
        public IEnumerable<int> Choices { get; set; } = new List<int>();
        public string? UiPrefix { get; set; }
        public string? UiSuffix { get; set; }
        public IFeatureConfiguration? Configuration { get; set; }
    }
    internal class DecimalFeatureSettings : IFeatureWithValueSettings<decimal>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public string? UiPrefix { get; set; }
        public string? UiSuffix { get; set; }
        public IFeatureConfiguration? Configuration { get; set; }
    }
    internal class DecimalFeatureWithChoicesSettings : IFeatureWithChoicesSettings<decimal>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public IEnumerable<decimal> Choices { get; set; } = new List<decimal>();
        public string? UiPrefix { get; set; }
        public string? UiSuffix { get; set; }
        public IFeatureConfiguration? Configuration { get; set; }
    }
    internal class StringFeatureSettings : IFeatureWithValueSettings<string>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string? UiPrefix { get; set; }
        public string? UiSuffix { get; set; }
        public IFeatureConfiguration? Configuration { get; set; }
    }
    internal class StringFeatureWithChoicesSettings : IFeatureWithChoicesSettings<string>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public IEnumerable<string> Choices { get; set; } = new List<string>();
        public string? UiPrefix { get; set; }
        public string? UiSuffix { get; set; }
        public IFeatureConfiguration? Configuration { get; set; }
    }
}
