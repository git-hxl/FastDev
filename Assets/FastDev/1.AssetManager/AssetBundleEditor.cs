#if UNITY_EDITOR
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FastDev
{
    public class AssetBundleEditor : EditorWindow
    {
        public enum CompressionType
        {
            LZMA = BuildAssetBundleOptions.None,
            UncompressedAssetBundle = BuildAssetBundleOptions.UncompressedAssetBundle,
            LZ4 = BuildAssetBundleOptions.ChunkBasedCompression
        }

        private AssetBundleEditorSetting assetBundleEditorSetting;

        [MenuItem("FastDev/AssetBundle工具")]
        public static void OpenWindow()
        {
            AssetBundleEditor window = (AssetBundleEditor)EditorWindow.GetWindow(typeof(AssetBundleEditor), false, "AssetBundle");
            window.Show();
        }

        private void Awake()
        {
            InitWindowSetting();
        }

        private void InitWindowSetting()
        {
            assetBundleEditorSetting = new AssetBundleEditorSetting();
            if (File.Exists(AssetBundleEditorSetting.SettingPath))
            {
                string jsonTxt = File.ReadAllText(AssetBundleEditorSetting.SettingPath);
                assetBundleEditorSetting = JsonConvert.DeserializeObject<AssetBundleEditorSetting>(jsonTxt);
            }
            AssetDatabase.RemoveUnusedAssetBundleNames();
        }

        private void OnGUI()
        {
            GUISelectBuildFolder();
            GUIBuildSetting();
            GUISelectBundles();
            GUIBuild();
        }

        private void GUISelectBuildFolder()
        {
            GUILayout.Label("打包路径");
            assetBundleEditorSetting.BuildPath = GUILayout.TextField(assetBundleEditorSetting.BuildPath);
            if (GUILayout.Button("选择文件夹"))
            {
                string selectPath = EditorUtility.OpenFolderPanel("打包目录", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selectPath))
                {
                    assetBundleEditorSetting.BuildPath = selectPath;
                }
            }
        }

        private void GUIBuildSetting()
        {
            GUILayout.Label("打包平台");
            assetBundleEditorSetting.Platform = (BuildTarget)EditorGUILayout.EnumPopup(assetBundleEditorSetting.Platform);

            GUILayout.Label("压缩方式");
            assetBundleEditorSetting.CompressionType = (CompressionType)EditorGUILayout.EnumPopup(assetBundleEditorSetting.CompressionType);

            GUILayout.Label("资源版本");
            assetBundleEditorSetting.AssetVersion = GUILayout.TextField(assetBundleEditorSetting.AssetVersion);
            GUILayout.Label("App版本");
            assetBundleEditorSetting.AppVersion = GUILayout.TextField(assetBundleEditorSetting.AppVersion);
        }


        private void GUISelectBundles()
        {
            foreach (var item in AssetDatabase.GetAllAssetBundleNames())
            {
                bool value = assetBundleEditorSetting.SelectBundles.Contains(item);
                if (GUILayout.Toggle(value, item))
                {
                    if (!assetBundleEditorSetting.SelectBundles.Contains(item))
                    {
                        assetBundleEditorSetting.SelectBundles.Add(item);
                    }
                }
                else if (assetBundleEditorSetting.SelectBundles.Contains(item))
                {
                    assetBundleEditorSetting.SelectBundles.Remove(item);
                }
            }
        }

        private void GUIBuild()
        {
            if (GUILayout.Button("打包"))
            {
                AssetBundleBuild[] builds = new AssetBundleBuild[assetBundleEditorSetting.SelectBundles.Count];

                for (int i = 0; i < builds.Length; i++)
                {
                    AssetBundleBuild build = new AssetBundleBuild();
                    build.assetBundleName = assetBundleEditorSetting.SelectBundles[i];
                    build.assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(build.assetBundleName);
                    builds[i] = build;
                }

                string outputPath = assetBundleEditorSetting.BuildPath + "/" + assetBundleEditorSetting.Platform;
                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath, builds, (BuildAssetBundleOptions)assetBundleEditorSetting.CompressionType, assetBundleEditorSetting.Platform);
                Dictionary<string, string> config = new Dictionary<string, string>();

                foreach (var bundle in manifest.GetAllAssetBundles())
                {
                    config[bundle] = manifest.GetAssetBundleHash(bundle).ToString();
                }
                config["AssetVersion"] = assetBundleEditorSetting.AssetVersion;
                config["AppVersion"] = assetBundleEditorSetting.AppVersion;

                string configJson = JsonConvert.SerializeObject(config, Formatting.Indented);

                File.WriteAllText(outputPath + "/config.json", configJson);

                AssetDatabase.Refresh();
            }
        }
    }
}
#endif