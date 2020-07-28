using System.Collections.Generic;

namespace AspNetCore.FeatureManagement.UI.Configuration
{
    internal interface IFeatureSettings
    {
        string Name { get; set; }
        string? Description { get; set; }
    }

    internal interface IServerFeatureSettings : IFeatureSettings { }
    internal interface IClientFeatureSettings : IFeatureSettings { }

    internal interface IServerFeatureWithValueSettings<T> : IServerFeatureSettings
    {
        T Value { get; set; }
    }
    internal interface IClientFeatureWithValueSettings<T> : IClientFeatureSettings
    {
        T Value { get; set; }
    }

    internal interface IServerFeatureWithChoicesSettings<T> : IServerFeatureWithValueSettings<T>
    {
        IEnumerable<T> Choices { get; set; }
    }
    internal interface IClientFeatureWithChoicesSettings<T> : IClientFeatureWithValueSettings<T>
    {
        IEnumerable<T> Choices { get; set; }
    }

    internal class BoolServerFeatureSettings : IServerFeatureWithValueSettings<bool>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Value { get; set; }
    }
    internal class IntServerFeatureSettings : IServerFeatureWithValueSettings<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Value { get; set; }
    }
    internal class IntServerFeatureWithChoicesSettings : IServerFeatureWithChoicesSettings<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Value { get; set; }
        public IEnumerable<int> Choices { get; set; } = new List<int>();
    }
    internal class DecimalServerFeatureSettings : IServerFeatureWithValueSettings<decimal>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
    }
    internal class DecimalServerFeatureWithChoicesSettings : IServerFeatureWithChoicesSettings<decimal>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public IEnumerable<decimal> Choices { get; set; } = new List<decimal>();
    }
    internal class StringServerFeatureSettings : IServerFeatureWithValueSettings<string>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Value { get; set; }
    }
    internal class StringServerFeatureWithChoicesSettings : IServerFeatureWithChoicesSettings<string>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Value { get; set; }
        public IEnumerable<string> Choices { get; set; } = new List<string>();
    }

    internal class BoolClientFeatureSettings : IClientFeatureWithValueSettings<bool>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Value { get; set; }
    }
    internal class IntClientFeatureSettings : IClientFeatureWithValueSettings<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Value { get; set; }
    }
    internal class IntClientFeatureWithChoicesSettings : IClientFeatureWithChoicesSettings<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Value { get; set; }
        public IEnumerable<int> Choices { get; set; } = new List<int>();
    }
    internal class DecimalClientFeatureSettings : IClientFeatureWithValueSettings<decimal>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
    }
    internal class DecimalClientFeatureWithChoicesSettings : IClientFeatureWithChoicesSettings<decimal>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Value { get; set; }
        public IEnumerable<decimal> Choices { get; set; } = new List<decimal>();
    }
    internal class StringClientFeatureSettings : IClientFeatureWithValueSettings<string>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Value { get; set; }
    }
    internal class StringClientFeatureWithChoicesSettings : IClientFeatureWithChoicesSettings<string>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Value { get; set; }
        public IEnumerable<string> Choices { get; set; } = new List<string>();
    }
}
