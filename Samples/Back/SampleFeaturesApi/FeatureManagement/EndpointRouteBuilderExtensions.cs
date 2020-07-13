using System.Collections.Generic;
using AspNetCore.FeatureManagement.UI.Middleware;
using Microsoft.AspNetCore.Routing;
using SampleFeaturesApi.FeatureManagement;

namespace Microsoft.AspNetCore.Builder
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapFeaturesUI(this IEndpointRouteBuilder builder)
        {
            var getAllFeaturesApiDelegate = builder.CreateApplicationBuilder()
                .UseMiddleware<GetAllFeaturesApiEndpointMiddleware>()
                .Build();

            var setFeatureApiDelegate = builder.CreateApplicationBuilder()
                .UseMiddleware<SetFeatureApiEndpointMiddleware>()
                .Build();

            var getAllfeaturesApiEndpoint = builder.MapGet("/features", getAllFeaturesApiDelegate)
                                .WithDisplayName("Get all Features - UI Api");

            var setFeatureApiEndpoint = builder.MapPost("/features/{featureName}/set", setFeatureApiDelegate)
                                .WithDisplayName("Set Feature value - UI Api");

            var endpointConventionBuilders = new List<IEndpointConventionBuilder>(
                new[] { getAllfeaturesApiEndpoint, setFeatureApiEndpoint }
            );
            
            return new FeaturesUIConventionBuilder(endpointConventionBuilders);
        }
    }
}