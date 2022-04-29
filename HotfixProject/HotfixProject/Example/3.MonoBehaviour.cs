using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfix
{
    public class HotfixMono :MonoBehaviour
    {
        public static void Add(GameObject obj)
        {
            obj.AddComponent<HotfixMono>();
        }

        private void Awake()
        {
            Debug.Log("Hotfix: Awake");
        }
        private void Start()
        {
            Debug.Log("Hotfix: Start");
        }

        private void Update()
        {
            Debug.Log("Hotfix: Update");
        }

        private void OnDestroy()
        {
            Debug.Log("Hotfix: OnDestroy");
        }
    }
}