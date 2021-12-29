using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace HotFixProject
{
    class Class2
    {
        public void Test()
        {
            DelegateDemo.actionTest += (a) => Debug.Log("HotFix:"+a);
        }
    }
}
