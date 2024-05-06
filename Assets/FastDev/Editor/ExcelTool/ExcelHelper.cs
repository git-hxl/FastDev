using System.Data;
using System.IO;
using System;
using Newtonsoft.Json;
using OfficeOpenXml;
using UnityEngine;
using System.Collections.Generic;

namespace FastDev
{
    public static class ExcelHelper
    {
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="firstSheetName"></param>
        /// <param name="dataTable"></param>
        public static void CreateExcel(string filePath, string firstSheetName, DataTable dataTable = null)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(firstSheetName);

                if (dataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            var value = dataTable.Rows[i][j];
                            worksheet.Cells[i + 1, j + 1].Value = value;
                        }
                    }
                }
                package.Save();
            }
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex">默认初始index= 1</param>
        /// <param name="dataTable"></param>
        public static void WriteToExcel(string filePath, int sheetIndex, DataTable dataTable)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetIndex];
                int lastRowIndex = worksheet.Dimension.End.Row;
                if (dataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            var value = dataTable.Rows[i][j];
                            worksheet.Cells[lastRowIndex + i + 1, j + 1].Value = value;
                        }
                    }
                }
                package.Save();
            }
        }

        /// <summary>
        /// 读取表
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="rowIndex">默认1 读取所有行</param>
        /// <returns></returns>
        public static List<DataTable> ReadExcelAllSheets(string filePath)
        {
            List<DataTable> tables = new List<DataTable>();

            FileInfo fileInfo = new FileInfo(filePath);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheets worksheets = package.Workbook.Worksheets;
                foreach (var item in worksheets)
                {
                    if (item.Dimension != null)
                    {
                        tables.Add(ExcekWorksheetConvertToDataTable(item));
                    }
                }
            }
            return tables;
        }

        /// <summary>
        /// 转换表内容
        /// </summary>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        private static DataTable ExcekWorksheetConvertToDataTable(ExcelWorksheet worksheet)
        {
            int rows = worksheet.Dimension.End.Row;
            int cols = worksheet.Dimension.End.Column;

            DataTable dataTable = new DataTable(worksheet.Name);

            for (int i = 1; i <= rows; i++)
            {
                DataRow row = dataTable.Rows.Add();

                for (int j = 1; j <= cols; j++)
                {
                    if (i == 1)
                        dataTable.Columns.Add(worksheet.Cells[i, j].Value.ToString());

                    row[j - 1] = worksheet.Cells[i, j].Value;
                }
            }
            return dataTable;
        }



        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetIndex">默认初始index= 1</param>
        /// <param name="rowIndex">默认初始index= 1</param>
        public static void DeleteExcelRow(string filePath, int sheetIndex, int rowIndex)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            using (var package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetIndex];
                worksheet.DeleteRow(rowIndex);
                package.Save();
            }
        }

        /// <summary>
        /// 获取表内容 数据类型根据第2行识别
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="rowIndex">初始index =0</param>
        /// <returns></returns>
        public static DataTable SelectContent(this DataTable dataTable, int rowIndex = 0)
        {
            DataTable newDataTable = new DataTable();

            for (int i = rowIndex; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = newDataTable.Rows.Add();
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    if (i == rowIndex)
                    {
                        string columnName = dataTable.Rows[0][j].ToString();
                        string typeStr = dataTable.Rows[1][j].ToString();

                        Type type = GetTypeByString(typeStr);

                        DataColumn dataColumn = new DataColumn(columnName, type);

                        newDataTable.Columns.Add(dataColumn);
                    }
                    dataRow[j] = ConvertDataTableData(dataTable.Rows[i][j].ToString(), newDataTable.Columns[j].DataType);
                }
            }
            return newDataTable;
        }

        public static DataTable SelectContentWithoutConvertType(this DataTable dataTable, int rowIndex = 0)
        {
            DataTable newDataTable = new DataTable();

            for (int i = rowIndex; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = newDataTable.Rows.Add();
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    if (i == rowIndex)
                    {
                        string columnName = dataTable.Rows[0][j].ToString();

                        DataColumn dataColumn = new DataColumn(columnName);

                        newDataTable.Columns.Add(dataColumn);
                    }
                    dataRow[j] = ConvertDataTableData(dataTable.Rows[i][j].ToString(), newDataTable.Columns[j].DataType);
                }
            }
            return newDataTable;
        }

        public static object ConvertDataTableData(string data, Type type)
        {
            if (string.IsNullOrEmpty(data))
            {
                return type.IsValueType ? Activator.CreateInstance(type) : null;
            }
            else if (data.Contains(","))
            {
                return JsonConvert.DeserializeObject(data, type);
            }
            else
            {
                return data;
            }
        }

        public static Type GetTypeByString(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "int":
                    return typeof(int);
                case "long":
                    return typeof(long);
                case "float":
                    return typeof(float);
                case "double":
                    return typeof(double);
                case "bool":
                    return typeof(bool);
                case "string":
                    return typeof(string);
                case "vector2":
                    return typeof(Vector2);
                case "vector3":
                    return typeof(Vector3);
                case "vector4":
                    return typeof(Vector4);
                case "color":
                    return typeof(Color);

                case "int[]":
                    return typeof(int[]);
                case "long[]":
                    return typeof(long[]);
                case "float[]":
                    return typeof(float[]);
                case "double[]":
                    return typeof(double[]);
                case "bool[]":
                    return typeof(bool[]);
                case "string[]":
                    return typeof(string[]);
                case "vector2[]":
                    return typeof(Vector2[]);
                case "vector3[]":
                    return typeof(Vector3[]);
                case "vector4[]":
                    return typeof(Vector4[]);
                case "color[]":
                    return typeof(Color[]);

                case "date":
                case "datetime":
                    return typeof(DateTime);
                default:
                    return typeof(string);
            }
        }
    }
}
