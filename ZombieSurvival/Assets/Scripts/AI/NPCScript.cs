using System.Collections;
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
        public GameObject player;
        public ZombieState zombie;

        private float atkTimer = 0;

        // Start is called before the first frame update
        void Start()
        {
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
