using Data;
using GameServer.ServerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer.Logic
{
    class LobbyController
    {
        private static LobbyController instance;
        public static LobbyController Instance
        {
            get 
            {
                if (instance == null)
                    instance = new LobbyController();
                return instance; 
            }
            private set { }
        }
        private LobbyController() { players = new Dictionary<byte, PlayerReady>(); }

        private readonly Dictionary<byte, PlayerReady> players;

        public void HandlePlayerReady(PlayerReady ready)
        {
            if (players.ContainsKey(ready.ID))
            {
                players[ready.ID].ready = ready.ready;
            } else
            {
                players.Add(ready.ID, ready);
            }
        }

        public Dictionary<byte, PlayerReady>.ValueCollection Players()
        {
            return players.Values;
        }

        public void RemovePlayer(byte ID)
        {
            players.Remove(ID);
        }

        public bool Ready()
        {
            foreach (PlayerReady p in players.Values)
            {
                if (!p.ready)
                    return false;
            }
            return true;
        }
    }
}
