using System;

namespace Data
{
    interface IData
    {
        byte Signature { get; }
        byte[] toBytes();
        int SizeOf();
    }
}
