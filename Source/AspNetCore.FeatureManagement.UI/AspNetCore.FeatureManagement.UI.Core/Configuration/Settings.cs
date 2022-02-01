namespace AspNetCore.FeatureManagement.UI.Core.Configuration;

public class Settings
{
    internal IServiceCollection? Services { get; }
    internal List<IFeatureSettings> Features { get; } = new List<IFeatureSettings>();

    /// <summary>
    /// Happens when a server feature is updated
    /// </summary>
    public Action<IFeature>? OnServerFeatureUpdated { get; set; }

    /// <summary>
    /// Happens when a client feature is updated
    /// </summary>
    public Action<IFeature, string>? OnClientFeatureUpdated { get; set; }

    /// <summary>
    /// List of authentication schemes applied in Features authentication (to be used in the UI)
    /// </summary>
    public List<IAuthenticationScheme> AuthSchemes { get; internal set; } = new List<IAuthenticationScheme>();

    public Settings() { }
    public Settings(IServiceCollection services)
    {
        Services = services;
    }
}
