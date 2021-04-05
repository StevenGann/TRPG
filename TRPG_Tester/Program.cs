using System;
using TRPG;

namespace TRPG_Tester
{
    internal static class Program
    {
        private static void Main()
        {
            Random RNG = new Random();
            double iterations = 10;

            //m.Get(1, 1);
            //m.Get(10, 10);
            //m.Get(100, 100);

            for (int j = 0; j < 100; j++)
            {
                Map m = new Map();

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();

                for (int i = 0; i < iterations; i++)
                {
                    int x = RNG.Next(20) - RNG.Next(20);
                    int y = RNG.Next(10) - RNG.Next(10);
                    MapCell cell = m.Get(x, y);
                    cell.Value = (byte)RNG.Next(5);
                }

                ColoredString.WriteLine(m.Render(-40, -20, 70, 25));

                watch.Stop();
                if (j > 0)
                {
                    Console.WriteLine($"Iterations:{iterations}\t Execution Time: {(((double)watch.ElapsedTicks) * 1000.0 * 10000.0) / (iterations * ((double)System.Diagnostics.Stopwatch.Frequency))}ms per 10,000 cells");
                    iterations *= 10;
                    Console.ReadLine();
                }
                Console.Clear();
            }

            Console.ReadLine();
            /*
            TRPG_core core = new TRPG_core();

            Console.WriteLine("What is your name?\n");
            core.player.Name = Console.ReadLine();

            Buff newStats;

            bool statsPicked = false;
            while (!statsPicked)
            {
                Console.Clear();
                newStats = new Buff();
                newStats.Scramble(RNG.Next(), 5 + RNG.Next(10));
                newStats.Clamp();
                Console.WriteLine("Are these stats acceptable? (y/n)");
                Console.WriteLine(newStats.ToString());
                string response = Console.ReadLine();
                if (string.Equals(response, "y", StringComparison.OrdinalIgnoreCase))
                {
                    statsPicked = true;
                }
            }

            core.Update("");
            while (true)
            {
                core.Update(Console.ReadLine());
            }
            */
        }
    }
}