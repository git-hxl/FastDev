#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.Linq;
namespace FastDev
{
    public class LanguageToolEditor : EditorWindow
    {
        private string inputStr = "";
        private string outputStr = "";

        private Vector2 scrollPos;

        [MenuItem("FastDev/多语言工具")]
        public static void OpenWindow()
        {
            LanguageToolEditor window = (LanguageToolEditor)EditorWindow.GetWindow(typeof(LanguageToolEditor), false, "Language");
            window.Show();
        }
        private void OnGUI()
        {
            EditorGUILayout.HelpBox("输入中文文本", MessageType.Info);
            inputStr = GUILayout.TextArea(inputStr);

            GUISelectText();

            RemoveText();

            if (!string.IsNullOrEmpty(outputStr))
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos);
                GUILayout.TextArea(outputStr);
                GUILayout.EndScrollView();
            }
        }

        private void GUISelectText()
        {
            if (GUILayout.Button("查询"))
            {
                string id = string.Format("{0:X}", inputStr.GetHashCode());
                var item = LanguageManager.Instance.LanguageDict.FirstOrDefault(x => x.Key == id);
                outputStr = JsonConvert.SerializeObject(item, Formatting.Indented);
            }
            if (GUILayout.Button("添加"))
            {
                string id = LanguageManager.Instance.RegisterText(inputStr);
                var item = LanguageManager.Instance.LanguageDict.FirstOrDefault(x => x.Key == id);
                outputStr = JsonConvert.SerializeObject(item, Formatting.Indented);
                AssetDatabase.Refresh();
            }
            if (GUILayout.Button("模糊查询"))
            {
                var selectResult = from item in LanguageManager.Instance.LanguageDict
                                   where item.Value.Chinese.Contains(inputStr)
                                   select item;
                outputStr = JsonConvert.SerializeObject(selectResult, Formatting.Indented);
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