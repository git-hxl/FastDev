using System.Collections.Generic;

namespace FastDev
{
    public class GoapState
    {
        private Dictionary<string, int> values = new Dictionary<string, int>();

        public Dictionary<string, int> Values { get { return values; } }

        public GoapState(Dictionary<string,int> values)
        {
            this.values = values;
        }

        public void SetValue(string key, int value)
        {
            values[key] = value;
        }

        public int GetValue(string key)
        {
            if (values.ContainsKey(key))
            {
                return values[key];
            }
            return 0;
        }
    }
}
