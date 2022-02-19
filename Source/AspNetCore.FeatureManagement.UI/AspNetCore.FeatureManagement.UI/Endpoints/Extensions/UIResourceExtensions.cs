using AspNetCore.FeatureManagement.UI.Core.Configuration;
using AspNetCore.FeatureManagement.UI.Core.Endpoints.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCore.FeatureManagement.UI.Core.Endpoints.Extensions
{
    internal static class UIResourceExtensions
    {
        public static IEnumerable<UIResource> ReplaceBasePaths(this IEnumerable<UIResource> resources, Options options)
        {
            var apiPath = options.UseRelativeApiPath
              ? options.ApiPath.AsRelativeResource()
              : options.ApiPath;

            var resourcePath = options.UseRelativeResourcesPath
                ? options.ResourcesPath.AsRelativeResource()
                : options.ResourcesPath;

            return resources
                .Select(r =>
                {
                    r.Content = r.Content
                        .Replace("#apiPath#", apiPath)
                        .Replace("#uiResourcePath#", resourcePath);
                    return r;
                })
                .ToList();
        }

        public static UIResource? GetMainUI(this IEnumerable<UIResource> resources)
        {
            return resources
                .FirstOrDefault(r => r.ContentType == ContentType.HTML && r.FileName == "index.html");
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
}