using AspNetCore.FeatureManagement.UI.Core.Configuration;
using AspNetCore.FeatureManagement.UI.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds features to the ASP.NET Core Web API.
        /// 
        /// This method configures the <see cref="IFeaturesService"/> and prepare the configuration of features.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="setupSettings">Apply <see cref="Settings"/> to configure features properly.</param>
        /// <returns>The <see cref="IServiceCollection"/> now configured.</returns>
        public static IServiceCollection AddFeatures(this IServiceCollection services, Action<Settings>? setupSettings = null)
        {
            var settings = new Settings(services);

            setupSettings?.Invoke(settings);

            services.TryAddSingleton(settings);
            services.TryAddScoped<IFeaturesService, FeaturesService>();

            return services;
        }
    }
}
