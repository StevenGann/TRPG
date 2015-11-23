using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRPG;

namespace TRPG_Tester
{
    class Program
    {
        static void Main(string[] args)
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
