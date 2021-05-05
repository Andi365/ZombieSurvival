using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Data
{
    class DataFactory
    {
        private static Dictionary<byte, Func<byte[], IData>> SignatureToImplementation = new Dictionary<byte, Func<byte[], IData>>()
        {
            { Position.Signature, n => new Position(n) },
            { Zombie.Signature, n => new Zombie(n) },
            { ZombieDead.Signature, n => new ZombieDead(n) }
        };

        public static IData BytesToData(byte[] bytes)
        {
            Func<byte[], IData> constructor;
            if (SignatureToImplementation.TryGetValue(bytes[0], out constructor))
            {
                return constructor.Invoke(bytes);
            }
            throw new ArgumentException(String.Format("{0} not a valid signature", bytes[0]));
        }
    }
}
