using AspNetCore.FeatureManagement.UI.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AspNetCore.FeatureManagement.UI.Configuration
{
    public class Settings
    {
        internal IServiceCollection Services { get; }
        internal List<IFeatureSettings> Features { get; } = new List<IFeatureSettings>();
        public Action<IFeature>? OnFeatureUpdated { get; set; }

        public Settings() { }
        public Settings(IServiceCollection services)
        {
            Services = services;
        }
    }
}
