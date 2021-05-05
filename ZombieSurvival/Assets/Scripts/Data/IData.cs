using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    interface IData
    {
        byte[] toBytes();
        byte Signature();
        int SizeOf();
    }
}
