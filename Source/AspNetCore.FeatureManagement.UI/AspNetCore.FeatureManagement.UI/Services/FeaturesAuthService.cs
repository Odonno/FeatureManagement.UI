using AspNetCore.FeatureManagement.UI.Core.Data;

namespace AspNetCore.FeatureManagement.UI.Services
{
    public interface IFeaturesAuthService
    {
        /// <summary>
        /// Retrieve client id used to identify the user of each client features.
        /// </summary>
        /// <returns></returns>
        string? GetClientId();

        /// <summary>
        /// Indicates if the user can see this feature.
        /// </summary>
        /// <param name="feature">The feature user wants to read.</param>
        /// <param name="clientId">The client id of the user.</param>
        /// <returns></returns>
        bool HandleReadAuth(Feature feature, string? clientId);

        /// <summary>
        /// Indicates if the user can update this feature (server features are often only managed by an admin).
        /// </summary>
        /// <param name="feature">The feature user wants to update.</param>
        /// <param name="clientId">The client id of the user.</param>
        /// <returns></returns>
        bool HandleWriteAuth(Feature feature, string? clientId);
    }

    public sealed class DefaultFeaturesAuthService : IFeaturesAuthService
    {
        public string? GetClientId()
        {
            return null;
        }

        public bool HandleReadAuth(Feature feature, string? clientId)
        {
            return true;
        }

        public bool HandleWriteAuth(Feature feature, string? clientId)
        {
            return feature.Type == FeatureTypes.Client;
        }
    }
}
