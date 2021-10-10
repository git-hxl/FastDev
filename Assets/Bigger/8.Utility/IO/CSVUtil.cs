using System;
using System.Collections;
using System.Collections.Generic;

namespace Bigger
{
    public class CSVUtil
    {
        public static CSVData FromCSV(string txt)
        {
            CSVData csv = new CSVData(txt);
			string[] condition = { "\r\n" };
			string[] rows = txt.Split(condition,StringSplitOptions.RemoveEmptyEntries);
            string[] title = rows[0].Split(',');
            for (int i = 0; i < rows.Length; i++)
            {
                csv[i] = rows[i];
                string[] columns = rows[i].Split(',');
                for (int j = 0; j < columns.Length; j++)
                {
                    csv[columns[0]][title[j]] = columns[j];
                }
            }
            return csv;
        }
    }
    public class CSVData : IEnumerable
    {
        public CSVData()
        {

        }
        public CSVData(string str)
        {
            this.str = str;
        }
        private string str;
        private IDictionary<string, CSVData> data = new Dictionary<string, CSVData>();
        public CSVData this[string key]
        {
            get
            {
                if (!data.ContainsKey(key))
                {
                    data[key] = new CSVData();
                }
                return data[key];
            }
            set
            {
                data[key] = value;
            }
        }
        public CSVData this[int key]
        {
            get
            {
                if (!data.ContainsKey(key.ToString()))
                {
                    data[key.ToString()] = new CSVData();
                }
                return data[key.ToString()];
            }
            set
            {
                data[key.ToString()] = value;
            }
        }
        public static implicit operator CSVData(string data)
        {
            return new CSVData(data);
        }
        public static explicit operator string(CSVData data)
        {
            return data.str;
        }
        public override string ToString()
        {
            return str;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
}