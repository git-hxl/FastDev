using UnityEngine;
using Protobuf;
using LitJson;
using Google.Protobuf;
using System.Collections.Generic;
using Bigger;
using System;

public class SerializeExample : MonoBehaviour
{
    public struct Test1
    {
        public string id;
    }
    // Start is called before the first frame update
    void Start()
    {
        List<int> data = new List<int>();
        data.Add(1);
        Debug.LogError(JsonMapper.ToJson(data));

        Person1 person1 = new Person1();
        person1.Name = "xx";
        person1.Age = 12;
        person1.Account = "11";

        byte[] bytes = person1.ToByteArray();

        try
        {
            Person2 person2 = Person2.Parser.ParseFrom(bytes);
            Debug.LogError(person2.ToString());
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }

        Test1 test1 = new Test1();
        test1.id = "test1";
        byte[] test1bytes = test1.ToByte();

        Test1 test2 = test1bytes.ToObject<Test1>();

        Debug.Log(test2.id);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
