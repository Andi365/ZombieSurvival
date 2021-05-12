using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.ServerData
{
    class Player
    {
        public string Name { get; private set; }
        public byte ID { get; private set; }
        public bool Ready { get; set; } = false;

        public Player(byte _ID, string _name)
        {
            ID = _ID;
            Name = _name;
        }
    }
}
