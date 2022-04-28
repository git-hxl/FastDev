using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev.Editor
{
    public static class GenScriptHelper
    {
        //protoc exe 位置
        public const string protocPath = "./protoc-3.10.1-win64/bin";
        //proto 协议文件位置
        public const string protoPath = protocPath + "/Proto";
        //生成的Csharp协议位置
        public const string genCsharpDestPath = "./Assets/ProtoClass";
        //用于热更的Csharp协议位置
        public const string genCsharpHotfixDestPath = "./Assets/ProtoClass Hotfix";

        public const string genCommonScriptPath = "./Assets/Generated";

        //多语言Json生成位置
        public static string multiLanguagePath = "./Assets/Resources/MultiLanguage.json";
    }

}
