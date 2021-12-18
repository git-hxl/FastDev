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
        public TestDelegate1 testDelegatefunc;
        public void Init()
        {
            testDelegatefunc += test;
            testDelegatefunc("123");

            //必须在unity侧注册
            DelegateDemo.testDelegatefunc += test;

            DelegateDemo.testDelegatefunc("345");
        }

        public void test(string a)
        { Debug.Log("hotfix delegate test:" + a); }
    }
}
