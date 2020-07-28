namespace AspNetCore.FeatureManagement.UI.Core.Data
{
    public class ServerFeatureData
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }

        public bool? BooleanValue { get; set; }
        public int? IntValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public string? StringValue { get; set; }

        public Feature Feature { get; set; }
    }
}
