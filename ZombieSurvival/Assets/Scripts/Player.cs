using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    public float speed = 10f;

    void Start()
    {
        player = GetComponent<Transform>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        int f = 0, r= 0;
        if(Input.GetKey(KeyCode.W)) {
            f++;
        }
        if(Input.GetKey(KeyCode.S)) {
            f--;
        }
        if(Input.GetKey(KeyCode.D)) {
            r++;
        }
        if(Input.GetKey(KeyCode.A)) {
            r--;
        }

        //Input.GetTouch()

        Vector3 vector = new Vector3(f,0,r);

        player.position += Time.deltaTime * speed * player.forward * f / vector.magnitude;
        player.position += Time.deltaTime * speed * player.right * r / vector.magnitude;
    }
}
