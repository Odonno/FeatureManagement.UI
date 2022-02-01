using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCore.FeatureManagement.UI.Core.Data.Configuration
{
    internal class GroupFeatureMap : IEntityTypeConfiguration<GroupFeature>
    {
        private readonly string _dbSchema;

        public GroupFeatureMap(string dbSchema)
        {
            _dbSchema = dbSchema;
        }

        public void Configure(EntityTypeBuilder<GroupFeature> builder)
        {
            builder.ToTable(nameof(GroupFeature), _dbSchema);

            builder.HasKey(gf => gf.Id);

            builder.HasIndex(gf => new { gf.FeatureId, gf.Group })
                .IsUnique();

            builder.Property(gf => gf.FeatureId)
                .IsRequired(true);
            builder.Property(gf => gf.Group)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(gf => gf.BooleanValue)
                .IsRequired(false);
            builder.Property(gf => gf.IntValue)
                .IsRequired(false);
            builder.Property(gf => gf.DecimalValue)
                .IsRequired(false);
            builder.Property(gf => gf.StringValue)
                .IsRequired(false)
                .HasColumnType("NVARCHAR(MAX)");

            builder.HasOne(gf => gf.Feature)
                .WithMany(f => f.GroupFeatures)
                .HasForeignKey(gf => gf.FeatureId)
                .IsRequired();
        }
    }
}
