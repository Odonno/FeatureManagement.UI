namespace AspNetCore.FeatureManagement.UI.Configuration.TimeWindowFeature;

public class TimeWindowFeature<T>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    public T Value { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
}
