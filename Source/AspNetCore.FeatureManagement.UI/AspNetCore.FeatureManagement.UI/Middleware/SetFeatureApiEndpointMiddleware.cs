using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Data;
using AspNetCore.FeatureManagement.UI.Middleware.Extensions;
using AspNetCore.FeatureManagement.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.FeatureManagement.UI.Middleware
{
    internal class SetFeatureValuePayload<T>
    {
        public T Value { get; set; }
    }

    internal class SetFeatureApiEndpointMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _jsonSerializationSettings;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly Settings _settings;

        public SetFeatureApiEndpointMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, IOptions<Settings> settings)
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
            using (var streamReader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                var featuresServices = scope.ServiceProvider.GetService<IFeaturesService>();

                string featureName = context.Request.RouteValues["featureName"] as string;

                var feature = await featuresServices.Get(featureName);

                string jsonBody = await streamReader.ReadToEndAsync();

                Feature updatedFeature = null;

                if (feature.Type == FeatureTypes.Boolean)
                {
                    var payload = JsonConvert.DeserializeObject<SetFeatureValuePayload<bool>>(jsonBody);
                    updatedFeature = await featuresServices.Set(featureName, payload.Value);
                }
                if (feature.Type == FeatureTypes.Integer)
                {
                    var payload = JsonConvert.DeserializeObject<SetFeatureValuePayload<int>>(jsonBody);
                    updatedFeature = await featuresServices.Set(featureName, payload.Value);
                }
                if (feature.Type == FeatureTypes.Decimal)
                {
                    var payload = JsonConvert.DeserializeObject<SetFeatureValuePayload<decimal>>(jsonBody);
                    updatedFeature = await featuresServices.Set(featureName, payload.Value);
                }
                if (feature.Type == FeatureTypes.String)
                {
                    var payload = JsonConvert.DeserializeObject<SetFeatureValuePayload<string>>(jsonBody);
                    updatedFeature = await featuresServices.Set(featureName, payload.Value);
                }

                var output = updatedFeature.ToOutput();

                var responseContent = JsonConvert.SerializeObject(output, _jsonSerializationSettings);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(responseContent);
            }
        }
    }
}