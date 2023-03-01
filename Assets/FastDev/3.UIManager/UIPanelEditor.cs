#if UNITY_EDITOR
using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FastDev
{
    public class UIPanelEditor
    {
        private enum UIElementType
        {
            TextMeshProUGUI,
            Text,
            RawImage,
            Button,
            Toggle,
            Slider,
            Scrollbar,
            ScrollRect,
            Dropdown,
            InputField,
            Image,
            RectTransform
        }

        private const string EditorToolName = "Assets/UI ����UIPanel.cs";
        private const string tag = "_";

        private static string GetVarName(UIElementType uIElementType)
        {
            switch (uIElementType)
            {
                case UIElementType.TextMeshProUGUI:
                    return "TxtPro";
                case UIElementType.Text:
                    return "Txt";
                case UIElementType.RawImage:
                    return "RImg";
                case UIElementType.Button:
                    return "Bt";
                case UIElementType.Toggle:
                    return "Tg";
                case UIElementType.Scrollbar:
                    return "Scrbar";
                case UIElementType.ScrollRect:
                    return "ScrRect";
                case UIElementType.Dropdown:
                    return "Dd";
                case UIElementType.InputField:
                    return "Input";
                case UIElementType.Image:
                    return "Img";
                case UIElementType.RectTransform:
                    return "Rect";
                case UIElementType.Slider:
                    return "Slider";
                default:
                    return "";
            }
        }

        public static string classStr =
@"using FastDev;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class $���� : UIPanel
{
    #region UIAttribute
    $UIAttribute
    #endregion UIAttribute
}";
        [MenuItem(EditorToolName, true)]
        private static bool ValidateFunc()
        {
            GameObject obj = Selection.activeGameObject;
            return (obj != null && obj.GetComponentInChildren<Canvas>() != null);
        }

        [MenuItem(EditorToolName)]
        public static void Create()
        {
            GameObject obj = Selection.activeGameObject;
            string className = obj.name.Replace(" ", "");
            string objPath = AssetDatabase.GetAssetPath(obj);
            string objDir = objPath.Substring(0, objPath.LastIndexOf('/'));
            string filePath = $"{objDir}/{className}.cs";
            if (File.Exists(filePath))
            {
                if (EditorUtility.DisplayDialog("CreateUIPanel", "�Ѵ���ͬ����,�Ƿ񸲸��Զ����ɲ��֣�", "��", "��"))
                {
                    classStr = File.ReadAllText(filePath);
                }
                else
                {
                    return;
                }
            }
            classStr = classStr.Replace("$����", className);
            string startTag = "#region UIAttribute\r\n";
            string endTag = "#endregion UIAttribute";
            int startIndex = classStr.IndexOf(startTag);
            int endIndex = classStr.IndexOf(endTag);
            string replaceStr = classStr.Substring(startIndex + startTag.Length, endIndex - startIndex - startTag.Length);
            classStr = classStr.Replace(replaceStr, CreatVariables(obj));
            using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                byte[] data = Encoding.UTF8.GetBytes(classStr);
                stream.Write(data, 0, data.Length);
            }
            AssetDatabase.Refresh();
            EditorPrefs.SetString("CreateUIPanel", className);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="obj"></param>
        public static string CreatVariables(GameObject obj)
        {
            string variables = "\t";//�Ʊ��� 
            Transform[] transforms = obj.GetComponentsInChildren<Transform>(true);
            foreach (var item in transforms)
            {
                if (!item.name.Contains(tag))
                    continue;
                foreach (var type in Enum.GetNames(typeof(UIElementType)))
                {
                    Component component = item.GetComponent(type);
                    if (component != null)
                    {
                        string varStr = "private $typeName $varName;\r\n\t";
                        string attrStr = "public $typeName $attrName { get { if ($varName == null) { $varName = transform.Find(\"$path\").GetComponent<$typeName>(); } return $varName; } }\r\n\t";
                        string typeName = component.GetType().Name;

                        string attrName = $"{GetVarName((UIElementType)Enum.Parse(typeof(UIElementType), type))}{component.gameObject.name}".ToAlphaNumber();
                        string varName = char.ToLower(attrName[0]) + attrName.Substring(1);
                        string path = component.transform.GetRouteNoRoot();

                        varStr = varStr.Replace("$typeName", typeName).Replace("$varName", varName);
                        attrStr = attrStr.Replace("$typeName", typeName).Replace("$varName", varName).Replace("$attrName", attrName).Replace("$path", path);

                        variables += varStr += attrStr;
                        break;
                    }
                }
            }
            return variables;
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        public static void OnScriptCompleted()
        {
            string className = EditorPrefs.GetString("CreateUIPanel", "");
            GameObject obj = Selection.activeGameObject;
            if (!string.IsNullOrEmpty(className) && obj != null)
            {
                Debug.Log($"create {className} successed");
                EditorPrefs.DeleteKey("CreateUIPanel");
                Type type = Type.GetType(className);
                if (type != null && obj.GetComponent(type) == null)
                    obj.AddComponent(type);
            }
        }
    }
}
#endif