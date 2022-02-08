using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    public class CoroutineUtil : MonoBehaviour
    {
        public static CoroutineNode Create(MonoBehaviour mono)
        {
            return new CoroutineNode(mono);
        }
    }
}