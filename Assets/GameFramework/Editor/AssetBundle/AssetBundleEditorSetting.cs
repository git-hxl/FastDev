using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameFramework.Editor
{
    public class AssetBundleEditorSetting
    {
        public static string SettingPath { get; } = "./AssetBundleEditorSetting.json";
        //Ab包打包保存目录
        public string BuildPath { get; set; }
        //打包平台
        public BuildTarget Platform { get; set; }
        //压缩方式
        public AssetBundleEditor.CompressionType CompressionType { get; set; }
        //需要打包的bundles
        public List<string> SelectBundles { get; set; } = new List<string>();
        public string AssetVersion { get; set; }
        public string AppVersion { get; set; }
        public AssetBundleEditorSetting()
        {
            BuildPath = Application.streamingAssetsPath;
            Platform = BuildTarget.StandaloneWindows;
            CompressionType = AssetBundleEditor.CompressionType.LZ4;
            AssetVersion = "1.0.0";
            AppVersion = "1.0.0";
        }
    }
}