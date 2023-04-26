using UnityEngine;
using UnityEditor;
namespace Framework.Editor
{
    [CustomEditor(typeof(LanguageText))]
    public class LanguageTextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LanguageText languageText = (LanguageText)target;
            if (GUILayout.Button("注册多语言"))
            {
                var languageData = LanguageManager.Instance.RegisterText(languageText.GetText());
                if(languageData != null )
                {
                    languageText.ID = languageData.ID;
                    AssetDatabase.Refresh();
                }
            }
        }
    }
}