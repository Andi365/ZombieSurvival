using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;
using Data;

namespace GameClient.Controllers
{
    class GameController : MonoBehaviour
    {
        public static GameController instance;
        public SpawnController sc { private get; set; }
        public ConcurrentQueue<IData> queue = new ConcurrentQueue<IData>();
        public ConcurrentQueue<IData> outgoingQueue = new ConcurrentQueue<IData>();

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("only one instance should exist");
                Destroy(this);
            }
        }

        private void Update()
        {
            IData data;
            if (queue.TryDequeue(out data))
            {
                Debug.Log("Signature: "+ data.Signature);
                switch (data.Signature)
                {
                    case AssignID.Signature:
                        PlayerController.MyID = (data as AssignID).ID;
                        UIController.instance.onConnect();
                        break;
                    case ZombieSpawn.Signature:
                        ZombieSpawn zom = data as ZombieSpawn;
                        SpawnController.instance.spawnEnemy(zom);
                        sc.spawnEnemy(data as ZombieSpawn);
                        break;
                    case ZombieDead.Signature:
                        ZombieSpawn zom1 = new ZombieSpawn(data.toBytes());
                        SpawnController.instance.killEnemy(zom1);
                        break;
                    case PlayerState.Signature:
                        PlayerState ps = data as PlayerState;
                        if (ps.playerId != PlayerController.MyID) 
                        {
                            FakePlayerController.instance.handlePlayer(ps);
                        }
                        break;
                    case PlayerReady.Signature:
                        UIController.instance.setPlayerReadiness(data as PlayerReady);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}