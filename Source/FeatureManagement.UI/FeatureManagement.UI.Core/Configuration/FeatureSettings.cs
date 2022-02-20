namespace FeatureManagement.UI.Core.Configuration;

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
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Type { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public bool Value { get; set; }
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
    public IFeatureConfiguration? Configuration { get; set; }
}
internal class IntFeatureSettings : IFeatureWithValueSettings<int>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Type { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public int Value { get; set; }
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
    public IFeatureConfiguration? Configuration { get; set; }
}
internal class IntFeatureWithChoicesSettings : IFeatureWithChoicesSettings<int>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Type { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public int Value { get; set; }
    public IEnumerable<int> Choices { get; set; } = new List<int>();
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
    public IFeatureConfiguration? Configuration { get; set; }
}
internal class DecimalFeatureSettings : IFeatureWithValueSettings<decimal>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Type { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public decimal Value { get; set; }
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
    public IFeatureConfiguration? Configuration { get; set; }
}
internal class DecimalFeatureWithChoicesSettings : IFeatureWithChoicesSettings<decimal>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Type { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public decimal Value { get; set; }
    public IEnumerable<decimal> Choices { get; set; } = new List<decimal>();
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
    public IFeatureConfiguration? Configuration { get; set; }
}
internal class StringFeatureSettings : IFeatureWithValueSettings<string>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Type { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Value { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
    public IFeatureConfiguration? Configuration { get; set; }
}
internal class StringFeatureWithChoicesSettings : IFeatureWithChoicesSettings<string>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string? Description { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Type { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Value { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public IEnumerable<string> Choices { get; set; } = new List<string>();
    public string? UiPrefix { get; set; }
    public string? UiSuffix { get; set; }
    public IFeatureConfiguration? Configuration { get; set; }
}
