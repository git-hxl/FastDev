using System;
using UnityEngine;

namespace HotFixProject
{
    /// <summary>
    /// 注意静态方法和非静态方法的调用
    /// </summary>
    public class Class1
    {
        public static void Test1()
        {
            Debug.Log("HotFix：Test1");
        }

        public void Test2()
        {
            Debug.Log("HotFix：Test2");
        }
    }
}
