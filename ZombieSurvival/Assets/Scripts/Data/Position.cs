using System;

namespace Data
{
    class Position : IData
    {
        public const byte Signature = 0x01;
        byte IData.Signature => Signature;

        private readonly float x, y, z;

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
