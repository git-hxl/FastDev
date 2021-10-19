using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace Bigger
{
    public enum LanguageType
    {
        None = 0,
        Chinese,
        English
    }
    public class LanguageManager : MonoSingleton<LanguageManager>
    {
        public struct LanguageStruct
        {
            public string Chinese;
            public string English;
        }

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
                PlayerPrefs.SetInt("Language", (int)value);
                _curLanguage = value;
            }
        }

        private Dictionary<string, LanguageStruct> languageDict = new Dictionary<string, LanguageStruct>();


        protected override void Awake()
        {
            base.Awake();
            InitLanguage();
        }

        public void InitLanguage()
        {
            TextAsset textAsset = ResManager.Instance.LoadAsset<TextAsset>("config", "Assets/Bigger/8.Utility/MultiLanguage/Language.json");
            languageDict = textAsset.text.ToObject<Dictionary<string, LanguageStruct>>();
        }
    }
}