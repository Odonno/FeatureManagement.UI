namespace AspNetCore.FeatureManagement.UI.Core.Data.Configuration;

internal class ClientFeatureDataMap : IEntityTypeConfiguration<ClientFeatureData>
{
    private readonly string _dbSchema;

    public ClientFeatureDataMap(string dbSchema)
    {
        _dbSchema = dbSchema;
    }

    public void Configure(EntityTypeBuilder<ClientFeatureData> builder)
    {
        builder.ToTable(nameof(ClientFeatureData), _dbSchema);

        builder.HasKey(c => c.Id);

        builder.HasIndex(c => new { c.FeatureId, c.ClientId })
            .IsUnique();

        builder.Property(c => c.FeatureId)
            .IsRequired(true);
        builder.Property(c => c.ClientId)
            .HasMaxLength(100)
            .IsRequired(true);

        builder.Property(c => c.BooleanValue)
            .IsRequired(false);
        builder.Property(c => c.IntValue)
            .IsRequired(false);
        builder.Property(c => c.DecimalValue)
            .IsRequired(false);
        builder.Property(c => c.StringValue)
            .IsRequired(false)
            .HasColumnType("NVARCHAR(MAX)");

        builder.HasOne(c => c.Feature)
            .WithMany(f => f.Clients)
            .HasForeignKey(c => c.FeatureId)
            .IsRequired();
    }
}
