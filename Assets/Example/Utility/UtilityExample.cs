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

        string s1 = " 123     abc哈      哈 ~！《》？：”{}|·。，、；’【】、,./;'[]~@213abc ";
        Debug.Log(s1.ToAlphaNumber());
        Debug.Log(s1.ToAlphaNumberAndChinese(false));


        string s2 = "ad   213";
        Debug.Log(s2.Replace(" ", ""));
        Debug.Log(s2.Replace(" ", "_"));

        //16位加密
        string txt = SecurityUtil.AESEncrypt("213asd,.';阿萨大大2", "1234567890123456");
        Debug.Log("加密："+ txt);
        Debug.Log("解密：" + SecurityUtil.AESDecrypt(txt, "1234567890123456"));
        //24位加密
        txt = SecurityUtil.AESEncrypt("213asd,.';阿萨大大2", "123456789012345678901234");
        Debug.Log("加密：" + txt);
        Debug.Log("解密：" + SecurityUtil.AESDecrypt(txt, "123456789012345678901234"));
        //32位加密
        txt = SecurityUtil.AESEncrypt("213asd,.';阿萨大大2", "12345678901234567890123456789012");
        Debug.Log("加密：" + txt);
        Debug.Log("解密：" + SecurityUtil.AESDecrypt(txt, "12345678901234567890123456789012"));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Stop"))
        {
            node.Stop();
        }
    }

}
