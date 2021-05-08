using System;

namespace Data
{
    class StopServer : IData
    {
        public const byte Signature = 0xFF;
        byte IData.Signature => Signature;

        public StopServer() { }
        public StopServer(byte[] data) { }

        public int SizeOf()
        {
            return 1;
        }

        public byte[] toBytes()
        {
            return new byte[] { Signature };
        }
    }
}
