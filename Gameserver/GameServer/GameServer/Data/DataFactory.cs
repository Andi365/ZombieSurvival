﻿using System;
using System.Collections.Generic;

namespace Data
{
    class DataFactory
    {
        private static Dictionary<byte, Func<byte[], IData>> SignatureToImplementation = new Dictionary<byte, Func<byte[], IData>>() 
        {
            { Position.Signature, n => new Position(n) },
            { PlayerState.Signature, n => new PlayerState(n) },
            { ZombieSpawn.Signature, n => new ZombieSpawn(n) },
            { ZombieDead.Signature, n => new ZombieDead(n) },
            { Zombie.Signature, n => new Zombie(n) },
            { DisconnectClient.Signature, n => new DisconnectClient(n) },
            { StopServer.Signature, n => new StopServer(n) },
            { AssignID.Signature, n => new AssignID(n) }
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
