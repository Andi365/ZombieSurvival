﻿using System;

namespace Data
{
    public class PlayerState : IData
    {
        public const byte Signature = 0x02;
        byte IData.Signature => Signature;

        public byte[] toBytes()
        {
            byte[] bytes = new byte[SizeOf()];
            bytes[0] = Signature;
            BitConverter.GetBytes(Hp).CopyTo(bytes, sizeof(int) * 0 + 1);
            BitConverter.GetBytes(Ammo).CopyTo(bytes, sizeof(int) * 1 + 1);
            return bytes;
        }

        public int SizeOf() => sizeof(int) * 2 + 1;
       

        public PlayerState(byte[] bytes)
        {
            Hp = BitConverter.ToInt32(bytes, sizeof(int) * 0 + 1);
            Ammo = BitConverter.ToInt32(bytes, sizeof(int) * 1 + 1);
        }

        public PlayerState()
        {
            Hp = 100;
            Ammo = 60;
        }

        public int Hp { get; set; } = 100;
        public int Ammo { get; set; } = 100;

    }
}