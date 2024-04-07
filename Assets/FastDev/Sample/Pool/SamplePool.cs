using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SamplePool : MonoBehaviour
{
    public Button btAllocate;
    // Start is called before the first frame update
    void Start()
    {
        btAllocate.onClick.AddListener(() =>
        {
            GameObject obj = ObjectPool.Instance.Allocate("Assets/FastDev/Sample/Pool/Cube.prefab");
            obj.SetActive(true);
            obj.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

            ObjectPool.Instance.Recycle(obj, 1000).Forget();
        });

        TestClass testClass = ReferencePool.Acquire<TestClass>();
        Debug.Log(testClass.name);

        testClass.name = "Test";

        ReferencePool.Release(testClass);

        Debug.Log(testClass.name);

        TestClass testClass2 = ReferencePool.Acquire<TestClass>();
        Debug.Log(testClass2.name);
        testClass2.name = "Test2";

        Debug.Log(ReferencePool.GetReferenceCollection(typeof(TestClass)).CurUsingRefCount);
        Debug.Log(ReferencePool.GetReferenceCollection(typeof(TestClass)).AcquireRefCount);
        Debug.Log(ReferencePool.GetReferenceCollection(typeof(TestClass)).ReleaseRefCount);
        Debug.Log(ReferencePool.GetReferenceCollection(typeof(TestClass)).AddRefCount);
        Debug.Log(ReferencePool.GetReferenceCollection(typeof(TestClass)).RemoveRefCount);
    }

    // Update is called once per frame
    void Update()
    {

    }
}


public class TestClass : IReference
{
    public string name;
    public void OnClear()
    {
        name = "";
    }
}