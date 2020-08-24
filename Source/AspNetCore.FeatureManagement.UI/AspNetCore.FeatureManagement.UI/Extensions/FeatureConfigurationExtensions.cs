using AspNetCore.FeatureManagement.UI.Configuration;
using System;
using System.Linq;

namespace AspNetCore.FeatureManagement.UI.Extensions
{
    internal static class FeatureConfigurationExtensions
    {
        internal static void EnsuresCorrectConfiguration<T>(IGroupFeatureConfiguration<T> configuration)
        {
            if (configuration?.Groups == null)
            {
                throw new Exception("The configuration cannot be null.");
            }

            bool hasNullGroup = configuration.Groups.Any(g => g.Group == null);
            if (hasNullGroup)
            {
                throw new Exception("A group name cannot be null.");
            }
        }
    }
}
