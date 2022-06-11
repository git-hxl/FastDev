using FastDev;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UdpClientExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MiniUdpClient miniUdpClient = new MiniUdpClient("0.0.0.0", 6666);
        miniUdpClient.Launch();
        miniUdpClient.socket.Connect(IPAddress.Parse("192.168.0.104"), 8888);
        byte[] data = Encoding.UTF8.GetBytes("Hello");
       // miniUdpClient.Send(0,data);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
