using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameServer.Data;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;
    public static GameObject[] spawnPoints;
    public GameObject enemy;
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

    IEnumerator Spawn(Zombie zombie)
    {
        yield return Instantiate(enemy, spawnPoints[zombie.spawnPoint].transform.position, Quaternion.identity);
    }

}
