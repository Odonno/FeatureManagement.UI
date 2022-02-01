namespace AspNetCore.FeatureManagement.UI.Middleware;

internal class GetAuthSchemesApiEndpointMiddleware
{
    private readonly JsonSerializerSettings _jsonSerializationSettings;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GetAuthSchemesApiEndpointMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
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
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var settings = scope.ServiceProvider.GetService<Settings>();

            var output = settings.AuthSchemes;

            var responseContent = JsonConvert.SerializeObject(output, _jsonSerializationSettings);
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(responseContent);
        }
    }
}
