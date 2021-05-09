using System;

namespace Data
{
    class PlayerState : IData
    {
        public const byte Signature = 0x02;
        byte IData.Signature => Signature;
        public byte playerId;

        public byte[] toBytes()
        {
            byte[] bytes = new byte[SizeOf];
            bytes[0] = Signature;
            bytes[1] = playerId;
            BitConverter.GetBytes(Hp).CopyTo(bytes, sizeof(int) * 0 + 2);
            BitConverter.GetBytes(Ammo).CopyTo(bytes, sizeof(int) * 1 + 2);
            position.toBytes().CopyTo(bytes, SizeOf - Position.SizeOf);
            return bytes;
        }

        public static int SizeOf => sizeof(int) * 2 + 2 + Position.SizeOf;
        int IData.SizeOf() => SizeOf;

        public PlayerState(byte[] bytes)
        {
            playerId = bytes[1];
            Hp = BitConverter.ToInt32(bytes, sizeof(int) * 0 + 2);
            Ammo = BitConverter.ToInt32(bytes, sizeof(int) * 1 + 2);
            byte[] positionBytes = new byte[Position.SizeOf];
            Array.Copy(bytes, SizeOf - Position.SizeOf, positionBytes, 0, Position.SizeOf);
            position = DataFactory.BytesToData(positionBytes) as Position;
        }

        public PlayerState(byte _playerId)
        {
            playerId = _playerId;
            Hp = 100;
            Ammo = 60;
            position = new Position(0, 0, 0, 0, 0, 0, 0);
        }

        public int Hp { get; set; } = 100;
        public int Ammo { get; set; } = 100;
        public Position position { get; set; }
    }
}
