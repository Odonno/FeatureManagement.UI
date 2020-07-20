namespace AspNetCore.FeatureManagement.UI.Core.Data
{
    public class IntFeatureChoice
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int Choice { get; set; }

        public Feature Feature { get; set; }
    }
}
