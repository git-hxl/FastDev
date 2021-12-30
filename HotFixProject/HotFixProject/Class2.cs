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
            DelegateDemo.actionTest += test;
            DelegateDemo.actionTest2 += test2;
        }

        private void test(string s)
        {
            Debug.Log("HotFix:" + s);
        }

        private void test2()
        {
            Debug.Log("HotFix: test2");
        }
    }
}
