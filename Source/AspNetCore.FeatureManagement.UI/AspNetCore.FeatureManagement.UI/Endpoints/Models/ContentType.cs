namespace AspNetCore.FeatureManagement.UI.Core.Endpoints.Models;

internal class ContentType
{
    public const string JAVASCRIPT = "text/javascript";
    public const string CSS = "text/css";
    public const string HTML = "text/html";
    public const string PNG = "image/png";
    public const string PLAIN = "text/plain";

    public static Dictionary<string, string> supportedContent = new(StringComparer.InvariantCultureIgnoreCase)
    {
        { "js", JAVASCRIPT },
        { "html", HTML },
        { "css", CSS },
        { "png", PNG }
    };

    public static string FromExtension(string fileExtension)
        => supportedContent.TryGetValue(fileExtension, out var result) ? result : PLAIN;
}
