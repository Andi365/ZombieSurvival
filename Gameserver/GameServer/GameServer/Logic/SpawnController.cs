using Data;
using System;
using System.Collections.Generic;
using System.Text;
using GameServer.Networking;

namespace GameServer.Logic
{
    class SpawnController
    {
        private static SpawnController instance;
        public static SpawnController Instance
        {
            private set { instance = value; }
            get
            {
                if (instance == null)
                    instance = new SpawnController();
                return instance;
            }
        }

        private SpawnController()
        {
            Zombies = new Dictionary<byte, ZombieState>();
        }

        float timer = 1f;
        private Dictionary<byte, ZombieState> Zombies;

        public void Init()
        {


        }

        public void Update()
        {

            timer -= Timer.deltaTime;

            if(timer < 0 && Zombies.Count < 20)
            {


                timer = 1f;
                //ZombieSpawn spawn = new ZombieSpawn();
                //Server.BroadcastData(ZombieSpawn);

            }

        }

        public void End()
        {

        }

   
    }
}
