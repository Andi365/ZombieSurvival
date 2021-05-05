using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{
    private Transform trans;

    public NavMeshAgent agent;

    public GameObject player;

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
        trans.position += trans.right * Time.deltaTime;

        agent.SetDestination(player.transform.position);
    }
}
