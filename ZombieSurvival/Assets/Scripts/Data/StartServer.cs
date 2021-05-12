using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class StartServer : IData
    {
        public const byte Signature = 0xFB;
        byte IData.Signature => Signature;

        public StartServer() { }
        public StartServer(byte[] bytes) { }

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
