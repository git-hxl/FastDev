using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFixProject
{
    class Class3:MonoBehaviour
    {
        public static void Add(GameObject go)
        {
            go.AddComponent<Class3>();
        }

        void Awake()
        {
            Debug.Log("Hotfix: Awake");
        }
        void Start()
        {
            Debug.Log("Hotfix: Start");
        }
    }
}
