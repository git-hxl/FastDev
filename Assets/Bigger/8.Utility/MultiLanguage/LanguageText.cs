using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
namespace Bigger
{
    public class LanguageText : MonoBehaviour
    {
        private Text text;
        private TextMeshProUGUI textMeshPro;
        private string multiKey;

        private void Awake()
        {
            InitKey();
            EventManager.Instance.Register(MsgID.OnLanguageChange, OnLanguageChange);
        }

        private void Start()
        {
            InitText();
        }

        private string InitKey()
        {
            text = GetComponent<Text>();
            textMeshPro = GetComponent<TextMeshProUGUI>();
            multiKey = text ? FileUtil.GetStrMD5(text.text) : FileUtil.GetStrMD5(textMeshPro.text);
            return text ? text.text : textMeshPro.text;
        }

        private void OnLanguageChange(Hashtable obj)
        {
            InitText();
        }

        private void InitText()
        {
            if (text != null)
                text.text = LanguageManager.Instance.GetText(multiKey);
            else
                textMeshPro.text = LanguageManager.Instance.GetText(multiKey);
        }

        [ContextMenu("多语言")]
        public void ExcuteUpdate()
        {
            string chineseStr = InitKey();
            if (string.IsNullOrEmpty(multiKey))
                return;
            string path = "Assets/Resources/Language.json";
            string str = FileUtil.ReadFromExternal(path);
            Dictionary<string, LanguageStruct> languageDict = new Dictionary<string, LanguageStruct>();
            if (!string.IsNullOrEmpty(str))
            {
                languageDict = str.ToObject<Dictionary<string, LanguageStruct>>();
            }
            if (!languageDict.ContainsKey(multiKey))
            {
                languageDict.Add(multiKey, new LanguageStruct() { Chinese = chineseStr });
            }
            File.WriteAllText(path, Regex.Unescape(languageDict.ToJson()));
            AssetDatabase.Refresh();
        }
    }
}