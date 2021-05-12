using Data;
using System;
using System.Collections.Generic;
using System.Text;
using GameServer.Networking;

namespace GameServer.Logic
{
    class SpawnController
    {
        float timer = 1f;
        byte nextZombie;
        private Dictionary<byte, ZombieState> Zombies;
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
            nextZombie = 0;
        }

        public void Init(){}

        public void Update()
        {

            timer -= Timer.deltaTime;

            if(timer < 0 && Zombies.Count < 20)
            {

                Zombies.Add(nextZombie, new ZombieState(100,nextZombie));

                Server.BroadcastData(new ZombieSpawn(nextZombie,0));
  
                nextZombie++;
                timer = 1f;
            }
        }

        public void End()
        {
        }

        public void DamageZombie(ZombieHit hit)
        {
            ZombieState zom = Zombies[hit.Id];
            zom.hp -= hit.damage;
        }
    }
}
