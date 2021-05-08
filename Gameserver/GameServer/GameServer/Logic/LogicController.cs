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

        private ConcurrentQueue<(byte, IData)> IncommingEventQueue;

        public ref ConcurrentQueue<(byte, IData)> getIncommingEventQueue()
        {
            return ref IncommingEventQueue;
        }

        private LogicController() 
        {
            IncommingEventQueue = new ConcurrentQueue<(byte, IData)>();
        }

        public void SetTickrate(int TPS) => Timer.TPS = TPS;

        private bool run = true;
        public void Start()
        {
            Timer.Init();

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
                    case DisconnectClient.Signature:
                        Console.WriteLine("Client Disconnected");
                        break;
                    case StopServer.Signature:
                        run = false;
                        break;
                    default:
                        break;
                }
            }
        }


        private void End()
        {

        }
    }
}
