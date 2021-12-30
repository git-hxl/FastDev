using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFixProject
{
    class Class5
    {
        public void Test()
        {
            Vector3 vector = new Vector3(1, 1, 1);
            Vector3 vertor2 = new Vector3(2, 2, 2);
            Debug.Log("Hotfix:"+(vector + vertor2) + ": Euler:" + Quaternion.identity);
        }
    }
}
