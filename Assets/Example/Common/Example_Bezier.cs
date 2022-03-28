using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
using Cysharp.Threading.Tasks;
public class Example_Bezier : MonoBehaviour
{
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;

    private Vector3 lastPoint;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= 100; i++)
        {
            lastPoint = Draw(i / 100f, lastPoint);
        }
    }

    private Vector3 Draw(float t,Vector3 lastPoint)
    {
        Vector3 point = BezierUtil.Curve(t, p1, p2, p3,p4);

        Debug.DrawLine(lastPoint, point,Color.red);
        return point;
    }

}
