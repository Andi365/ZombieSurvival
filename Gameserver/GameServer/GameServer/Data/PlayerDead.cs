using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class PlayerDead : IData
    {
        public const byte Signature = 0x03;
        byte IData.Signature => Signature;

        public byte playerId;

        public PlayerDead(byte _id)
        {
            playerId = _id;
        }

        public PlayerDead(byte[] data)
        {
            playerId = data[1];
        }

        public static int SizeOf => 2;
        int IData.SizeOf() => SizeOf;

        public byte[] toBytes()
        {
            return new byte[] { Signature, playerId };
        }
    }
}
