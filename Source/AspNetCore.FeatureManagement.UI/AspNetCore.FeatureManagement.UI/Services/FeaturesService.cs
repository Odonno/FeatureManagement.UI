using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Data;
using AspNetCore.FeatureManagement.UI.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.FeatureManagement.UI.Services
{
    public interface IFeaturesService
    {
        /// <summary>
        /// Retrieve the list of every feature.
        /// </summary>
        /// <returns></returns>
        Task<List<Feature>> GetAll();

        /// <summary>
        /// Retrieve a single feature by its name.
        /// </summary>
        /// <param name="featureName">The name of the feature to get.</param>
        /// <returns></returns>
        Task<Feature> Get(string featureName);

        /// <summary>
        /// Retrieve the value of a feature.
        /// </summary>
        /// <typeparam name="T">Value type of the feature. Only <see cref="bool"/>, <see cref="int"/>, <see cref="decimal"/> and <see cref="string"/> are allowed.</typeparam>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="clientId">Id of the client.</param>
        /// <returns>The value of the feature.</returns>
        Task<T> GetValue<T>(string featureName, string? clientId = null);

        /// <summary>
        /// Update the value of a feature.
        /// </summary>
        /// <typeparam name="T">Value type of the feature. Only <see cref="bool"/>, <see cref="int"/>, <see cref="decimal"/> and <see cref="string"/> are allowed.</typeparam>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="value">The new value of the feature.</param>
        /// <param name="clientId">Id of the client.</param>
        /// <returns>The updated feature.</returns>
        Task<Feature> SetValue<T>(string featureName, T value, string? clientId = null);
    }

    internal class FeaturesService : IFeaturesService
    {
        private readonly FeatureManagementDb _featureManagementDb;
        private readonly Settings _settings;

        public FeaturesService(FeatureManagementDb featureManagementDb, Settings settings)
        {
            _featureManagementDb = featureManagementDb;
            _settings = settings;
        }

        private async Task<ClientFeatureData> EnsuresClientDataExists(Feature feature, string clientId)
        {
            var clientFeatureData = await _featureManagementDb.ClientFeatureDatas
                .SingleOrDefaultAsync(c => c.FeatureId == feature.Id && c.ClientId == clientId);

            if (clientFeatureData != null)
            {
                return clientFeatureData;
            }

            var featureSettings = _settings.Features
                .SingleOrDefault(f => f.Name == feature.Name);

            var newClientFeatureData = new ClientFeatureData
            {
                FeatureId = feature.Id,
                ClientId = clientId,
                BooleanValue = featureSettings is IFeatureWithValueSettings<bool> fBool
                    ? (bool?)fBool.Value
                    : null,
                IntValue = featureSettings is IFeatureWithValueSettings<int> fInt
                    ? (int?)fInt.Value
                    : null,
                DecimalValue = featureSettings is IFeatureWithValueSettings<decimal> fDecimal
                    ? (decimal?)fDecimal.Value
                    : null,
                StringValue = featureSettings is IFeatureWithValueSettings<string> fString
                    ? fString.Value
                    : null
            };

            _featureManagementDb.ClientFeatureDatas.Add(newClientFeatureData);
            await _featureManagementDb.SaveChangesAsync();

            return newClientFeatureData;
        }

        public Task<List<Feature>> GetAll()
        {
            return _featureManagementDb.Features
                .Include(f => f.Server)
                .Include(f => f.IntFeatureChoices)
                .Include(f => f.DecimalFeatureChoices)
                .Include(f => f.StringFeatureChoices)
                .ToListAsync();
        }

        public Task<Feature> Get(string featureName)
        {
            return _featureManagementDb.Features
                .Include(f => f.Server)
                .Include(f => f.IntFeatureChoices)
                .Include(f => f.DecimalFeatureChoices)
                .Include(f => f.StringFeatureChoices)
                .SingleOrDefaultAsync(f => f.Name == featureName);
        }

        public async Task<T> GetValue<T>(string featureName, string? clientId = null)
        {
            var existingFeature = await Get(featureName);

            if (existingFeature == null)
            {
                throw new Exception($"The feature {featureName} does not exist...");
            }

            if (existingFeature.ValueType == FeatureValueTypes.Boolean)
            {
                if (typeof(T) != typeof(bool))
                {
                    throw new Exception($"The feature {featureName} is a boolean feature...");
                }

                if (existingFeature.Type == FeatureTypes.Server)
                {
                    // Server feature
                    return (T)(object)existingFeature.Server.BooleanValue.Value;
                }

                // Client feature
                if (string.IsNullOrWhiteSpace(clientId))
                {
                    throw new Exception("A client id is required for client features...");
                }

                var clientData = await EnsuresClientDataExists(existingFeature, clientId);
                return (T)(object)clientData.BooleanValue.Value;
            }

            if (existingFeature.ValueType == FeatureValueTypes.Integer)
            {
                if (typeof(T) != typeof(int))
                {
                    throw new Exception($"The feature {featureName} is an integer feature...");
                }

                if (existingFeature.Type == FeatureTypes.Server)
                {
                    // Server feature
                    return (T)(object)existingFeature.Server.IntValue.Value;
                }

                // Client feature
                if (string.IsNullOrWhiteSpace(clientId))
                {
                    throw new Exception("A client id is required for client features...");
                }

                var clientData = await EnsuresClientDataExists(existingFeature, clientId);
                return (T)(object)clientData.IntValue.Value;
            }

            if (existingFeature.ValueType == FeatureValueTypes.Decimal)
            {
                if (typeof(T) != typeof(decimal))
                {
                    throw new Exception($"The feature {featureName} is a decimal feature...");
                }

                if (existingFeature.Type == FeatureTypes.Server)
                {
                    // Server feature
                    return (T)(object)existingFeature.Server.DecimalValue.Value;
                }

                // Client feature
                if (string.IsNullOrWhiteSpace(clientId))
                {
                    throw new Exception("A client id is required for client features...");
                }

                var clientData = await EnsuresClientDataExists(existingFeature, clientId);
                return (T)(object)clientData.DecimalValue.Value;
            }

            if (existingFeature.ValueType == FeatureValueTypes.String)
            {
                if (typeof(T) != typeof(string))
                {
                    throw new Exception($"The feature {featureName} is a string feature...");
                }

                if (existingFeature.Type == FeatureTypes.Server)
                {
                    // Server feature
                    return (T)(object)existingFeature.Server.StringValue;
                }

                // Client feature
                if (string.IsNullOrWhiteSpace(clientId))
                {
                    throw new Exception("A client id is required for client features...");
                }

                var clientData = await EnsuresClientDataExists(existingFeature, clientId);
                return (T)(object)clientData.StringValue;
            }

            throw new Exception("Only value of types bool, int, decimal and string are allowed...");
        }

        public async Task<Feature> SetValue<T>(string featureName, T value, string? clientId = null)
        {
            var existingFeature = await Get(featureName);

            if (existingFeature == null)
            {
                throw new Exception($"The feature {featureName} does not exist...");
            }

            if (existingFeature.ValueType == FeatureValueTypes.Boolean)
            {
                if (value is bool boolValue)
                {
                    if (existingFeature.Type == FeatureTypes.Server)
                    {
                        // Server feature
                        existingFeature.Server.BooleanValue = boolValue;
                    }

                    if (existingFeature.Type == FeatureTypes.Client)
                    {
                        // Client feature
                        if (string.IsNullOrWhiteSpace(clientId))
                        {
                            throw new Exception("A client id is required for client features...");
                        }

                        var clientData = await EnsuresClientDataExists(existingFeature, clientId);
                        clientData.BooleanValue = boolValue;
                    }
                }
                else
                {
                    throw new Exception($"The feature {featureName} is a boolean feature...");
                }
            }

            if (existingFeature.ValueType == FeatureValueTypes.Integer)
            {
                if (value is int intValue)
                {
                    if (existingFeature.IntFeatureChoices.Any(c => c.Choice == intValue))
                    {
                        if (existingFeature.Type == FeatureTypes.Server)
                        {
                            // Server feature
                            existingFeature.Server.IntValue = intValue;
                        }

                        if (existingFeature.Type == FeatureTypes.Client)
                        {
                            // Client feature
                            if (string.IsNullOrWhiteSpace(clientId))
                            {
                                throw new Exception("A client id is required for client features...");
                            }

                            var clientData = await EnsuresClientDataExists(existingFeature, clientId);
                            clientData.IntValue = intValue;
                        }
                    }
                    else
                    {
                        throw new Exception($"The value {intValue} is not a valid choice for feature {featureName}...");
                    }
                }
                else
                {
                    throw new Exception($"The feature {featureName} is an integer feature...");
                }
            }

            if (existingFeature.ValueType == FeatureValueTypes.Decimal)
            {
                if (value is decimal decimalValue)
                {
                    if (existingFeature.DecimalFeatureChoices.Any(c => c.Choice == decimalValue))
                    {
                        if (existingFeature.Type == FeatureTypes.Server)
                        {
                            // Server feature
                            existingFeature.Server.DecimalValue = decimalValue;
                        }

                        if (existingFeature.Type == FeatureTypes.Client)
                        {
                            // Client feature
                            if (string.IsNullOrWhiteSpace(clientId))
                            {
                                throw new Exception("A client id is required for client features...");
                            }

                            var clientData = await EnsuresClientDataExists(existingFeature, clientId);
                            clientData.DecimalValue = decimalValue;
                        }
                    }
                    else
                    {
                        throw new Exception($"The value {decimalValue} is not a valid choice for feature {featureName}...");
                    }
                }
                else
                {
                    throw new Exception($"The feature {featureName} is a decimal feature...");
                }
            }

            if (existingFeature.ValueType == FeatureValueTypes.String)
            {
                if (value is string stringValue)
                {
                    if (existingFeature.StringFeatureChoices.Any(c => c.Choice == stringValue))
                    {
                        if (existingFeature.Type == FeatureTypes.Server)
                        {
                            // Server feature
                            existingFeature.Server.StringValue = stringValue;
                        }

                        if (existingFeature.Type == FeatureTypes.Client)
                        {
                            // Client feature
                            if (string.IsNullOrWhiteSpace(clientId))
                            {
                                throw new Exception("A client id is required for client features...");
                            }

                            var clientData = await EnsuresClientDataExists(existingFeature, clientId);
                            clientData.StringValue = stringValue;
                        }
                    }
                    else
                    {
                        throw new Exception($"The value {stringValue} is not a valid choice for feature {featureName}...");
                    }
                }
                else
                {
                    throw new Exception($"The feature {featureName} is a string feature...");
                }
            }

            await _featureManagementDb.SaveChangesAsync();

            if (existingFeature.Type == FeatureTypes.Server)
            {
                var output = await existingFeature.ToOutput(this, _settings, clientId);
                _settings.OnServerFeatureUpdated?.Invoke(output);
            }
            else
            {
                var output = await existingFeature.ToOutput(this, _settings, clientId);
                _settings.OnClientFeatureUpdated?.Invoke(output, clientId);
            }

            return existingFeature;
        }
    }
}
