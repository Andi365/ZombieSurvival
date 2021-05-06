using System;
using System.Threading;
using GameServer.Logic;
using GameServer.Networking;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "GameServer";

            Server.init(4, 33333);
            Thread t = new Thread(new ThreadStart(Server.Start));
            t.IsBackground = true;
            t.Start();


            LogicController lc = LogicController.getInstance();
            lc.Start();

            //Console.ReadKey();
        }
    }
}
