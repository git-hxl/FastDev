#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;

namespace FastDev
{
    public class LanguageToolEditor : EditorWindow
    {
        private string inputStr = "";
        private string outputStr = "";

        [MenuItem("FastDev/多语言工具")]
        public static void OpenWindow()
        {
            LanguageToolEditor window = (LanguageToolEditor)EditorWindow.GetWindow(typeof(LanguageToolEditor), false, "Language");
            window.Show();
        }
        private void OnGUI()
        {
            GUILayout.Label("输入中文文本:");
            inputStr = GUILayout.TextArea(inputStr);

            GUISelectText();

            RemoveText();

            if (!string.IsNullOrEmpty(outputStr))
            {
                GUILayout.TextArea(outputStr);
            }
        }

        private void GUISelectText()
        {
            if (GUILayout.Button("查询"))
            {
                outputStr = "";
                string id = string.Format("{0:X}", inputStr.GetHashCode());
                if (LanguageManager.Instance.LanguageDict.ContainsKey(id))
                {
                    LanguageData languageData = LanguageManager.Instance.LanguageDict[id];
                    outputStr = id + "\n" + JsonConvert.SerializeObject(languageData, Formatting.Indented);
                }
                    
            }

            if (GUILayout.Button("添加"))
            {
                string id = LanguageManager.Instance.RegisterText(inputStr);
                LanguageData languageData = LanguageManager.Instance.LanguageDict[id];
                outputStr = id + "\n" + JsonConvert.SerializeObject(languageData, Formatting.Indented);
                AssetDatabase.Refresh();
            }
        }

        private void RemoveText()
        {
            if (GUILayout.Button("移除"))
            {
                LanguageManager.Instance.RemoveText(inputStr);
                outputStr = "";
                AssetDatabase.Refresh();
            }
        }
    }
}
#endif