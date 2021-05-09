using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace GameClient.Controllers
{
    class SpawnController : MonoBehaviour
    {
        public static SpawnController instance;
        public static GameObject[] spawnPoints;
        public GameObject zombieObject;
        public Dictionary<int, GameObject> zombies = new Dictionary<int, GameObject>();
        private void Awake()
        {
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

        private void Start()
        {
            GameController.instance.sc = this;
            spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        }

        public void spawnEnemy(ZombieSpawn zombie)
        {
            Debug.Log("array size: "+spawnPoints.Length);
            Debug.Log("spawn point: "+zombie.spawnPoint);
            zombies.Add(zombie.id, Instantiate(zombieObject, spawnPoints[zombie.spawnPoint].transform.position, Quaternion.identity));
        }


        public void killEnemy(ZombieSpawn zombie)
        {
            if (zombies.ContainsKey(zombie.id))
            {
                GameObject z;
                zombies.TryGetValue(zombie.id, out z);
                Destroy(z);
            }
        }
    }
}
