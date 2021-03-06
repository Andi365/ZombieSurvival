using System;

namespace Data
{
    class Position : IData
    {
        public const byte Signature = 0x01;
        byte IData.Signature => Signature;

        public float x, y, z, yRot, yVel, xMov, zMov;

        public Position(float x, float y, float z, float yRot, float yVel, float xMov, float zMov)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.yRot = yRot;
            this.yVel = yVel;
            this.xMov = xMov;
            this.zMov = zMov;
        }

        public Position(byte[] bytes)
        {
            x = BitConverter.ToSingle(bytes, sizeof(float) * 0 + 1);
            y = BitConverter.ToSingle(bytes, sizeof(float) * 1 + 1);
            z = BitConverter.ToSingle(bytes, sizeof(float) * 2 + 1);
            yRot = BitConverter.ToSingle(bytes, sizeof(float) * 3 + 1);
            yVel = BitConverter.ToSingle(bytes, sizeof(float) * 4 + 1);
            xMov = BitConverter.ToSingle(bytes, sizeof(float) * 5 + 1);
            zMov = BitConverter.ToSingle(bytes, sizeof(float) * 6 + 1);
        }

        public static int SizeOf => sizeof(float) * 7 + 1;
        int IData.SizeOf() => SizeOf;

        public byte[] toBytes()
        {
            byte[] bytes = new byte[SizeOf];
            bytes[0] = Signature;

            BitConverter.GetBytes(x).CopyTo(bytes, sizeof(float) * 0 + 1);
            BitConverter.GetBytes(y).CopyTo(bytes, sizeof(float) * 1 + 1);
            BitConverter.GetBytes(z).CopyTo(bytes, sizeof(float) * 2 + 1);
            BitConverter.GetBytes(yRot).CopyTo(bytes, sizeof(float) * 3 + 1);
            BitConverter.GetBytes(yVel).CopyTo(bytes, sizeof(float) * 4 + 1);
            BitConverter.GetBytes(xMov).CopyTo(bytes, sizeof(float) * 5 + 1);
            BitConverter.GetBytes(zMov).CopyTo(bytes, sizeof(float) * 6 + 1);
            return bytes;
        }

        override public string ToString()
        {
            return String.Format("X: {0}, Y: {1}, Z: {2}, yRot: {3}", x, y, z, yRot);
        }
    }
}
