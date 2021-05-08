using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameClient.Controllers;

public class NPCScript : MonoBehaviour
{
    private Transform trans;
    public NavMeshAgent agent;
    public GameObject player;

    private float atkTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello world");
        trans = GetComponent<Transform>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
          agent.SetDestination(player.transform.position);
    }

    private void Update()
    {
        atkTimer -= Time.deltaTime;

        if (player != null)
        {
            if (Vector3.Distance(player.transform.position, trans.transform.position) <= 2)
            {

                if(atkTimer < 0)
                {
                    Debug.Log("i atked");

                    PlayerController.instance.updateHP(-50);
                    atkTimer = 1;
                }
            }
        }
    }
}
