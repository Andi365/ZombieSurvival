/*using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Data
{
    public class ZombieSpawner : IData
    {
        static public byte Signature => 0b00000101;
        byte IData.Signature() => Signature;
        private byte id;

        public int xPos, zPos;
        public static int enemyCount;
        public static Dictionary<int, ZombieSpawner> zombies = new Dictionary<int, ZombieSpawner>();

        public ZombieSpawner(byte _id)
        {
            id = _id;
            Random num = new Random();
            xPos = num.Next(-100, -50);
            zPos = num.Next(-40, 0);
        }

        public int SizeOf()
        {

            return zombies;
        }

        public byte[] toBytes()
        {
            throw new NotImplementedException();
        }

        

        public static void Start(int _enemyCount)
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                ZombieSpawner zombie = new ZombieSpawner(i);
                zombies.Add(i, zombie);
            }
            enemyCount = _enemyCount;
            Server.BroadcastData();
        }
    }
}
*/