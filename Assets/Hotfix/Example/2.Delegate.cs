using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
namespace Hotfix
{
    public class Delegate
    {
        public void Test()
        {
            MsgManager.instance.Register(111, test);
        }

        private void test(Hashtable s)
        {
            Debug.Log(" HotFix: " + s[0]);
        }

    }

}
