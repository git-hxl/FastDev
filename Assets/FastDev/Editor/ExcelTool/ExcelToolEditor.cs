using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FastDev.Editor
{
    public class ExcelToolEditor : EditorWindow
    {
        private ExcelToolSetting setting;
        private string outStr;

        private Vector2 scrollPos;

        private List<string> ExcelSheets;

        private List<string> ExcelSheetsSelected;

        [MenuItem("Tools/ExcelToJson工具")]
        public static void OpenWindow()
        {
            ExcelToolEditor window = (ExcelToolEditor)EditorWindow.GetWindow(typeof(ExcelToolEditor), false, "ExcelToJson");
            window.Show();
        }

        private void OnEnable()
        {
            setting = new ExcelToolSetting();
            if (File.Exists(ExcelToolSetting.SettingPath))
            {
                string settingTxt = File.ReadAllText(ExcelToolSetting.SettingPath);
                setting = JsonConvert.DeserializeObject<ExcelToolSetting>(settingTxt);
            }

            ExcelSheets = new List<string>();
            ExcelSheetsSelected = new List<string>();
            ReadAllExcel();
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("第1行固定为字段名 第2行为类型，从Head开始读数据", MessageType.Info);

            GUILayout.Label("当前Head:" + setting.Head.ToString());
            setting.Head = EditorGUILayout.IntSlider(setting.Head, 1, 10);

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
            GUILayout.Label(outStr);

            DrawExcelFiles();

            //刷新
            if (GUILayout.Button("刷新"))
                RefreshAllExcelFiles();
            if (GUILayout.Button("全选/取消全选"))
                SelectAll();
            if (GUILayout.Button("导出成Json"))
                ExportSelectedExcelToJsonFile();
        }

        private void DrawExcelFiles()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            GUILayout.Box("Excel Files");
            int column = 5;//列
            int row = ExcelSheets.Count / column + 1;//行
            GUILayout.BeginVertical();
            for (int i = 0; i < row; i++)
            {
                GUILayout.BeginHorizontal();
                for (int j = i * column; j < (i + 1) * column; j++)
                {
                    if (j < ExcelSheets.Count)
                    {
                        if (GUILayout.Toggle(ExcelSheetsSelected.Contains(ExcelSheets[j]), Path.GetFileNameWithoutExtension(ExcelSheets[j])))
                        {
                            if (!ExcelSheetsSelected.Contains(ExcelSheets[j]))
                            {
                                ExcelSheetsSelected.Add(ExcelSheets[j]);
                            }
                        }
                        else
                        {
                            if (ExcelSheetsSelected.Contains(ExcelSheets[j]))
                            {
                                ExcelSheetsSelected.Remove(ExcelSheets[j]);
                            }
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            GUILayout.EndScrollView();
        }

        private void RefreshAllExcelFiles()
        {
            ExcelSheets.Clear();
            ExcelSheetsSelected.Clear();
            ReadAllExcel();
        }

        private void ReadAllExcel()
        {
            string[] fileExtensions = new string[] { ".xls", ".xlsx" };

            if (!string.IsNullOrEmpty(setting.InputExcelDir) && Directory.Exists(setting.InputExcelDir))
            {
                string[] excelFiles = Directory.GetFiles(setting.InputExcelDir).Where(file => fileExtensions.Contains(Path.GetExtension(file)))
                .ToArray();

                ExcelSheets.AddRange(excelFiles);
                ExcelSheetsSelected.AddRange(excelFiles);
            }
        }

        private void SelectAll()
        {
            if (ExcelSheetsSelected.Count > 0)
                ExcelSheetsSelected.Clear();
            else
                ExcelSheetsSelected.AddRange(ExcelSheets);
        }

        private void ExportSelectedExcelToJsonFile()
        {
            foreach (var file in ExcelSheetsSelected)
            {
                var tables = ExcelHelper.ReadExcelAllSheets(file);

                foreach (DataTable table in tables)
                {
                    if (table.Rows.Count > 0)
                    {
                        var newTable = table.SelectContent(setting.Head - 1);
                        string json = JsonConvert.SerializeObject(newTable, Formatting.Indented);
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
            Debug.Log("导出成功");
            AssetDatabase.Refresh();
        }


        private void OnDisable()
        {
            string jsonTxt = JsonConvert.SerializeObject(setting);

            File.WriteAllText(ExcelToolSetting.SettingPath, jsonTxt);
        }
    }
}