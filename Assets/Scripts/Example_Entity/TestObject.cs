using FastDev;
using UnityEngine;

public class TestObject : EntityBase
{
    float randomTime;
    public override void OnSpawn()
    {
        base.OnSpawn();

        Object.SetActive(true);

        Object.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

        randomTime = Random.Range(5, 20);
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
        Object.SetActive(false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        randomTime -= Time.deltaTime;

        if (randomTime < 0)
            EntityManager.Instance.HideEntity<TestObject>(EntityID);
    }

    public override void OnClear()
    {
        base.OnClear();
    }
}
