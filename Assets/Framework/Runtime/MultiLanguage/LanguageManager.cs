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

        private string languagePath;

        protected override void OnInit()
        {
            base.OnInit();
            LanguageDatas = new List<LanguageData>();
            languagePath = Application.streamingAssetsPath + "/MultiLanguage.json";
            if (File.Exists(languagePath))
            {
                string json = File.ReadAllText(languagePath);
                LanguageDatas = JsonConvert.DeserializeObject<List<LanguageData>>(json);
            }
            LanguageType = (LanguageType)PlayerPrefs.GetInt("Language", 0);
            Debug.Log("curlanguage:" + LanguageType);
        }

        public LanguageData RegisterText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            var languageData = LanguageDatas.FirstOrDefault((a) => a.Chinese == text);
            if (languageData == null)
            {
                int id = 1000;
                if (LanguageDatas.Count > 0)
                {
                    id = LanguageDatas.Last().ID + 1;
                }
                languageData = new LanguageData(id, text);
                LanguageDatas.Add(languageData);
                File.WriteAllText(languagePath, JsonConvert.SerializeObject(LanguageDatas, Formatting.Indented));
                return languageData;
            }
            return languageData;
        }

        public bool RemoveLanguageData(LanguageData languageData)
        {
            if (LanguageDatas.Contains(languageData))
            {
                LanguageDatas.Remove(languageData);
                File.WriteAllText(languagePath, JsonConvert.SerializeObject(LanguageDatas, Formatting.Indented));
                return true;
            }
            return false;
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

        public int GetID(string text)
        {
            LanguageData languageData = LanguageDatas.FirstOrDefault((a) => a.Chinese == text);
            if (languageData != null)
            {
                return languageData.ID;
            }
            return -1;
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