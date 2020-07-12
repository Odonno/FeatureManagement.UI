using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleFeaturesApi.FeatureManagement
{
    public class Settings
    {
        internal IServiceCollection Services { get; }
        internal List<FeatureSettings> Features { get; } = new List<FeatureSettings>();

        public Settings() { }
        public Settings(IServiceCollection services)
        {
            Services = services;
        }
    }
}
