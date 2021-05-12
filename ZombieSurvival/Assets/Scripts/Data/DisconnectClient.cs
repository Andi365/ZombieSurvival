using System;

namespace Data
{
    class DisconnectClient : IData
    {
        public const byte Signature = 0xFE;
        byte IData.Signature => Signature;

        public byte ClientID;

        public DisconnectClient(byte id)
        {
            ClientID = id;
        }

        public DisconnectClient(byte[] data)
        {
            ClientID = data[1];
        }

        public static int SizeOf => 2;
        int IData.SizeOf() => SizeOf;

        public byte[] toBytes()
        {
            return new byte[] { Signature, ClientID };
        }
    }
}
