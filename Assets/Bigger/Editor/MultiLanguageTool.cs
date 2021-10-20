using System.Collections.Generic;
using System.IO;
using UnityEditor;
namespace Bigger
{
    public class MultiLanguageTool
    {
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
            File.WriteAllText("Assets/Resources/MultiLanguage.json",languageDict.ToJson(true));
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
    }
}
