using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Bigger
{
    public class MiniTcpServer : Singleton<MiniTcpServer>
    {
        private int port;
        private byte[] recvBuffer = new byte[1024];
        private List<TcpClient> remoteClients;
        private TcpListener tcpListener;
        private DataPacker dataPacker;
        public void Launch(int port)
        {
            this.port = port;
            remoteClients = new List<TcpClient>();
            dataPacker = new DataPacker();
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start(12);
                tcpListener.BeginAcceptTcpClient(AcceptResult, tcpListener);
                Debug.Log("服务端启动成功:" + port);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        private void AcceptResult(IAsyncResult ar)
        {
            tcpListener = (TcpListener)ar.AsyncState;
            TcpClient remoteClient = tcpListener.EndAcceptTcpClient(ar);
            remoteClients.Add(remoteClient);
            NetworkStream networkStream = remoteClient.GetStream();
            networkStream.BeginRead(recvBuffer, 0, recvBuffer.Length, ReadResult, remoteClient);
            tcpListener.BeginAcceptTcpClient(AcceptResult, tcpListener);
            Debug.Log("远程客户端:" + remoteClient.Client.RemoteEndPoint + "接入成功");
        }

        private void ReadResult(IAsyncResult ar)
        {
            TcpClient tcpClient = (TcpClient)ar.AsyncState;
            if(tcpClient.Connected)
            {
                NetworkStream stream = tcpClient.GetStream();
                int recvLength = stream.EndRead(ar);
                if (recvLength <= 0)
                {
                    Debug.Log("远程客户端:" + tcpClient.Client.RemoteEndPoint + "已经主动断开");
                    remoteClients.Remove(tcpClient);
                    tcpClient.Close();
                    return;
                }
                byte[] recvBytes = new byte[recvLength];
                Array.Copy(recvBuffer, 0, recvBytes, 0, recvLength);
                dataPacker.UnPack(recvBytes);
                stream.BeginRead(recvBuffer, 0, recvBuffer.Length, ReadResult, tcpClient);
            }
        }
        public void Send(int msgID, byte[] bodyData)
        {
            byte[] sendData = dataPacker.Packer(msgID, bodyData);
            for (int i = 0; i < remoteClients.Count; i++)
            {
                TcpClient client = remoteClients[i];
                if (client.Connected)
                {
                    client.GetStream().BeginWrite(sendData, 0, sendData.Length, SendResult, client);
                }
            }
        }
        private void SendResult(IAsyncResult ar)
        {
            TcpClient tcpClient = (TcpClient)ar.AsyncState;
            NetworkStream stream = tcpClient.GetStream();
            stream.EndWrite(ar);
        }

        public void Clear()
        {
            if (remoteClients != null)
            {
                foreach (var item in remoteClients)
                {
                    if (item.Connected)
                    {
                        item.Close();
                    }
                }
                remoteClients.Clear();
                Debug.Log("已断开远程客户端");
            }
        }
        public void Close()
        {
            Clear();
            if (tcpListener != null)
            {
                tcpListener.Stop();
                tcpListener = null;
                Debug.Log("已关闭服务器监听");
            }
        }
    }
}