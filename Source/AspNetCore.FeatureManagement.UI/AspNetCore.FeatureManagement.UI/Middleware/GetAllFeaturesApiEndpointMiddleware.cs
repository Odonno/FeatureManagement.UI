namespace AspNetCore.FeatureManagement.UI.Middleware;

internal class GetAllFeaturesApiEndpointMiddleware
{
    private readonly JsonSerializerSettings _jsonSerializationSettings;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAllFeaturesApiEndpointMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _jsonSerializationSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new[] { new StringEnumConverter() },
            DateTimeZoneHandling = DateTimeZoneHandling.Local,
            NullValueHandling = NullValueHandling.Ignore
        };
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var featuresServices = scope.ServiceProvider.GetService<IFeaturesService>();
        var featuresAuthServices = scope.ServiceProvider.GetService<IFeaturesAuthService>();

        string? clientId = featuresAuthServices.GetClientId();
        var clientGroups = featuresAuthServices.GetClientGroups(clientId);

        var features = await featuresServices.GetAll();

        var readableFeatures = features
            .Where(f => featuresAuthServices.HandleReadAuth(f, clientId));

        var output = new List<IFeature>();

        foreach (var feature in readableFeatures)
        {
            bool @readonly = !string.IsNullOrWhiteSpace(feature.ConfigurationType) || !featuresAuthServices.HandleWriteAuth(feature, clientId);
            var outputFeature = await feature.ToOutput(featuresServices, @readonly, clientId, clientGroups);

            output.Add(outputFeature);
        }

        var responseContent = JsonConvert.SerializeObject(output, _jsonSerializationSettings);
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(responseContent);
    }
}
