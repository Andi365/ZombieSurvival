using System;

namespace Data
{
    class Zombie : IData
    {
        public const byte Signature = 0x10;
        byte IData.Signature => Signature;
        public byte id;
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

        public static int SizeOf => 3;
        int IData.SizeOf() => SizeOf;

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