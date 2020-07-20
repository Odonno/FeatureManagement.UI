namespace AspNetCore.FeatureManagement.UI.Core.Data
{
    public class DecimalFeatureChoice
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public decimal Choice { get; set; }

        public Feature Feature { get; set; }
    }
}
