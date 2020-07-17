using System;
using System.Collections.Generic;
using System.IO;

namespace AspNetCore.FeatureManagement.UI.Configuration
{
    public class Options
    {
        internal ICollection<string> CustomStylesheets { get; } = new List<string>();
        public string UIPath { get; set; } = "/features-ui";
        public string ApiPath { get; set; } = "/features";
        public bool UseRelativeApiPath = true;
        public bool UseRelativeWebhookPath = true;
        public string ResourcesPath { get; set; } = "/features-ui";
        public bool UseRelativeResourcesPath = true;

        public Options AddCustomStylesheet(string path)
        {
            string stylesheetPath = path;

            if (!Path.IsPathFullyQualified(stylesheetPath))
            {
                stylesheetPath = Path.Combine(Environment.CurrentDirectory, path);
            }

            if (!File.Exists(stylesheetPath))
            {
                throw new Exception($"Could not find style sheet at path {stylesheetPath}");
            }

            CustomStylesheets.Add(stylesheetPath);

            return this;
        }
    }
}