using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

class program {
    static void Main(string[] args) 
        {
            Multiplayer mp = Multiplayer.Instance;
        }
}

public sealed class Multiplayer
{
    private Multiplayer(bool isHost) 
    {
        IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddr = ipHost.AddressList[0];
        IPEndPoint endPoint = new IPEndPoint(ipAddr,9999);
        Socket socket = new Socket(ipAddr.AddressFamily,SocketType.Stream,ProtocolType.Udp);

        Byte[] sendmsg;

        if(isHost) {
            Console.WriteLine("Local endpoint is: {0}", endPoint);
            
            socket.Bind(endPoint);

            socket.Listen((int) SocketOptionName.MaxConnections);

            Socket clientSocket = socket.Accept();

            string txt = "Hello from server";
            sendmsg = Encoding.ASCII.GetBytes(txt);

            clientSocket.Send(sendmsg);
        } else {
            socket.Connect(endPoint);

            sendmsg = Encoding.ASCII.GetBytes("Hello from Client");

            byte[] recieved = new byte[1024];

            Console.WriteLine("Message from server: {0}", Encoding.ASCII.GetString(recieved,0,socket.Receive(recieved)));
        }

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }

    public static Multiplayer Instance 
    {
        get {
            if(Instance == null) 
                Instance = new Multiplayer(false);
            
            return Instance;
        }

        set { Instance = value; }
    }
}


