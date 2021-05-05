using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;
using GameServer.Data;

public class GameController : MonoBehaviour
{
    public SpawnController sc { private get; set; }
    private ConcurrentQueue<IData> queue;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() {
        IData data;
        queue.TryDequeue(out data);
        switch (data.Signature())
        {
            default:
                break;
        }
    }
}
