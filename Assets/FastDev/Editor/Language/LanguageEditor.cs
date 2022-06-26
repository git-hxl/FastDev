using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;
namespace FastDev
{
    public class LanguageEditor : EditorWindow
    {
        private string inputStr = "";
        private string outputStr = "";
        private Dictionary<string, LanguageStruct> dict;
        [MenuItem("FastDev/多语言工具")]
        public static void OpenWindow()
        {
            LanguageEditor window = (LanguageEditor)EditorWindow.GetWindow(typeof(LanguageEditor), false, "Language");
            window.Show();
        }
        private void Awake()
        {
            dict = LanguageManager.Instance.ReadLanguageJson();
        }
        private void OnGUI()
        {
            GUILayout.Label("输入中文文本:");
            inputStr = GUILayout.TextField(inputStr);
            string id = FileUtil.GetMD5(inputStr);
            if (GUILayout.Button("查询/添加"))
            {
                if (!dict.ContainsKey(id) && !string.IsNullOrEmpty(id))
                {
                    dict[id] = new LanguageStruct() { Chinese = inputStr };
                    LanguageManager.Instance.SaveToPath(dict);
                    AssetDatabase.Refresh();
                }
                JsonWriter jsonWriter = new JsonWriter();
                jsonWriter.PrettyPrint = true;
                JsonMapper.ToJson(dict[id], jsonWriter);
                outputStr = id + "\n" + jsonWriter.ToString().UnicodeToChinese();
            }
            if (GUILayout.Button("移除"))
            {
                if (dict.ContainsKey(id))
                {
                    dict.Remove(id);
                    LanguageManager.Instance.SaveToPath(dict);
                    AssetDatabase.Refresh();
                }
            }
            if (GUILayout.Button("刷新LanguageConstant.cs"))
            {
                CreateLanguageConstant(dict);
            }
            if (!string.IsNullOrEmpty(outputStr))
            {
                GUILayout.TextArea(outputStr);
            }
        }


        /// <summary>
        /// 创建LanguageConstant.cs
        /// </summary>
        /// <param name="languageDict"></param>
        private static void CreateLanguageConstant(Dictionary<string, LanguageStruct> languageDict)
        {
            string path = "./Assets/FastDev/Core/6.MultiLanguage/LanguageConstant.cs";
            string classStr = @"
namespace FastDev
{
    public static class LanguageConstant
    {
        $变量
    }
}";
            string var = "";
            foreach (var item in languageDict)
            {
                string txtTag = item.Value.Chinese;
                int maxLength = 6;
                if (txtTag.Length > maxLength)
                    txtTag = $"{txtTag.Substring(0, maxLength)} 省略 {txtTag.Length - maxLength} 字";
                var += $"public const string {txtTag.ToAlphaNumberAndChinese(false).Replace(" ", "_").Replace("\n", "n")} = \"{item.Key}\";\r\n\t\t";
            }
            classStr = classStr.Replace("$变量", var);

            File.WriteAllText(path, classStr);
            AssetDatabase.Refresh();
        }
    }

}
