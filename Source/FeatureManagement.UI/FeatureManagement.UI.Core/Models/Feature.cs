namespace FeatureManagement.UI.Core.Models;

public interface IFeature
{
    string Name { get; set; }
    string? Description { get; set; }
}

public interface IFeatureWithValue<T> : IFeature
{
    T Value { get; set; }
    bool Readonly { get; set; }
    string? UiPrefix { get; set; }
    string? UiSuffix { get; set; }
}

public class BoolFeature : IFeatureWithValue<bool?>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
    public bool? Value { get; set; }
    public bool Readonly { get; set; }
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
}
public class IntFeature : IFeatureWithValue<int?>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
    public int? Value { get; set; }
    public List<int>? Choices { get; set; }
    public bool Readonly { get; set; }
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
}
public class DecimalFeature : IFeatureWithValue<decimal?>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
    public decimal? Value { get; set; }
    public List<decimal>? Choices { get; set; }
    public bool Readonly { get; set; }
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
}
public class StringFeature : IFeatureWithValue<string?>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
    public string? Value { get; set; }
    public List<string>? Choices { get; set; }
    public bool Readonly { get; set; }
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
}
