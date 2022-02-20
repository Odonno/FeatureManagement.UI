namespace FeatureManagement.UI.Core.Configuration;

public class Options
{
    internal ICollection<string> CustomStylesheets { get; } = new List<string>();
    public string UIPath { get; set; } = "/features-ui";
    public string ApiPath { get; set; } = "/features";
    public bool UseRelativeApiPath = true;
    public string ResourcesPath { get; set; } = "/features-ui";
    public bool UseRelativeResourcesPath = true;
}