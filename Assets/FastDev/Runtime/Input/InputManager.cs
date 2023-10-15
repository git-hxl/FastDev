using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class InputManager : Singleton<InputManager>, IInputManager
    {
        public bool InputEnabled { get; set; } = true;

        public Dictionary<string, InputData> RegisterInputDatas { get; private set; }

        private string configPath;
        protected override void OnInit()
        {
            base.OnInit();
            RegisterInputDatas = new Dictionary<string, InputData>();
            configPath = Application.streamingAssetsPath + "/InputSetting.json";
            if (File.Exists(configPath))
            {
                string json = File.ReadAllText(configPath);
                RegisterInputDatas = JsonConvert.DeserializeObject<Dictionary<string, InputData>>(json);
            }
        }

        private InputData GetInputData(string id)
        {
            if (RegisterInputDatas.ContainsKey(id))
            {
                return RegisterInputDatas[id];
            }
            return null;
        }

        public bool GetKey(string id)
        {
            if (!InputEnabled)
                return false;
            bool result = false;
            InputData inputData = GetInputData(id);
            if (inputData != null && inputData.Type == InputData.InputType.Key)
            {
                result = true;
                foreach (var keyCode in RegisterInputDatas[id].KeyCodes)
                {
                    if (result)
                        result = Input.GetKey(keyCode);
                    else
                        return result;
                }
            }
            return result;
        }

        public bool GetKeyDown(string id)
        {
            if (!InputEnabled)
                return false;
            bool result = false;
            InputData inputData = GetInputData(id);
            if (inputData != null && inputData.Type == InputData.InputType.Key)
            {
                result = true;

                for (int i = 0; i < inputData.KeyCodes.Count; i++)
                {
                    if (result)
                    {
                        if (i == inputData.KeyCodes.Count - 1)
                            result = Input.GetKeyDown(inputData.KeyCodes[i]);
                        else
                            result = Input.GetKey(inputData.KeyCodes[i]);
                    }
                    else
                        return result;
                }
            }
            return result;
        }

        public bool GetKeyUp(string id)
        {
            if (!InputEnabled)
                return false;
            bool result = false;
            InputData inputData = GetInputData(id);
            if (inputData != null && inputData.Type == InputData.InputType.Key)
            {
                KeyCode upKey = KeyCode.None;

                foreach (var item in inputData.KeyCodes)
                {
                    if (Input.GetKeyUp(item))
                    {
                        upKey = item;
                        result = true;
                        break;
                    }
                }
                if (inputData.KeyCodes.Count > 1)
                {
                    foreach (var item in inputData.KeyCodes)
                    {
                        if (item != upKey && result)
                        {
                            result = Input.GetKey(item);
                        }
                    }
                }
            }
            return result;
        }


        public float GetAxis(string id)
        {
            if (!InputEnabled)
                return 0f;
            InputData inputData = GetInputData(id);
            if (inputData != null && inputData.Type == InputData.InputType.Axes)
                return Input.GetAxis(inputData.Axis);
            return 0f;
        }

        public float GetAxisRaw(string id)
        {
            if (!InputEnabled)
                return 0f;
            InputData inputData = GetInputData(id);
            if (inputData != null && inputData.Type == InputData.InputType.Axes)
                return Input.GetAxisRaw(inputData.Axis);
            return 0f;
        }


        public void RegisterAction(string id, InputData action)
        {
            RegisterInputDatas[id] = action;
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(RegisterInputDatas, Formatting.Indented);
            File.WriteAllText(configPath, json);
        }
    }
}