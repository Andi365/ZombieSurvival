using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Data
{
    class Zombie : IData
    {
        static public byte Signature => 0b00000100;
        byte IData.Signature() => Signature;
        private byte id;
        // Between 0..n-1 spawnpoints
        public byte spawnPoint;

        public Zombie(byte _id, byte _spawnPoint)
        {
            this.id = _id;
            this.spawnPoint = _spawnPoint;
        }

        public Zombie(byte[] bytes)
        {
            this.id = bytes[1];
            this.spawnPoint = bytes[2];
        }

        public int SizeOf() => 3;

        public byte[] toBytes()
        {
            byte[] bytes = new byte[3];
            bytes[0] = Signature;
            bytes[1] = id;
            bytes[2] = spawnPoint;
            return bytes;
        }
    }
}