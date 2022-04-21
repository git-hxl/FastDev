using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FastDev
{
    public class MultiLanguageTool
    {
        private static string path = "Assets/Resources/MultiLanguage.json";
        [MenuItem("Assets/注册当前对象的多语言")]
        static void ExcuteLanguageUpdate()
        {
            if (Selection.activeGameObject == null) return;
            List<LanguageText> languageTexts = new List<LanguageText>();
            foreach (var ui in Selection.gameObjects)
            {
                LanguageText[] itemTexts = ui.GetComponentsInChildren<LanguageText>(true);
                languageTexts.AddRange(itemTexts);
            }
            if (languageTexts != null && languageTexts.Count > 0)
            {
                var languageDict = ReadEditorLanguageJson();
                foreach (var item in languageTexts)
                {
                    AddNewLanguageText(item.InitKey(), item.GetDefaultStr(), languageDict);
                }
                SaveEditorLanguageJson(languageDict);
                AutoCreateLanguageConstant(languageDict);
            }
        }

        [MenuItem("FastDev/生成LanguageConstant.cs",false, 0)]
        static void GenerateMultiLanguageConstant()
        {
            var languageDict = ReadEditorLanguageJson();
            AutoCreateLanguageConstant(languageDict);
        }

        private static Dictionary<string, LanguageStruct> ReadEditorLanguageJson()
        {
            string str = File.ReadAllText(path);
            Dictionary<string, LanguageStruct> languageDict = new Dictionary<string, LanguageStruct>();
            if (!string.IsNullOrEmpty(str))
            {
                languageDict = str.ToObjectByJson<Dictionary<string, LanguageStruct>>();
            }
            return languageDict;
        }

        private static Dictionary<string, LanguageStruct> AddNewLanguageText(string multiKey, string chineseStr, Dictionary<string, LanguageStruct> languageDict)
        {
            if (!languageDict.ContainsKey(multiKey))
            {
                languageDict.Add(multiKey, new LanguageStruct() { Chinese = chineseStr });
            }
            return languageDict;
        }

        private static void SaveEditorLanguageJson(Dictionary<string, LanguageStruct> languageDict)
        {
            for (int i = 0; i < languageDict.Count; i++)
            {
                LanguageStruct languageStruct = languageDict.ElementAt(i).Value;
                languageStruct.Chinese = languageStruct.Chinese.Replace("\n", "\\n");
                if (languageStruct.English != null)
                    languageStruct.English = languageStruct.English.Replace("\n", "\\n");
                languageDict[languageDict.ElementAt(i).Key] = languageStruct;
            }

            File.WriteAllText(path, languageDict.ToJson(true));
            AssetDatabase.Refresh();
        }

        private static void AutoCreateLanguageConstant(Dictionary<string, LanguageStruct> languageDict)
        {
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
            File.WriteAllText($"{Application.dataPath}/FastDev/Runtime/MultiLanguage/LanguageConstant.cs", classStr);
            AssetDatabase.Refresh();
        }
    }
}
