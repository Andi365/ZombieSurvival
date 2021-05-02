using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace custom
{
    public class Server : MonoBehaviour
    {
        private byte channel;
        private const int PORT = 14000;

        [System.Obsolete]
        public Server()
        {
            NetworkTransport.Init();

            ConnectionConfig config = new ConnectionConfig();
            channel = config.AddChannel(QosType.Unreliable);

            HostTopology hostTopology = new HostTopology(config, 4);

            NetworkTransport.AddHost(hostTopology, PORT, "localhost");

        }
    }
}

