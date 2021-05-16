using System;

namespace Data
{
    class Respawn : IData
    {
        public const byte Signature = 0x80;
        byte IData.Signature => Signature;
        public Respawn()
        {
        }

        public Respawn(byte[] data)
        {
        }

        public static int SizeOf => 1;
        int IData.SizeOf() => SizeOf;

        public byte[] toBytes()
        {
            return new byte[] { Signature };
        }
    }
}
