using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCore.FeatureManagement.UI.Core.Data.Configuration
{
    internal class IntFeatureChoiceMap : IEntityTypeConfiguration<IntFeatureChoice>
    {
        private readonly string _dbSchema;

        public IntFeatureChoiceMap(string dbSchema)
        {
            _dbSchema = dbSchema;
        }

        public void Configure(EntityTypeBuilder<IntFeatureChoice> builder)
        {
            builder.ToTable(nameof(IntFeatureChoice), _dbSchema);

            builder.HasKey(f => f.Id);

            builder.HasIndex(f => new { f.FeatureId, f.Choice })
                .IsUnique();

            builder.Property(f => f.FeatureId)
                .IsRequired(true);

            builder.Property(f => f.Choice)
                .IsRequired(true);

            builder.HasOne(c => c.Feature)
                .WithMany(f => f.IntFeatureChoices)
                .HasForeignKey(c => c.FeatureId)
                .IsRequired();
        }
    }
}
