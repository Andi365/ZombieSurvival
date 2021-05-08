using System;

namespace Data
{
    public class PlayerState : IData
    {
        public const byte Signature = 0x02;
        byte IData.Signature => Signature;
        byte playerId;

        public byte[] toBytes()
        {
            byte[] bytes = new byte[SizeOf()];
            bytes[0] = Signature;
            bytes[1] = playerId;
            BitConverter.GetBytes(Hp).CopyTo(bytes, sizeof(int) * 0 + 2);
            BitConverter.GetBytes(Ammo).CopyTo(bytes, sizeof(int) * 1 + 2);
            return bytes;
        }

        public int SizeOf() => sizeof(int) * 2 + 2;

        public PlayerState(byte[] bytes)
        {
            playerId = bytes[1];
            Hp = BitConverter.ToInt32(bytes, sizeof(int) * 0 + 2);
            Ammo = BitConverter.ToInt32(bytes, sizeof(int) * 1 + 2);
        }

        public PlayerState(byte _playerId)
        {
            playerId = _playerId;
            Hp = 100;
            Ammo = 60;
        }

        public int Hp { get; set; } = 100;
        public int Ammo { get; set; } = 100;

    }
}
