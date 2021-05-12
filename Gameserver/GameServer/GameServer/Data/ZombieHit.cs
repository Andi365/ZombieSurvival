using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class ZombieHit : IData
    {
        public const byte Signature = 0x13;
        byte IData.Signature => Signature;
        public readonly byte Id;
        public int damage;

        public ZombieHit(byte _id, int _damage)
        {
            this.Id = _id;
            this.damage = _damage;
        }

        public ZombieHit(byte[] bytes)
        {
            this.Id = bytes[1];
            this.damage = BitConverter.ToInt32(bytes, sizeof(int) + 2);
        }

        public static int SizeOf => 2 + sizeof(int);

        public byte[] toBytes()
        {
            byte[] bytes = new byte[SizeOf];
            bytes[0] = Signature;
            bytes[1] = Id;
            BitConverter.GetBytes(damage).CopyTo(bytes, sizeof(int) + 2);
            return bytes;
        }

        int IData.SizeOf() => SizeOf;
    }
}

