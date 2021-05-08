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
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        private static Queue<int> availableIDs;

        private static TcpListener tcpListener;

        public static void init(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;
            availableIDs = new Queue<int>();
            for (int i = 0; i < MaxPlayers; i++)
            {
                availableIDs.Enqueue(i);
            }
        }

        public static void Start ()
        {
            //InitializeServerData();

            Console.WriteLine("Starting server");
            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Server booted on {Port}");

        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            int newClientID = availableIDs.Dequeue();
            Client newClient = new Client(newClientID);
            clients.Add(newClientID, newClient);
            newClient.tcp.Connect(_client);
            newClient.tcp.SendData(new AssignID((byte)newClientID));

            Console.WriteLine($"Inbound connection from {_client.Client.RemoteEndPoint}, connected as id {newClientID}");
            /*
            for (int i = 0; i < MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(_client);
                    clients[i].tcp.SendData(new AssignID((byte)i));
                    return;
                }
            }
            */
        }

        private static void InitializeServerData()
        {
            for(int i = 0; i < MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }

        }

        private static void InitializeZombieSpawn()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                Zombie zom = new Zombie(Convert.ToByte(i), 1);

                BroadcastData(zom);
            }
        }

        public static void SendData(int _clientId, IData _data)
        {
            clients[_clientId].tcp.SendData(_data);
        }

        public static void BroadcastData(IData _data)
        {
            foreach (Client c in clients.Values) {
                c.tcp.SendData(_data);
            }

            /*
            for(int i = 0; i < MaxPlayers; i++)
            {
                clients[i].tcp.SendData(_data);
            }*/
        }
    }
}
