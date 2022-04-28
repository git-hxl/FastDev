using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
namespace FastDev.Editor
{
    public class MultiLanguageTool
    {
        [MenuItem("Assets/注册当前对象的多语言(可以多选)", true)]
        private static bool ValidateFunc()
        {
            return Selection.activeGameObject != null && Selection.activeGameObject.GetComponentsInChildren<LanguageText>(true).Length > 0;
        }

        [MenuItem("Assets/注册当前对象的多语言(可以多选)")]
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
            }
        }


        public static Dictionary<string, LanguageStruct> ReadEditorLanguageJson()
        {
            string languageJson = "";
            using (FileStream stream = new FileStream(GenScriptHelper.multiLanguagePath,FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                languageJson = Encoding.UTF8.GetString(data);
            }
               
            Dictionary<string, LanguageStruct> languageDict = new Dictionary<string, LanguageStruct>();
            if (!string.IsNullOrEmpty(languageJson))
            {
                languageDict = languageJson.ToObjectByJson<Dictionary<string, LanguageStruct>>();
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

            File.WriteAllText(GenScriptHelper.multiLanguagePath, languageDict.ToJson(true));
            AssetDatabase.Refresh();
        }


    }
}
