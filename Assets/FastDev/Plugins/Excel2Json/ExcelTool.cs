
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Excel2Json
{
    public class ExcelTool
    {
        public int StartHead { get; private set; }
        public string OutputJsonDir { get; private set; }
        public string OutputCSDir { get; private set; }

        public ExcelTool(int startHead, string outJsonDir, string outCSDir)
        {
            StartHead = startHead;
            OutputJsonDir = outJsonDir;
            OutputCSDir = outCSDir;
            InitDirectory();
        }


        private void InitDirectory()
        {
            if (!Directory.Exists(OutputJsonDir))
            {
                Directory.CreateDirectory(OutputJsonDir);
            }

            if (!Directory.Exists(OutputCSDir))
            {
                Directory.CreateDirectory(OutputCSDir);
            }
        }

        /// <summary>
        /// 读取目录下所有Excel
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public string[] ReadExcelFiles(string directory)
        {
            if (Directory.Exists(directory))
            {
                string[] fileExtensions = new string[] { ".xls", ".xlsx" };

                string[] excelFiles = Directory.GetFiles(directory).Where(file => fileExtensions.Contains(Path.GetExtension(file)) && !file.Contains("~$"))
               .ToArray();

                return excelFiles;
            }

            return null;
        }

        /// <summary>
        /// 导出成Json
        /// </summary>
        public void ExportToJson(string[] excelFiles)
        {
            foreach (var file in excelFiles)
            {
                ExportToJson(file);
            }
        }
            
        public void ExportToJson(string file)
        {
            var tables = ExcelHelper.ReadExcelAllSheets(file);

            foreach (DataTable table in tables)
            {
                if (table.Rows.Count > 0)
                {
                    var newTable = ExcelHelper.SelectContent(table, StartHead);

                    string json = JsonConvert.SerializeObject(newTable, Formatting.Indented);
                    if (!string.IsNullOrEmpty(json))
                    {
                        string fileName = table.TableName + ".json";
                        using (FileStream stream = new FileStream(OutputJsonDir + "/" + fileName, FileMode.Create, FileAccess.ReadWrite))
                        {
                            byte[] data = Encoding.UTF8.GetBytes(json);
                            stream.Write(data, 0, data.Length);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 导出成CS
        /// </summary>
        public void ExportToCS(string[] excelFiles)
        {
            foreach (var file in excelFiles)
            {
                ExportToCS(file);
            }
        }

        public void ExportToCS(string file)
        {
            var tables = ExcelHelper.ReadExcelAllSheets(file);

            foreach (DataTable table in tables)
            {
                if (table.Rows.Count > 0)
                {
                    ExcelToCS.Generate(table, OutputCSDir);
                }
            }
        }
    }
}