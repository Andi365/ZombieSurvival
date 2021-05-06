using Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Logic
{
    class LogicController
    {
        private static LogicController instance;
        public static LogicController getInstance()
        {
            if (instance == null)
                instance = new LogicController();
            return instance;
        }

        private ConcurrentQueue<IData> IncommingEventQueue;
        private ConcurrentQueue<IData> OutgoingEventQueue;

        public ref ConcurrentQueue<IData> getIncommingEventQueue()
        {
            return ref IncommingEventQueue;
        }
        public ref ConcurrentQueue<IData> getOutgoingEventQueue()
        {
            return ref OutgoingEventQueue;
        }

        private LogicController() 
        {
            IncommingEventQueue = new ConcurrentQueue<IData>();
            OutgoingEventQueue = new ConcurrentQueue<IData>();
        }

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
            IData data;
            if (IncommingEventQueue.TryDequeue(out data))
            {
                run = false;
            }
        }


        private void End()
        {

        }
    }
}
