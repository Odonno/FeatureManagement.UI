using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCore.FeatureManagement.UI.Core.Data.Configuration
{
    internal class ServerFeatureDataMap : IEntityTypeConfiguration<ServerFeatureData>
    {
        private readonly string _dbSchema;

        public ServerFeatureDataMap(string dbSchema)
        {
            _dbSchema = dbSchema;
        }

        public void Configure(EntityTypeBuilder<ServerFeatureData> builder)
        {
            builder.ToTable(nameof(ServerFeatureData), _dbSchema);

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.FeatureId)
                .IsUnique();

            builder.Property(s => s.FeatureId)
                .IsRequired(true);

            builder.Property(s => s.BooleanValue)
                .IsRequired(false);
            builder.Property(s => s.IntValue)
                .IsRequired(false);
            builder.Property(s => s.DecimalValue)
                .IsRequired(false);
            builder.Property(s => s.StringValue)
                .IsRequired(false)
                .HasColumnType("NVARCHAR(MAX)");

            builder.HasOne(s => s.Feature)
                .WithOne(f => f.Server!)
                .HasForeignKey<ServerFeatureData>(s => s.FeatureId)
                .IsRequired();
        }
    }
}
