using Microsoft.EntityFrameworkCore;
using AspNetCore.FeatureManagement.UI.Core.Data.Configuration;

namespace AspNetCore.FeatureManagement.UI.Core.Data
{
    public class FeatureManagementDb : DbContext
    {
        public DbSet<Feature> Features { get; set; }
        public DbSet<ServerFeatureData> ServerFeatureDatas { get; set; }
        public DbSet<ClientFeatureData> ClientFeatureDatas { get; set; }
        public DbSet<IntFeatureChoice> IntFeatureChoices { get; set; }
        public DbSet<DecimalFeatureChoice> DecimalFeatureChoices { get; set; }
        public DbSet<StringFeatureChoice> StringFeatureChoices { get; set; }
        public DbSet<GroupFeature> GroupFeatures { get; set; }
        public DbSet<TimeWindowFeature> TimeWindowFeatures { get; set; }

        public FeatureManagementDb(DbContextOptions<FeatureManagementDb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string schemaDb = "FeatureManagement";

            modelBuilder.ApplyConfiguration(new FeatureMap(schemaDb));
            modelBuilder.ApplyConfiguration(new ServerFeatureDataMap(schemaDb));
            modelBuilder.ApplyConfiguration(new ClientFeatureDataMap(schemaDb));
            modelBuilder.ApplyConfiguration(new IntFeatureChoiceMap(schemaDb));
            modelBuilder.ApplyConfiguration(new DecimalFeatureChoiceMap(schemaDb));
            modelBuilder.ApplyConfiguration(new StringFeatureChoiceMap(schemaDb));
            modelBuilder.ApplyConfiguration(new GroupFeatureMap(schemaDb));
            modelBuilder.ApplyConfiguration(new TimeWindowFeatureMap(schemaDb));
        }
    }
}
