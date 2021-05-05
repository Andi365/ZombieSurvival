using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameServer.Data;

class SpawnController : MonoBehaviour
{
    public static SpawnController instance;
    private GameObject[] spawnPoints;
    public GameObject zombieObject;
    private Dictionary<int, GameObject> zombies = new Dictionary<int, GameObject>();
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
        GameController gc = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
        gc.sc = this;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    public void spawnEnemy(Zombie zombie)
    {
        Debug.Log("Here");
        Instantiate(zombieObject, spawnPoints[zombie.spawnPoint].transform.position, Quaternion.identity);

        //zombies.Add(zombie.id, Instantiate(zombieObject, spawnPoints[zombie.spawnPoint].transform.position, Quaternion.identity));


    }

    public void killEnemy(Zombie zombie)
    {
        if (zombies.ContainsKey(zombie.id))
        {
            GameObject z;
            zombies.TryGetValue(zombie.id, out z);
            Destroy(z);
        }

    }
}
