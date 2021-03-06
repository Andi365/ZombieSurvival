using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Data;
using System.Collections.Concurrent;

namespace GameServer.Networking
{
    class Client
    {
        public const int dataBufferSize = 4096;

        public byte id;
        public TCP tcp;

        public Client(byte _clientId)
        {
            id = _clientId;
            tcp = new TCP(id, ref Logic.LogicController.Instance.getIncommingEventQueue());
        }

        public class TCP
        {
            public TcpClient socket;

            private ConcurrentQueue<(byte, IData)> eventQueue;
            private byte clientID;

            private NetworkStream stream;
            private byte[] receiveBuffer;

            public TCP(byte id, ref ConcurrentQueue<(byte, IData)> queue)
            {
                clientID = id;
                eventQueue = queue;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, new AsyncCallback(ReceiveCallback), null);
            }
            private void ReceiveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);

                    //Console.WriteLine(receiveBuffer[0]);
                    IData d;
                    switch (receiveBuffer[0])
                    {
                        case DisconnectClient.Signature:
                            socket.Close();
                            d = DataFactory.BytesToData(receiveBuffer);
                            eventQueue.Enqueue((clientID, d));
                            Server.removeUser(clientID);
                            return;
                        default:
                            d = DataFactory.BytesToData(receiveBuffer);
                            eventQueue.Enqueue((clientID, d));
                            break;
                    }
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, new AsyncCallback(ReceiveCallback), null);
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            public void SendData(IData _data)
            {
                try
                {
                    if (socket == null)
                        return;
                    if(socket.Connected)
                    {
                        byte[] _packet = _data.toBytes();
                        stream.BeginWrite(_packet, 0, _data.SizeOf(), new AsyncCallback(n => stream.EndWrite(n)), null);
                    }
                }
                catch
                {
                    Console.WriteLine($"Trying to send {_data} and something went wrong");
                }
            }

            public static void WriteCallback(IAsyncResult streamResult)
            {
            }
        }
    }
}
