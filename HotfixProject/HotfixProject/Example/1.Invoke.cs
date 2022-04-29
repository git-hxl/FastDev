using UnityEngine;

namespace Hotfix
{
    public class Invoke
    {
        public static void Test1()
        {
            Debug.Log("Here is Hotfix");
        }

        public string Test2()
        {
            return "Here is Hotfix";
        }

        public void Test3(string s)
        {
            Debug.Log(s);
        }
    }

}