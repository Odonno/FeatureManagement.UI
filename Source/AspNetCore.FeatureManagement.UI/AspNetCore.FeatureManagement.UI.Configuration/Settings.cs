using AspNetCore.FeatureManagement.UI.Core.Data;
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

        /// <summary>
        /// Retrieve client id used to identify the user of each client features
        /// </summary>
        public Func<string>? GetClientId { get; set; }

        /// <summary>
        /// Indicates if the user can see this feature
        /// </summary>
        public Func<Feature, string?, bool> HandleReadAuth { get; set; }

        /// <summary>
        /// Indicates if the user can update this feature (server features are often only managed by an admin)
        /// </summary>
        public Func<Feature, string?, bool> HandleWriteAuth { get; set; }

        /// <summary>
        /// Happens when a server feature is updated
        /// </summary>
        public Action<IFeature>? OnServerFeatureUpdated { get; set; }

        /// <summary>
        /// Happens when a client feature is updated
        /// </summary>
        public Action<IFeature, string>? OnClientFeatureUpdated { get; set; }

        /// <summary>
        /// List of authentication schemes applied in Features authentication (to be used in the UI)
        /// </summary>
        public List<IAuthenticationScheme> AuthSchemes { get; internal set; } = new List<IAuthenticationScheme>();

        public Settings() 
        {
            Initialize();    
        }
        public Settings(IServiceCollection services)
        {
            Services = services;
            Initialize();
        }

        private void Initialize()
        {
            HandleReadAuth = (Feature feature, string? clientId) =>
            {
                return true;
            };
            HandleWriteAuth = (Feature feature, string? clientId) =>
            {
                return feature.Type == FeatureTypes.Client;
            };
        }
    }
}
