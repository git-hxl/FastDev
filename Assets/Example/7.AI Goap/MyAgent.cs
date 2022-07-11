using FastDev;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MyAgent : MonoBehaviour, IGoapAgent
{
    public Slider curProcessSlider;

    public Slider curHPSlider;

    public Text text;

    public List<IGoapAction> AIActions { get; private set; } = new List<IGoapAction>();

    public IGoapAction CurAction { get; private set; }
    private GoapNode goapNode;
    public GoapNode GoapNode => goapNode;

    public GoapState GoapState { get; private set; }

    public GameObject Self { get; private set; }

    private void Awake()
    {
        Self = gameObject;

        GoapState = new GoapState(new Dictionary<string, int>()
        {
            {AIStateKey.HP,100 },
            {AIStateKey.Wood,0 },
            {AIStateKey.Stone,0 },
            {AIStateKey.WoodLog,0 },
            {AIStateKey.Axe,0 },
        });
    }

    public void AddAction(IGoapAction action)
    {
        if (!AIActions.Contains(action))
            AIActions.Add(action);
    }

    public void UpdateAction()
    {
        if (goapNode != null)
        {
            foreach (var item in goapNode.GoapActions)
            {
                if (!item.CheckIsDone())
                {
                    item.Update();
                    CurAction = item;
                    return;
                }
            }
            goapNode = goapNode.Parent;
        }
    }

    void Start()
    {
        AddAction(new ProcessTool(this));
        AddAction(new SleepAction(this));
        AddAction(new CollectStone(this));
        AddAction(new CollectWood(this));
        AddAction(new ProcessWood(this));
        goapNode = GoapPlanner.Plan(this, AIActions.FirstOrDefault((a) => a is ProcessTool));
    }

    void Update()
    {
        UpdateAction();

        if (CurAction is ProgressGoapAction)
            curProcessSlider.value = (CurAction as ProgressGoapAction).Progress;
        curHPSlider.value = GoapState.GetValue(AIStateKey.HP) / 100f;
        text.text = CurAction.Name;
    }

    public void OnActionDone(IGoapAction goapAction)
    {

    }

    public void OnActionFailed(IGoapAction goapAction)
    {
        Debug.LogError("Reset,Start Plan...");
        goapNode = GoapPlanner.Plan(this, AIActions.FirstOrDefault((a) => a is ProcessTool));
    }

    public void OnPlanFailed() { }
}
