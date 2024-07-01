using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Excel2Json;
using Vector2 = UnityEngine.Vector2;


namespace FastDev.Editor
{
    public class ExcelEditor : EditorWindow
    {
        private ExcelEditorConfig config;

        private Vector2 scrollPos;
        private List<string> ExcelSheets;
        private List<string> ExcelSheetsSelected;


        private ExcelTool excelTool;
        [MenuItem("Tools/ExcelTool")]
        public static void OpenWindow()
        {
            ExcelEditor window = (ExcelEditor)EditorWindow.GetWindow(typeof(ExcelEditor), false, "ExcelTool");
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
            config = new ExcelEditorConfig();
            if (File.Exists(ExcelEditorConfig.ConfigPath))
            {
                string configTxt = File.ReadAllText(ExcelEditorConfig.ConfigPath);
                if (!string.IsNullOrEmpty(configTxt) && configTxt != "null")
                    config = JsonConvert.DeserializeObject<ExcelEditorConfig>(configTxt);
            }

            ExcelToolConfig toolConfig = new ExcelToolConfig();

            toolConfig.StartHead = config.StartHead;
            toolConfig.InputExcelDir = config.InputExcelDir;
            toolConfig.OutputJsonDir = config.OutputJsonDir;
            toolConfig.OutputCSDir = config.OutputCSDir;

            excelTool = new ExcelTool(toolConfig);
        }

        private void DrawWindow()
        {
            EditorGUILayout.HelpBox($"从第{config.StartHead}行开始读数据", MessageType.Info);

            GUILayout.Label("当前ContentRow:" + config.StartHead.ToString());

            config.StartHead = EditorGUILayout.IntSlider(config.StartHead, 2, 10);

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

            //DrawExcelFiles();

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
            excelTool.ExportToJsonFile();
            Debug.Log("导出成功");
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 导出成CS
        /// </summary>
        private void ExportToCSFile()
        {
            excelTool.ExportToCSFile();
            Debug.Log("导出成功");
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void SaveConfig()
        {
            string jsonTxt = JsonConvert.SerializeObject(config);

            File.WriteAllText(ExcelEditorConfig.ConfigPath, jsonTxt);
        }

        private void OnDisable()
        {
            SaveConfig();
        }
    }
}