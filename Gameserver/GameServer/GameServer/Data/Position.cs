﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    class Position : IData
    {
        static public byte Signature => 0b00000001;

        byte IData.Signature() => Signature;

        private float x, y, z;

        public Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Position(byte[] bytes)
        {
            x = BitConverter.ToSingle(bytes, 1);
            y = BitConverter.ToSingle(bytes, 5);
            z = BitConverter.ToSingle(bytes, 9);
        }

        public int SizeOf() => 13;

        public byte[] toBytes()
        {
            byte[] bytes = new byte[13];
            bytes[0] = Signature;
            BitConverter.GetBytes(x).CopyTo(bytes, 1);
            BitConverter.GetBytes(y).CopyTo(bytes, 5);
            BitConverter.GetBytes(z).CopyTo(bytes, 9);
            return bytes;
        }

        override public string ToString()
        {
            return String.Format("X: {0}, Y: {1}, Z: {2}", x, y, z);
        }
    }
}
