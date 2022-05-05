using UnityEditor;
using System.IO;
using System.Text;

namespace FastDev.Editor
{
    public class GenABConstant
    {
        public static string classStr =
@"namespace FastDev.Res
{
    public static class ABConstant
    {
        $变量
    }
}";
        [MenuItem("Assets/生成AB资源的定义文件")]
        public static void Create()
        {
            string s = "";
            foreach (var item in AssetDatabase.GetAllAssetBundleNames())
            {
                s += $"public const string {item} = \"{item}\";\r\n\t\t";
            }
            classStr = classStr.Replace("$变量", s);
            using (FileStream stream = new FileStream(GenScriptHelper.genCommonScriptPath + "/ABConstant.cs", FileMode.Create, FileAccess.ReadWrite))
            {
                byte[] data = Encoding.UTF8.GetBytes(classStr);
                stream.Write(data, 0, data.Length);
            }
            AssetDatabase.Refresh();
        }
    }
}