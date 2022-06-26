using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
namespace FastDev
{
    [CustomEditor(typeof(LanguageText))]
    public class LanguageTextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            LanguageText text = (LanguageText)target;
            EditorGUILayout.TextField("ID", text.ID);
            if (GUILayout.Button("注册多语言"))
            {
                text.ID = FileUtil.GetMD5(text.GetCurStr());
                RegisterLanguage(text.ID, text.GetCurStr());
            }
        }

        private void RegisterLanguage(string id, string str)
        {
            Dictionary<string, LanguageStruct> dict = LanguageManager.Instance.ReadLanguageJson();
            if (dict == null)
                dict = new Dictionary<string, LanguageStruct>();
            if (!dict.ContainsKey(id))
            {
                LanguageStruct languageStruct = new LanguageStruct();
                languageStruct.Chinese = str;
                dict[id] = languageStruct;
                LanguageManager.Instance.SaveToPath(dict);
                AssetDatabase.Refresh();
            }
        }
    }
}