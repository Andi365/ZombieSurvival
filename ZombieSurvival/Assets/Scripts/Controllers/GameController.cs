using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;
using Data;
using UnityEngine.SceneManagement;

namespace GameClient.Controllers
{
    class GameController : MonoBehaviour
    {
        public bool GameActive { get; private set; } = false;
        public static GameController instance;
        public SpawnController sc { private get; set; }
        public ConcurrentQueue<IData> queue = new ConcurrentQueue<IData>();
        public ConcurrentQueue<IData> outgoingQueue = new ConcurrentQueue<IData>();
        public GameObject PlayerPrefab;

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
                switch (data.Signature)
                {
                    case AssignID.Signature:
                        PlayerController.MyID = (data as AssignID).ID;
                        UIController.instance.onConnect();
                        break;
                    case Connect.Signature:
                        if (!(data as Connect).connectionAccepted)
                            Client.instance.connectionDenied();
                        break;
                    case ZombieSpawn.Signature:
                        sc.spawnEnemy(data as ZombieSpawn);
                        break;
                    case ZombieDead.Signature:
                        sc.killEnemy(data as ZombieDead);
                        break;
                    case PlayerState.Signature:
                        PlayerState ps = data as PlayerState;
                        if (ps.playerId != PlayerController.MyID)
                        {
                            FakePlayerController.instance.handlePlayer(ps);
                        }
                        break;
                    case PlayerDead.Signature:
                        PlayerDead pd = data as PlayerDead;
                        FakePlayerController.instance.RemoveFakePlayer(pd.playerId);
                        break;
                    case PlayerReady.Signature:
                        UIController.instance.setPlayerReadiness(data as PlayerReady);
                        break;
                    case DisconnectClient.Signature:
                        DisconnectClient dc = data as DisconnectClient;
                        if (GameActive)
                        {
                            FakePlayerController.instance.RemoveFakePlayer(dc.ClientID);
                        }
                        else
                        {
                            UIController.instance.RemovePlayer(dc.ClientID);
                        }
                        break;
                    case StartServer.Signature:
                        GameActive = true;
                        SceneManager.LoadScene(1, LoadSceneMode.Single);
                        break;
                    case Respawn.Signature:
                        GameUIController.Instance.ShowRespawnTimer();
                        Invoke("RespawnPlayer", 10);
                        break;
                    default:
                        break;
                }
            }
        }

        private void RespawnPlayer() 
        {
            Instantiate(PlayerPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}