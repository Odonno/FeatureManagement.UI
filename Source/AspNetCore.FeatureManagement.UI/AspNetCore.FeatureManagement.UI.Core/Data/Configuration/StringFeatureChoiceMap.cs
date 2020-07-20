using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCore.FeatureManagement.UI.Core.Data.Configuration
{
    internal class StringFeatureChoiceMap : IEntityTypeConfiguration<StringFeatureChoice>
    {
        private readonly string _dbSchema;

        public StringFeatureChoiceMap(string dbSchema)
        {
            _dbSchema = dbSchema;
        }

        public void Configure(EntityTypeBuilder<StringFeatureChoice> builder)
        {
            builder.ToTable(nameof(StringFeatureChoice), _dbSchema);

            builder.HasKey(f => f.Id);

            builder.HasIndex(f => new { f.FeatureId, f.Choice })
                .IsUnique();

            builder.Property(f => f.FeatureId)
                .IsRequired(true);

            builder.Property(f => f.Choice)
                .IsRequired(true)
                .HasColumnType("NVARCHAR(450)");

            builder.HasOne(c => c.Feature)
                .WithMany(f => f.StringFeatureChoices)
                .HasForeignKey(c => c.FeatureId)
                .IsRequired();
        }
    }
}
