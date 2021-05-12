using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class PlayerReady : IData
    {
        public const byte Signature = 0xFC;
        byte IData.Signature => Signature;
        public byte ID;
        public bool ready;
        public string name;

        
        public PlayerReady (byte _id, bool _ready, string _name)
        {
            ID = _id;
            ready = _ready;
            name = _name;
        }

        public PlayerReady (byte[] bytes)
        {
            ID = bytes[1];
            ready = bytes[2] == 0x01;
            name = Encoding.ASCII.GetString(bytes, 3, 20);
        }

        public static int SizeOf() => 23;
        int IData.SizeOf() => SizeOf();

        public byte[] toBytes()
        {
            byte[] bytes = new byte[SizeOf()];
            bytes[0] = Signature;
            bytes[1] = ID;
            bytes[2] = (byte)(ready ? 0x01 : 0x00);
            byte[] stringBytes = Encoding.ASCII.GetBytes(name);
            stringBytes.CopyTo(bytes, 3);
            return bytes;
        }
    }
}
