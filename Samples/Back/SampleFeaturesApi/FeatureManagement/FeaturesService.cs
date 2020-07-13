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
    }
}
