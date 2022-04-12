using FastDev;
using Google.Protobuf;
using UnityEngine;

namespace HotFixProject
{
    class Class8
    {
        public void Test()
        {
            Student student = new Student();
            student.Name = "asd1231ac";
            byte[] bytes = student.ToByteArray();

            Student student2 = new Student();
            student2.MergeFrom(bytes);

            Debug.Log(student2.Name + ".." + bytes.Length);
        }
    }
}
