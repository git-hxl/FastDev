using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Framework
{
    public class LanguageText : MonoBehaviour
    {
        public int ID = -1;
        private Text text;
        private TextMeshProUGUI textMeshPro;

        private void Awake()
        {
            text = GetComponent<Text>();
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            if (text != null)
                text.text = LanguageManager.Instance.GetText(ID);
            else
                textMeshPro.text = LanguageManager.Instance.GetText(ID);
        }

        public string GetText()
        {
            text = GetComponent<Text>();
            textMeshPro = GetComponent<TextMeshProUGUI>();
            return text != null ? text.text : textMeshPro.text;
        }
    }
}