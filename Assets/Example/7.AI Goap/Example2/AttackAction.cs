using FastDev;
using UnityEngine;

public class AttackAction : GoapAction
{
    public override string Name { get; protected set; } = "攻击";
    public override int Cost { get; protected set; }
    public override GoapState PreCondition { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, object>()
    {
        { AIStateKey.NoEnemyInRagne,false }
    });
    public override GoapState Effect { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, object>()
    {
        { AIStateKey.NoEnemyInRagne,true }
    });

    public override IGoapAgent Agent { get; protected set; }

    public override GameObject Target { get; protected set; } = GameObject.FindGameObjectWithTag("Player");

    public AttackAction(IGoapAgent goapAgent) : base(goapAgent) { }



    public override bool MoveToTarget()
    {

        Target = GameObject.FindGameObjectWithTag("Player");
        if (Target == null || Vector3.Distance(Target.transform.position, Agent.Self.transform.position) > 10)
        {
            Agent.GoapState.SetValue(AIStateKey.NoEnemyInRagne, true);
            return false;
        }
        return base.MoveToTarget();
    }

    protected override void OnDone()
    {

    }

    protected override void OnFailedByConditon()
    {

    }

    protected override void OnFailedByPath()
    {

    }

    protected override void OnStart()
    {
        if (Target == null)
            Target = GameObject.FindGameObjectWithTag("Player");
    }

    private float atkTime = 1;
    private float count = 1;
    protected override void OnUpdate()
    {
        if (count >= atkTime)
        {
            Debug.Log("攻击");
            Target.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            if (Random.value <= 0.1f)
            {
                GameObject.Destroy(Target.gameObject);
                Agent.GoapState.SetValue(AIStateKey.NoEnemyInRagne, true);
            }
            count = 0;
        }
        else
        {
            count += Time.deltaTime;
            Target.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
    }
}