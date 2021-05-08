using System;

namespace Data
{
    class ZombieDead : IData
    {
        public const byte Signature = 0x11;
        byte IData.Signature => Signature;
        private readonly byte id;

        public ZombieDead(byte id)
        {
            this.id = id;
        }

        public ZombieDead(byte[] bytes)
        {
            this.id = bytes[0];
        }

        public int SizeOf() => 2;

        public byte[] toBytes()
        {
            byte[] bytes = new byte[2];
            bytes[0] = Signature;
            bytes[1] = id;
            return bytes;
        }
    }
}