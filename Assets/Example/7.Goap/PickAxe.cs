using UnityEngine;
using FastDev;
public class PickAxe : AIAction
{
    private bool hasAxe;
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
        return hasAxe == true;
    }

    protected override void OnUpdate()
    {
        hasAxe = true;
    }

    protected override void SetEndState()
    {
        EndState = new State() { Name = "Axe1", Value = true };
    }

    protected override void SetPreState()
    {
        PreState = null;
    }

    protected override void SetTarget()
    {
        target = FindObjectOfType<Axe>().gameObject;
    }
}