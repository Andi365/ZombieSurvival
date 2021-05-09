using System;

namespace Data
{
    class Position : IData
    {
        public const byte Signature = 0x01;
        byte IData.Signature => Signature;

        public float x, y, z, yRot, yVel;
        public int f, r;

        public Position(float x, float y, float z, float yRot, float yVel, int f, int r)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.yRot = yRot;
            this.yVel = yVel;
            this.f = f;
            this.r = r;
        }

        public Position(byte[] bytes)
        {
            byte fr = bytes[1];

            f = ((fr >> 4) & 0b11) - 2;
            r = ((fr) & 0b11) - 2;

            x = BitConverter.ToSingle(bytes, sizeof(float) * 0 + 2);
            y = BitConverter.ToSingle(bytes, sizeof(float) * 1 + 2);
            z = BitConverter.ToSingle(bytes, sizeof(float) * 2 + 2);
            yRot = BitConverter.ToSingle(bytes, sizeof(float) * 3 + 2);
        }

        public static int SizeOf => sizeof(float) * 4 + 2;
        int IData.SizeOf() => SizeOf;

        public byte[] toBytes()
        {
            byte[] bytes = new byte[SizeOf];
            bytes[0] = Signature;
            bytes[1] = 0;
            if (f == 0)
                bytes[1] += (byte)((0b10) << 4);
            else
                bytes[1] += (byte)((f==1 ? 0b11 : 0b1) << 4);
            if (r == 0)
                bytes[1] += (byte)((0b10));
            else
                bytes[1] += (byte)(r==1 ? 0b11 : 0b1);

            BitConverter.GetBytes(x).CopyTo(bytes, sizeof(float) * 0 + 2);
            BitConverter.GetBytes(y).CopyTo(bytes, sizeof(float) * 1 + 2);
            BitConverter.GetBytes(z).CopyTo(bytes, sizeof(float) * 2 + 2);
            BitConverter.GetBytes(yRot).CopyTo(bytes, sizeof(float) * 3 + 2);
            return bytes;
        }

        override public string ToString()
        {
            return String.Format("X: {0}, Y: {1}, Z: {2}, yRot: {3}", x, y, z, yRot);
        }
    }
}
