using System.Collections.Generic;

namespace AspNetCore.FeatureManagement.UI.Core.Models
{
    public interface IFeature
    {
        string Name { get; set; }
        string? Description { get; set; }
    }

    public interface IFeatureWithValue<T> : IFeature
    {
        T Value { get; set; }
        bool Readonly { get; set; }
    }

    public class BoolFeature : IFeatureWithValue<bool?>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? Value { get; set; }
        public bool Readonly { get; set; }
    }
    public class IntFeature : IFeatureWithValue<int?>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Value { get; set; }
        public List<int>? Choices { get; set; }
        public bool Readonly { get; set; }
    }
    public class DecimalFeature : IFeatureWithValue<decimal?>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal? Value { get; set; }
        public List<decimal>? Choices { get; set; }
        public bool Readonly { get; set; }
    }
    public class StringFeature : IFeatureWithValue<string?>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
        public List<string>? Choices { get; set; }
        public bool Readonly { get; set; }
    }
}
