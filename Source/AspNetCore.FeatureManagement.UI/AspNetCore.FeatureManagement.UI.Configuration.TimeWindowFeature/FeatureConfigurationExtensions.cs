namespace AspNetCore.FeatureManagement.UI.Configuration.TimeWindowFeature;

internal static class FeatureConfigurationExtensions
{
    internal static void EnsuresCorrectConfiguration<T>(ITimeWindowFeatureConfiguration<T> configuration)
    {
        if (configuration?.TimeWindows == null)
        {
            throw new Exception("The configuration cannot be null.");
        }

        bool hasNullConfiguration = configuration.TimeWindows
            .Any(g => g.StartDate == null && g.EndDate == null);
        if (hasNullConfiguration)
        {
            throw new Exception("A time window name cannot be null. At least start date or end date should be defined.");
        }

        bool hasDateOverlappingConfiguration = configuration.TimeWindows
            .Any(g => g.StartDate.HasValue && g.EndDate.HasValue && g.StartDate > g.EndDate);
        if (hasDateOverlappingConfiguration)
        {
            throw new Exception("A start date should always be anterior to the end date...");
        }
    }
}
