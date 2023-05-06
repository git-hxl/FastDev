using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace Framework.Editor
{
    public class JsonToolEditor : EditorWindow
    {
        private JsonToolSetting setting;
        private string outStr;

        private Vector2 scrollPos;

        private List<string> ExcelSheets;

        private List<string> ExcelSheetsTmp;

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

            ExcelSheets = new List<string>();
            ExcelSheetsTmp = new List<string>();

            ReadAllExcelFiles();
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("第一行字段名，第二行类型定义，第三行预留", MessageType.Info);
            EditorGUILayout.HelpBox("AutoParse true：根据单元格内容自动识别类型,flase：根据第二行定义类型转化", MessageType.Info);
            setting.AutoParse = GUILayout.Toggle(setting.AutoParse, "AutoParse");

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

            if (GUILayout.Button("刷新"))
            {
                ReadAllExcelFiles();
            }

            if (GUILayout.Button("全选"))
            {
                foreach (string file in ExcelSheets)
                {
                    if (!ExcelSheetsTmp.Contains(file))
                    {
                        ExcelSheetsTmp.Add(file);
                    }
                }
            }
            if (GUILayout.Button("取消全选"))
            {
                ExcelSheetsTmp.Clear();
            }

            if (GUILayout.Button("Generate"))
            {
                GenJsonFile();

                AssetDatabase.Refresh();
            }



            DrawExcelFiles();

            scrollPos = GUILayout.BeginScrollView(scrollPos);
            GUILayout.Label(outStr);
            GUILayout.EndScrollView();
        }


        private void ReadAllExcelFiles()
        {
            if (!string.IsNullOrEmpty(setting.InputExcelDir))
            {
                string[] excelFiles = Directory.GetFiles(setting.InputExcelDir, "*.xlsx", SearchOption.AllDirectories);

                foreach (string file in excelFiles)
                {
                    ExcelSheets.Add(file);
                }
            }

            ExcelSheetsTmp.Clear();
        }

        private void DrawExcelFiles()
        {
            foreach (string file in ExcelSheets)
            {
                if (GUILayout.Toggle(ExcelSheetsTmp.Contains(file), Path.GetFileNameWithoutExtension(file)))
                {
                    if (!ExcelSheetsTmp.Contains(file))
                    {
                        ExcelSheetsTmp.Add(file);
                    }
                }
            }
        }


        private void GenJsonFile()
        {
            foreach (var file in ExcelSheetsTmp)
            {
                var tables = ExcelToJson.ReadTable(file);

                foreach (DataTable table in tables)
                {
                    string json = ExcelToJson.ToJson(table, setting.AutoParse);
                    if (!string.IsNullOrEmpty(json))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file) + "_" + table.TableName + ".json";
                        using (FileStream stream = new FileStream(setting.OutputJsonDir + "/" + fileName, FileMode.Create, FileAccess.ReadWrite))
                        {
                            byte[] data = Encoding.UTF8.GetBytes(json);
                            stream.Write(data, 0, data.Length);
                        }
                    }
                }
            }
        }


        private void OnDisable()
        {
            string jsonTxt = JsonConvert.SerializeObject(setting);

            File.WriteAllText(JsonToolSetting.SettingPath, jsonTxt);

        }
    }
}