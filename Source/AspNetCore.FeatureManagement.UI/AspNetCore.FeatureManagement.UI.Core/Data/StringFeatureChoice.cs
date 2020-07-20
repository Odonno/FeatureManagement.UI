namespace AspNetCore.FeatureManagement.UI.Core.Data
{
    public class StringFeatureChoice
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public string Choice { get; set; }

        public Feature Feature { get; set; }
    }
}
