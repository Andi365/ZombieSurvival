﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameClient.Controllers;
using Data;

namespace GameClient.AI
{
    class NPCScript : MonoBehaviour
    {
        private Transform trans;
        public NavMeshAgent agent;
        public GameObject[] players;
        public ZombieState zombie;

        private float atkTimer = 0;
        private float closestTarget;
        private int target = 0;

        // Start is called before the first frame update
        void Start()
        {
            trans = GetComponent<Transform>();
            players = GameObject.FindGameObjectsWithTag("Player");
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {   
            players = GameObject.FindGameObjectsWithTag("Player");
            closestTarget = float.MaxValue;
            for (int i = 0; i < players.Length; i++)
            {   
                if(Vector3.Distance(trans.transform.position,players[i].transform.position) < closestTarget){
                    closestTarget = Vector3.Distance(trans.transform.position,players[i].transform.position);
                    target = i;
                }
                Vector3.Distance(trans.transform.position,players[i].transform.position);
            }
            

            if (players[target] != null)
                agent.SetDestination(players[target].transform.position);
        }

        private void Update()
        {
            atkTimer -= Time.deltaTime;

            if (players[target] != null)
            {
                if (Vector3.Distance(players[target].transform.position, trans.transform.position) <= 2)
                {
                    if (atkTimer < 0)
                    {
                        PlayerController.instance.updateHP(-10);
                        atkTimer = 1;
                    }
                }
            }
        }

        public void Damage(ZombieHit _zombie)
        {
            GameController.instance.outgoingQueue.Enqueue(_zombie);
        }

        public void setId(byte _id)
        {
            zombie = new ZombieState(100, _id);
        }
    }
}
