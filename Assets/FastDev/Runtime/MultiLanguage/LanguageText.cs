using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace FastDev
{
    public class LanguageText : MonoBehaviour
    {
        private Text text;
        private TextMeshProUGUI textMeshPro;
        private string multiKey;

        private void Awake()
        {
            InitKey();
            MsgManager.instance.Register(MsgID.OnLanguageChange, OnLanguageChange);
        }

        private void Start()
        {
            InitText();
        }

        public string InitKey()
        {
            text = GetComponent<Text>();
            textMeshPro = GetComponent<TextMeshProUGUI>();
            multiKey = text ? FileUtil.GetStrMD5(text.text) : FileUtil.GetStrMD5(textMeshPro.text);
            return multiKey;
        }
        public string GetDefaultStr()
        {
            string str = text ? text.text : textMeshPro.text;
            return str;
        }

        private void OnLanguageChange(Hashtable obj)
        {
            InitText();
        }

        private void InitText()
        {
            if (text != null)
                text.text = LanguageManager.instance.GetText(multiKey);
            else
                textMeshPro.text = LanguageManager.instance.GetText(multiKey);
        }
    }
}