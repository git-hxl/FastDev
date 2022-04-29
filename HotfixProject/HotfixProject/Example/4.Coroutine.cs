using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hotfix
{
    public class Coroutine : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine(Test());
        }
        // Start is called before the first frame update
        IEnumerator Test()
        {
            Debug.Log("Hotfix:" + Time.time);
            yield return new WaitForSeconds(5);
            Debug.Log("Hotfix:" + Time.time);
        }

    }

}
