using UnityEngine;
using UnityEditor;
using System.IO;
namespace Bigger
{
    public class CreateUIPanel
    {
        public static string classStr =
@"namespace Bigger
{
    public class $类名 : UIPanel
    {

    }
}";
        [MenuItem("Bigger/Tools/Script/CreateUIPanel")]
        [MenuItem("Assets/CreateUIPanel")]
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
            if (!File.Exists(filePath))
            {
                classStr = classStr.Replace("$类名", className);
                File.WriteAllText(filePath, classStr);
                AssetDatabase.Refresh();
            }
        }
    }
}