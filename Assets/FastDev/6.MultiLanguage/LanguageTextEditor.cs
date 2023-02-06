#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
namespace FastDev
{
    [CustomEditor(typeof(LanguageText))]
    public class LanguageTextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            LanguageText languageText = (LanguageText)target;
            EditorGUILayout.TextField("ID", languageText.ID);
            if (GUILayout.Button("注册多语言"))
            {
                languageText.ID = LanguageManager.Instance.RegisterText(languageText.GetText());
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif