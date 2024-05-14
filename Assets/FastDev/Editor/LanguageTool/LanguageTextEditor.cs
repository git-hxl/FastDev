using UnityEngine;
using UnityEditor;
using System.Data;
using System;
using System.IO;

namespace FastDev.Editor
{
    [CustomEditor(typeof(LanguageText))]
    public class LanguageTextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            LanguageText languageText = (LanguageText)target;

            EditorGUILayout.LabelField(languageText.ID.ToString());

            if (GUILayout.Button("注册多语言"))
            {
                languageText.ID = RegisterText(languageText.GetText());
            }
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

            string id = LanguageTool.GetID(inputStr);

            string filePath = Application.streamingAssetsPath + "/MultiLanguage.xlsx";

            if (!File.Exists(filePath))
            {
                Debug.LogError("多语言路径不存在");
                return "";
            }

            DataTable dataTable = Utility.Excel.ReadExcelSheet(filePath);

            foreach (DataRow row in dataTable.Rows)
            {
                if (row[0].ToString() == id)
                {
                    Debug.Log("ID 已存在");
                    return id;
                }
            }

            DataTable addContent = new DataTable();
            addContent.Columns.Add();
            addContent.Columns.Add();

            var newRow = addContent.NewRow();
            newRow[0] = id;
            newRow[1] = inputStr;
            addContent.Rows.Add(newRow);

            Utility.Excel.WriteToExcel(filePath, 1, addContent);
            AssetDatabase.Refresh();
            return id;

        }
    }
}