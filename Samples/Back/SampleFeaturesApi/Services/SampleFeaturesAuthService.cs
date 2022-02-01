namespace SampleFeaturesApi.Services;

public class SampleFeaturesAuthService : IFeaturesAuthService
{
    private static readonly string _uniqueId = Guid.NewGuid().ToString();

    private readonly IHttpContextAccessor _httpContextAccessor;

    public SampleFeaturesAuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetClientId()
    {
        return _uniqueId;
    }

    public IEnumerable<string> GetClientGroups(string? clientId)
    {
        return new List<string> { "Ring1" };
    }

    public bool HandleReadAuth(Feature feature, string? clientId)
    {
        return true;
    }

    public bool HandleWriteAuth(Feature feature, string? clientId)
    {
        if (feature.Type == FeatureTypes.Client)
        {
            return true;
        }

        _httpContextAccessor.HttpContext.Request.Query.TryGetValue("Username", out var username);
        return username == "Odonno";
    }
}
