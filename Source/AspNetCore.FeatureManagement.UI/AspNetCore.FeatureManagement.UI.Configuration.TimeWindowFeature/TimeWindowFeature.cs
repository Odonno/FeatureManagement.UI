using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.FeatureManagement.UI.Configuration.TimeWindowFeature
{
    public class TimeWindowFeature<T>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public T Value { get; set; }
    }
}
