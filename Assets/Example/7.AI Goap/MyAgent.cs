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

    private void Awake()
    {
        AddAction(new SleepAction(this));
        AddAction(new CollectStone(this));
        AddAction(new CollectWood(this));
        AddAction(new ProcessWood(this));

        Goal = new ProcessTool(this);
    }

}
