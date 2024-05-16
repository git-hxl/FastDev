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
    public class ExcelTool : EditorWindow
    {
        private ExcelToolConfig config;

        private Vector2 scrollPos;
        private List<string> ExcelSheets;
        private List<string> ExcelSheetsSelected;

        [MenuItem("Tools/ExcelTool")]
        public static void OpenWindow()
        {
            ExcelTool window = (ExcelTool)EditorWindow.GetWindow(typeof(ExcelTool), false, "ExcelTool");
            window.Show();
        }

        private void OnEnable()
        {
            InitConfig();

            ReadAllExcel();
        }

        private void OnGUI()
        {
            DrawWindow();
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        private void InitConfig()
        {
            config = new ExcelToolConfig();
            if (File.Exists(ExcelToolConfig.ConfigPath))
            {
                string configTxt = File.ReadAllText(ExcelToolConfig.ConfigPath);
                if (!string.IsNullOrEmpty(configTxt) && configTxt != "null")
                    config = JsonConvert.DeserializeObject<ExcelToolConfig>(configTxt);
            }
        }

        private void DrawWindow()
        {
            EditorGUILayout.HelpBox($"从第{config.ContentRow}行开始读数据", MessageType.Info);

            GUILayout.Label("当前ContentRow:" + config.ContentRow.ToString());

            config.ContentRow = EditorGUILayout.IntSlider(config.ContentRow, 2, 10);

            GUILayout.Label("Excel路径");
            config.InputExcelDir = GUILayout.TextField(config.InputExcelDir);
            if (GUILayout.Button("选择文件夹"))
            {
                string selectPath = EditorUtility.OpenFolderPanel("输入路径", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selectPath))
                {
                    config.InputExcelDir = selectPath;
                }
            }

            GUILayout.Label("Json路径");
            config.OutputJsonDir = GUILayout.TextField(config.OutputJsonDir);
            if (GUILayout.Button("选择文件夹"))
            {
                string selectPath = EditorUtility.OpenFolderPanel("输出路径", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selectPath))
                {
                    config.OutputJsonDir = selectPath;
                }
            }

            GUILayout.Label("CS路径");
            config.OutputCSDir = GUILayout.TextField(config.OutputCSDir);
            if (GUILayout.Button("选择文件夹"))
            {
                string selectPath = EditorUtility.OpenFolderPanel("输出路径", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selectPath))
                {
                    config.OutputCSDir = selectPath;
                }
            }

            DrawExcelFiles();

            //刷新
            if (GUILayout.Button("刷新"))
            {
                ReadAllExcel();
            }

            if (GUILayout.Button("全选/取消全选"))
            {
                SelectAll();
            }

            if (GUILayout.Button("导出成Json"))
            {
                ExportToJsonFile();
            }

            if (GUILayout.Button("导出成CS"))
            {
                ExportToCSFile();
            }

        }

        /// <summary>
        /// 绘制Excel列表
        /// </summary>
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

        /// <summary>
        /// 读取所有的Excel
        /// </summary>
        private void ReadAllExcel()
        {
            ExcelSheets = new List<string>();
            ExcelSheetsSelected = new List<string>();

            string[] fileExtensions = new string[] { ".xls", ".xlsx" };

            if (!string.IsNullOrEmpty(config.InputExcelDir) && Directory.Exists(config.InputExcelDir))
            {
                string[] excelFiles = Directory.GetFiles(config.InputExcelDir).Where(file => fileExtensions.Contains(Path.GetExtension(file)))
                .ToArray();

                ExcelSheets.AddRange(excelFiles);
                ExcelSheetsSelected.AddRange(excelFiles);
            }
        }

        /// <summary>
        /// 全选/取消全选
        /// </summary>
        private void SelectAll()
        {
            if (ExcelSheetsSelected.Count > 0)
                ExcelSheetsSelected.Clear();
            else
                ExcelSheetsSelected.AddRange(ExcelSheets);
        }

        /// <summary>
        /// 导出成Json
        /// </summary>
        private void ExportToJsonFile()
        {
            foreach (var file in ExcelSheetsSelected)
            {
                var tables = Utility.Excel.ReadExcelAllSheets(file);

                foreach (DataTable table in tables)
                {
                    if (table.Rows.Count > 0)
                    {
                        var newTable = Utility.Excel.SelectContent(table, config.ContentRow);

                        string json = JsonConvert.SerializeObject(newTable, Formatting.Indented);
                        if (!string.IsNullOrEmpty(json))
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file) + "_" + table.TableName + ".json";
                            using (FileStream stream = new FileStream(config.OutputJsonDir + "/" + fileName, FileMode.Create, FileAccess.ReadWrite))
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

        /// <summary>
        /// 导出成CS
        /// </summary>
        private void ExportToCSFile()
        {
            foreach (var file in ExcelSheetsSelected)
            {
                var tables = Utility.Excel.ReadExcelAllSheets(file);

                foreach (DataTable table in tables)
                {
                    if (table.Rows.Count > 0)
                    {
                        ExcelToCS.Generate(table, config.OutputCSDir);
                    }
                }
            }
            Debug.Log("导出成功");
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void SaveConfig()
        {
            string jsonTxt = JsonConvert.SerializeObject(config);

            File.WriteAllText(ExcelToolConfig.ConfigPath, jsonTxt);
        }

        private void OnDisable()
        {
            SaveConfig();
        }
    }
}