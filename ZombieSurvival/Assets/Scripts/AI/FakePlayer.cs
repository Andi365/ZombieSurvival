using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

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
            Debug.Log("xVel: " + playerState.position.xVel);
            Debug.Log("yVel: " + playerState.position.yVel);
            Debug.Log("zVel: " + playerState.position.zVel);
            rigidbody.velocity = new Vector3(playerState.position.xVel, playerState.position.yVel, playerState.position.zVel);
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