using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class InputData
    {
        public enum InputType
        {
            Key,
            Axes
        }

        public InputType Type { get; set; }
        public List<KeyCode> KeyCodes { get; set; } = new List<KeyCode>();
        public string Axis { get; set; } = "";
    }
}
