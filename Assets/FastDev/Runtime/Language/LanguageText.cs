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
            GameEntry.Message.Register(MsgID.UpdateLanguage, UpdateText);
        }

        public void UpdateText()
        {
            if (GameEntry.Language == null)
            {
                return;
            }

            if (text != null)
                text.text = GameEntry.Language.GetText(ID);
            else
                textMeshPro.text = GameEntry.Language.GetText(ID);
        }

        public string GetText()
        {
            text = GetComponent<Text>();
            textMeshPro = GetComponent<TextMeshProUGUI>();
            return text != null ? text.text : textMeshPro.text;
        }

        private void OnDestroy()
        {
            GameEntry.Message.UnRegister(MsgID.UpdateLanguage, UpdateText);
        }
    }
}