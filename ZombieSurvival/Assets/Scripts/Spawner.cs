using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    private float tts;
    public float spawnTime = 2f; 
    public GameObject zombie;
    void Start()
    {
        tts = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetKeyDown(KeyCode.Space);
        tts -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space)) {
            tts = spawnTime;
            Instantiate(zombie,new Vector3(0,0,0),Quaternion.identity);
        }
        
    }
}
