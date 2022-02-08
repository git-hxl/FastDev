using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace FastDev
{
    public class AssetBundleEditorAttribute
    {
        public static string serializePath = "./abEditorAttr.txt";
        //Ab包保存目录
        public string saveDir;
        //打包平台
        public BuildTarget platform;
        //打包方式
        public BuildAssetBundleOptions option;
        //需要打包的bundles
        public List<string> bundles = new List<string>();
        public string resVersion;
        public string appVersion;
        public AssetBundleEditorAttribute()
        {
            saveDir = Application.streamingAssetsPath;
            platform = BuildTarget.StandaloneWindows;
            option = BuildAssetBundleOptions.ChunkBasedCompression;
            resVersion = "1.0.0";
            appVersion = "1.0.0";
        }
    }
}