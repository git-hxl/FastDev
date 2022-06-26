using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace FastDev
{
    public class LanguageManager : Singleton<LanguageManager>
    {
        private LanguageType languageType;
        public LanguageType LanguageType
        {
            get
            {
                languageType = (LanguageType)PlayerPrefs.GetInt("Language", 0);
                return languageType;
            }
            set
            {
                languageType = value;
                PlayerPrefs.SetInt("Language", (int)value);
                MsgManager.Instance.Dispatch(MsgID.OnLanguageChange, null);
            }
        }
        private static Dictionary<string, LanguageStruct> languageDict = new Dictionary<string, LanguageStruct>();

        public LanguageManager()
        {
            string path = Application.streamingAssetsPath + "/language.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                languageDict = LitJson.JsonMapper.ToObject<Dictionary<string, LanguageStruct>>(json);
                Debug.Log("curlanguage:" + LanguageType);
            }
        }

        public string GetText(string id)
        {
            string text = null;
            if (languageDict.ContainsKey(id))
            {
                switch (LanguageType)
                {
                    case LanguageType.Chinese:
                        text = languageDict[id].Chinese;
                        break;
                    case LanguageType.English:
                        text = languageDict[id].English;
                        break;
                }
            }
            return text;
        }

        public Dictionary<string, LanguageStruct> ReadLanguageJson()
        {
            string languagePath = Application.streamingAssetsPath + "/language.json";
            if (File.Exists(languagePath))
            {
                string json = File.ReadAllText(languagePath);
                return LitJson.JsonMapper.ToObject<Dictionary<string, LanguageStruct>>(json);
            }
            return null;
        }

        public void SaveToPath(Dictionary<string, LanguageStruct> dict)
        {
            string languagePath = Application.streamingAssetsPath + "/language.json";
            LitJson.JsonWriter jsonWriter = new LitJson.JsonWriter();
            jsonWriter.PrettyPrint = true;
            LitJson.JsonMapper.ToJson(dict, jsonWriter);
            string json = jsonWriter.ToString();
            File.WriteAllText(languagePath, json.UnicodeToChinese());
        }

        [ContextMenu("SetToEnglish")]
        public void SetToEnglish()
        {
            LanguageType = LanguageType.English;
        }

        [ContextMenu("SetToChinese")]
        public void SetToChinese()
        {
            LanguageType = LanguageType.Chinese;
        }
    }
}