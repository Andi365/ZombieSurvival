using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float speedCap = 20f;
    public float magnitude;
    private Rigidbody rigidbody = null;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int f = 0, r = 0;
        if (Input.GetKey(KeyCode.W))
            f++;
        if (Input.GetKey(KeyCode.S))
            f--;
        if (Input.GetKey(KeyCode.A))
            r++;
        if (Input.GetKey(KeyCode.D))
            r--;

        Vector3 v = new Vector3(f, 0, r);

        rigidbody.AddForce(v.normalized * speed * Time.deltaTime, ForceMode.Acceleration);
        if (rigidbody.velocity.magnitude > speedCap)
        {
            rigidbody.velocity *= speedCap / rigidbody.velocity.magnitude;
        }

        magnitude = rigidbody.velocity.magnitude;
//        trans.position += v.normalized * Time.deltaTime;
    }
}
