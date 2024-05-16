using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FastDev
{
    public class LanguageManager : GameModule, ILanguageManager
    {
        public LanguageType LanguageType { get; private set; }

        private List<LanguageData> LanguageDatas;

        public LanguageManager()
        {
            InitLanguageData();
        }

        /// <summary>
        /// 初始化多语言
        /// </summary>
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

        /// <summary>
        /// 获取多语言文本
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetText(string id)
        {
            var languageData = LanguageDatas.FirstOrDefault((a) => a.ID == id);
            if (languageData != null)
            {
                return languageData.GetText();
            }
            return "Null Language ID";
        }

        /// <summary>
        /// 设置当前语言
        /// </summary>
        /// <param name="languageType"></param>
        public void SetLanguageType(LanguageType languageType)
        {
            LanguageType = languageType;
            PlayerPrefs.SetInt("Language", (int)languageType);

            GameEntry.Message.Dispatch(MsgID.UpdateLanguage);
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            //throw new NotImplementedException();
        }

        internal override void Shutdown()
        {
            //throw new NotImplementedException();

            LanguageDatas.Clear();
        }
    }
}