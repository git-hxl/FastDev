using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class AssetBundleConfig
    {
        public Dictionary<string, string> Bundles { get; set; }
        public string AssetVersion { get; set; }
        public string AppVersion { get; set; }

        public string DateTime { get; set; }
    }
}