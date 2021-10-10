using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using LitJson;
namespace Bigger
{
    public class AssetBundleEditor : EditorWindow
    {
        private AssetBundleEditorAttribute baseAttr;
        private string[] buildBundles;
        [MenuItem("Bigger/AssetBundle")]
        public static void OpenWindow()
        {
            AssetBundleEditor window = (AssetBundleEditor)EditorWindow.GetWindow(typeof(AssetBundleEditor), false, "AssetBundle");
            window.Show();
        }
        private void Awake()
        {
            if (File.Exists(AssetBundleEditorAttribute.serializePath))
            {
                string jsonTxt = File.ReadAllText(AssetBundleEditorAttribute.serializePath);
                baseAttr = JsonMapper.ToObject<AssetBundleEditorAttribute>(jsonTxt);
            }
            else
            {
                baseAttr = new AssetBundleEditorAttribute();
            }
            AssetDatabase.RemoveUnusedAssetBundleNames();
            buildBundles = AssetDatabase.GetAllAssetBundleNames();
        }
        private void OnGUI()
        {
            GUILayout.Label("保存路径");
            baseAttr.saveDir = GUILayout.TextField(baseAttr.saveDir);
            if (GUILayout.Button("选择文件夹"))
            {
                string selectPath = EditorUtility.OpenFolderPanel("资源保持路径", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selectPath))
                {
                    baseAttr.saveDir = selectPath;
                }
            }
            GUILayout.Label("打包平台");
            baseAttr.platform = (BuildTarget)EditorGUILayout.EnumPopup(baseAttr.platform);

            GUILayout.Label("压缩方式");
            baseAttr.option = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup(baseAttr.option);

            GUILayout.Label("资源版本");
            baseAttr.resVersion = GUILayout.TextField(baseAttr.resVersion);
            GUILayout.Label("App版本");
            baseAttr.appVersion = GUILayout.TextField(baseAttr.appVersion);
            foreach (var item in buildBundles)
            {
                bool value = baseAttr.bundles.Contains(item);
                if(GUILayout.Toggle(value, item))
                {
                    if(!baseAttr.bundles.Contains(item))
                    {
                        baseAttr.bundles.Add(item);
                    }
                }
                else if(baseAttr.bundles.Contains(item))
                {
                    baseAttr.bundles.Remove(item);
                }
            }
            if (GUILayout.Button("打包"))
            {
                Build();
            }
        }

        private void Build()
        {
            AssetBundleBuild[] builds = new AssetBundleBuild[baseAttr.bundles.Count];

            for (int i = 0; i < builds.Length; i++)
            {
                AssetBundleBuild build = new AssetBundleBuild();
                build.assetBundleName = baseAttr.bundles[i];
                build.assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(build.assetBundleName);
                builds[i] = build;
            }
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(GetTargetPath(), builds, baseAttr.option, baseAttr.platform);
            Dictionary<string, string> hash = FileUtil.LoadABManifest(manifest);
            CreateConfig(hash);
            AssetDatabase.Refresh();
        }
        private void OnDestroy()
        {
            string jsonText = JsonMapper.ToJson(baseAttr);
            File.WriteAllText(AssetBundleEditorAttribute.serializePath, jsonText);
        }
        /// <summary>
        /// 根据打包平台获取存放路径
        /// </summary>
        /// <returns></returns>
        private string GetTargetPath()
        {
            string outputPath = baseAttr.saveDir + "/" + baseAttr.platform;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            return outputPath;
        }

        /// <summary>
        /// 生成配置文件
        /// </summary>
        /// <param name="hash"></param>
        private void CreateConfig(Dictionary<string, string> hash)
        {
            string configPath = GetTargetPath() + "/resConfig.txt";
            ResConfig ResConfig = new ResConfig();
            ResConfig.appVersion = baseAttr.appVersion;
            ResConfig.resVersion = baseAttr.resVersion;
            ResConfig.resDict = hash;
            File.WriteAllText(configPath, JsonMapper.ToJson(ResConfig));
            Debug.Log("写入成功");
        }
    }
}