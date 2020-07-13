using Microsoft.EntityFrameworkCore;
using SampleFeaturesApi.FeatureManagement.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleFeaturesApi.FeatureManagement
{
    public interface IFeaturesService
    {
        Task<List<Feature>> GetAll();
        Task<bool> IsEnabled(string featureName);
        Task<Feature> Set(string featureName, bool value);
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
            return _featureManagementDb.Features.ToListAsync();
        }

        public Task<bool> IsEnabled(string featureName)
        {
            return _featureManagementDb.Features
                .AnyAsync(f => f.Name == featureName && f.Enabled);
        }

        public async Task<Feature> Set(string featureName, bool value)
        {
            var existingFeature = await _featureManagementDb.Features
                .SingleOrDefaultAsync(f => f.Name == featureName);

            existingFeature.Enabled = value;

            await _featureManagementDb.SaveChangesAsync();

            return existingFeature;
        }
    }
}
