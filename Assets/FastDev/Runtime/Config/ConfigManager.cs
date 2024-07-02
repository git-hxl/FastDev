

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace FastDev
{
    public class ConfigManager : Singleton<ConfigManager>
    {

        private Dictionary<string, string> _configs = new Dictionary<string, string>();
        private Dictionary<Type, List<IConfig>> _configValues = new Dictionary<Type, List<IConfig>>();

        public void Init(string[] files)
        {
            if (files == null || files.Length == 0)
            {
                return;
            }
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = Path.GetFileNameWithoutExtension(files[i]);
                string json = File.ReadAllText(files[i]);
                AddConfig(fileName, json); 
            }
        }


        public void AddConfig(string key, string value)
        {
            if (_configs.ContainsKey(key))
            {
                _configs[key] = value;
            }
            else
            {
                _configs.Add(key, value);
            }

            Debug.Log("Add Config:" + key);
        }

        public T GetConfig<T>(int id) where T : IConfig
        {
            string fileName = typeof(T).Name;
            if (_configs.ContainsKey(fileName))
            {
                List<IConfig> values = null;
                if (_configValues.ContainsKey(typeof(T)))
                {
                    values = _configValues[typeof(T)];
                }

                if (values != null)
                {
                    return (T)values.FirstOrDefault((a) => a.ID == id);
                }

                if (values == null)
                {
                    var values2 = JsonConvert.DeserializeObject<List<T>>(_configs[fileName]);

                    _configValues[typeof(T)] = new List<IConfig>();

                    foreach (var item in values2)
                    {
                        _configValues[typeof(T)].Add(item);
                    }
                    return values2.FirstOrDefault((a) => a.ID == id);
                }

            }
            return default(T);
        }
    }
}
