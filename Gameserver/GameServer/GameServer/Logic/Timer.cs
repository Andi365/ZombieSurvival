using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameServer.Logic
{
    static class Timer
    {
        private static readonly int MSPerSec = 1000;
        private static int tps = 1;
        private static int MSPerTick = MSPerSec / tps;
        public static int TPS {
            get { return tps; }
            set 
            { 
                tps = value;
                MSPerTick = MSPerSec / tps;
            }
        }

        private static DateTime last;
        private static float MissingTicks;
        public static void Init()
        {
            last = DateTime.Now;

            MissingTicks = 0f;
        }

        public static bool Update()
        {
            DateTime now = DateTime.Now;
            double timeDiff = (now - last).TotalMilliseconds;
            MissingTicks += (float)(timeDiff / MSPerTick);

            last = now;

            return MissingTick();
        }

        public static bool MissingTick()
        {
            return MissingTicks > 1.0f;
        }

        public static void Tick()
        {
            MissingTicks -= 1.0f;
        }
    }
}
