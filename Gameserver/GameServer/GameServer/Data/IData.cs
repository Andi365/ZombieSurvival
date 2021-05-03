using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Data
{
    interface IData
    {
        public byte[] toBytes();
        public byte Signature();
        public int SizeOf();
    }
}
