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
        /// Update the value of a boolean feature.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="value">The new value of the feature.</param>
        /// <returns></returns>
        Task<Feature> Set(string featureName, bool value);

        /// <summary>
        /// Update the value of an integer feature.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="value">The new value of the feature.</param>
        /// <returns></returns>
        Task<Feature> Set(string featureName, int value);

        /// <summary>
        /// Update the value of a decimal feature.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="value">The new value of the feature.</param>
        /// <returns></returns>
        Task<Feature> Set(string featureName, decimal value);

        /// <summary>
        /// Update the value of a string feature.
        /// </summary>
        /// <param name="featureName">The name of the feature.</param>
        /// <param name="value">The new value of the feature.</param>
        /// <returns></returns>
        Task<Feature> Set(string featureName, string value);
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

        public async Task<Feature> Set(string featureName, bool value)
        {
            var existingFeature = await Get(featureName);

            if (existingFeature == null)
            {
                throw new Exception($"The feature {featureName} does not exist...");
            }
            if (existingFeature.Type != FeatureTypes.Boolean)
            {
                throw new Exception($"The feature {featureName} is not a boolean feature...");
            }

            existingFeature.BooleanValue = value;

            await _featureManagementDb.SaveChangesAsync();

            return existingFeature;
        }

        public async Task<Feature> Set(string featureName, int value)
        {
            var existingFeature = await Get(featureName);

            if (existingFeature == null)
            {
                throw new Exception($"The feature {featureName} does not exist...");
            }
            if (existingFeature.Type != FeatureTypes.Integer)
            {
                throw new Exception($"The feature {featureName} is not an integer feature...");
            }

            // TODO : Check if value is inside Choices list (if possible)

            existingFeature.IntValue = value;

            await _featureManagementDb.SaveChangesAsync();

            return existingFeature;
        }

        public async Task<Feature> Set(string featureName, decimal value)
        {
            var existingFeature = await Get(featureName);

            if (existingFeature == null)
            {
                throw new Exception($"The feature {featureName} does not exist...");
            }
            if (existingFeature.Type != FeatureTypes.Decimal)
            {
                throw new Exception($"The feature {featureName} is not a decimal feature...");
            }

            // TODO : Check if value is inside Choices list (if possible)

            existingFeature.DecimalValue = value;

            await _featureManagementDb.SaveChangesAsync();

            return existingFeature;
        }

        public async Task<Feature> Set(string featureName, string value)
        {
            var existingFeature = await Get(featureName);

            if (existingFeature == null)
            {
                throw new Exception($"The feature {featureName} does not exist...");
            }
            if (existingFeature.Type != FeatureTypes.String)
            {
                throw new Exception($"The feature {featureName} is not a string feature...");
            }

            // TODO : Check if value is inside Choices list (if possible)

            existingFeature.StringValue = value;

            await _featureManagementDb.SaveChangesAsync();

            return existingFeature;
        }
    }
}
