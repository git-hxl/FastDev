using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace FastDev.Editor
{
    public class ABTool : EditorWindow
    {
        public enum CompressionType
        {
            LZMA = BuildAssetBundleOptions.None,
            UncompressedAssetBundle = BuildAssetBundleOptions.UncompressedAssetBundle,
            LZ4 = BuildAssetBundleOptions.ChunkBasedCompression
        }

        private ABToolConfig abToolConfig;

        [MenuItem("Tools/AB工具")]
        public static void OpenWindow()
        {
            ABTool window = (ABTool)EditorWindow.GetWindow(typeof(ABTool), false, "AB工具");
            window.Show();

            AssetDatabase.RemoveUnusedAssetBundleNames();
        }

        private void Awake()
        {
            InitConfig();
        }

        private void OnGUI()
        {
            DrawWindow();
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        private void InitConfig()
        {
            abToolConfig = new ABToolConfig();
            if (File.Exists(ABToolConfig.ConfigPath))
            {
                string jsonTxt = File.ReadAllText(ABToolConfig.ConfigPath);
                if (!string.IsNullOrEmpty(jsonTxt) && jsonTxt != "null")
                    abToolConfig = JsonConvert.DeserializeObject<ABToolConfig>(jsonTxt);
            }
        }

        /// <summary>
        /// 绘制窗口
        /// </summary>
        private void DrawWindow()
        {
            GUILayout.Label("打包平台");
            abToolConfig.Platform = (BuildTarget)EditorGUILayout.EnumPopup(abToolConfig.Platform);

            GUILayout.Label("压缩方式");
            abToolConfig.CompressionType = (CompressionType)EditorGUILayout.EnumPopup(abToolConfig.CompressionType);

            GUILayout.Label("资源版本");
            abToolConfig.AssetVersion = GUILayout.TextField(abToolConfig.AssetVersion);
            GUILayout.Label("App版本");
            abToolConfig.AppVersion = GUILayout.TextField(abToolConfig.AppVersion);

            GUILayout.Label("输出路径");
            abToolConfig.BuildOutPath = GUILayout.TextField(abToolConfig.BuildOutPath);

            if (GUILayout.Button("选择文件夹"))
            {
                string selectPath = EditorUtility.OpenFolderPanel("打包目录", Application.dataPath, "");
                abToolConfig.BuildOutPath = selectPath;
            }

            foreach (var item in AssetDatabase.GetAllAssetBundleNames())
            {
                bool value = GUILayout.Toggle(abToolConfig.SelectBundles.Contains(item), item);

                if (value && !abToolConfig.SelectBundles.Contains(item))
                {
                    abToolConfig.SelectBundles.Add(item);
                }

                if (!value && abToolConfig.SelectBundles.Contains(item))
                {
                    abToolConfig.SelectBundles.Remove(item);
                }
            }

            if (GUILayout.Button("打包"))
            {
                Build();
            }

            if (GUILayout.Button("清除打包目录"))
            {
                ClearOutPath();
            }
        }

        /// <summary>
        /// 打包
        /// </summary>
        private void Build()
        {
            AssetBundleBuild[] builds = new AssetBundleBuild[abToolConfig.SelectBundles.Count];

            for (int i = 0; i < builds.Length; i++)
            {
                AssetBundleBuild build = new AssetBundleBuild();
                build.assetBundleName = abToolConfig.SelectBundles[i];
                build.assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(build.assetBundleName);
                builds[i] = build;
            }

            string outputPath = abToolConfig.BuildOutPath + "/" + abToolConfig.Platform;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(outputPath, builds, (BuildAssetBundleOptions)abToolConfig.CompressionType, abToolConfig.Platform);

            ResourceConfig abConfig = new ResourceConfig();
            abConfig.Bundles = new Dictionary<string, string>();
            foreach (var bundle in manifest.GetAllAssetBundles())
            {
                abConfig.Bundles[bundle] = manifest.GetAssetBundleHash(bundle).ToString();
            }
            abConfig.AssetVersion = abToolConfig.AssetVersion;
            abConfig.AppVersion = abToolConfig.AppVersion;
            abConfig.DateTime = DateTime.Now.ToString();
            string configJson = JsonConvert.SerializeObject(abConfig, Formatting.Indented);

            File.WriteAllText(outputPath + "/AssetConfig.json", configJson);
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 清除目录
        /// </summary>
        private void ClearOutPath()
        {
            string outPath = abToolConfig.BuildOutPath + "/" + abToolConfig.Platform;
            if (!string.IsNullOrEmpty(outPath))
            {
                Directory.Delete(outPath,true);
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void SaveConfig()
        {
            string jsonTxt = JsonConvert.SerializeObject(abToolConfig);

            File.WriteAllText(ABToolConfig.ConfigPath, jsonTxt);
        }

        private void OnDisable()
        {
            SaveConfig();
        }
    }
}