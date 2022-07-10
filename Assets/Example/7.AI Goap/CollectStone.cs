using FastDev;
using UnityEngine;

public class CollectStone : IGoapAction
{
    public string Name => "收集石头";

    public GoapState PreCondition => new GoapState(new System.Collections.Generic.Dictionary<string, int>());

    public GoapState Effect => new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
        {AIStateKey.Stone,999 }
    });

    public int Cost => 5;

    public float Progress { get; private set; }

    public IGoapAgent Agent { get; private set; }

    public GameObject Target { get; private set; }

    public CollectStone(IGoapAgent agent)
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
            Target = GameObject.FindGameObjectWithTag("Stone");
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
        return Agent.GoapState.GetValue(AIStateKey.Stone) >= 100;
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
                Agent.GoapState.Values[AIStateKey.Stone] += 2;
                Progress = Agent.GoapState.Values[AIStateKey.Stone] / 100f;
                time = 0.1f;

                Debug.Log("Stone:" + Agent.GoapState.Values[AIStateKey.Stone]);
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