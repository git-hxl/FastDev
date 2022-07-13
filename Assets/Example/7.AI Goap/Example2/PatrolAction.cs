using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAction : GoapAction
{
    public override string Name { get; protected set; } = "巡逻";
    public override int Cost { get; protected set; } = 5;
    public override GoapState PreCondition { get; protected set; } = new GoapState(new Dictionary<string, int>() {

        {AIStateKey.NoEnemyInRagne,1 },
    });
    public override GoapState Effect { get; protected set; } = new GoapState();
    public override IGoapAgent Agent { get; protected set; }
    public override GameObject Target { get; protected set; } = GameObject.Find("Patrol Area");

    public PatrolAction(IGoapAgent goapAgent) : base(goapAgent) { }


    //巡逻行为没有结束
    public override bool CheckIsDone()
    {
        return false;
    }

    public override bool MoveToTarget()
    {
        return true;
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
        nextPos = Target.transform.position;
      
    }

    GameObject enemy;

    private Vector3 nextPos;

    protected override void OnUpdate()
    {
        if (Vector3.Distance(Agent.Self.transform.position, nextPos) < 1)
            nextPos = new Vector3(Random.Range(Target.transform.position.x - 10, Target.transform.position.x + 10), nextPos.y, Random.Range(Target.transform.position.z - 10, Target.transform.position.z + 10));

        else
        {
            Vector3 dir = (nextPos - Agent.Self.transform.position).normalized;
            Agent.Self.transform.Translate(dir * 5 * Time.deltaTime);
        }

        if (enemy != null)
        {
            if (Vector3.Distance(Agent.Self.transform.position, enemy.transform.position) < 5)
            {
                Agent.GoapState.SetValue(AIStateKey.NoEnemyInRagne, 0);
            }
        }
        else
        {
            enemy = GameObject.FindGameObjectWithTag("Player");
        } 
    }
}
