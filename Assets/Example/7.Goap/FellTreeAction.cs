using FastDev;
using UnityEngine;
/// <summary>
/// 砍树
/// </summary>
public class FellTreeAction : AIAction
{
    private int woodCount;
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
        return woodCount == 10;
    }

    private float time = 1;
    protected override void OnUpdate()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            woodCount++;
            Debug.Log("正在砍树 当前数量：" + woodCount);
            time = 1;
        }
    }

    protected override void SetEndState()
    {
        EndState = new State() { Name = "WoodCount10", Value = true };
    }

    protected override void SetPreState()
    {
        PreState = new State() { Name = "Axe1", Value = true };
    }

    protected override void SetTarget()
    {
        target = FindObjectOfType<Tree>().gameObject;
    }
}