namespace Microsoft.AspNetCore.Builder;

public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Adds endpoints for Features actions to the <see cref="IEndpointRouteBuilder"/>. And also display the UI.
    /// </summary>
    /// <param name="builder">The <see cref="IEndpointRouteBuilder"/>.</param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> for endpoints associated with controller actions.</returns>
    public static IEndpointConventionBuilder MapFeaturesUI(this IEndpointRouteBuilder builder)
    {
        var getAuthSchemesApiDelegate = builder.CreateApplicationBuilder()
            .UseMiddleware<GetAuthSchemesApiEndpointMiddleware>()
            .Build();

        var getAllFeaturesApiDelegate = builder.CreateApplicationBuilder()
            .UseMiddleware<GetAllFeaturesApiEndpointMiddleware>()
            .Build();

        var setFeatureApiDelegate = builder.CreateApplicationBuilder()
            .UseMiddleware<SetFeatureApiEndpointMiddleware>()
            .Build();

        var getAuthSchemesApiEndpoint = builder.MapGet("/features/auth/schemes", getAuthSchemesApiDelegate)
                            .WithDisplayName("Get Features auth schemes - UI Api");

        var getAllfeaturesApiEndpoint = builder.MapGet("/features", getAllFeaturesApiDelegate)
                            .WithDisplayName("Get all Features - UI Api");

        var setFeatureApiEndpoint = builder.MapPost("/features/{featureName}/set", setFeatureApiDelegate)
                            .WithDisplayName("Set Feature value - UI Api");

        var resourcesEndpoints = new UIEndpointsResourceMapper()
            .Map(builder, new Options());

        var endpointConventionBuilders = new List<IEndpointConventionBuilder>(
            new[] { getAuthSchemesApiEndpoint, getAllfeaturesApiEndpoint, setFeatureApiEndpoint }.Union(resourcesEndpoints)
        );

        return new FeaturesUIConventionBuilder(endpointConventionBuilders);
    }
}
