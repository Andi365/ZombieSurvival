using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;
using Data;

namespace GameClient.Controllers
{
    class GameController : MonoBehaviour
    {
        public SpawnController sc { private get; set; }
        private ConcurrentQueue<IData> queue;
        public static GameController instance;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            IData data;
            queue.TryDequeue(out data);
            switch (data.Signature())
            {
                case 0x04:
                    sc.spawnEnemy(data as Zombie);
                    break;
                case 0x05:
                    sc.killEnemy(data as Zombie);
                    break;
                default:
                    break;
            }
        }
    }
}