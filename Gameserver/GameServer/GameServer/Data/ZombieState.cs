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
        private readonly byte Id;
        private int ZombieHp { get; set; } = 100;

        public ZombieState(int _ZombieHp, byte _id)
        {
            this.Id = _id;
            this.ZombieHp = _ZombieHp;
        }

        public ZombieState(byte[] bytes)
        {
            this.Id = bytes[1];
            this.ZombieHp = BitConverter.ToInt32(bytes, sizeof(int) + 2);
        }

        public static int SizeOf => 2+sizeof(int);

        public byte[] toBytes()
        {
            byte[] bytes = new byte[SizeOf];
            bytes[0] = Signature;
            bytes[1] = Id;
            BitConverter.GetBytes(ZombieHp).CopyTo(bytes, sizeof(int) + 2);
            return bytes;
        }

        int IData.SizeOf() => SizeOf;
    }
}
