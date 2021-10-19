using System.Collections.Generic;
using UnityEngine;
namespace Bigger
{
    public enum LanguageType
    {
        None = 0,
        Chinese,
        English
    }
    public struct LanguageStruct
    {
        public string Chinese;
        public string English;
    }

    public class LanguageManager : MonoSingleton<LanguageManager>
    {
        private LanguageType _curLanguage;
        public LanguageType curLanguage
        {
            get
            {
                if (_curLanguage == LanguageType.None)
                    _curLanguage = (LanguageType)PlayerPrefs.GetInt("Language", 1);
                return _curLanguage;
            }
            set
            {
                if (_curLanguage == value)
                    return;
                PlayerPrefs.SetInt("Language", (int)value);
                _curLanguage = value;
                EventManager.Instance.Dispatch(MsgID.OnLanguageChange, null);
            }
        }
        private static Dictionary<string, LanguageStruct> languageDict = new Dictionary<string, LanguageStruct>();

        protected override void Init()
        {
            TextAsset textAsset = ResManager.Instance.LoadAsset<TextAsset>("config", "Assets/Resources/Language.json");
            languageDict = textAsset.text.ToObject<Dictionary<string, LanguageStruct>>();

            Debug.Log("curlanguage:" + curLanguage);
        }

        public string GetText(string key)
        {
            string text = null;
            if (languageDict.ContainsKey(key))
            {
                switch (curLanguage)
                {
                    case LanguageType.Chinese:
                        text = languageDict[key].Chinese;
                        break;
                    case LanguageType.English:
                        text = languageDict[key].English;
                        break;
                }
            }
            return text;
        }

        [ContextMenu("SetToEnglish")]
        public void SetToEnglish()
        {
            curLanguage = LanguageType.English;
        }

        [ContextMenu("SetToChinese")]
        public void SetToChinese()
        {
            curLanguage = LanguageType.Chinese;
        }

        public Dictionary<string, LanguageStruct> ReadEditorLanguageJson()
        {
            string path = "Assets/Resources/Language.json";
            string str = FileUtil.ReadFromExternal(path);
            Dictionary<string, LanguageStruct> languageDict = new Dictionary<string, LanguageStruct>();
            if (!string.IsNullOrEmpty(str))
            {
                languageDict = str.ToObject<Dictionary<string, LanguageStruct>>();
            }
            return languageDict;
        }

        public void SaveEditorLanguageJson(Dictionary<string, LanguageStruct> languageDict)
        {
            System.IO.File.WriteAllText("Assets/Resources/Language.json", System.Text.RegularExpressions.Regex.Unescape(languageDict.ToJson()));
            UnityEditor.AssetDatabase.Refresh();
        }
    }
}