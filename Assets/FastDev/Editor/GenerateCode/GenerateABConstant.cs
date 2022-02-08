using UnityEngine;
using UnityEditor;
using System.IO;
namespace FastDev
{
    public class GenerateABConstant
    {
        public static string classStr =
@"namespace Bigger
{
    public static class ABConstant
    {
        $变量
    }
}";
        [MenuItem("Assets/生成ABConstant.cs")]
        public static void Create()
        {
            string s = "";
            foreach (var item in AssetDatabase.GetAllAssetBundleNames())
            {
                s += $"public const string {item} = \"{item}\";\r\n\t\t";
            }
            classStr = classStr.Replace("$变量", s);
            File.WriteAllText($"{Application.dataPath}/Bigger/1.Res/ABConstant.cs", classStr);
            AssetDatabase.Refresh();
        }
    }
}