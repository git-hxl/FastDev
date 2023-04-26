using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Text;

namespace Framework.Editor
{
    public class JsonToolEditor : EditorWindow
    {
        private JsonToolSetting setting;
        private string outStr;

        private Vector2 scrollPos;

        [MenuItem("FastDev/Json工具")]
        public static void OpenWindow()
        {
            JsonToolEditor window = (JsonToolEditor)EditorWindow.GetWindow(typeof(JsonToolEditor), false, "Json");
            window.Show();
        }

        private void OnEnable()
        {
            setting = new JsonToolSetting();
            if (File.Exists(JsonToolSetting.SettingPath))
            {
                string settingTxt = File.ReadAllText(JsonToolSetting.SettingPath);
                setting = JsonConvert.DeserializeObject<JsonToolSetting>(settingTxt);
            }
        }

        private void OnGUI()
        {
            GUILayout.Label("Excel路径");
            setting.InputExcelDir = GUILayout.TextField(setting.InputExcelDir);
            if (GUILayout.Button("选择文件夹"))
            {
                string selectPath = EditorUtility.OpenFolderPanel("输入路径", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selectPath))
                {
                    setting.InputExcelDir = selectPath;
                }
            }

            GUILayout.Label("Json路径");
            setting.OutputJsonDir = GUILayout.TextField(setting.OutputJsonDir);
            if (GUILayout.Button("选择文件夹"))
            {
                string selectPath = EditorUtility.OpenFolderPanel("输出路径", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selectPath))
                {
                    setting.OutputJsonDir = selectPath;
                }
            }

            if (GUILayout.Button("执行批处理"))
            {
                RunBat();
            }

            scrollPos = GUILayout.BeginScrollView(scrollPos);
            GUILayout.Label(outStr);
            GUILayout.EndScrollView();
        }

        /// <summary>
        /// 运行批处理文件
        /// </summary>
        private void RunBat()
        {
            string cmdWorkDir = Application.dataPath + "/Framework/Editor/ExcelToJson";
            cmdWorkDir = cmdWorkDir.Replace("/", "\\");

            string excelDir = setting.InputExcelDir.Replace("/", "\\");
            string jsonDir = setting.OutputJsonDir.Replace("/", "\\");
            if (Directory.Exists(cmdWorkDir))
            {
                RunCmd("JsonTool.bat", excelDir + " " + jsonDir, cmdWorkDir).Forget();
            }
        }

        public async UniTaskVoid RunCmd(string cmd, string args, string workingDir = "")
        {
            outStr = "";

            await UniTask.SwitchToThreadPool();

            var process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding("gb2312");
            process.StartInfo.StandardErrorEncoding = Encoding.GetEncoding("gb2312");

            process.StartInfo.WorkingDirectory = workingDir;

            process.Start();

            process.StandardInput.WriteLine(cmd + " " + args);

            process.StandardInput.WriteLine("exit");

            outStr = await process.StandardOutput.ReadToEndAsync();

            await UniTask.WaitUntil(() => process.HasExited);

            await UniTask.SwitchToMainThread();

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        private void OnDisable()
        {
            string jsonTxt = JsonConvert.SerializeObject(setting);

            File.WriteAllText(JsonToolSetting.SettingPath, jsonTxt);

        }
    }
}