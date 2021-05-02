using System.Collections;
using UnityEngine;
using custom;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        //each on their own thread
        new custom.Server();
        new custom.Client();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
