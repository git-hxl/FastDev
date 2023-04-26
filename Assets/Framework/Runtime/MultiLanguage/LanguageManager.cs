using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace Framework
{
    public class LanguageManager : Singleton<LanguageManager>, ILanguageManager
    {
        public LanguageType LanguageType { get; private set; }
        public List<LanguageData> LanguageDatas { get; private set; }

        private string path;

        protected override void OnInit()
        {
            base.OnInit();
            LanguageDatas = new List<LanguageData>();
            path = Application.streamingAssetsPath + "/MultiLanguage.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                LanguageDatas = JsonConvert.DeserializeObject<List<LanguageData>>(json);
            }
            LanguageType = (LanguageType)PlayerPrefs.GetInt("Language", 0);
            Debug.Log("curlanguage:" + LanguageType);
        }

        public string GetText(int id)
        {
            var languageData = LanguageDatas.FirstOrDefault((a) => a.ID == id);
            if (languageData != null)
            {
                return languageData.GetText();
            }
            return "";
        }

        public LanguageData RegisterText(string text)
        {
            var languageData = LanguageDatas.FirstOrDefault((a) => a.Chinese == text);
            if (languageData == null)
            {
                int id = 1000 + LanguageDatas.Count;
                languageData = new LanguageData(id, text);
                LanguageDatas.Add(languageData);
                File.WriteAllText(path, JsonConvert.SerializeObject(LanguageDatas, Formatting.Indented));
                return languageData;
            }

            return languageData;
        }

        public int GetID(string text)
        {
            LanguageData languageData = LanguageDatas.FirstOrDefault((a) => a.Chinese == text);
            if (languageData != null)
            {
                return languageData.ID;
            }
            return -1;
        }

        public void RemoveText(int id)
        {
            LanguageData languageData = LanguageDatas.FirstOrDefault((a) => a.ID == id);
            RemoveLanguageData(languageData);
        }

        public bool RemoveLanguageData(LanguageData languageData)
        {
            if (LanguageDatas.Contains(languageData))
            {
                LanguageDatas.Remove(languageData);
                File.WriteAllText(path, JsonConvert.SerializeObject(LanguageDatas, Formatting.Indented));
                return true;
            }
            return false;
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