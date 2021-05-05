using System.Collections;
using UnityEngine;
using custom;

public class GameController : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
