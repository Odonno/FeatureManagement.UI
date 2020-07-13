using AspNetCore.FeatureManagement.UI.Middleware;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapFeaturesUI(this IEndpointRouteBuilder builder)
        {
            var featuresApiDelegate = builder.CreateApplicationBuilder()
                .UseMiddleware<FeaturesApiEndpointMiddleware>()
                .Build();

            var featuresApiEndpoint = builder.Map("/features", featuresApiDelegate)
                                .WithDisplayName("Features UI Api");

            return featuresApiEndpoint;
        }
    }
}