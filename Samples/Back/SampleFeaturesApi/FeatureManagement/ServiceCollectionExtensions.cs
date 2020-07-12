using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleFeaturesApi.FeatureManagement;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services, Action<Settings> setupSettings = null)
        {
            var settings = new Settings(services);

            setupSettings?.Invoke(settings);

            services.TryAddSingleton<Settings>(settings);
            services.TryAddScoped<IFeaturesService, FeaturesService>();

            return services;
        }
    }
}
