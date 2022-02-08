using System.Net.Sockets;
using Cysharp.Threading.Tasks;
using System;

namespace FastDev
{
    public class MiniTcpClient : Singleton<MiniTcpClient>
    {
        private TcpClient tcpClient;
        private DataPacker dataPacker;
        private byte[] readBuffer = new byte[1024];
        public async UniTask<bool> Connect(string address, int port, int timeout)
        {
            tcpClient = new TcpClient();
            dataPacker = new DataPacker();
            var result = tcpClient.BeginConnect(address.ParseIP(), port, ConnectResult, tcpClient);
            await UniTask.WhenAny(UniTask.WaitUntil(() => result.IsCompleted), UniTask.Delay(timeout));
            return tcpClient.Connected;
        }

        private void ConnectResult(IAsyncResult ar)
        {
            var tcp = (TcpClient)ar.AsyncState;
            if (tcp.Connected)
            {
                tcp.EndConnect(ar);
                NetworkStream stream = tcp.GetStream();
                stream.BeginRead(readBuffer, 0, readBuffer.Length, ReadResult, tcp);
                EventManager.Instance.Dispatch(EventMsgID.ConnectSuccess, null);
            }
        }

        private void ReadResult(IAsyncResult ar)
        {
            var tcp = (TcpClient)ar.AsyncState;
            if (tcp.Connected)
            {
                NetworkStream stream = tcp.GetStream();
                int recvLength = stream.EndRead(ar);
                if (recvLength > 0)
                {
                    DataPacker dataPacker = new DataPacker();
                    byte[] recvBytes = new byte[recvLength];
                    Array.Copy(readBuffer, 0, recvBytes, 0, recvLength);
                    dataPacker.UnPack(recvBytes);
                    stream.BeginRead(readBuffer, 0, readBuffer.Length, ReadResult, tcp);
                }
                else
                {
                    UnityEngine.Debug.LogError("连接中断！");
                    EventManager.Instance.Dispatch(EventMsgID.ConnectFailed, null);
                }
            }
        }

        public void Send(int msgID, byte[] data)
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                byte[] sendData = dataPacker.Packer(msgID, data);
                tcpClient.GetStream().BeginWrite(sendData, 0, sendData.Length, SendResult, tcpClient);
            }
        }

        private void SendResult(IAsyncResult ar)
        {
            TcpClient tcpClient = (TcpClient)ar.AsyncState;
            if (tcpClient != null && tcpClient.Connected)
            {
                NetworkStream stream = tcpClient.GetStream();
                stream.EndWrite(ar);
            }
        }

        public void Close()
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                tcpClient.Close();
                tcpClient = null;
            }
        }
    }
}