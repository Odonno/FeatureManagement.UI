using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCore.FeatureManagement.UI.Core.Data.Configuration
{
    internal class DecimalFeatureChoiceMap : IEntityTypeConfiguration<DecimalFeatureChoice>
    {
        private readonly string _dbSchema;

        public DecimalFeatureChoiceMap(string dbSchema)
        {
            _dbSchema = dbSchema;
        }

        public void Configure(EntityTypeBuilder<DecimalFeatureChoice> builder)
        {
            builder.ToTable(nameof(DecimalFeatureChoice), _dbSchema);

            builder.HasKey(f => f.Id);

            builder.HasIndex(f => new { f.FeatureId, f.Choice })
                .IsUnique();

            builder.Property(f => f.FeatureId)
                .IsRequired(true);

            builder.Property(f => f.Choice)
                .IsRequired(true);

            builder.HasOne(c => c.Feature)
                .WithMany(f => f.DecimalFeatureChoices)
                .HasForeignKey(c => c.FeatureId)
                .IsRequired();
        }
    }
}
