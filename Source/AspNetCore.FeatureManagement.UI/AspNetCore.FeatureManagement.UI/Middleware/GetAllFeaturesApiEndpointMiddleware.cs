using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Extensions;
using AspNetCore.FeatureManagement.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.FeatureManagement.UI.Middleware
{
    internal class GetAllFeaturesApiEndpointMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _jsonSerializationSettings;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Settings _settings;

        public GetAllFeaturesApiEndpointMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, IOptions<Settings> settings)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
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
                var featuresServices = scope.ServiceProvider.GetService<IFeaturesService>();

                var features = await featuresServices.GetAll();

                var output = features.Select(f => f.ToOutput());

                var responseContent = JsonConvert.SerializeObject(output, _jsonSerializationSettings);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(responseContent);
            }
        }
    }
}