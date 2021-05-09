using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace GameClient.AI
{
    class FakePlayer : MonoBehaviour
    {
        private PlayerState playerState;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdatePlayerState(PlayerState ps) 
        {
            playerState = ps;
            Position psPos = playerState.position;
            transform.position = new Vector3(psPos.x, psPos.y, psPos.z);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(psPos.xVel, psPos.yVel, psPos.zVel);
            transform.rotation = Quaternion.Euler(0, psPos.yRot, 0);
        }
    }
}