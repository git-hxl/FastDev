using UnityEngine;
using UnityEditor;
using System.IO;
namespace Bigger
{
    public class CreatBundleConstant
    {
        public static string classStr =
@"namespace Bigger
{
    public static class BundleConstant
    {
        $变量
    }
}";
        [MenuItem("Bigger/Tools/Script/CreateBundleConstant")]
        public static void Create()
        {
            string s = "";
            foreach (var item in AssetDatabase.GetAllAssetBundleNames())
            {
                s += $"public const string {item} = \"{item}\";\r\n\t\t";
            }
            classStr = classStr.Replace("$变量", s);
            File.WriteAllText($"{Application.dataPath}/Bigger/1.Res/BundleConstant.cs", classStr);
            AssetDatabase.Refresh();
        }
    }
}