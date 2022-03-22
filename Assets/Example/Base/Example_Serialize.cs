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
        Person person = new Person();
        person.Age = 12;
        person.Name = "xxx";
        person.Time = Time.time;
        byte[] personBytes = person.ToByteArray();

        person = Person.Parser.ParseFrom(personBytes);

        Student student = new Student();
        student.age = 16;
        student.name = "aaa";
        student.time = Time.time;
        string studentJson = student.ToJson();
        byte[] studentBytes = student.ToByte();
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
