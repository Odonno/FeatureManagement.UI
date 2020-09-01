using System.Collections.Generic;

namespace AspNetCore.FeatureManagement.UI.Core.Data
{
    public class Feature
    {
        public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string? Description { get; set; }
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string ValueType { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string ConfigurationType { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string Type => Server != null ? FeatureTypes.Server : FeatureTypes.Client;
        public string? UiPrefix { get; set; }
        public string? UiSuffix { get; set; }

        public ServerFeatureData? Server { get; set; }
        public List<ClientFeatureData>? Clients { get; set; }
        public List<IntFeatureChoice> IntFeatureChoices { get; set; } = new List<IntFeatureChoice>();
        public List<DecimalFeatureChoice> DecimalFeatureChoices { get; set; } = new List<DecimalFeatureChoice>();
        public List<StringFeatureChoice> StringFeatureChoices { get; set; } = new List<StringFeatureChoice>();
        public List<GroupFeature> GroupFeatures { get; set; } = new List<GroupFeature>();
        public List<TimeWindowFeature> TimeWindowFeatures { get; set; } = new List<TimeWindowFeature>();
    }
}
