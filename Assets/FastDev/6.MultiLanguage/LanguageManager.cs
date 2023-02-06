using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace FastDev
{
    public class LanguageManager : Singleton<LanguageManager>, ILanguageManager
    {
        public LanguageType LanguageType { get; private set; }
        public Dictionary<string, LanguageData> LanguageDict { get; private set; } = new Dictionary<string, LanguageData>();

        private string path;

        public LanguageManager()
        {
            path = Application.streamingAssetsPath + "/MultiLanguage.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                LanguageDict = JsonConvert.DeserializeObject<Dictionary<string, LanguageData>>(json);
            }
            LanguageType = (LanguageType)PlayerPrefs.GetInt("Language", 0);
            Debug.Log("curlanguage:" + LanguageType);
        }

        public string GetText(string id)
        {
            if (LanguageDict.ContainsKey(id))
            {
                return LanguageDict[id].GetText();
            }

            return "";
        }

        public string RegisterText(string text)
        {
            string id = string.Format("{0:X}", text.GetHashCode());
            if (!LanguageDict.ContainsKey(id))
            {
                LanguageDict[id] = new LanguageData(text);

                File.WriteAllText(path, JsonConvert.SerializeObject(LanguageDict, Formatting.Indented));
            }
            return id;
        }

        public void RemoveText(string text)
        {
            string id = string.Format("{0:X}", text.GetHashCode());

            if (LanguageDict.ContainsKey(id))
            {
                LanguageDict.Remove(id);

                File.WriteAllText(path, JsonConvert.SerializeObject(LanguageDict, Formatting.Indented));
            }
        }

        public void SetLanguageType(LanguageType languageType)
        {
            LanguageType = languageType;
            PlayerPrefs.SetInt("Language", (int)languageType);

            LanguageText[] languageTexts = GameObject.FindObjectsOfType<LanguageText>();

            foreach (var item in languageTexts)
            {
                item.UpdateText();
            }
        }
    }
}