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
        public static ConcurrentQueue<IData> queue;

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
                    case Zombie.Signature:
                        Zombie zom = new Zombie(data.toBytes());
                        SpawnController.instance.spawnEnemy(zom);
                        sc.spawnEnemy(data as Zombie);
                        break;
                    case ZombieDead.Signature:
                        Zombie zom1 = new Zombie(data.toBytes());
                        SpawnController.instance.killEnemy(zom1);
                        break;
                    default:
                        break;
                }
            }

        }
    }
}