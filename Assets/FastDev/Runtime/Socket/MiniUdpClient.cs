using System;
using System.Net;
using UnityEngine;
namespace FastDev
{
    public class MiniUdpClient
    {
        private string address;
        private int port;
        private IPAddress ip;
        private IPEndPoint iPEndPoint;
        private byte[] recvBuffer;
        public System.Net.Sockets.UdpClient socket;
        private DataPacker dataPacker;
        public MiniUdpClient(string address, int port)
        {
            this.address = address;
            this.port = port;
            ip = address.ParseIP();
            iPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.0.104"), 8888);
        }
        public void Launch()
        {
            try
            {
                dataPacker = new DataPacker();
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
                socket = new System.Net.Sockets.UdpClient(endPoint);
                socket.EnableBroadcast = true;
                // socket.BeginReceive(ReceiveResult, socket);
                Debug.Log("主机初始化成功");
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        private void ReceiveResult(IAsyncResult ar)
        {
            socket = (System.Net.Sockets.UdpClient)ar.AsyncState;
            IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
            recvBuffer = socket.EndReceive(ar, ref remote);
            dataPacker.UnPack(recvBuffer);
            socket.BeginReceive(ReceiveResult, socket);
        }
        public void Broadcast(int msgID, byte[] data)
        {
            if (socket != null)
            {
                byte[] sendData = dataPacker.Packer(msgID, data);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Broadcast, port);
                socket.BeginSend(sendData, sendData.Length, remoteEP, SendResult, socket);
            }
        }
        public void Send(int msgID, byte[] data)
        {
            if (socket != null)
            {
                byte[] sendData = dataPacker.Packer(msgID, data);
                socket.BeginSend(sendData, sendData.Length, iPEndPoint, SendResult, socket);
            }
        }
        private void SendResult(IAsyncResult ar)
        {
            socket = (System.Net.Sockets.UdpClient)ar.AsyncState;
            socket.EndSend(ar);
        }

        public void Close()
        {
            if (socket != null)
            {
                socket.Close();
                socket = null;
                Debug.LogError("网络中断");
            }
        }

    }
}