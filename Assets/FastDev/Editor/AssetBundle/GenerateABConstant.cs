using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace FastDev
{
    public class GenerateABConstant
    {
        public static string classStr =
@"namespace FastDev.Res
{
    public static class ABConstant
    {
        $变量
    }
}";
        [MenuItem("FastDev/生成ABConstant.cs",false,0)]
        public static void Create()
        {
            string s = "";
            foreach (var item in AssetDatabase.GetAllAssetBundleNames())
            {
                s += $"public const string {item} = \"{item}\";\r\n\t\t";
            }
            classStr = classStr.Replace("$变量", s);
            File.WriteAllText($"{Application.dataPath}/FastDev/Runtime/Res/ABConstant.cs", classStr);
            AssetDatabase.Refresh();
        }
    }
}