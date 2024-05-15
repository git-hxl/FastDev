using System;
using System.Collections.Generic;
namespace FastDev
{
    public class ResourceConfig
    {
        public Dictionary<string, string> Bundles { get; set; }
        public string AssetVersion { get; set; }
        public string AppVersion { get; set; }
        public string DateTime { get; set; }

        public ResourceConfig()
        {
            Bundles = new Dictionary<string, string>();
            AssetVersion = "";
            AppVersion = "";
            DateTime = System.DateTime.Now.ToString();
        }
    }
}