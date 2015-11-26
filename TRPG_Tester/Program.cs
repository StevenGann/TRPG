using System;
using TRPG;

namespace TRPG_Tester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TRPG_core core = new TRPG_core();
            core.Update("");
            while (true)
            {
                core.Update(Console.ReadLine());
            }
        }
    }
}