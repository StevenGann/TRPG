using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    public class Message
    {
        public string Text = "";
        public ConsoleColor Background = ConsoleColor.Black;
        public ConsoleColor Foreground = ConsoleColor.Gray;

        public Message()
        {

        }

        public Message(string _text)
        {
            Text = _text;
        }

        public void Write()
        {
            Console.BackgroundColor = Background;
            Console.ForegroundColor = Foreground;
            Console.Write(Text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
