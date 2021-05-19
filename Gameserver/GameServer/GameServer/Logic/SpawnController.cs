using Data;
using System;
using System.Collections.Generic;
using System.Text;
using GameServer.Networking;


namespace GameServer.Logic
{
    class SpawnController
    {
        int spawnPoint;
        Random rand = new Random();
        float timer = 2f;
        byte nextZombie;
        int difficulty = 0;
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
                spawnPoint = rand.Next(0, 3);
                Server.BroadcastData(new ZombieSpawn(nextZombie, (byte)spawnPoint));
  
                nextZombie++;

                if(difficulty < 50)
                {
                    timer = 5f;
                    difficulty++;
                } else if (difficulty < 100)
                {
                    timer = 2f;
                    difficulty++;
                }
                else
                {
                    timer = 1f;
                }
            }
        }

        public void End()
        {
        }

        public void DamageZombie(ZombieHit hit)
        {
            ZombieState zom;
            try
            {
                zom = Zombies[hit.Id];
            }
            catch (Exception)
            {
                zom = null;
            }

            if (zom != null)
            {
                zom.hp -= hit.damage;
                if (zom.hp <= 0)
                {
                    Server.BroadcastData(new ZombieDead(zom.Id));
                    Zombies.Remove(zom.Id);
                }
            }
            else
            {
                Server.BroadcastData(new ZombieDead(hit.Id));
                Console.WriteLine("Error: could not find zombie in Dictionary");
            }
        }
    }
}
