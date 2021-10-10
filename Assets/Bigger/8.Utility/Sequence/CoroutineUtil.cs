using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bigger
{
    public class CoroutineUtil : MonoBehaviour
    {
        public static CoroutineNode Create(MonoBehaviour mono)
        {
            return new CoroutineNode(mono);
        }
    }
}