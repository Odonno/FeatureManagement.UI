using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCore.FeatureManagement.UI.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Endpoints.Extensions;
using AspNetCore.FeatureManagement.UI.Core.Endpoints.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AspNetCore.FeatureManagement.UI.Core
{
    internal class UIEndpointsResourceMapper
    {
        public IEnumerable<IEndpointConventionBuilder> Map(IEndpointRouteBuilder builder, Options options)
        {
            var embeddedResourcesAssembly = typeof(UIResource).Assembly;
            var embeddedResources = embeddedResourcesAssembly.GetManifestResourceNames();

            var outputFolderStructure = new Folder
            {
                Folders = new List<Folder>
                {
                    new Folder
                    {
                        Name = "assets",
                        Folders = new List<Folder>
                        {
                            new Folder
                            {
                                Name = "icons"
                            }
                        }
                    },
                    new Folder
                    {
                        Name = "route-home"
                    },
                    new Folder
                    {
                        Name = "route-notfound"
                    }
                }
            };

            var resources = ParseEmbeddedResources(embeddedResourcesAssembly, outputFolderStructure, embeddedResources);

            var endpoints = new List<IEndpointConventionBuilder>();

            foreach (var resource in resources)
            {
                endpoints.Add(builder.MapGet($"{options.ResourcesPath}/{resource.Folder}{resource.FileName}", async context =>
                {
                    context.Response.ContentType = resource.ContentType;
                    await context.Response.WriteAsync(resource.Content);
                }));
            }

            var ui = resources.GetMainUI(options);

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

        private IEnumerable<UIResource> ParseEmbeddedResources(
            Assembly assembly,
            Folder outputFolderStructure,
            string[] embeddedFiles
        )
        {
            const char SPLIT_SEPARATOR = '.';
            string prefixEmbeddedResource = assembly.GetName().Name + ".ui.";

            var resourceList = new List<UIResource>();

            var flattenedFolders = new List<FlattenedFolder>();

            foreach (var f1 in outputFolderStructure.Folders)
            {
                if (f1.Folders != null)
                {
                    foreach (var f2 in f1.Folders)
                    {
                        flattenedFolders.Add(new FlattenedFolder
                        {
                            Level = 2,
                            Path = $"{f1.Name}/{f2.Name}/"
                        });
                    }
                }

                flattenedFolders.Add(new FlattenedFolder
                {
                    Level = 1,
                    Path = $"{f1.Name}/"
                });
            }

            var orderedFlattenedFolders = flattenedFolders
                .OrderByDescending(f => f.Level)
                .ToList();

            foreach (var file in embeddedFiles)
            {
                string fileWithoutBaseFolderPrefix = file.Replace(prefixEmbeddedResource, "");

                var relativeFolder = orderedFlattenedFolders
                    .FirstOrDefault(f =>
                    {
                        return fileWithoutBaseFolderPrefix.StartsWith(f.Path.Replace("/", ".").Replace("-", "_"));
                    });

                string fileWithoutFolderPrefix = relativeFolder == null
                    ? fileWithoutBaseFolderPrefix
                    : fileWithoutBaseFolderPrefix.Substring(relativeFolder.Path.Length);

                var segments = fileWithoutFolderPrefix.Split(SPLIT_SEPARATOR);
                string extension = segments.Last();
                string fileName = fileWithoutFolderPrefix.Substring(0, fileWithoutFolderPrefix.Length - 1 - extension.Length);

                using (var contentStream = assembly.GetManifestResourceStream(file))
                using (var reader = new StreamReader(contentStream))
                {
                    string result = reader.ReadToEnd();

                    resourceList.Add(
                        new UIResource(
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
}