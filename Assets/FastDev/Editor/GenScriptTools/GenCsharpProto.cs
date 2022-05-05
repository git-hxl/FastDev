using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using UnityEditor;

namespace FastDev.Editor
{
    public class GenCsharpProto
    {
        [MenuItem("Assets/生成C#协议文件", true)]
        [MenuItem("Assets/生成C#协议文件(支持热更)", true)]
        private static bool ValidateFunc()
        {
            string filePath = AssetDatabase.GetAssetPath(Selection.activeObject);
            return filePath.Contains(".proto") || (Directory.Exists(filePath) && Directory.GetFiles(filePath, "*.proto").Length > 0);
        }

        [MenuItem("Assets/生成C#协议文件", false)]
        private static void Proto2Csharp()
        {
            Proto2Csharp(true).Forget();
        }
        [MenuItem("Assets/生成C#协议文件(支持热更)", false)]
        private static async void Proto2HotfixCsharp()
        {
            await Proto2Csharp(false);
            Proto2HotfixCsharp(true);
        }

        private static async UniTask Proto2Csharp(bool needRefrsh)
        {
            if (Selection.activeObject == null) return;
            if (!Directory.Exists(GenScriptHelper.protoPath))
            {
                Directory.CreateDirectory(GenScriptHelper.protoPath);
            }
            if (!Directory.Exists(GenScriptHelper.genCsharpDestPath))
            {
                Directory.CreateDirectory(GenScriptHelper.genCsharpDestPath);
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
            if (needRefrsh)
                AssetDatabase.Refresh();
        }

        private static async UniTask GenCsharp(string filePath)
        {
            string fileName = filePath.GetFileName();
            if (filePath.Contains(".proto"))
            {
                string protoFilePath = GenScriptHelper.protoPath + "/" + fileName;
                File.Copy(filePath, protoFilePath, true);
                List<string> commands = new List<string>();
                string command1 = "cd /d " + Path.GetFullPath(GenScriptHelper.protocPath);
                string command2 = "protoc.exe -IPATH=./ Proto/" + fileName + " --csharp_out=" + Path.GetFullPath(GenScriptHelper.genCsharpDestPath);
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

        private static void Proto2HotfixCsharp(bool needRefresh)
        {
            if (!Directory.Exists(GenScriptHelper.genCsharpHotfixDestPath))
                Directory.CreateDirectory(GenScriptHelper.genCsharpHotfixDestPath);

            if (Directory.Exists(GenScriptHelper.genCsharpDestPath))
            {
                //批量Copy
                string[] files = Directory.GetFiles(GenScriptHelper.genCsharpDestPath, "*.cs");
                foreach (var item in files)
                {
                    string classStr = File.ReadAllText(item);
                    Regex regex = new Regex(@"IMessage<(.*?)>", RegexOptions.None);
                    classStr = regex.Replace(classStr, "IMessage");

                    regex = new Regex(@"MessageParser<.*?>", RegexOptions.None);
                    classStr = regex.Replace(classStr, "MessageParser");

                    regex = new Regex(@"new.*?MessageParser.*?$", RegexOptions.Multiline);
                    classStr = regex.Replace(classStr, "null;");
                    using (FileStream stream = new FileStream(GenScriptHelper.genCsharpHotfixDestPath + "/" + Path.GetFileName(item), FileMode.Create, FileAccess.ReadWrite))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(classStr);
                        stream.Write(data, 0, data.Length);
                    }
                }
                Directory.Delete(GenScriptHelper.genCsharpDestPath, true);
            }
            if (needRefresh)
                AssetDatabase.Refresh();
        }
    }
}