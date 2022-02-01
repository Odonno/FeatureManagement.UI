namespace AspNetCore.FeatureManagement.UI.Configuration.GroupFeature;

public class GroupFeature<T>
{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public string Group { get; set; }
    public T Value { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
}
