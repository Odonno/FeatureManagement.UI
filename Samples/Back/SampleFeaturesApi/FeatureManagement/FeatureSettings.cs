using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleFeaturesApi.FeatureManagement
{
    public class FeatureSettings
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }
    }
}
