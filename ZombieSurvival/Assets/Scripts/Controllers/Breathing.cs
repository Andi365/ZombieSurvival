using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathing : MonoBehaviour
{   

    private Transform flesh;
    private Vector3 size;
    private bool Grow = true;

    // Start is called before the first frame update
    void Start()
    {
        flesh = gameObject.transform;
        size = flesh.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        if(size.x > 7)
            Grow = false;
        else if(size.x  < 5)
            Grow = true;

        if(Grow){
            size.x = size.x + 0.001f;
            size.y = size.y + 0.001f;
            size.z = size.z + 0.001f;
        } else {
            size.x = size.x - 0.001f;
            size.y = size.y - 0.001f;
            size.z = size.z - 0.001f;
        }
        flesh.localScale = size;
    }
}
