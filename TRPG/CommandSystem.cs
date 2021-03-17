using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    public static class CommandSystem
    {
        private static List<ICommandProvider> Context = new();
        private static Dictionary<string, ICommandProvider> CommandMap = new();

        public static void Add(ICommandProvider Provider)
        {
            Context.Add(Provider);
        }

        public static void Add(Room Room)
        {
            Add(Room as ICommandProvider);
            foreach (Item item in Room.Contents)
            {
                Add(item);
            }
        }

        public static void Clear()
        {
            Context.Clear();
        }

        public static void Build()
        {
            List<string> commands = new();
            foreach (ICommandProvider provider in Context)
            {
                string[] c = provider.GetCommands();
                foreach (string s in c)
                {
                    Console.WriteLine(s);
                    commands.Add(s);
                }
            }
            commands = commands.Distinct().ToList();
        }
    }

    public interface ICommandProvider
    {
        public string[] GetCommands();
    }
}