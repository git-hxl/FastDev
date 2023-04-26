using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.Linq;
namespace Framework.Editor
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
                LanguageData languageData = LanguageManager.Instance.LanguageDatas.FirstOrDefault(x => x.Chinese == inputStr);
                outputStr = JsonConvert.SerializeObject(languageData, Formatting.Indented);
            }
            if (GUILayout.Button("模糊查询"))
            {
                var selectResult = from item in LanguageManager.Instance.LanguageDatas
                                   where item.Chinese.Contains(inputStr)
                                   select item;
                outputStr = JsonConvert.SerializeObject(selectResult, Formatting.Indented);
            }
            if (GUILayout.Button("添加"))
            {
                LanguageData languageData = LanguageManager.Instance.RegisterText(inputStr);
                outputStr = JsonConvert.SerializeObject(languageData, Formatting.Indented);
                AssetDatabase.Refresh();
            }
        }

        private void RemoveText()
        {
            if (GUILayout.Button("移除"))
            {
                LanguageData languageData = LanguageManager.Instance.LanguageDatas.FirstOrDefault((a) => a.Chinese == inputStr);
                LanguageManager.Instance.RemoveLanguageData(languageData);
                outputStr = "";
                AssetDatabase.Refresh();
            }
        }
    }
}