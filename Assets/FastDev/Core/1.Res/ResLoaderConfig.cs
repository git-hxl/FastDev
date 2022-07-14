using Newtonsoft.Json;
using System.Collections.Generic;
namespace FastDev
{
    public class ResLoaderConfig
    {
        [JsonIgnore]
        public const string fileName = "resconfig.json";
        public string resVersion;
        public string appVersion;
        public Dictionary<string,string> resDict = new Dictionary<string, string>();
    }
    
}