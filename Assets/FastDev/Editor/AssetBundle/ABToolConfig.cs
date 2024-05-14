using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FastDev.Editor
{
    public class ABToolConfig
    {
        public static string ConfigPath { get; } = "./ABToolConfig.json";
        //Ab包打包保存目录
        public string BuildOutPath { get; set; }
        //打包平台
        public BuildTarget Platform { get; set; }
        //压缩方式
        public ABTool.CompressionType CompressionType { get; set; }
        //需要打包的bundles
        public List<string> SelectBundles { get; set; } = new List<string>();
        public string AssetVersion { get; set; }
        public string AppVersion { get; set; }
        public ABToolConfig()
        {
            BuildOutPath = Application.streamingAssetsPath;
            Platform = BuildTarget.StandaloneWindows;
            CompressionType = ABTool.CompressionType.LZ4;
            AssetVersion = "1.0.0";
            AppVersion = "1.0.0";
        }
    }
}