using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFixProject
{
    class Class3 : MonoBehaviour
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
            Hashtable hash = new System.Collections.Hashtable();
            hash.Add("xx", "1111");
            MsgManager.Instance.Dispatch(666, hash);
        }
    }
}
