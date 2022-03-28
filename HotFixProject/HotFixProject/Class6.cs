using FastDev;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFixProject
{
    class Class6
    {
        public void Test()
        {
            MsgManager.instance.Register(-1, Test2);
        }

        private void Test2(Hashtable hashtable)
        {
            Debug.Log("Hotfix:" + hashtable[0]);
        }
    }
}
