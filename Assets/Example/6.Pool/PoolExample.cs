using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
public class PoolExample : MonoBehaviour
{
    private void Start() {
        
    }
    public int num;

   private GameObject[] objects;
   private void OnEnable() {
       objects = PoolManager.instance.Allocate("Cube",num);
       foreach (var item in objects)
       {
           item.gameObject.SetActive(true);
       }
   }

   private void OnDisable() {
        PoolManager.instance.Recycle(objects);
   }
}
