using FastDev;
using UnityEngine;

public class ObjectPoolExample : MonoBehaviour
{
    IObjectPool<TestObject> pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = ObjectPoolManager.Instance.CreateSpawnObjectPool<TestObject>("TestObject", 15, 10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (pool.CanSpawn() == false)
            {
                TestObject testObject = ReferencePool.Acquire<TestObject>();

                var objAsset = ResourceManager.Instance.LoadAsset<GameObject>("prefab", "Assets/Arts/Prefab/Cube.prefab");
                var obj = Instantiate(objAsset);
                testObject.Init(obj.name, obj);

                pool.Register(testObject, true);
            }
            else
            {
                pool.Spawn();
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            pool.UnspawnAll();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            pool.ReleaseAllUnused();
        }

    }
}
