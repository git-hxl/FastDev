using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAgent : GoapAgent
{

    public Slider curProcessSlider;

    public Slider curHPSlider;

    public Text text;

    public int MaxHP = 100;
    public float CurHP;

    private void Start()
    {

        AddAction(new AttackAction(this));

        GoapState = new GoapState(new Dictionary<string, object>() {

             {AIStateKey.NoEnemyInRagne,true }
        });

        SetGoal(new PatrolAction(this));
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
