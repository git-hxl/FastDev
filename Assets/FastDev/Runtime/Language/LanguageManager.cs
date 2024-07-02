using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FastDev
{
    public class LanguageManager : Singleton<LanguageManager>, ILanguageManager
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
            string languagePath = Application.streamingAssetsPath + "/MultiLanguage.json";
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
            Debug.LogError($"多语言ID：{id} 不存在");
            return "";
        }

        /// <summary>
        /// 获取多语言文本
        /// </summary>
        /// <param name="txt">中文文本</param>
        /// <returns></returns>
        public string GetDynamicText(string txt)
        {
            string id = GetID(txt);
            string languageTxt = GetText(id);
            if (languageTxt == "")
            {
                Debug.LogError($"多语言缺失：{txt}");
            }
            return languageTxt;
        }

        /// <summary>
        /// 设置当前语言
        /// </summary>
        /// <param name="languageType"></param>
        public void SetLanguageType(LanguageType languageType)
        {
            LanguageType = languageType;
            PlayerPrefs.SetInt("Language", (int)languageType);

            MessageManager.Instance.Dispatch(MsgID.UpdateLanguage);
        }

        public static string GetID(string text)
        {
            return string.Format("{0:X}", text.GetHashCode());
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            LanguageDatas.Clear();
        }
    }
}