using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace FastDev
{
    public class LanguageText : MonoBehaviour
    {
        public string ID;
        private Text text;
        private TextMeshProUGUI textMeshPro;

        private void Start()
        {
            InitText();
            SetText();
            MsgManager.Instance.Register(MsgID.OnLanguageChange, OnLanguageChange);
        }

        private void InitText()
        {
            text = GetComponent<Text>();
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        public string GetCurStr()
        {
            InitText();
            string str = text ? text.text : textMeshPro.text;
            return str;
        }

        private void SetText()
        {
            if (text != null)
                text.text = LanguageManager.Instance.GetText(ID);
            else
                textMeshPro.text = LanguageManager.Instance.GetText(ID);
        }

        private void OnLanguageChange(Hashtable obj)
        {
            SetText();
        }

        [ContextMenu("SetToEnglish")]
        public void SetToEnglish()
        {
            LanguageManager.Instance.LanguageType = LanguageType.English;
        }

        [ContextMenu("SetToChinese")]
        public void SetToChinese()
        {
            LanguageManager.Instance.LanguageType = LanguageType.Chinese;
        }

    }
}