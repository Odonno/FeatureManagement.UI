namespace AspNetCore.FeatureManagement.UI.Core.Data
{
    public class DecimalFeatureChoice
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public decimal Choice { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public Feature Feature { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    }
}
