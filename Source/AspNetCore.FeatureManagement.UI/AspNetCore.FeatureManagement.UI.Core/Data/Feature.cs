using System.Collections.Generic;

namespace AspNetCore.FeatureManagement.UI.Core.Data
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }        

        public ServerFeatureData? Server { get; set; }
        public List<ClientFeatureData>? Clients { get; set; }
        public List<IntFeatureChoice> IntFeatureChoices { get; set; }
        public List<DecimalFeatureChoice> DecimalFeatureChoices { get; set; }
        public List<StringFeatureChoice> StringFeatureChoices { get; set; }
    }
}
