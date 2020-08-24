using AspNetCore.FeatureManagement.UI.Core.Data;
using AspNetCore.FeatureManagement.UI.Extensions;
using AspNetCore.FeatureManagement.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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

        public SetFeatureApiEndpointMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
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
            using (var streamReader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                var featuresServices = scope.ServiceProvider.GetService<IFeaturesService>();
                var featuresAuthServices = scope.ServiceProvider.GetService<IFeaturesAuthService>();

                string? featureName = context.Request.RouteValues["featureName"] as string;
                string? clientId = featuresAuthServices.GetClientId();
                var clientGroups = featuresAuthServices.GetClientGroups(clientId);

                if (string.IsNullOrWhiteSpace(featureName))
                {
                    throw new Exception($"Property '{nameof(featureName)}' is required...");
                }

                var feature = await featuresServices.Get(featureName);

                bool canRead = featuresAuthServices.HandleReadAuth(feature, clientId);
                if (!canRead)
                {
                    throw new Exception($"You do not have permission to read the feature {featureName}...");
                }

                bool canWrite = featuresAuthServices.HandleWriteAuth(feature, clientId);
                if (!canWrite)
                {
                    throw new Exception($"You do not have permission to update the feature {featureName}...");
                }

                string jsonBody = await streamReader.ReadToEndAsync();

                Feature updatedFeature;

                if (feature.ValueType == FeatureValueTypes.Boolean)
                {
                    var payload = JsonConvert.DeserializeObject<SetFeatureValuePayload<bool>>(jsonBody);
                    updatedFeature = await featuresServices.SetValue(featureName, payload.Value, clientId);
                }
                else if (feature.ValueType == FeatureValueTypes.Integer)
                {
                    var payload = JsonConvert.DeserializeObject<SetFeatureValuePayload<int>>(jsonBody);
                    updatedFeature = await featuresServices.SetValue(featureName, payload.Value, clientId);
                }
                else if (feature.ValueType == FeatureValueTypes.Decimal)
                {
                    var payload = JsonConvert.DeserializeObject<SetFeatureValuePayload<decimal>>(jsonBody);
                    updatedFeature = await featuresServices.SetValue(featureName, payload.Value, clientId);
                }
                else
                {
                    var payload = JsonConvert.DeserializeObject<SetFeatureValuePayload<string>>(jsonBody);
                    updatedFeature = await featuresServices.SetValue(featureName, payload.Value, clientId);
                }

                bool @readonly = !featuresAuthServices.HandleWriteAuth(feature, clientId);
                var output = await updatedFeature.ToOutput(featuresServices, @readonly, clientId, clientGroups);

                var responseContent = JsonConvert.SerializeObject(output, _jsonSerializationSettings);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(responseContent);
            }
        }
    }
}