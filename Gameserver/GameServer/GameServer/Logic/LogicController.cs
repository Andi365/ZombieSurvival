using Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using GameServer.Networking;
using System.Threading;

namespace GameServer.Logic
{
    class LogicController
    {
        private static LogicController instance;
        public static LogicController Instance 
        {
            private set { instance = value; }
            get 
            {
                if (instance == null)
                    instance = new LogicController();
                return instance;
            }
        }

        private readonly SpawnController SC;
        private readonly LobbyController LC;

        private ConcurrentQueue<(byte, IData)> IncommingEventQueue;

        public ref ConcurrentQueue<(byte, IData)> getIncommingEventQueue()
        {
            return ref IncommingEventQueue;
        }

        private LogicController() 
        {
            IncommingEventQueue = new ConcurrentQueue<(byte, IData)>(); 
            SC = SpawnController.Instance;
            LC = LobbyController.Instance;
        }

        public void SetTickrate(int TPS) => Timer.TPS = TPS;

        private void HandleMessage((byte, IData) data)
        {
            switch (data.Item2.Signature)
            {
                case Position.Signature:
                    Server.BroadcastData(data.Item2);
                    break;
                case PlayerState.Signature:
                    Server.BroadcastData(data.Item2);
                    break;
                case PlayerDead.Signature:
                    Server.BroadcastData(data.Item2);
                    break;
                case ZombieHit.Signature:
                    SC.DamageZombie(data.Item2 as ZombieHit);
                    break;
                case DisconnectClient.Signature:
                    Console.WriteLine("Client Disconnected");
                    if (!run)
                    {
                        LC.RemovePlayer(data.Item1);
                    }
                    Server.BroadcastData(data.Item2);
                    if (Server.GameEmpty() && run)
                    {
                        Console.WriteLine("Stopping server due to all players disconnecting");
                        run = false;
                    }
                    break;
                case StopServer.Signature:
                    run = false;
                    break;
                case PlayerReady.Signature:
                    LC.HandlePlayerReady(data.Item2 as PlayerReady);
                    Server.BroadcastData(data.Item2);
                    break;
                case StartServer.Signature:
                    if (LC.Ready())
                    {
                        run = true;
                        Server.BroadcastData(data.Item2);
                    }
                    break;
                default:
                    break;
            }
        }

        public void OnClientConnected(byte id)
        {
            PlayerReady[] players = new PlayerReady[LC.Players().Count];
            LC.Players().CopyTo(players, 0);
            foreach (PlayerReady player in players)
            {
                Thread.Sleep(100);
                Server.SendData(id, player);
            }
        }

        public bool run { get; private set; } = false;
        public void Start()
        {
            while (!run)
            {
                (byte, IData) data;
                if (IncommingEventQueue.TryDequeue(out data))
                {
                    HandleMessage(data);
                }
            }
            Timer.Init();
            SC.Init();

            while (run)
            {
                if (Timer.Update())
                {
                    Timer.Tick();
                    Update();
                }
            }

            End();
        }

        private void Update()
        {
            for (int i = 0; i < 5; i++)
            {
                (byte, IData) data;
                if (IncommingEventQueue.TryDequeue(out data))
                {
                    HandleMessage(data);
                } else
                    break;
            }

            SC.Update();

        }


        private void End()
        {
            SC.End();
        }
    }
}
