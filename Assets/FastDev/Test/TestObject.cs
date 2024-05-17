using FastDev;
using UnityEngine;

public class TestObject : ObjectBase
{
    public GameObject Object { get; private set; }

    public override void OnSpawn()
    {
        Object = (Target as GameObject);

        Object.SetActive(true);

        Object.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
    }

    public override void OnUnspawn()
    {
        Object.SetActive(false);
    }

    public override void OnClear()
    {
        base.OnClear();

        if (Object != null)
        {
            GameObject.Destroy(Object);

            Object = null;
        }

    }
}
