using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/JsonConfig/Test1.json");
        Test1[] tests = JsonConvert.DeserializeObject<Test1[]>(json);
        Test1 test1 = tests[0];
        Debug.Log($"{test1.ID} {test1.Name} {test1.Speed} {test1.Damage.ToString()}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
