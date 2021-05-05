using System;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "GameServer";
            Server.Start(4, 33333);
            Console.ReadKey();

        }

    }
}
