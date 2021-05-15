using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class Connect : IData
    {
        public const byte Signature = 0xFA;
        byte IData.Signature => Signature;

        public bool connectionAccepted;

        public Connect(bool accept)
        {
            connectionAccepted = accept;
        }

        public Connect(byte[] data)
        {
            connectionAccepted = data[1] == 0b1;
        }

        public int SizeOf()
        {
            return 2;
        }

        public byte[] toBytes()
        {
            return new byte[] { Signature, (byte)(connectionAccepted ? 0b1 : 0b0) };
        }
    }
}
