using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Bigger
{
    public class MultiLanguageTool
    {
        [MenuItem("Bigger/自动生成/当前对象的多语言")]
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
                AutoCreateLanguageConstant();
            }
        }

        public static Dictionary<string, LanguageStruct> ReadEditorLanguageJson()
        {
            string path = "Assets/Resources/MultiLanguage.json";
            string str = FileUtil.ReadFromExternal(path);
            Dictionary<string, LanguageStruct> languageDict = new Dictionary<string, LanguageStruct>();
            if (!string.IsNullOrEmpty(str))
            {
                languageDict = str.ToObject<Dictionary<string, LanguageStruct>>();
            }
            return languageDict;
        }

        public static void SaveEditorLanguageJson(Dictionary<string, LanguageStruct> languageDict)
        {
            File.WriteAllText("Assets/Resources/MultiLanguage.json", languageDict.ToJson(true));
            AssetDatabase.Refresh();
        }

        public static Dictionary<string, LanguageStruct> AddNewLanguageText(string multiKey, string chineseStr, Dictionary<string, LanguageStruct> languageDict)
        {
            if (!languageDict.ContainsKey(multiKey))
            {
                languageDict.Add(multiKey, new LanguageStruct() { Chinese = chineseStr });
            }
            return languageDict;
        }

        public static void AutoCreateLanguageConstant()
        {
            string classStr = @"
namespace Bigger
{
    public static class LanguageConstant
    {
        $变量
    }
}";
            string var = "";
            var dict = ReadEditorLanguageJson();
            foreach (var item in dict)
            {
                var += $"public const string {item.Value.Chinese.ToAlphaNumberAndChinese(false).Replace(" ", "_")} = \"{item.Key}\";\r\n\t\t";
            }
            classStr = classStr.Replace("$变量", var);
            File.WriteAllText($"{Application.dataPath}/Bigger/8.Utility/MultiLanguage/LanguageConstant.cs", classStr);
            AssetDatabase.Refresh();
        }
    }
}
