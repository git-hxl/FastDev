using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FastDev.Editor
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

        private const string EditorToolName = "Assets/UI 生成UIPanel.cs";
        private const char tag = '_';

        private static string GetVarName(string typeName)
        {
            UIElementType uIElementType = Enum.Parse<UIElementType>(typeName);

            switch (uIElementType)
            {
                case UIElementType.TextMeshProUGUI:
                    return "Txt";
                case UIElementType.Text:
                    return "Txt";
                case UIElementType.RawImage:
                    return "RImg";
                case UIElementType.Button:
                    return "Bt";
                case UIElementType.Toggle:
                    return "Tg";
                case UIElementType.Scrollbar:
                    return "Scr";
                case UIElementType.ScrollRect:
                    return "ScrR";
                case UIElementType.Dropdown:
                    return "Drop";
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
@"
using FastDev;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class $类名 : UIPanel
{
    #region
    $UIAttribute
    #endregion
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
            string filePath = $"./{objDir}/{className}.cs";

            string[] filesGUIDs = AssetDatabase.FindAssets($"{className} t:Script");

            if (filesGUIDs != null && filesGUIDs.Length > 0)
            {
                filePath = AssetDatabase.GUIDToAssetPath(filesGUIDs[0]);
            }

            if (File.Exists(filePath))
            {
                if (EditorUtility.DisplayDialog("CreateUIPanel", "已存在同名类,是否覆盖自动生成部分？", "确定", "取消"))
                {
                    classStr = File.ReadAllText(filePath);
                }
                else
                {
                    return;
                }
            }
            classStr = classStr.Replace("$类名", className);
            string startTag = "#region\r\n";
            string endTag = "#endregion";
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
        }
        /// <summary>
        /// 创建变量
        /// </summary>
        /// <param name="obj"></param>
        public static string CreatVariables(GameObject obj)
        {
            string variables = "\t";//制表符 
            Transform[] transforms = obj.GetComponentsInChildren<Transform>(true);
            foreach (var item in transforms)
            {
                if (item.name[0] != tag)
                    continue;
                foreach (var type in Enum.GetNames(typeof(UIElementType)))
                {
                    Component component = item.GetComponent(type);
                    if (component != null)
                    {
                        string varStr = "private $typeName $varName;\r\n\t";
                        string attrStr = "public $typeName $attrName { get { if ($varName == null) { $varName = transform.Find(\"$path\").GetComponent<$typeName>(); } return $varName; } }\r\n\t";
                        string typeName = component.GetType().Name;

                        string attrName = $"{GetVarName(type)}{tag}{component.gameObject.name.Split(tag)[1]}";

                        //移除空格
                        attrName = attrName.Replace(" ", "");

                        string varName = component.gameObject.name.ToLower();

                        string path = Utility.Transform.GetRouteNoRoot(component.transform);

                        varStr = varStr.Replace("$typeName", typeName).Replace("$varName", varName);
                        attrStr = attrStr.Replace("$typeName", typeName).Replace("$varName", varName).Replace("$attrName", attrName).Replace("$path", path);

                        variables += varStr += attrStr;
                        break;
                    }
                }
            }
            return variables;
        }
    }
}