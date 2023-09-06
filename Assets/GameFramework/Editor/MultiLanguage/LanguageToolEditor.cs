using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.Linq;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System;

namespace GameFramework.Editor
{
    public class LanguageToolEditor : EditorWindow
    {
        private string inputStr = "";
        private string outputStr = "";

        private Vector2 scrollPos;

        public static string filePath { private set; get; }
        public static DataTable LanguageDataTable { private set; get; }

        [MenuItem("Tools/多语言工具")]
        public static void OpenWindow()
        {
            LanguageToolEditor window = (LanguageToolEditor)EditorWindow.GetWindow(typeof(LanguageToolEditor), false, "Language");
            window.Show();
        }

        private void OnEnable()
        {
            InitLanguageData();
        }

        private static void InitLanguageData()
        {
            filePath = Application.streamingAssetsPath + "/MultiLanguage.xlsx";
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
                ExcelHelper.CreateExcel(filePath, "MultiLanguage", initTable);
            }

            LanguageDataTable = ExcelHelper.ReadExcelAllSheets(filePath, true, 2)[0];
            Debug.Log("读取多语言表：" + JsonConvert.SerializeObject(LanguageDataTable, Formatting.Indented));
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("输入中文文本", MessageType.Info);
            inputStr = GUILayout.TextArea(inputStr);

            GUIQueryText();

            RemoveText();

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
                string id = GetID(inputStr);
                foreach (DataRow row in LanguageDataTable.Rows)
                {
                    if (row[0].ToString() == id)
                    {
                        outputStr = JsonConvert.SerializeObject(row.ItemArray, Formatting.Indented);
                    }
                }
            }
            if (GUILayout.Button("模糊查询"))
            {
                List<object[]> datas = new List<object[]> { };
                foreach (DataRow row in LanguageDataTable.Rows)
                {
                    if (row[1].ToString().Contains(inputStr))
                    {
                        datas.Add(row.ItemArray);
                        outputStr = JsonConvert.SerializeObject(datas, Formatting.Indented);
                    }
                }
            }
            if (GUILayout.Button("添加"))
            {
                string id = RegisterText(inputStr);
                outputStr = id;
            }
        }

        private void RemoveText()
        {
            if (GUILayout.Button("移除"))
            {
                outputStr = RemoveLanguageData(inputStr).ToString();
            }
        }

        public static string RegisterText(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
                return null;

            string id = GetID(inputStr);

            if (LanguageDataTable == null)
            {
                InitLanguageData();
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
            ExcelHelper.WriteToExcel(filePath, 1, dataTable);

            AssetDatabase.Refresh();
            return id;

        }

        public static bool RemoveLanguageData(string inputStr)
        {
            string id = GetID(inputStr);

            int index = 0;
            foreach (DataRow row in LanguageDataTable.Rows)
            {
                index++;
                if (row[0].ToString() == id)
                {
                    //这里需要过滤首两行
                    ExcelHelper.DeleteExcelRow(filePath, 1, index + 2);
                    AssetDatabase.Refresh();
                    return true;
                }
            }
            return false;
        }


        public static string GetID(string text)
        {
            return string.Format("{0:X}", text.GetHashCode());
        }
    }
}