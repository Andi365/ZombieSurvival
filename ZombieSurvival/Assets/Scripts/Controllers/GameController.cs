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

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            IData data;
            queue.TryDequeue(out data);
            switch (data.Signature)
            {
                case Zombie.Signature:
                    sc.spawnEnemy(data as Zombie);
                    break;
                case ZombieDead.Signature:
                    sc.killEnemy(data as Zombie);
                    break;
                default:
                    break;
            }
        }
    }
}