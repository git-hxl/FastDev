using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bigger;
using System;

public class UtilityExample : MonoBehaviour
{
    CoroutineNode node;
    // Start is called before the first frame update
    void Start()
    {
        long timestamp = TimeUtil.GetCurTimestamp();
        Debug.Log("当前时间戳：" + timestamp);
        DateTime dateTime = TimeUtil.TimestampToDateTime(timestamp);
        Debug.Log("当前时间：" + dateTime.ToString());

        CoroutineUtil.Create(this).AppendEvent(() => Debug.Log("按下Space")).AppendUntil(() => Input.GetKeyDown(KeyCode.Space)).AppendEvent(() => Debug.Log("开始倒计时3s")).
         AppendRepeat(3, 1, () => Debug.Log(Time.time)).Start();

        node = CoroutineUtil.Create(this).AppendRepeat(-1, 1, () => Debug.Log("xxx"));
        node.Start();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Stop"))
        {
            node.Stop();
        }
    }

}
