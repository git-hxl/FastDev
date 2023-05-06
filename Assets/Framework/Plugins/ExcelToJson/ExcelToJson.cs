using ExcelDataReader;
using System.Data;
using System.IO;
using System;
using UnityEngine;
using Newtonsoft.Json;
using Framework.Editor;

public class ExcelToJson
{
    public static DataTableCollection ReadTable(string excelPath)
    {
        using (var stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet result = reader.AsDataSet();

                return result.Tables;
            }
        }
    }

    public static string ToJson(DataTable dataTable, bool autoParse = false)
    {
        return ToJson(dataTable, autoParse, 3);
    }

    public static string ToJson(DataTable dataTable, bool autoParse, int head)
    {
        DataTable newDataTable = new DataTable();

        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
            try
            {
                string columnName = dataTable.Rows[0][i].ToString();
                string columnType = dataTable.Rows[1][i].ToString();
                object columnValue = dataTable.Rows[head][i];

                if (!string.IsNullOrEmpty(columnName))
                {
                    DataColumn dataColumn = new DataColumn(columnName, TypeHelper.GetTypebyValue(columnValue.ToString()));
                    if (autoParse == false)
                    {
                        dataColumn.DataType = TypeHelper.GetTypeByString(columnType);
                    }
                    newDataTable.Columns.Add(dataColumn);
                }

            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        for (int i = head; i < dataTable.Rows.Count; i++)
        {
            DataRow newRow = newDataTable.NewRow();

            for (int j = 0; j < newDataTable.Columns.Count; j++)
            {
                try
                {
                    string columnName = dataTable.Rows[0][j].ToString();
                    string columnType = dataTable.Rows[1][j].ToString();
                    object columnValue = dataTable.Rows[i][j];

                    if (columnValue.ToString().Contains(","))
                    {
                        columnValue = JsonConvert.DeserializeObject(columnValue.ToString());

                        if (autoParse == false)
                        {
                            columnValue = JsonConvert.DeserializeObject(columnValue.ToString(), TypeHelper.GetTypeByString(columnType));
                        }
                    }

                    newRow[columnName] = columnValue;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
            newDataTable.Rows.Add(newRow);
        }

        if (newDataTable.Rows.Count > 0)
        {
            string json = JsonConvert.SerializeObject(newDataTable, Formatting.Indented);
            return json;
        }
        return "";
    }
}