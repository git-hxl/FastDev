using FastDev;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MyAgent : GoapAgent
{
    public Slider curProcessSlider;

    public Slider curHPSlider;

    public Text text;

    public int MaxHP = 100;
    public float CurHP;

    public override void OnActionDone(IGoapAction goapAction)
    {
        base.OnActionDone(goapAction);

        if (goapAction.Name == Goal.Name)
        {
            SetGoal(new ProcessTool(this));
        }
    }

    private void Start()
    {
        AddAction(new SleepAction(this));
        AddAction(new CollectStone(this));
        AddAction(new CollectWood(this));
        AddAction(new ProcessWood(this));

        SetGoal(new ProcessTool(this));
    }


    protected override void Update()
    {
        base.Update();

        if (CurAction != null)
        {
            curProcessSlider.value = CurAction.Progress;
        }
        curHPSlider.value = CurHP / MaxHP;
    }
}
