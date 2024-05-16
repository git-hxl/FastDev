using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.Linq;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;

namespace FastDev.Editor
{
    public class LanguageTool : EditorWindow
    {
        private string inputStr = "";
        private string outputStr = "";

        private Vector2 scrollPos;

        public static string filePath { private set; get; }
        public static string filePathJson { private set; get; }

        public static DataTable LanguageDataTable { private set; get; }

        [MenuItem("Tools/多语言工具")]
        public static void OpenWindow()
        {
            LanguageTool window = (LanguageTool)EditorWindow.GetWindow(typeof(LanguageTool), false, "Language");
            window.Show();
        }

        private void OnEnable()
        {
            ReadLocalExcel();
        }

        private void ReadLocalExcel()
        {
            filePath = Application.streamingAssetsPath + "/MultiLanguage.xlsx";
            filePathJson = Application.streamingAssetsPath + "/MultiLanguage.json";

            if (!File.Exists(filePath))
            {
                DataTable initTable = new DataTable();
                DataRow dataRow = initTable.NewRow();

                initTable.Rows.Add(dataRow);

                var languageTypes = Enum.GetValues(typeof(LanguageType));
                initTable.Columns.Add("ID", typeof(string));
                dataRow[0] = "ID";
                for (int i = 0; i < languageTypes.Length; i++)
                {
                    string language = languageTypes.GetValue(i).ToString();
                    initTable.Columns.Add(language, typeof(string));
                    dataRow[i + 1] = language;
                }
                Utility.Excel.CreateExcel(filePath, "MultiLanguage", initTable);
            }

            LanguageDataTable = Utility.Excel.ReadExcelAllSheets(filePath)[0];

            Debug.Log("读取多语言表：" + JsonConvert.SerializeObject(LanguageDataTable, Formatting.Indented));
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("输入中文文本", MessageType.Info);
            inputStr = GUILayout.TextArea(inputStr);


            GUIQueryText();

            if (!string.IsNullOrEmpty(outputStr))
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos);
                GUILayout.TextArea(outputStr);
                GUILayout.EndScrollView();
            }
        }

        private void GUIQueryText()
        {
            if (GUILayout.Button("查询"))
            {
                outputStr = QueryText();
            }

            if (GUILayout.Button("模糊查询"))
            {
                outputStr = FuzzyQueryText();
            }

            if (GUILayout.Button("添加"))
            {
                string id = RegisterText(inputStr);
                outputStr = id;
            }

            if (GUILayout.Button("移除"))
            {
                outputStr = RemoveText(inputStr).ToString();
            }

            if (GUILayout.Button("生成Json"))
            {
                SaveToJson();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        private string QueryText()
        {
            string id = GetID(inputStr);
            foreach (DataRow row in LanguageDataTable.Rows)
            {
                if (row[0].ToString() == id)
                {
                    return JsonConvert.SerializeObject(row.ItemArray, Formatting.Indented);
                }
            }

            return "";
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <returns></returns>
        private string FuzzyQueryText()
        {
            List<object[]> datas = new List<object[]> { };
            foreach (DataRow row in LanguageDataTable.Rows)
            {
                if (row[1].ToString().Contains(inputStr))
                {
                    datas.Add(row.ItemArray);
                }
            }

            return JsonConvert.SerializeObject(datas, Formatting.Indented);
        }

        /// <summary>
        /// 注册文本
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        private string RegisterText(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
                return null;

            string id = GetID(inputStr);

            if (LanguageDataTable == null)
            {
                ReadLocalExcel();
            }

            foreach (DataRow row in LanguageDataTable.Rows)
            {
                if (row[0].ToString() == id)
                {
                    Debug.Log("ID 已存在");
                    return id;
                }
            }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add();
            dataTable.Columns.Add();

            var newRow = dataTable.NewRow();
            newRow[0] = id;
            newRow[1] = inputStr;
            dataTable.Rows.Add(newRow);

            var newRow2 = LanguageDataTable.Rows.Add();
            newRow2[0] = id;
            newRow2[1] = inputStr;

            Utility.Excel.WriteToExcel(filePath, 1, dataTable);
            AssetDatabase.Refresh();
            return id;

        }

        /// <summary>
        /// 移除文本
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        private bool RemoveText(string inputStr)
        {
            string id = GetID(inputStr);

            int index = 0;
            foreach (DataRow row in LanguageDataTable.Rows)
            {
                if (row[0].ToString() == id)
                {
                    Utility.Excel.DeleteExcelRow(filePath, 1, index + 1);

                    LanguageDataTable.Rows.Remove(row);

                    Debug.Log($"{id} 移除成功");

                    AssetDatabase.Refresh();
                    return true;
                }

                index++;
            }

            return false;
        }

        /// <summary>
        /// 保存成Json
        /// </summary>
        private void SaveToJson()
        {
            string json = JsonConvert.SerializeObject(Utility.Excel.SelectContentWithoutConvertType(LanguageDataTable, 1), Formatting.Indented);

            using (FileStream stream = new FileStream(filePathJson, FileMode.Create, FileAccess.ReadWrite))
            {
                byte[] data = Encoding.UTF8.GetBytes(json);
                stream.Write(data, 0, data.Length);

                Debug.Log("生成成功");
            }
        }

        public static string GetID(string text)
        {
            return string.Format("{0:X}", text.GetHashCode());
        }
    }
}