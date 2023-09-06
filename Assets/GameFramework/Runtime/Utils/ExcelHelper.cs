using ExcelDataReader;
using System.Data;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;

public class ExcelHelper
{

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
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="userHeaderRow"></param>
    /// <param name="head">默认首行空两行 第一行为字段名 第二行预留注释</param>
    /// <returns></returns>
    public static DataTableCollection ReadExcelAllSheets(string filePath, bool userHeaderRow = true, int head = 2)
    {
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                ExcelDataSetConfiguration config = new ExcelDataSetConfiguration();
                config.UseColumnDataType = true;
                config.ConfigureDataTable = (value) =>
                {
                    return new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = userHeaderRow,
                        //过滤行
                        FilterRow = (rowReader) =>
                        {
                            //depth 默认从0开始
                            return rowReader.Depth >= head;
                        },
                    };
                };
                DataSet result = reader.AsDataSet(config);
                return result.Tables;
            }
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

    public static DataTable ConvertDataTableColumnType(DataTable dataTable)
    {
        DataTable newDataTable = new DataTable();
        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
            string columnName = dataTable.Columns[i].ColumnName;
            string value = dataTable.Rows[0][i].ToString();

            Type type = GetTypeByStringValue(value);
            DataColumn dataColumn = new DataColumn(columnName, type);
            newDataTable.Columns.Add(dataColumn);
        }

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow dataRow = newDataTable.NewRow();
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                if (dataTable.Rows[i][j].ToString() == "")
                {
                    Type targetType = newDataTable.Columns[j].DataType;
                    dataRow[j] = targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
                }
                else if (dataTable.Rows[i][j].ToString().Contains(","))
                {
                    dataRow[j] = JsonConvert.DeserializeObject(dataTable.Rows[i][j].ToString());
                }
                else
                {
                    dataRow[j] = dataTable.Rows[i][j].ToString();
                }
            }
            newDataTable.Rows.Add(dataRow);
        }

        return newDataTable;
    }

    public static Type GetTypeByStringValue(string value)
    {
        long longValue;
        double doubleValue;
        bool boolValue;
        DateTime dateTimeValue;
        if (long.TryParse(value, out longValue))
        {
            return typeof(long);
        }
        else if (double.TryParse(value, out doubleValue))
        {
            return typeof(double);
        }
        else if (bool.TryParse(value, out boolValue))
        {
            return typeof(bool);
        }
        else if (DateTime.TryParse(value, out dateTimeValue))
        {
            return typeof(DateTime);
        }
        else if (value.Contains(","))
        {
            //借用Json的数据类型 方便序列化
            return typeof(JArray);
        }
        return typeof(string);
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

            case "date":
            case "datetime":
                return typeof(DateTime);
            default:
                return typeof(string);
        }
    }
}