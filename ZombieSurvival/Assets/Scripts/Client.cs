using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using GameServer.Data;
using System.Threading;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;
    public string ip = "127.0.0.1";
    public int port = 14000;
    public int myId = 0;
    public TCP tcp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("only one instance should exist");
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SendMessage();
        }
    }

    private void SendMessage()
    {
        tcp.Send();
    }

    public void ConnectToServer()
    {
        tcp.Connect();
    }

    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        private byte[] receiveBuffer;
        private Thread clientThread;
        public void Connect()
        {
            try
            {
                clientThread = new Thread(new ThreadStart(ListenForData));
                clientThread.IsBackground = true;
                clientThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }

        private void ListenForData()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };
            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(instance.ip, instance.port, (n) => Debug.Log("Connected"), socket);
        }

        private void ConnectCallback(IAsyncResult _result)
        {
            socket.EndConnect(_result);

            if (!socket.Connected)
            {
                return;
            }

            stream = socket.GetStream();

            //stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        public void Send()
        {
            if (socket == null)
            {
                return;
            }
            try
            {
                stream = socket.GetStream();
                Position pos = new Position(1, 2, 3);
                stream.Write(pos.toBytes(), 0, pos.SizeOf());
            }
            catch (Exception)
            {
                Console.WriteLine("Dø i et hul");
            }
        }
    }
}

