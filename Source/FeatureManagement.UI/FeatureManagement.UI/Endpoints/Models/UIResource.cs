namespace FeatureManagement.UI.Core.Endpoints.Models;

internal class UIResource
{
    public string Name { get; }
    public string? Folder { get; }
    public string Content { get; internal set; }
    public string ContentType { get; }
    public string FileName { get; }

    public UIResource(string name, string? folder, string fileName, string content, string contentType)
    {
        Name = name;
        Folder = folder;
        Content = content;
        ContentType = contentType;
        FileName = fileName;
    }
}