using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace TRPG
{
    public class GUI
    {
        public int Width;
        public int Height;
        public bool DynamicSize = false;
        public int InventorySize = 1;

        public string MainText = "";
        public string MainTitle = "";
        public int MainScroll = 0;

        public GUI()
        {
            if (!DynamicSize)
            {
                Maximize();
            }
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
        }

        public GUI(int _width, int _height, bool _dynamic)
        {
            if (!_dynamic)
            {
                Maximize();
            }
            Width = _width;
            Height = _height;
            DynamicSize = _dynamic;

            bool setH = false;
            bool setW = false;
            while (!setH || !setW)
            {
                try
                {
                    Console.WindowWidth = Width;
                    setW = true;
                }
                catch
                {
                    setW = false;
                    Width--;
                }
                try
                {
                    Console.WindowHeight = Height;
                    setH = true;
                }
                catch
                {
                    setH = false;
                    Height--;
                }
            }
        }

        public void Render(TRPG_core _gamestate)
        {
            //Wipe the console
            Console.Clear();

            //Scale for window (or scale the window) and draw frame
            if (DynamicSize)
            {
                Width = Console.WindowWidth;
                Height = Console.WindowHeight;
                while (DrawBox(0, 0, Width, Height, "", true) != 0)
                {
                    Width = Console.WindowWidth;
                    Height = Console.WindowHeight;
                }
            }
            else
            {
                Console.WindowWidth = Width;
                Console.WindowHeight = Height;
                DrawBox(0, 0, Width, Height, "", true);
            }

            //Draw center text region
            DrawBox(1, 3 + InventorySize, Width - 2, Height - (12 + InventorySize), MainTitle, false);
            DrawBigText(2, 4 + InventorySize, Width - 2, Height - (5 + InventorySize), MainText, MainScroll);

            //Draw Inventory
            DrawInventory(_gamestate.player.Contents, InventorySize);

            //Draw the message log
            DrawMessagebox(_gamestate.messages, 3);

            //Draw the Command Box and place the cursor
            DrawBox(1, Height - 4, Width - 2, 3, "COMMAND", true);
            Console.SetCursorPosition(3, Height - 3);
        }

        public void DrawInventory(Inventory _inventory, int _lines)
        {
            string title = "INVENTORY [" + _inventory.Weight + "/" + _inventory.MaxWeight + "lbs] [" + _inventory.Count + "/" + _inventory.Capacity + "]";

            DrawBox(1, 1, Width - 2, _lines + 2, title, true);
            int i = 0;
            int column_width = 20;
            int column = 0;

            /*while (i < _lines && i < _inventory.Count)
            {
                Console.SetCursorPosition(3, 2 + i);
                _inventory[i].Write();
                i++;
            }*/

            while ((column + 1) * column_width < Width)
            {
                while (i < (column + 1) * _lines && i < _inventory.Count)
                {
                    if (column > 0)
                    {
                        Console.SetCursorPosition(column * column_width, 1);
                        //Console.Write("╤");
                        for (int j = 1; j <= _lines; j++)
                        {
                            Console.SetCursorPosition(column * column_width, 1 + j);
                            Console.Write("│");
                        }
                        Console.SetCursorPosition(column * column_width, 2 + _lines);
                        Console.Write("╧");
                    }

                    Console.SetCursorPosition(3 + (column * column_width), (i - column * _lines) + 2);
                    _inventory[i].Write();
                    //Console.Write(column);
                    i++;
                }
                column++;
            }
        }

        public int DrawBigText(int _x, int _y, int _width, int _height, string _bigtext, int _scroll)
        {
            int result = 0;
            //Scrub \n characters
            string bigText = _bigtext.Replace("\n", " \n "); //Line breaks are their own words.
            //Split big text into List of words
            char[] delimiterChars = { ' ', '\t' };
            List<string> Words = bigText.Split(delimiterChars).ToList<string>();

            //Combine words into shorter substrings
            List<string> Lines = new List<string>();
            string tempString = "";
            foreach (string word in Words)
            {
                if (word == "\n" || tempString.Length + word.Length + 2 >= _width)
                {
                    Lines.Add(tempString);
                    tempString = "";
                }
                if (word != "\n")
                {
                    tempString += word + " ";
                }
            }
            if (tempString != "") { Lines.Add(tempString); }

            //Print substrings into given area
            int i = 0;
            while (i < Lines.Count + _scroll && i < _height - 9)
            {
                Console.SetCursorPosition(_x, _y + i);
                try
                { Console.Write(Lines[i + _scroll]); }
                catch { }
                i++;
            }

            return result;
        }

        public void DrawMessagebox(List<Message> _messages, int _lines)
        {
            DrawBox(1, Height - (_lines + 6), Width - 2, _lines + 2, "MESSAGES", false);
            for (int i = 0; i < _lines && i < _messages.Count; i++)
            {
                //Console.SetCursorPosition(3, Height - ((_lines + 5) - i));
                Console.SetCursorPosition(3, Height - (_lines + 3) - i);
                _messages[_messages.Count - (i + 1)].Write();
            }
        }

        public int DrawBox(int _x, int _y, int _width, int _height, string _title, bool _bold)
        {
            int result = 0;
            int x = _x;
            int y = _y;
            char h = '═';
            char v = '║';
            char tl = '╔';
            char bl = '╚';
            char tr = '╗';
            char br = '╝';

            if (!_bold)
            {
                h = '─';
                v = '│';
                tl = '┌';
                bl = '└';
                tr = '┐';
                br = '┘';
            }

            //Top
            for (x = _x; x < (_x + _width); x++)
            {
                try
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(h);
                }
                catch { result = -1; }
            }
            //Bottom
            y = _y + _height - 1;
            for (x = _x; x < (_x + _width); x++)
            {
                try
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(h);
                }
                catch { result = -1; }
            }
            //Left
            x = _x;
            for (y = _y; y < (_y + _height); y++)
            {
                try
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(v);
                }
                catch { result = -1; }
            }
            //Right
            x = _x + _width - 1;
            for (y = _y; y < (_y + _height - 1); y++)
            {
                try
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(v);
                }
                catch { result = -1; }
            }

            try
            {
                //Bottom-Left
                Console.SetCursorPosition(_x, _y + _height - 1);
                Console.Write(bl);
                //Bottom-Right
                Console.SetCursorPosition(_x + _width - 1, _y + _height - 1);
                Console.Write(br);
                //Top-Right
                Console.SetCursorPosition(_x + _width - 1, _y);
                Console.Write(tr);
                //Top-Left
                Console.SetCursorPosition(_x, _y);
                Console.Write(tl);
            }
            catch { result = -1; }

            Console.SetCursorPosition(_x + 1, _y);
            Console.Write(_title);

            return result;
        }

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        private static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }
    }
}