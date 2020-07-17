namespace AspNetCore.FeatureManagement.UI.Core.Endpoints.Models
{
    internal class UIResource
    {
        public string Folder { get; }
        public string Content { get; internal set; }
        public string ContentType { get; }
        public string FileName { get; }

        public UIResource(string folder, string fileName, string content, string contentType)
        {
            Folder = folder;
            Content = content;
            ContentType = contentType;
            FileName = fileName;
        }
    }
}