using UnityEngine;
using FastDev;

public class CollectWood : AIAction
{
    public int WoodCount;
    protected override bool CheckIsInRange()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 1)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.forward = dir;
            transform.Translate(Vector3.forward * 10 * Time.deltaTime, Space.Self);
        }
        else
        {
            return true;
        }
        return false;
    }

    public override bool IsDone()
    {
        return WoodCount>=100;
    }

    private float time = 1;
    protected override void OnUpdate()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            WoodCount++;
            Debug.Log("正在放置木头 当前数量：" + WoodCount);
            time = 1;
        }
    }

    protected override void SetEndState()
    {
        EndState = new State() { Name = "CollectWoodCount100", Value = true };
    }

    protected override void SetPreState()
    {
         PreState = new State() { Name = "WoodCount10", Value = true };
    }

    protected override void SetTarget()
    {
        target = FindObjectOfType<Room>().gameObject;
    }
}