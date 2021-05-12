using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class ZombieState : IData
    {
        public const byte Signature = 0x12;
        byte IData.Signature => Signature;
        public readonly byte Id;
        public int hp = 100;

        public ZombieState(int _hp, byte _id)
        {
            this.Id = _id;
            this.hp = _hp;
        }

        public ZombieState(byte[] bytes)
        {
            this.Id = bytes[1];
            this.hp = BitConverter.ToInt32(bytes, 2);
        }

        public static int SizeOf => 2+sizeof(int);

        public byte[] toBytes()
        {
            byte[] bytes = new byte[SizeOf];
            bytes[0] = Signature;
            bytes[1] = Id;
            BitConverter.GetBytes(hp).CopyTo(bytes, 2);
            return bytes;
        }

        int IData.SizeOf() => SizeOf;
    }
}
