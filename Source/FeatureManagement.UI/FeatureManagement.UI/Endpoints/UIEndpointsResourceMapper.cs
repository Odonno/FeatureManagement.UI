namespace FeatureManagement.UI.Core;

internal class UIEndpointsResourceMapper
{
    public static IEnumerable<IEndpointConventionBuilder> Map(IEndpointRouteBuilder builder, Options options)
    {
        var embeddedResourcesAssembly = typeof(UIResource).Assembly;
        var embeddedResources = embeddedResourcesAssembly.GetManifestResourceNames();

        var outputFolderStructure = new Folder
        {
            Folders = new List<Folder>
            {
                new Folder
                {
                    Name = "_app",
                    Folders = new List<Folder>
                    {
                        new Folder
                        {
                            Name = "assets",
                            Folders = new List<Folder>
                            {
                                new Folder
                                {
                                    Name = "pages"
                                }
                            }
                        },
                        new Folder
                        {
                            Name = "chunks"
                        },
                        new Folder
                        {
                            Name = "pages"
                        }
                    }
                }
            }
        };

        var flattenedFolders = ExtractFlattenedFolders(outputFolderStructure);

        var resources = ParseEmbeddedResources(embeddedResourcesAssembly, flattenedFolders, embeddedResources)
            .ReplaceBasePaths(options);

        var endpoints = new List<IEndpointConventionBuilder>();

        var ui = resources.GetMainUI();

        if (ui is null)
        {
            return endpoints;
        }

        foreach (var resource in resources)
        {
            if (resource.ContentType == ContentType.PNG)
            {
                endpoints.Add(builder.MapGet($"{options.ResourcesPath}/{resource.Folder}{resource.FileName}", async context =>
                {
                    context.Response.ContentType = resource.ContentType;

                    using var contentStream = embeddedResourcesAssembly.GetManifestResourceStream(resource.Name);
                    if (contentStream is not null)
                    {
                        await contentStream.CopyToAsync(context.Response.Body);
                    }
                }));

                continue;
            }

            endpoints.Add(builder.MapGet($"{options.ResourcesPath}/{resource.Folder}{resource.FileName}", async context =>
            {
                context.Response.ContentType = resource.ContentType;
                await context.Response.WriteAsync(resource.Content);
            }));
        }

        endpoints.Add(builder.MapGet($"{options.UIPath}", async context =>
        {
            context.Response.OnStarting(() =>
            {
                if (!context.Response.Headers.ContainsKey("Cache-Control"))
                {
                    context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                }

                return Task.CompletedTask;
            });

            context.Response.ContentType = ui.ContentType;
            await context.Response.WriteAsync(ui.Content);
        }));

        var styleSheets = ui.GetCustomStylesheets(options);

        foreach (var item in styleSheets)
        {
            endpoints.Add(builder.MapGet(item.ResourcePath, async context =>
            {
                context.Response.ContentType = "text/css";
                await context.Response.Body.WriteAsync(item.Content, 0, item.Content.Length);
            }));
        }

        return endpoints;
    }

    private static IEnumerable<FlattenedFolder> ExtractFlattenedFolders(Folder folder, int level = 1, string path = "")
    {
        var flattenedFolders = new List<FlattenedFolder>();

        if (folder.Folders is null)
        {
            return flattenedFolders;
        }

        foreach (var nestedFolder in folder.Folders)
        {
            string nestedFolderPath = $"{path}{nestedFolder.Name}/";

            flattenedFolders.Add(new FlattenedFolder
            {
                Level = level,
                Path = nestedFolderPath
            });

            flattenedFolders.AddRange(
                ExtractFlattenedFolders(nestedFolder, level + 1, nestedFolderPath)
            );
        }

        return flattenedFolders;
    }

    private static IEnumerable<UIResource> ParseEmbeddedResources(
        Assembly assembly,
        IEnumerable<FlattenedFolder> flattenedFolders,
        string[] embeddedFiles
    )
    {
        const char SPLIT_SEPARATOR = '.';
        string prefixEmbeddedResource = assembly.GetName().Name + ".ui.";

        var resourceList = new List<UIResource>();

        var orderedFlattenedFolders = flattenedFolders
            .OrderByDescending(f => f.Level)
            .ToList();

        foreach (var file in embeddedFiles)
        {
            string fileWithoutBaseFolderPrefix = file.Replace(prefixEmbeddedResource, "");

            var relativeFolder = orderedFlattenedFolders
                .FirstOrDefault(f =>
                {
                    return !string.IsNullOrWhiteSpace(f.Path) &&
                        fileWithoutBaseFolderPrefix.StartsWith(f.Path.Replace("/", ".").Replace("-", "_"));
                });

            string fileWithoutFolderPrefix = string.IsNullOrWhiteSpace(relativeFolder?.Path)
                ? fileWithoutBaseFolderPrefix
                : fileWithoutBaseFolderPrefix[relativeFolder.Path.Length..];

            var segments = fileWithoutFolderPrefix.Split(SPLIT_SEPARATOR);
            string extension = segments.Last();
            string fileName = fileWithoutFolderPrefix.Substring(0, fileWithoutFolderPrefix.Length - 1 - extension.Length);

            using var contentStream = assembly.GetManifestResourceStream(file);
            if (contentStream is not null)
            {
                using var reader = new StreamReader(contentStream);
                string result = reader.ReadToEnd();

                resourceList.Add(
                    new UIResource(
                        file,
                        relativeFolder?.Path,
                        $"{fileName}.{extension}",
                        result,
                        ContentType.FromExtension(extension)
                    )
                );
            }
        }

        return resourceList;
    }
}
