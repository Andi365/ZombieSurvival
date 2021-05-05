using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    interface IData
    {
        public byte[] toBytes();
        public byte Signature();
        public int SizeOf();
    }
}
