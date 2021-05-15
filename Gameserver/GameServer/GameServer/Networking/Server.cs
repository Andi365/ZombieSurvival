using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Data;

using System.Threading;

namespace GameServer.Networking
{
    static class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<byte, Client> clients = new Dictionary<byte, Client>();
        private static Queue<byte> availableIDs;

        private static TcpListener tcpListener;

        public static void init(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;
            availableIDs = new Queue<byte>();
            for (byte i = 0; i < MaxPlayers; i++)
            {
                availableIDs.Enqueue(i);
            }
        }

        public static void Start ()
        {
            Console.WriteLine("Starting server");
            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Server started on {Port}");
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
            bool availableID = !(availableIDs.Count == 0);
            byte newClientID = 0xFF;
            if (availableID)
                newClientID = availableIDs.Dequeue();
            Client newClient = new Client(newClientID);
            newClient.tcp.Connect(_client);
            bool canConnect = availableID && (Logic.LogicController.Instance.run == false);
            newClient.tcp.SendData(new Connect(canConnect));

            if (canConnect)
            {
                clients.Add(newClientID, newClient);
                Thread.Sleep(100);
                newClient.tcp.SendData(new AssignID(newClientID));
                Console.WriteLine($"Inbound connection from {_client.Client.RemoteEndPoint}, connected as id {newClientID}");
                Logic.LogicController.Instance.OnClientConnected(newClientID);
            } else
            {
                newClient.tcp.socket.Close();
            }
        }

        private static void InitializeZombieSpawn()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                ZombieSpawn zom = new ZombieSpawn(Convert.ToByte(i), 1);

                BroadcastData(zom);
            }
        }

        public static void SendData(byte _clientId, IData _data)
        {
            clients[_clientId].tcp.SendData(_data);
        }

        public static void BroadcastData(IData _data)
        {
            foreach (Client c in clients.Values)
            {
                c.tcp.SendData(_data);
            }
        }

        public static void removeUser(byte id)
        {
            clients.Remove(id);
            availableIDs.Enqueue(id);
        }

        public static bool GameEmpty()
        {
            return clients.Count == 0;
        }
    }
}
