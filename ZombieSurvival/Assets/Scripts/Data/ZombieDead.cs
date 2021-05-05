using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Data
{
    class ZombieDead : IData
    {
        static public byte Signature => 0b00000101;
        byte IData.Signature() => Signature;
        private byte id;

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