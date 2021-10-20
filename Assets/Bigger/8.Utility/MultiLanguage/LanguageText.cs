using System.Collections;
using TMPro;
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

        public string InitKey()
        {
            text = GetComponent<Text>();
            textMeshPro = GetComponent<TextMeshProUGUI>();
            multiKey = text ? FileUtil.GetStrMD5(text.text) : FileUtil.GetStrMD5(textMeshPro.text);
            return multiKey;
        }
        public string GetDefaultStr()
        {
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
    }
}