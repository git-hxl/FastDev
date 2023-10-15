using UnityEngine;
using UnityEditor;
namespace FastDev.Editor
{
    [CustomEditor(typeof(LanguageText))]
    public class LanguageTextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            LanguageText languageText = (LanguageText)target;

            EditorGUILayout.LabelField(languageText.ID.ToString());

            if (GUILayout.Button("注册多语言"))
            {
                languageText.ID = LanguageToolEditor.RegisterText(languageText.GetText());
            }
        }
    }
}