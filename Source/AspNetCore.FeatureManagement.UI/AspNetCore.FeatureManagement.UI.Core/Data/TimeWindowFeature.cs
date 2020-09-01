using System;

namespace AspNetCore.FeatureManagement.UI.Core.Data
{
    public class TimeWindowFeature
    {
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool? BooleanValue { get; set; }
        public int? IntValue { get; set; }
        public decimal? DecimalValue { get; set; }
        public string? StringValue { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public Feature Feature { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
    }
}
