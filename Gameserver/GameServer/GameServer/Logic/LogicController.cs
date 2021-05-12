using Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using GameServer.Networking;

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
                    Console.WriteLine($"{data.Item2 as Position} sent by {data.Item1}");
                    Server.BroadcastData(data.Item2);
                    break;
                case PlayerState.Signature:
                    Console.WriteLine($"{data.Item2 as PlayerState} sent by {data.Item1}");
                    Server.BroadcastData(data.Item2);
                    break;
                case ZombieHit.Signature:
                    SC.DamageZombie(data.Item2 as ZombieHit);
                    break;
                case DisconnectClient.Signature:
                    Console.WriteLine("Client Disconnected");
                    break;
                case StopServer.Signature:
                    run = false;
                    break;
                case PlayerReady.Signature:
                    Server.BroadcastData(data.Item2);
                    break;
                default:
                    break;
            }
        }

        public void OnClientConnected(byte id)
        {
            foreach (PlayerReady player in LC.Players())
            {
                Server.SendData(id, player);
            }
        }

        private bool run = false;
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
            (byte, IData) data;
            if (IncommingEventQueue.TryDequeue(out data))
            {
                HandleMessage(data);
            }

            SC.Update();

        }


        private void End()
        {
            SC.End();
        }
    }
}
