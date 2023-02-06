using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class SampleMsg : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            MsgManager.Instance.Register(0, Test);

            SampleMsg2 sampleMsg2 = new SampleMsg2();

            MsgManager.Instance.Register(0, sampleMsg2.Test_SampleMsg2);
        }
        private void Test(object[] arg)
        {
            Debug.Log(arg[0].ToString());
        }

    }


    public class SampleMsg2
    {
        ~SampleMsg2()
        {
            Debug.LogError("SampleMsg2 Îö¹¹Ö´ÐÐ");
        }

        public void Test_SampleMsg2(object[] arg)
        {
            Debug.Log(arg[0].ToString());
        }
    }
}
