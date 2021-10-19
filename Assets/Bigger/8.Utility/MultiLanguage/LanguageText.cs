using ExcelDataReader;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Bigger
{
    public class LanguageText : MonoBehaviour
    {
        private LanguageType languageType;
        private Text text;
        private TextMeshProUGUI textMeshPro;

        public string multiStr;
        private void Awake()
        {
            text = GetComponent<Text>();
            textMeshPro = GetComponent<TextMeshProUGUI>();
            EventManager.Instance.Register(MsgID.OnLanguageChange, OnLanguageChange);
        }
        private void OnLanguageChange(Hashtable obj)
        {
            languageType = (LanguageType)obj[0];
        }

        [ContextMenu("Excute")]
        public void ExcuteUpdate()
        {

            //FileUtil.ReadExcel("Assets/StreamingAssets/Language.xlsx");
        }
    }
}