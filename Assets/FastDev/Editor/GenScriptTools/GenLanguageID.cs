using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;

namespace FastDev.Editor
{
    public class GenLanguageID
    {
        [MenuItem("Assets/生成多语言ID的定义文件",true)]
        private static bool ValidateFunc()
        {
            return File.Exists(GenScriptHelper.multiLanguagePath);
        }
        [MenuItem("Assets/生成多语言ID的定义文件")]
        static void GenerateMultiLanguageConstant()
        {
            var languageDict = MultiLanguageTool.ReadEditorLanguageJson();
            AutoCreateLanguageConstant(languageDict);
        }

        private static void AutoCreateLanguageConstant(Dictionary<string, LanguageStruct> languageDict)
        {
            string classStr = @"
namespace FastDev
{
    public static class LanguageConstant
    {
        $变量
    }
}";
            string var = "";
            foreach (var item in languageDict)
            {
                string txtTag = item.Value.Chinese;
                int maxLength = 6;
                if (txtTag.Length > maxLength)
                    txtTag = $"{txtTag.Substring(0, maxLength)} 省略 {txtTag.Length - maxLength} 字";
                var += $"public const string {txtTag.ToAlphaNumberAndChinese(false).Replace(" ", "_").Replace("\n", "n")} = \"{item.Key}\";\r\n\t\t";
            }
            classStr = classStr.Replace("$变量", var);
            if (!Directory.Exists(GenScriptHelper.genCommonScriptPath))
                Directory.CreateDirectory(GenScriptHelper.genCommonScriptPath);
            using (FileStream stream = new FileStream(GenScriptHelper.genCommonScriptPath + "/LanguageConstant.cs", FileMode.Create, FileAccess.ReadWrite))
            {
                byte[] data = Encoding.UTF8.GetBytes(classStr);
                stream.Write(data, 0, data.Length);
            }
            AssetDatabase.Refresh();
        }
    }
}