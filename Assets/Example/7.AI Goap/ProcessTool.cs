using FastDev;
using UnityEngine;

public class ProcessTool : IGoapAction
{
    public string Name => "生产斧头";
    public GoapState PreCondition => new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
        { AIStateKey.WoodLog,10},
        { AIStateKey.Stone,10},
        { AIStateKey.HP,5 }
    });

    public GoapState Effect => new GoapState(new System.Collections.Generic.Dictionary<string, int>()
    {
        {AIStateKey.Axe,999 }
    });

    public int Cost => 5;

    public float Progress { get; private set; }

    public IGoapAgent Agent { get; private set; }

    public GameObject Target { get; private set; }

    public ProcessTool(IGoapAgent agent)
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
            Target = GameObject.FindGameObjectWithTag("Process");
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
        return Agent.GoapState.GetValue(AIStateKey.Axe) >= 10f;
    }

    private float time = 1f;

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
                Agent.GoapState.Values[AIStateKey.Axe] += 1;

                Agent.GoapState.Values[AIStateKey.WoodLog] -= 10;
                Agent.GoapState.Values[AIStateKey.Stone] -= 10;
                Agent.GoapState.Values[AIStateKey.HP] -= 10;

                Progress = Agent.GoapState.Values[AIStateKey.Axe] / 10f;
                time = 0.1f;
                Debug.Log("Axe:" + Agent.GoapState.Values[AIStateKey.Axe]);
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