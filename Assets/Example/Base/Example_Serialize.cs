using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using FastDev;
using System.Runtime.InteropServices;

public class Example_Serialize : MonoBehaviour
{
    struct Student
    {
        public string name;
        public int age;
        public float time;
    }
    private void Awake()
    {
        string s = Example_MonoSingleton.instance.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        //json
        Student student = new Student();
        student.name = "aaaaaaaaaaaa";
        string studentStr = student.ObjectToJson();
        student = studentStr.JsonToObject<Student>();

        //proto
        Person person = new Person();
        person.Name = "bbbbbbbbbbb";
        byte[] bytes = person.ProtoToByte();
        person = bytes.ByteToProto<Person>();

        //Marshal
        Student student1 = new Student();
        student1.name = "cccccccc";
        byte[] bytes2 = student1.ObjectToByte();
        student1 = bytes2.ByteToObject<Student>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (!Example_MonoSingleton.isNull)
            Debug.Log(Example_MonoSingleton.instance.name);
    }
}
