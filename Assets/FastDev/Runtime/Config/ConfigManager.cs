

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

        public void Init()
        {
           
        }


        public void AddConfig<T>(string value) where T : IConfig
        {
            string key = typeof(T).Name;
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
                List<T> values = null;
                if (_configValues.ContainsKey(typeof(T)))
                {
                    values = _configValues[typeof(T)] as List<T>;
                }

                if (values != null)
                {
                    return values.FirstOrDefault((a) => a.ID == id);
                }
                else
                {
                    values = JsonConvert.DeserializeObject<List<T>>(_configs[fileName]);

                    _configValues[typeof(T)] = values as List<IConfig>;
                    return values.FirstOrDefault((a) => a.ID == id);
                }

            }
            return default(T);
        }

        public List<T> GetAllConfig<T>() where T : IConfig
        {
            string fileName = typeof(T).Name;
            if (_configs.ContainsKey(fileName))
            {
                List<T> values = null;
                if (_configValues.ContainsKey(typeof(T)))
                {
                    values = _configValues[typeof(T)] as List<T>;
                }

                if (values != null)
                {
                    return values;
                }
                else
                {
                    values = JsonConvert.DeserializeObject<List<T>>(_configs[fileName]);

                    _configValues[typeof(T)] = values as List<IConfig>;
                    return values;
                }
            }
            return null;
        }
    }
}
