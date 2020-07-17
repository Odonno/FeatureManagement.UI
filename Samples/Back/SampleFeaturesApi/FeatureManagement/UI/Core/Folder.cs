using System.Collections.Generic;

namespace AspNetCore.FeatureManagement.UI.Core
{
    internal class Folder
    {
        public string Name { get; set; }
        public List<Folder> Folders { get; set; }
    }
}