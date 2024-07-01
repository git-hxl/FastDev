using System;
using System.Data;

namespace Excel2Json
{
    public static class ExcelToCS
    {
        public static void Generate(DataTable dataTable, string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            string csName = dataTable.TableName;

            string filePath = path + "/" + $"{csName}.cs";
            System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath);
            writer.WriteLine("// This code was automatically generated");
            writer.WriteLine();

            writer.WriteLine($"public class {csName}:IConfig");

            writer.WriteLine("{");

            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                string columnName = dataTable.Rows[0][j].ToString();

                if (string.IsNullOrEmpty(columnName))
                    continue;

                string typeStr = dataTable.Rows[1][j].ToString();

                Type type = JsonConvertHelper.GetTypeByString(typeStr);

                writer.WriteLine($"     public {type} {columnName} {{ get; set;}}");
            }

            writer.WriteLine("}");
            writer.Close();
        }
    }
}