using System;

namespace Data
{
    class DisconnectClient : IData
    {
        public const byte Signature = 0xFE;
        byte IData.Signature => Signature;

        private byte ClientID;

        public DisconnectClient(byte id)
        {
            ClientID = id;
        }

        public DisconnectClient(byte[] data)
        {
            ClientID = data[1];
        }

        public int SizeOf()
        {
            return 2;
        }

        public byte[] toBytes()
        {
            return new byte[] { Signature, ClientID };
        }
    }
}
