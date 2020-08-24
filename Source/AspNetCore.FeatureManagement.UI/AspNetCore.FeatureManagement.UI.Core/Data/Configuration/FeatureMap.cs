using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCore.FeatureManagement.UI.Core.Data.Configuration
{
    internal class FeatureMap : IEntityTypeConfiguration<Feature>
    {
        private readonly string _dbSchema;

        public FeatureMap(string dbSchema)
        {
            _dbSchema = dbSchema;
        }

        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.ToTable(nameof(Feature), _dbSchema);

            builder.HasKey(f => f.Id);

            builder.HasIndex(f => f.Name)
                .IsUnique();

            builder.Property(f => f.Name)
                .IsRequired(true)
                .HasMaxLength(150);

            builder.Property(f => f.Description)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(f => f.ValueType)
                .IsRequired(true)
                .HasMaxLength(10);

            builder.Property(f => f.ConfigurationType)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(f => f.UiPrefix)
                .IsRequired(false)
                .HasMaxLength(20);

            builder.Property(f => f.UiSuffix)
                .IsRequired(false)
                .HasMaxLength(20);
        }
    }
}
