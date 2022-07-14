using System.Collections.Generic;

namespace FastDev
{
    public class GoapState
    {
        public Dictionary<string, object> Values { get; private set; }

        public GoapState()
        {
            Values = new Dictionary<string, object>();
        }

        public GoapState(Dictionary<string, object> values)
        {
            this.Values = new Dictionary<string, object>(values);
        }

        public void SetValue(string key, object value)
        {
            Values[key] = value;
        }

        public object GetValue(string key)
        {
            if (Values.ContainsKey(key))
            {
                return Values[key];
            }
            return null;
        }
    }
}
