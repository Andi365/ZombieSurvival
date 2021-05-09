using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using GameClient.Controllers;

namespace GameClient.AI
{
    class FakePlayer : MonoBehaviour
    {
        private PlayerState playerState;
        private new Rigidbody rigidbody;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 v = new Vector3(playerState.position.r, 0, playerState.position.f);
            v = Quaternion.AngleAxis(playerState.position.yRot, Vector3.up) * v;
            rigidbody.position = Vector3.MoveTowards(rigidbody.position, v.normalized + rigidbody.position, Time.fixedDeltaTime * PlayerController.instance.speed);
        }

        public void UpdatePlayerState(PlayerState ps) 
        {
            playerState = ps;
            Position psPos = playerState.position;
            transform.position = new Vector3(psPos.x, psPos.y, psPos.z);
            transform.rotation = Quaternion.Euler(0, psPos.yRot, 0);
        }
    }
}