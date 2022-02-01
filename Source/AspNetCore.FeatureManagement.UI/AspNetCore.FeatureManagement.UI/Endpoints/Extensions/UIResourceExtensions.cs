namespace AspNetCore.FeatureManagement.UI.Core.Endpoints.Extensions;

internal static class UIResourceExtensions
{
    public static UIResource GetMainUI(this IEnumerable<UIResource> resources, Options options)
    {
        var resource = resources
            .FirstOrDefault(r => r.ContentType == ContentType.HTML && r.FileName == "index.html");

        var apiPath = options.UseRelativeApiPath
            ? options.ApiPath.AsRelativeResource()
            : options.ApiPath;

        resource.Content = resource.Content
            .Replace("#apiPath#", apiPath);

        var resourcePath = options.UseRelativeResourcesPath
            ? options.ResourcesPath.AsRelativeResource()
            : options.ResourcesPath;

        resource.Content = resource.Content
            .Replace("#uiResourcePath#", resourcePath);

        return resource;
    }

    public static ICollection<UIStylesheet> GetCustomStylesheets(this UIResource resource, Options options)
    {
        List<UIStylesheet> styleSheets = new List<UIStylesheet>();

        if (!options.CustomStylesheets.Any())
        {
            resource.Content = resource.Content.Replace("#customstylesheets#", string.Empty);
            return styleSheets;
        }

        foreach (var stylesheet in options.CustomStylesheets)
        {
            styleSheets.Add(UIStylesheet.Create(options, stylesheet));
        }

        var htmlStyles = styleSheets.Select
            (s =>
        {
            var linkHref = options.UseRelativeResourcesPath ? s.ResourcePath.AsRelativeResource() : s.ResourcePath;
            return $"<link rel='stylesheet' href='{linkHref}'/>";
        });

        resource.Content = resource.Content.Replace("#customstylesheets#",
            string.Join("\n", htmlStyles));

        return styleSheets;
    }
}
