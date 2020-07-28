namespace AspNetCore.FeatureManagement.UI.Core.Models
{
    public interface IAuthenticationScheme
    {
        string Type { get; }
    }

    public class NoAuthenticationScheme : IAuthenticationScheme
    {
        public string Type { get; } = "None";
    }

    public class QueryAuthenticationScheme : IAuthenticationScheme
    {
        public string Type { get; } = "Query";
        public string? Key { get; set; }
    }

    public class HeaderAuthenticationScheme : IAuthenticationScheme
    {
        public string Type { get; } = "Header";
        public string? Key { get; set; }
    }
}
