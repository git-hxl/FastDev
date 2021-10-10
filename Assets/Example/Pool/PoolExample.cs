using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bigger;
public class PoolExample : MonoBehaviour
{
    private void Start() {
        
    }
    public int num;

   private GameObject[] objects;
   private void OnEnable() {
       objects = PoolManager.Instance.Allocate("Cube",num);
       foreach (var item in objects)
       {
           item.gameObject.SetActive(true);
       }
   }

   private void OnDisable() {
        PoolManager.Instance.Recycle(objects);
   }
}
