using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace FastDev
{
    public class LanguageManager : MonoSingleton<LanguageManager>, ILanguageManager
    {
        public LanguageType LanguageType { get; private set; }

        private List<LanguageData> LanguageDatas;

        protected override void OnInit()
        {
            base.OnInit();
            InitLanguageData();
        }

        public void InitLanguageData()
        {
            LanguageDatas = new List<LanguageData>();
            string languagePath = Application.streamingAssetsPath + "/MultiLanguage_MultiLanguage.json";
            if (File.Exists(languagePath))
            {
                string json = File.ReadAllText(languagePath);
                LanguageDatas = JsonConvert.DeserializeObject<List<LanguageData>>(json);
            }
            LanguageType = (LanguageType)PlayerPrefs.GetInt("Language", 0);
            Debug.Log("curlanguage:" + LanguageType);
        }

        public string GetText(string id)
        {
            var languageData = LanguageDatas.FirstOrDefault((a) => a.ID == id);
            if (languageData != null)
            {
                return languageData.GetText();
            }
            return "Null Language ID";
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