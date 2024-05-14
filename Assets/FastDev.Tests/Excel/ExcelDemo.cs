using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class ExcelDemo : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            string json1 = File.ReadAllText(Application.streamingAssetsPath + "/JsonConfig/Test_Test1.json");
            string json2 = File.ReadAllText(Application.streamingAssetsPath + "/JsonConfig/Test_Test2.json");

            //Test1[] test1 = JsonConvert.DeserializeObject<Test1[]>(json1);
            //Test2[] test2 = JsonConvert.DeserializeObject<Test2[]>(json2);

            

            //Debug.Log(json1 + "\n" + JsonConvert.SerializeObject(test1, Formatting.Indented));

            //Debug.Log(json2 + "\n" + JsonConvert.SerializeObject(test2, Formatting.Indented));
            
            


            Debug.Log(Utility.Converter.ValueConvert(1000));//1千
            Debug.Log(Utility.Converter.ValueConvert(1000000));//1百万
            Debug.Log(Utility.Converter.ValueConvert(1000000000));//10亿
            Debug.Log(Utility.Converter.ValueConvert(10000000000000));//10万亿

            Debug.Log(Utility.Converter.ByteConvert(1000));
            Debug.Log(Utility.Converter.ByteConvert(100000));
            Debug.Log(Utility.Converter.ByteConvert(1000000000));
            Debug.Log(Utility.Converter.ByteConvert(10000000000000));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
