using AspNetCore.FeatureManagement.UI.Core.Data;
using AspNetCore.FeatureManagement.UI.Core.Models;
using AspNetCore.FeatureManagement.UI.Middleware.Extensions;
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
        /// <returns>The value of the feature.</returns>
        Task<T> GetValue<T>(string featureName);

        /// <summary>
        /// Update the value of a feature.
        /// </summary>
        /// <typeparam name="T">Value type of the feature. Only <see cref="bool"/>, <see cref="int"/>, <see cref="decimal"/> and <see cref="string"/> are allowed.</typeparam>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="value">The new value of the feature.</param>
        /// <returns>The updated feature.</returns>
        Task<Feature> SetValue<T>(string featureName, T value);
    }

    internal class FeaturesService : IFeaturesService
    {
        private readonly FeatureManagementDb _featureManagementDb;
        private readonly Action<IFeature>? _onFeatureUpdated;

        public FeaturesService(FeatureManagementDb featureManagementDb, Action<IFeature>? onFeatureUpdated)
        {
            _featureManagementDb = featureManagementDb;
            _onFeatureUpdated = onFeatureUpdated;
        }

        public Task<List<Feature>> GetAll()
        {
            return _featureManagementDb.Features
                .Include(f => f.IntFeatureChoices)
                .Include(f => f.DecimalFeatureChoices)
                .Include(f => f.StringFeatureChoices)
                .ToListAsync();
        }

        public Task<Feature> Get(string featureName)
        {
            return _featureManagementDb.Features
                .Include(f => f.IntFeatureChoices)
                .Include(f => f.DecimalFeatureChoices)
                .Include(f => f.StringFeatureChoices)
                .SingleOrDefaultAsync(f => f.Name == featureName);
        }

        public async Task<T> GetValue<T>(string featureName)
        {
            var existingFeature = await Get(featureName);

            if (existingFeature == null)
            {
                throw new Exception($"The feature {featureName} does not exist...");
            }

            if (existingFeature.Type == FeatureTypes.Boolean)
            {
                if (existingFeature.BooleanValue.HasValue && typeof(T) == typeof(bool))
                {
                    return (T)(object)existingFeature.BooleanValue.Value;
                }
                else
                {
                    throw new Exception($"The feature {featureName} is a boolean feature...");
                }
            }

            if (existingFeature.Type == FeatureTypes.Integer)
            {
                if (existingFeature.IntValue.HasValue && typeof(T) == typeof(int))
                {
                    return (T)(object)existingFeature.IntValue.Value;
                }
                else
                {
                    throw new Exception($"The feature {featureName} is not an integer feature...");
                }
            }

            if (existingFeature.Type == FeatureTypes.Decimal)
            {
                if (existingFeature.DecimalValue.HasValue && typeof(T) == typeof(decimal))
                {
                    return (T)(object)existingFeature.DecimalValue.Value;
                }
                else
                {
                    throw new Exception($"The feature {featureName} is not a decimal feature...");
                }
            }

            if (existingFeature.Type == FeatureTypes.String)
            {
                if (!string.IsNullOrWhiteSpace(existingFeature.StringValue) && typeof(T) == typeof(string))
                {
                    return (T)(object)existingFeature.StringValue;
                }
                else
                {
                    throw new Exception($"The feature {featureName} is not a string feature...");
                }
            }

            throw new Exception("Only value of types bool, int, decimal and string are allowed...");
        }

        public async Task<Feature> SetValue<T>(string featureName, T value)
        {
            var existingFeature = await Get(featureName);

            if (existingFeature == null)
            {
                throw new Exception($"The feature {featureName} does not exist...");
            }

            if (existingFeature.Type == FeatureTypes.Boolean)
            {
                if (value is bool boolValue)
                {
                    existingFeature.BooleanValue = boolValue;
                }
                else
                {
                    throw new Exception($"The feature {featureName} is a boolean feature...");
                }
            }

            if (existingFeature.Type == FeatureTypes.Integer)
            {
                if (value is int intValue)
                {
                    if (existingFeature.IntFeatureChoices.Any(c => c.Choice == intValue))
                    {
                        existingFeature.IntValue = intValue;
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

            if (existingFeature.Type == FeatureTypes.Decimal)
            {
                if (value is decimal decimalValue)
                {
                    if (existingFeature.DecimalFeatureChoices.Any(c => c.Choice == decimalValue))
                    {
                        existingFeature.DecimalValue = decimalValue;
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

            if (existingFeature.Type == FeatureTypes.String)
            {
                if (value is string stringValue)
                {
                    if (existingFeature.StringFeatureChoices.Any(c => c.Choice == stringValue))
                    {
                        existingFeature.StringValue = stringValue;
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

            _onFeatureUpdated?.Invoke(existingFeature.ToOutput());

            return existingFeature;
        }
    }
}
