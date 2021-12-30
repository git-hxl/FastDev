using System.Collections;
using UnityEngine;

namespace HotFixProject
{
    class Class4
    {
        public void Test(MonoBehaviour mono)
        {
            mono.StartCoroutine(TestCor());
        }


        public IEnumerator TestCor()
        {
            Debug.Log("Hotfix:"+Time.time);
            yield return new WaitForSeconds(3);
            Debug.Log("Hotfix:" + Time.time);
        }
    }
}
