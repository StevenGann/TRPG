using System;
using TRPG;

namespace TRPG_Tester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TRPG_core core = new TRPG_core();

            Console.WriteLine("What is your name?\n");
            core.player.Name = Console.ReadLine();

            Random RNG = new Random();
            Buff newStats = new Buff(10, 10, 10, 10, 10, 10);

            bool statsPicked = false;
            while (!statsPicked)
            {
                Console.Clear();
                newStats = new Buff(10, 10, 10, 10, 10, 10);
                newStats.Scramble(RNG.Next(), 5 + RNG.Next(10));
                newStats.Clamp();
                Console.WriteLine("Are these stats acceptable? (y/n)");
                Console.WriteLine(newStats.ToString());
                string response = Console.ReadLine();
                if (response.ToLower() == "y")
                {
                    statsPicked = true;
                }
            }

            core.Update("");
            while (true)
            {
                core.Update(Console.ReadLine());
            }
        }
    }
}