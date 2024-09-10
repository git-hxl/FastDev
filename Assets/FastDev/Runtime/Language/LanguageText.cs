using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace FastDev
{
    public class LanguageText : MonoBehaviour
    {
        public string ID = "";
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

        private void Start()
        {
            MessageManager.Instance.Register(-1, UpdateText);
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

        private void OnDestroy()
        {
            MessageManager.Instance.UnRegister(-1, UpdateText);
        }
    }
}