using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiescript : MonoBehaviour
{
    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello world");
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trans.position += trans.right * Time.deltaTime;
    }
}
