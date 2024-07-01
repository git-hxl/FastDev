
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Excel2Json
{
    public class ExcelTool
    {
        private ExcelToolConfig _config;

        public ExcelTool(string configJson)
        {
            _config = JsonConvert.DeserializeObject<ExcelToolConfig>(configJson);

            InitDirectory();
        }

        public ExcelTool(ExcelToolConfig config)
        {
            _config = config;

            InitDirectory();
        }


        private void InitDirectory()
        {
            if (!Directory.Exists(_config.OutputCSDir))
            {
                Directory.CreateDirectory(_config.OutputCSDir);
            }

            if (!Directory.Exists(_config.OutputJsonDir))
            {
                Directory.CreateDirectory(_config.OutputJsonDir);
            }
        }

        /// <summary>
        /// 读取所有的Excel
        /// </summary>
        public string[] ReadAllExcel()
        {
            string[] fileExtensions = new string[] { ".xls", ".xlsx" };

            string[] excelFiles = null;

            if (!string.IsNullOrEmpty(_config.InputExcelDir) && Directory.Exists(_config.InputExcelDir))
            {
                excelFiles = Directory.GetFiles(_config.InputExcelDir).Where(file => fileExtensions.Contains(Path.GetExtension(file)) && !file.Contains("~$"))
               .ToArray();
            }

            return excelFiles;
        }

        /// <summary>
        /// 导出成Json
        /// </summary>
        public void ExportToJsonFile()
        {
            string[] excelFiles = ReadAllExcel();

            foreach (var file in excelFiles)
            {
                var tables = ExcelHelper.ReadExcelAllSheets(file);

                foreach (DataTable table in tables)
                {
                    if (table.Rows.Count > 0)
                    {
                        var newTable = ExcelHelper.SelectContent(table, _config.StartHead);

                        string json = JsonConvert.SerializeObject(newTable, Formatting.Indented);
                        if (!string.IsNullOrEmpty(json))
                        {
                            string fileName = table.TableName + ".json";
                            using (FileStream stream = new FileStream(_config.OutputJsonDir + "/" + fileName, FileMode.Create, FileAccess.ReadWrite))
                            {
                                byte[] data = Encoding.UTF8.GetBytes(json);
                                stream.Write(data, 0, data.Length);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 导出成CS
        /// </summary>
        public void ExportToCSFile()
        {
            string[] excelFiles = ReadAllExcel();
            foreach (var file in excelFiles)
            {
                var tables = ExcelHelper.ReadExcelAllSheets(file);

                foreach (DataTable table in tables)
                {
                    if (table.Rows.Count > 0)
                    {
                        ExcelToCS.Generate(table, _config.OutputCSDir);
                    }
                }
            }
        }
    }
}