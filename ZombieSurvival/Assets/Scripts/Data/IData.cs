using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Data
{
    interface IData
    {
        byte[] toBytes();
        byte Signature();
        int SizeOf();
    }
}
