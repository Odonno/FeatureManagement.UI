using AspNetCore.FeatureManagement.UI.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        /// Update the value of a feature.
        /// </summary>
        /// <typeparam name="T">Value type of the feature. Only <see cref="bool"/>, <see cref="int"/>, <see cref="decimal"/> and <see cref="string"/> are allowed.</typeparam>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="value">The new value of the feature.</param>
        /// <returns>The updated feature.</returns>
        Task<Feature> SetValue<T>(string featureName, T value);
    }

    public class FeaturesService : IFeaturesService
    {
        private readonly FeatureManagementDb _featureManagementDb;

        public FeaturesService(FeatureManagementDb featureManagementDb)
        {
            _featureManagementDb = featureManagementDb;
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

        public async Task<Feature> SetValue<T>(string featureName, T value)
        {
            var existingFeature = await Get(featureName);

            if (existingFeature == null)
            {
                throw new Exception($"The feature {featureName} does not exist...");
            }

            if (existingFeature.Type == FeatureTypes.Boolean && value is bool boolValue)
            {
                existingFeature.BooleanValue = boolValue;
            }
            else
            {
                throw new Exception($"The feature {featureName} is not a boolean feature...");
            }

            if (existingFeature.Type == FeatureTypes.Integer && value is int intValue)
            {
                // TODO : Check if value is inside Choices list (if possible)
                existingFeature.IntValue = intValue;
            }
            else
            {
                throw new Exception($"The feature {featureName} is not an integer feature...");
            }

            if (existingFeature.Type == FeatureTypes.Decimal && value is decimal decimalValue)
            {
                // TODO : Check if value is inside Choices list (if possible)
                existingFeature.DecimalValue = decimalValue;
            }
            else
            {
                throw new Exception($"The feature {featureName} is not a decimal feature...");
            }

            if (existingFeature.Type == FeatureTypes.String && value is string stringValue)
            {
                // TODO : Check if value is inside Choices list (if possible)
                existingFeature.StringValue = stringValue;
            }
            else
            {
                throw new Exception($"The feature {featureName} is not a string feature...");
            }

            await _featureManagementDb.SaveChangesAsync();

            return existingFeature;
        }
    }
}
