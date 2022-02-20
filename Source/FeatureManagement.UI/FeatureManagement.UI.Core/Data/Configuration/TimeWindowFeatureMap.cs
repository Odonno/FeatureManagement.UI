namespace FeatureManagement.UI.Core.Data.Configuration;

internal class TimeWindowFeatureMap : IEntityTypeConfiguration<TimeWindowFeature>
{
    private readonly string _dbSchema;

    public TimeWindowFeatureMap(string dbSchema)
    {
        _dbSchema = dbSchema;
    }

    public void Configure(EntityTypeBuilder<TimeWindowFeature> builder)
    {
        builder.ToTable(nameof(TimeWindowFeature), _dbSchema);

        builder.HasKey(gf => gf.Id);

        builder.HasIndex(gf => new { gf.FeatureId, gf.StartDate, gf.EndDate })
            .IsUnique();

        builder.Property(gf => gf.FeatureId)
            .IsRequired(true);
        builder.Property(gf => gf.StartDate)
            .IsRequired(false);
        builder.Property(gf => gf.EndDate)
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
            .WithMany(f => f.TimeWindowFeatures)
            .HasForeignKey(gf => gf.FeatureId)
            .IsRequired();
    }
}
