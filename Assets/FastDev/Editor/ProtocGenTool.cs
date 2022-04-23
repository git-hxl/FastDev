using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using System.IO;
using Cysharp.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FastDev
{
    public class ProtocGenTool
    {
        private const string protocPath = "./protoc-3.10.1-win64/bin";
        private const string protoPath = protocPath + "/Proto";

        private const string genDestPath = "./Assets/ProtoClass";

        [MenuItem("Assets/转化Proto文件")]
        public static async void Proto2Csharp()
        {
            if (Selection.activeObject == null) return;
            if (!Directory.Exists(protoPath))
            {
                Directory.CreateDirectory(protoPath);
            }
            if (!Directory.Exists(genDestPath))
            {
                Directory.CreateDirectory(genDestPath);
            }
            string filePath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (Directory.Exists(filePath))
            {
                //批量生成
                string[] files = Directory.GetFiles(filePath, "*.proto");
                foreach (var item in files)
                {
                    await GenCsharp(item.Replace("\\", "/"));
                }
            }
            else if (File.Exists(filePath))
            {
                await GenCsharp(filePath);
            }
            AssetDatabase.Refresh();
        }

        private static async UniTask GenCsharp(string filePath)
        {
            string fileName = filePath.GetFileName();
            if (filePath.Contains(".proto"))
            {
                string protoFilePath = protoPath + "/" + fileName;
                File.Copy(filePath, protoFilePath, true);
                List<string> commands = new List<string>();
                string command1 = "cd /d " + Path.GetFullPath(protocPath);
                string command2 = "protoc.exe -IPATH=./ Proto/" + fileName + " --csharp_out=" + Path.GetFullPath(genDestPath);
                commands.Add(command1);
                commands.Add(command2);

                await UniTask.SwitchToThreadPool();
                RunCmd(commands);
                await UniTask.SwitchToMainThread();
                UnityEngine.Debug.Log("gen file:" + filePath);
            }
        }
        private static void RunCmd(List<string> commands)
        {
            ProcessStartInfo info = new ProcessStartInfo("cmd.exe");

            info.CreateNoWindow = true;
            info.ErrorDialog = true;
            info.UseShellExecute = false;

            if (info.UseShellExecute)
            {
                info.RedirectStandardOutput = false;
                info.RedirectStandardError = false;
                info.RedirectStandardInput = false;
            }
            else
            {
                info.RedirectStandardOutput = true;
                info.RedirectStandardError = true;
                info.RedirectStandardInput = true;
                info.StandardErrorEncoding = System.Text.Encoding.UTF8;
                info.StandardOutputEncoding = System.Text.Encoding.UTF8;
            }
            Process process = Process.Start(info);
            for (int i = 0; i < commands.Count; i++)
            {
                process.StandardInput.WriteLine(commands[i]);
            }
            process.StandardInput.AutoFlush = true;
            //退出命令
            process.StandardInput.WriteLine("exit");
            //如果不执行该命令 程序无法运行
            process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
        }

        [MenuItem("FastDev/批量复制协议到热更")]
        private static void CopyToHotFix()
        {
            string destPath = "./Assets/Hotfix/ProtoClass";

            string tmpStr = @"
namespace Hotfix
{
    $Message
}";
            if (Directory.Exists(genDestPath))
            {
                //批量Copy
                string[] files = Directory.GetFiles(genDestPath, "*.cs");
                foreach (var item in files)
                {
                    string classStr = File.ReadAllText(item);
                    Regex regex = new Regex(@"IMessage<(.*?)>", RegexOptions.None);
                    classStr = regex.Replace(classStr, "IMessage");

                    regex = new Regex(@"MessageParser<.*?>", RegexOptions.None);
                    classStr = regex.Replace(classStr, "MessageParser");

                    regex = new Regex(@"new.*?MessageParser.*?$", RegexOptions.Multiline);
                    classStr = regex.Replace(classStr, "null;");

                    string startTag = "#region Designer generated code";
                    string endTag = "#endregion Designer generated code";
                    int startIndex = classStr.IndexOf(startTag);
                    int endIndex = classStr.IndexOf(endTag);
                    string message = @classStr.Substring(startIndex, endIndex + endTag.Length - startIndex);

                    if (!Directory.Exists(destPath))
                        Directory.CreateDirectory(destPath);

                    File.WriteAllText(destPath + "/" + Path.GetFileName(item), classStr);
                }

                AssetDatabase.Refresh();
            }
        }
    }
}