using System;

namespace Data
{
    class StopServer : IData
    {
        public const byte Signature = 0xFF;
        byte IData.Signature => Signature;

        public StopServer() { }
        public StopServer(byte[] data) { }

        public static int SizeOf => 1;
        int IData.SizeOf() => SizeOf;

        public byte[] toBytes()
        {
            return new byte[] { Signature };
        }
    }
}
