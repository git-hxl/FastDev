using FastDev;
using System;
using UnityEngine;
namespace HotFixProject
{
    public class Attr
    {
        public string name;
        public int age;
    }
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

        public void Test3()
        {
            Attr attr = new Attr();
            attr.name = "xxx";
            attr.age = 12;

            string json = ILRuntime.JsonMapper.ToJson(attr);

            Debug.Log("hotfix:" + json);

            attr = ILRuntime.JsonMapper.ToObject<Attr>(json);
            Debug.Log("hotfix:" + attr.name + ":" + attr.age);
        }
    }
}
