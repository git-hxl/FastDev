using FastDev;
using UnityEngine;

public class AttackAction : GoapAction
{
    public override string Name { get; protected set; } = "攻击";
    public override int Cost { get; protected set; }
    public override GoapState PreCondition { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {

    });
    public override GoapState Effect { get; protected set; } = new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
        { AIStateKey.NoEnemyInRagne,1 }
    });

    public override IGoapAgent Agent { get; protected set; }

    public override GameObject Target { get; protected set; } = GameObject.FindGameObjectWithTag("Player");

    public AttackAction(IGoapAgent goapAgent) : base(goapAgent) { }


    public override bool CheckIsDone()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        return Target == null || Vector3.Distance(Target.transform.position, Agent.Self.transform.position) > 5;
    }

    protected override void OnDone()
    {
        Agent.GoapState.SetValue(AIStateKey.NoEnemyInRagne, 1);
    }

    protected override void OnFailedByConditon()
    {

    }

    protected override void OnFailedByPath()
    {

    }

    protected override void OnStart()
    {
       
    }

    private float atkTime = 1;
    private float count = 0;
    protected override void OnUpdate()
    {
        if (count >= atkTime)
        {
            Debug.Log("攻击");
            Target.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            if (Random.value <= 0.1)
            {
                GameObject.Destroy(Target.gameObject);
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