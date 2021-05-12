using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using GameClient.AI;

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

        public void spawnEnemy(ZombieSpawn _zombieSpawn)
        {
            Debug.Log("array size: " + spawnPoints.Length);
            Debug.Log("spawn point: " + _zombieSpawn.spawnPoint);
            GameObject zombie = Instantiate(zombieObject, spawnPoints[_zombieSpawn.spawnPoint].transform.position, Quaternion.identity);
            zombies.Add(_zombieSpawn.id, zombie);
            zombie.GetComponent<NPCScript>().setId(_zombieSpawn.id);
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
