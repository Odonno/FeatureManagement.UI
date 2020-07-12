using Microsoft.EntityFrameworkCore;
using SampleFeaturesApi.FeatureManagement.Data.Configuration;

namespace SampleFeaturesApi.FeatureManagement.Data
{
    public class FeatureManagementDb : DbContext
    {
        public DbSet<Feature> Features { get; set; }

        public FeatureManagementDb(DbContextOptions<FeatureManagementDb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string schemaDb = "FeatureManagement";

            modelBuilder.ApplyConfiguration(new FeatureMap(schemaDb));
        }
    }
}
