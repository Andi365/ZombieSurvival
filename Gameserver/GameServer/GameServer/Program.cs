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

            //Init data
            LogicController game = LogicController.Instance;
            game.SetTickrate(30);
            Server.init(4, 33333);

            //Start networking thread
            Thread t = new Thread(new ThreadStart(Server.Start));
            t.IsBackground = true;
            t.Start();

            //Start main thread
            game.Start();

            //Console.ReadKey();
        }
    }
}
