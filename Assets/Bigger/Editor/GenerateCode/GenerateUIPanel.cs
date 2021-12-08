using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using System;
namespace Bigger
{
    public class GenerateUIPanel
    {
        public enum UIElementType
        {
            TextMeshProUGUI,
            Text,
            RawImage,
            Button,
            Toggle,
            Slider,
            Scrollbar,
            Dropdown,
            InputField,
            ScrollRect,
            Image,
        }
        public static string classStr =
@"using Bigger;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class $类名 : UIPanel
{
    #region UIAttribute
    $UIAttribute
    #endregion UIAttribute
}";
        [MenuItem("Bigger/自动生成/当前对象的UIPanel.cs")]
        [MenuItem("Assets/自动生成/当前对象的UIPanel.cs")]
        public static void Create()
        {
            GameObject obj = Selection.activeGameObject;
            if (obj == null || obj.GetComponent<Canvas>() == null)
            {
                Debug.LogError("should select a ui!!!");
                return;
            }
            string className = obj.name.Replace(" ", "");
            string objPath = AssetDatabase.GetAssetPath(obj);
            string objDir = objPath.Substring(0, objPath.LastIndexOf('/'));
            string filePath = $"{objDir}/{className}.cs";
            if (File.Exists(filePath))
            {
                if (EditorUtility.DisplayDialog("CreateUIPanel", "已存在同名类,是否覆盖自动生成部分？", "是", "全部覆盖"))
                {
                    classStr = File.ReadAllText(filePath);
                }
            }
            classStr = classStr.Replace("$类名", className);
            string startTag = "#region UIAttribute\r\n";
            string endTag = "#endregion UIAttribute";
            int startIndex = classStr.IndexOf(startTag);
            int endIndex = classStr.IndexOf(endTag);
            string replaceStr = classStr.Substring(startIndex + startTag.Length, endIndex - startIndex - startTag.Length);
            classStr = classStr.Replace(replaceStr, CreatVariables(obj));
            File.WriteAllText(filePath, classStr);
            AssetDatabase.Refresh();
            EditorPrefs.SetString("CreateUIPanel", className);
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
                if (!item.name.Contains("_"))
                    continue;
                foreach (var type in Enum.GetNames(typeof(UIElementType)))
                {
                    Component component = item.GetComponent(type);
                    if (component != null)
                    {
                        string varStr = "private $typeName $varName;\r\n\t";
                        string attrStr = "public $typeName $attrName { get { if ($varName == null) { $varName = transform.Find(\"$path\").GetComponent<$typeName>(); } return $varName; } }\r\n\t";
                        string typeName = component.GetType().Name;
                        string attrName = $"{ component.GetType().Name.ToLower()}{component.gameObject.name}".ToAlphaNumber();
                        string varName = "_" + attrName;
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
                Type type = Assembly.Load("Assembly-CSharp").GetType(className);
                if (type != null && obj.GetComponent(type) == null)
                    obj.AddComponent(type);
            }
        }
    }
}