using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using Data;
using System.Threading;
using GameClient.Controllers;

class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 4096;
    public string ip = "127.0.0.1";
    public int port = 14000;
    private TCP tcp;

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
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        tcp = new TCP();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            SendData(new StopServer());

        IData data;
        if (GameController.instance.outgoingQueue.TryDequeue(out data))
        {
            SendData(data);
        }

    }

    public void SendData(IData data)
    {
        tcp.Send(data.toBytes(), data.SizeOf());
    }

    public void ConnectToServer()
    {
        tcp.Connect();
    }

    class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        private byte[] receiveBuffer;
        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize
            };
            receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(instance.ip, instance.port, new AsyncCallback(ConnectedCallback), socket);
        }

        private void ConnectedCallback(IAsyncResult _result)
        {
            TcpClient socket = (TcpClient)_result.AsyncState;
            socket.EndConnect(_result);
            if (!socket.Connected)
                return;
            stream = socket.GetStream();
            Debug.Log("Connected");
            stream.BeginRead(receiveBuffer, 0, dataBufferSize, new AsyncCallback(DataCallback), stream);
        }

        private void DataCallback(IAsyncResult _result)
        {
            NetworkStream stream = (NetworkStream)_result.AsyncState;
            int _byteLength = stream.EndRead(_result);
            GameController.instance.queue.Enqueue(DataFactory.BytesToData(receiveBuffer));
            stream.BeginRead(receiveBuffer, 0, dataBufferSize, new AsyncCallback(DataCallback), stream);
        }

        public void Send(byte[] data, int size)
        {
            if (socket == null)
            {
                return;
            }
            try
            {
                stream = socket.GetStream();
                stream.Write(data, 0, size);
            }
            catch (Exception)
            {
                Console.WriteLine("Dø i et hul");
            }
        }

    }
    private void OnApplicationQuit()
    {
        SendData(new DisconnectClient(0));
        tcp.socket.Close();
    }
}

