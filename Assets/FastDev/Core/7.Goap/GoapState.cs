using System.Collections.Generic;

namespace FastDev
{
    public class GoapState
    {
        public Dictionary<string, int> Values { get; private set; }

        public GoapState()
        {
            Values = new Dictionary<string, int>();
        }

        public GoapState(Dictionary<string, int> values)
        {
            this.Values = new Dictionary<string, int>(values);
        }

        public void SetValue(string key, int value)
        {
            Values[key] = value;
        }

        public void AddValue(string key, int value)
        {
            if (!Values.ContainsKey(key))
                Values[key] = 0;
            Values[key] += value;
        }

        public int GetValue(string key)
        {
            if (Values.ContainsKey(key))
            {
                return Values[key];
            }
            return 0;
        }
    }
}
