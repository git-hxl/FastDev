using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgent : GoapAgent
{
    private void Start()
    {

        AddAction(new AttackAction(this));

        SetGoal(new PatrolAction(this));
    }
}
