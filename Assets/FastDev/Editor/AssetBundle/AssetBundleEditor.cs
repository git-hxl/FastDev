using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FastDev
{
    public class AssetBundleEditor : EditorWindow
    {
        private ABBuildSetting abBuildSetting;
        [MenuItem("FastDev/AssetBundle工具")]
        public static void OpenWindow()
        {
            AssetBundleEditor window = (AssetBundleEditor)EditorWindow.GetWindow(typeof(AssetBundleEditor), false, "AssetBundle");
            window.Show();
        }
        private void Awake()
        {
            if (File.Exists(ABBuildSetting.serializePath))
            {
                string jsonTxt = File.ReadAllText(ABBuildSetting.serializePath);
                abBuildSetting = JsonConvert.DeserializeObject<ABBuildSetting>(jsonTxt);
            }
            else
            {
                abBuildSetting = new ABBuildSetting();
            }
            AssetDatabase.RemoveUnusedAssetBundleNames();
        }
        private void OnGUI()
        {
            GUILayout.Label("保存路径");
            abBuildSetting.saveDir = GUILayout.TextField(abBuildSetting.saveDir);
            if (GUILayout.Button("选择文件夹"))
            {
                string selectPath = EditorUtility.OpenFolderPanel("资源保持路径", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selectPath))
                {
                    abBuildSetting.saveDir = selectPath;
                }
            }
            GUILayout.Label("打包平台");
            abBuildSetting.platform = (BuildTarget)EditorGUILayout.EnumPopup(abBuildSetting.platform);

            GUILayout.Label("压缩方式");
            abBuildSetting.option = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup(abBuildSetting.option);

            GUILayout.Label("资源版本");
            abBuildSetting.resVersion = GUILayout.TextField(abBuildSetting.resVersion);
            GUILayout.Label("App版本");
            abBuildSetting.appVersion = GUILayout.TextField(abBuildSetting.appVersion);
            foreach (var item in AssetDatabase.GetAllAssetBundleNames())
            {
                bool value = abBuildSetting.bundles.Contains(item);
                if (GUILayout.Toggle(value, item))
                {
                    if (!abBuildSetting.bundles.Contains(item))
                    {
                        abBuildSetting.bundles.Add(item);
                    }
                }
                else if (abBuildSetting.bundles.Contains(item))
                {
                    abBuildSetting.bundles.Remove(item);
                }
            }
            if (GUILayout.Button("打包"))
            {
                Build();
            }

            if (GUILayout.Button("刷新ABConstant.cs"))
            {
                CreateABConstant();
            }
        }

        private void Build()
        {
            AssetBundleBuild[] builds = new AssetBundleBuild[abBuildSetting.bundles.Count];

            for (int i = 0; i < builds.Length; i++)
            {
                AssetBundleBuild build = new AssetBundleBuild();
                build.assetBundleName = abBuildSetting.bundles[i];
                build.assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(build.assetBundleName);
                builds[i] = build;
            }
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(GetTargetPath(), builds, abBuildSetting.option, abBuildSetting.platform);
            Dictionary<string, string> hash = FileUtil.LoadABHash(manifest);
            hash.Add(abBuildSetting.platform.ToString(), FileUtil.GetFileMD5(GetTargetPath() + "/" + abBuildSetting.platform));
            UpdateConfig(hash);
            AssetDatabase.Refresh();
        }
        private void OnDestroy()
        {
            string jsonText = JsonConvert.SerializeObject(abBuildSetting);
            File.WriteAllText(ABBuildSetting.serializePath, jsonText);
        }
        /// <summary>
        /// 根据打包平台获取存放路径
        /// </summary>
        /// <returns></returns>
        private string GetTargetPath()
        {
            string outputPath = abBuildSetting.saveDir + "/" + abBuildSetting.platform;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            return outputPath;
        }

        /// <summary>
        /// 更新配置文件
        /// </summary>
        /// <param name="hash"></param>
        private void UpdateConfig(Dictionary<string, string> hash)
        {
            ResLoaderConfig ResConfig = new ResLoaderConfig();
            string configPath = GetTargetPath() + "/" + ResLoaderConfig.fileName;
            if (File.Exists(configPath))
            {
                ResConfig = JsonConvert.DeserializeObject<ResLoaderConfig>(File.ReadAllText(configPath));
            }
            foreach (var item in hash)
            {
                if (ResConfig.resDict.ContainsKey(item.Key))
                {
                    ResConfig.resDict[item.Key] = item.Value;
                }
                else
                    ResConfig.resDict.Add(item.Key, item.Value);
            }
            ResConfig.appVersion = abBuildSetting.appVersion;
            ResConfig.resVersion = abBuildSetting.resVersion;
            File.WriteAllText(configPath, JsonConvert.SerializeObject(ResConfig,Formatting.Indented));
            Debug.Log("写入成功");
        }


        private string path = "./Assets/FastDev/Core/1.Res/ABConstant.cs";
        public static string classStr = @"
namespace FastDev
{
    public static class ABConstant
    {
        $变量
    }
}";
        private void CreateABConstant()
        {
            string s = "";
            foreach (var item in AssetDatabase.GetAllAssetBundleNames())
            {
                s += $"public const string {item} = \"{item}\";\r\n\t\t";
            }
            classStr = classStr.Replace("$变量", s);
            File.WriteAllText(path, classStr);
            AssetDatabase.Refresh();
        }
    }
}