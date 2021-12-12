using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEditor;
using System.IO;
using Cysharp.Threading.Tasks;
namespace Bigger
{
    public class ProtocGenTool
    {
        [MenuItem("Assets/转化Proto文件")]
        public static async void Proto2Csharp()
        {
            if (Selection.activeObject == null) return;
            string filePath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string fileName = filePath.GetFileName();
            if (filePath.Contains(".proto"))
            {
                string projectPath = Directory.GetParent(Application.dataPath).ToString();
                string protocPath = projectPath + "/protoc-3.10.1-win64/bin/protoc.exe";
                string destPath = projectPath + "/protoc-3.10.1-win64/bin/Proto";
                string genCsharpPath = Application.dataPath + "/ProtoClass";
                if (!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                }
                if (!Directory.Exists(genCsharpPath))
                {
                    Directory.CreateDirectory(genCsharpPath);
                }

                File.Copy(filePath, destPath + "/" + fileName, true);

                List<string> commands = new List<string>();
                string command1 = "cd /d " + Directory.GetParent(protocPath);
                string command2 = "protoc.exe --proto_path=./ Proto/" + fileName + " --csharp_out=" + genCsharpPath;
                commands.Add(command1);
                commands.Add(command2);

                await UniTask.SwitchToThreadPool();
                RunCmd(commands);
                await UniTask.SwitchToMainThread();
                string genFile = genCsharpPath + "/" + fileName;
                UnityEngine.Debug.Log("gen file:" + genFile);
                AssetDatabase.Refresh();
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
    }
}
