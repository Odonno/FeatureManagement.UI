namespace FeatureManagement.UI.Core.Endpoints.Models;

internal class Folder
{
    public string? Name { get; set; }
    public List<Folder>? Folders { get; set; }
}
