using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameClient.AI;
using Data;

namespace GameClient.Controllers
{
    
    class FakePlayerController : MonoBehaviour
    {
        public static FakePlayerController instance;
        public static byte clientID { private get; set; }
        public GameObject FakePlayerPrefab;
        private Dictionary<byte, FakePlayer> fakePlayers;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("only one instance of FakePlayerController should exist");
                Destroy(this);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            fakePlayers = new Dictionary<byte, FakePlayer>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void handlePlayer(PlayerState ps) 
        {
            if (fakePlayers.ContainsKey(ps.playerId)) 
            {
                fakePlayers[ps.playerId].UpdatePlayerState(ps);
            } else
            {
                AddFakePlayer(ps);
            }
        }

        public void AddFakePlayer(PlayerState ps) 
        {
            Position pos = ps.position;
            Vector3 vPos = new Vector3(pos.x, pos.y, pos.z);
            GameObject fakePlayer = Instantiate(FakePlayerPrefab, vPos, Quaternion.Euler(0, pos.yRot, 0));
            fakePlayers.Add(ps.playerId, fakePlayer.GetComponent<FakePlayer>());
        }
    }
}