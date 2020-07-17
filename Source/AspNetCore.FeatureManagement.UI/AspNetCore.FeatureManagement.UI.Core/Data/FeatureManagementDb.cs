using Microsoft.EntityFrameworkCore;
using AspNetCore.FeatureManagement.UI.Core.Data.Configuration;

namespace AspNetCore.FeatureManagement.UI.Core.Data
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
