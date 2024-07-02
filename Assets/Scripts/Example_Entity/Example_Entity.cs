using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_Entity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CreateCube();
        }
    }

    private void OnGUI()
    {

        GUILayout.Label(ReferencePool.Count.ToString());

        if(ReferencePool.GetReferenceCollection(typeof(TestObject))!=null)
        {
            var pool = ReferencePool.GetReferenceCollection(typeof(TestObject));
            GUILayout.Label("Cur:"+pool.CurUsingRefCount.ToString() +"Acq:"+ pool.AcquireRefCount+ "Release:"+pool.ReleaseRefCount);
        }

        if (ReferencePool.GetReferenceCollection(typeof(TestObjectData)) != null)
        {
            var pool = ReferencePool.GetReferenceCollection(typeof(TestObjectData));
            GUILayout.Label("Cur:" + pool.CurUsingRefCount.ToString() + "Acq:" + pool.AcquireRefCount + "Release:" + pool.ReleaseRefCount);
        }
    }

    private void CreateCube()
    {
        TestObjectData entityData = ReferencePool.Acquire<TestObjectData>();
        entityData.ID = Time.frameCount;

        TestObject testObject = EntityManager.Instance.ShowEntity<TestObject>("Assets/Scripts/Example_Entity/Cube.prefab", entityData);


        Debug.Log(testObject.EntityID.ToString() + " " + (testObject.EntityData as TestObjectData).ID);
    }
}
