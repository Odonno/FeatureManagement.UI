using System.Collections.Generic;
using System.Linq;
using AspNetCore.FeatureManagement.UI;
using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Core;
using AspNetCore.FeatureManagement.UI.Middleware;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder
{
    public static class EndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Adds endpoints for Features actions to the <see cref="IEndpointRouteBuilder"/>. And also display the UI.
        /// </summary>
        /// <param name="builder">The <see cref="IEndpointRouteBuilder"/>.</param>
        /// <returns>An <see cref="IEndpointConventionBuilder"/> for endpoints associated with controller actions.</returns>
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

            var resourcesEndpoints = new UIEndpointsResourceMapper()
                .Map(builder, new Options());
                
            var endpointConventionBuilders = new List<IEndpointConventionBuilder>(
                new[] { getAllfeaturesApiEndpoint, setFeatureApiEndpoint }.Union(resourcesEndpoints)
            );
            
            return new FeaturesUIConventionBuilder(endpointConventionBuilders);
        }
    }
}