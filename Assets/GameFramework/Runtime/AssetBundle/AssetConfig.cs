using System.Collections.Generic;
namespace GameFramework
{
    public class AssetConfig
    {
        public Dictionary<string, string> Bundles { get; set; }
        public string AssetVersion { get; set; }
        public string AppVersion { get; set; }
        public string DateTime { get; set; }
    }
}