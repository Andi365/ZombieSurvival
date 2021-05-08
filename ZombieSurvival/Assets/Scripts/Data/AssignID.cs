using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class AssignID : IData
    {
        public const byte Signature = 0xFD;
        byte IData.Signature => Signature;
        public byte ID;

        public AssignID(byte _ID)
        {
            ID = _ID;
        }

        public AssignID(byte[] bytes)
        {
            ID = bytes[1];
        }

        public static int SizeOf => 2;
        int IData.SizeOf() => SizeOf;

        public byte[] toBytes()
        {
            return new byte[] { Signature, ID };
        }
    }
}
