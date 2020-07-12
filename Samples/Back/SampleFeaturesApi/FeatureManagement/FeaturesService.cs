using SampleFeaturesApi.FeatureManagement.Data;
using System.Collections.Generic;
using System.Linq;

namespace SampleFeaturesApi.FeatureManagement
{
    public interface IFeaturesService
    {
        IEnumerable<Feature> GetAll();
        bool IsEnabled(string featureName);
    }

    public class FeaturesService : IFeaturesService
    {
        private readonly FeatureManagementDb _featureManagementDb;

        public FeaturesService(FeatureManagementDb featureManagementDb)
        {
            _featureManagementDb = featureManagementDb;
        }

        public IEnumerable<Feature> GetAll()
        {
            return _featureManagementDb.Features;
        }

        public bool IsEnabled(string featureName)
        {
            return _featureManagementDb.Features
                .Any(f => f.Name == featureName && f.Enabled);
        }
    }
}
