using FastDev;
using UnityEngine;

public class SleepAction : IGoapAction
{
    public string Name => "睡觉";

    public GoapState PreCondition => new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
        {AIStateKey.HP,-99 }
    });

    public GoapState Effect => new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
        {AIStateKey.HP,100 }
    });

    public int Cost => 5;

    public float Progress { get; private set; }

    public IGoapAgent Agent { get; private set; }

    public GameObject Target { get; private set; }

    public SleepAction(IGoapAgent agent)
    {
        Agent = agent;
    }

    public bool CheckForRun()
    {
        return GoapPlanner.ComPareState(Agent.GoapState, PreCondition);
    }

    public bool MoveToTarget()
    {
        if (Target == null)
            Target = GameObject.FindGameObjectWithTag("Bed");
        if (Target == null)
        {
            OnFailed();
            return false;
        }
        if (Vector3.Distance(Agent.Self.transform.position, Target.transform.position) > 1f)
        {
            Vector3 dir = (Target.transform.position - Agent.Self.transform.position).normalized;
            Agent.Self.transform.Translate(dir * 10 * Time.deltaTime);
            return false;
        }
        return true;
    }

    public bool CheckIsDone()
    {
        return Agent.GoapState.GetValue(AIStateKey.HP) >= Effect.GetValue(AIStateKey.HP);
    }

    private float time = 0.1f;

    public void Update()
    {
        if (!CheckForRun())
        {
            OnFailed();
            return;
        }
        if (MoveToTarget())
        {
            time -= Time.deltaTime;
            if (time <= 0f)
            {
                Agent.GoapState.Values[AIStateKey.HP] += 10;

                Progress = Agent.GoapState.Values[AIStateKey.HP] / (float)Effect.Values[AIStateKey.HP];
                time = 0.1f;
                Debug.Log("HP:" + Agent.GoapState.Values[AIStateKey.HP]);
                if (CheckIsDone())
                {
                    OnDone();
                }
            }
        }
    }

    public void OnDone()
    {
        Debug.Log(Name + ": Done!");
        Agent.OnActionDone(this);
    }

    public void OnFailed()
    {
        Debug.LogError(Name + ": Run failed!");
        Agent.OnActionFailed(this);
    }
}