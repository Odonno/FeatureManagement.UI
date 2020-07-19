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

            builder.HasKey(f => f.Name);

            builder.Property(f => f.Name)
                .IsRequired(true)
                .HasMaxLength(150);

            builder.Property(f => f.Description)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(f => f.Type)
                .IsRequired(true)
                .HasMaxLength(10);

            builder.Property(f => f.BooleanValue)
                .IsRequired(false);
            builder.Property(f => f.IntValue)
                .IsRequired(false);
            builder.Property(f => f.DecimalValue)
                .IsRequired(false);
            builder.Property(f => f.StringValue)
                .IsRequired(false)
                .HasColumnType("TEXT");
        }
    }
}
