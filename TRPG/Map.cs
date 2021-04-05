using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    public class Map
    {
        private readonly MapCell root = new();
        private readonly List<MapCell> cells = new();

        private readonly List<Tuple<char, ConsoleColor, ConsoleColor>> Values = new()
        {
            new Tuple<char, ConsoleColor, ConsoleColor>(' ', ConsoleColor.Black, ConsoleColor.Black),
            new Tuple<char, ConsoleColor, ConsoleColor>(' ', ConsoleColor.Black, ConsoleColor.Gray),
            new Tuple<char, ConsoleColor, ConsoleColor>(' ', ConsoleColor.Black, ConsoleColor.DarkGray),
            new Tuple<char, ConsoleColor, ConsoleColor>(' ', ConsoleColor.Black, ConsoleColor.DarkGreen),
            new Tuple<char, ConsoleColor, ConsoleColor>(' ', ConsoleColor.Black, ConsoleColor.DarkBlue),
        };

        public ColoredString Render(int X, int Y, int W, int H)
        {
            ColoredString result = new();
            for (int j = 0; j < H; j++)
            {
                for (int i = 0; i < W; i++)
                {
                    MapCell cell = Get(X + i, Y + j);
                    result += new ColoredString(Values[cell.Value].Item1, Values[cell.Value].Item2, Values[cell.Value].Item3);
                }
                result += '\n';
            }

            return result;
        }

        public MapCell Get(int X, int Y)
        {
            MapCell cell = GetCell(X, Y);

            if (cell != null) { return cell; }

            cell = new()
            {
                X = X,
                Y = Y
            };

            AddCell(cell);

            return cell;
        }

        private MapCell GetCell(int X, int Y, MapCell Start = null)
        {
            MapCell start = Start ?? root;

            if (start.X == X && start.Y == Y) { return start; }

            int index = 0;
            if (X == start.X)
            {
                if (Y < start.Y) { index = 0; }
                if (Y > start.Y) { index = 2; }
            }
            else if (Y == start.Y)
            {
                if (X < start.X) { index = 3; }
                if (X > start.X) { index = 1; }
            }

            if (start.Children[index] == null) { return null; }

            return GetCell(X, Y, start.Children[index]);
        }

        private void AddCell(MapCell NewCell, MapCell Start = null)
        {
            if (Start == null) { cells.Add(NewCell); }
            MapCell start = Start ?? root;

            if (start.X == NewCell.X && start.Y == NewCell.Y) { throw new Exception("Cell already exists"); }

            int index = 0;
            if (NewCell.X == start.X)
            {
                if (NewCell.Y < start.Y) { index = 0; }
                if (NewCell.Y > start.Y) { index = 2; }
            }
            else if (NewCell.Y == start.Y)
            {
                if (NewCell.X < start.X) { index = 3; }
                if (NewCell.X > start.X) { index = 1; }
            }

            if (start.Children[index] == null) { start.Children[index] = NewCell; }
            else { AddCell(NewCell, start.Children[index]); }
        }
    }

    public class MapCell
    {
        public int X;
        public int Y;
        public byte[] Contents = new byte[8];
        public byte Value { get => Contents[0]; set => Contents[0] = value; }
        public MapCell[] Children = new MapCell[4];
    }

    public class ColoredString
    {
        private char[] Chars;
        private ConsoleColor[] ForeColors;
        private ConsoleColor[] BackColors;

        public int Length { get { if (Chars == null) { return 0; } else { return Chars.Length; } } }

        public static void Write(ColoredString ColoredString)
        {
            ConsoleColor fc = Console.ForegroundColor;
            ConsoleColor bc = Console.BackgroundColor;
            for (int i = 0; i < ColoredString.Chars.Length; i++)
            {
                Console.ForegroundColor = ColoredString.ForeColors[i];
                Console.BackgroundColor = ColoredString.BackColors[i];
                Console.Write(ColoredString.Chars[i]);
            }
            Console.ForegroundColor = fc;
            Console.BackgroundColor = bc;
        }

        public static void WriteLine(ColoredString ColoredString)
        {
            Write(ColoredString);
            Console.Write('\n');
        }

        public ColoredString()
        {
        }

        public ColoredString(string String)
        {
            Chars = String.ToCharArray();
            ForeColors = new ConsoleColor[Chars.Length];
            BackColors = new ConsoleColor[Chars.Length];
        }

        public ColoredString(char Char)
        {
            Chars = new char[] { Char };
            ForeColors = new ConsoleColor[Chars.Length];
            BackColors = new ConsoleColor[Chars.Length];
        }

        public ColoredString(char Char, ConsoleColor ForeColor, ConsoleColor BackColor)
        {
            Chars = new char[] { Char };
            ForeColors = new ConsoleColor[] { ForeColor };
            BackColors = new ConsoleColor[] { BackColor };
        }

        public override string ToString()
        {
            return new string(Chars);
        }

        public char[] ToCharArray()
        {
            return Chars;
        }

        public void SetColor(ConsoleColor ForeColor, ConsoleColor BackColor, int Index, int Length = 1)
        {
            for (int i = 0; i < Length; i++)
            {
                ForeColors[Index + i] = ForeColor;
                BackColors[Index + i] = BackColor;
            }
        }

        public void SetColor(ConsoleColor ForeColor, int Index, int Length = 1)
        {
            for (int i = 0; i < Length; i++)
            {
                ForeColors[Index + i] = ForeColor;
            }
        }

        public static ColoredString operator +(ColoredString a, ColoredString b)
        {
            if (a == null || a.Length == 0) { return b; }
            if (b == null || b.Length == 0) { return a; }

            ColoredString result = new(a.ToString() + b.ToString());

            Array.Copy(a.ForeColors, result.ForeColors, a.ForeColors.Length);
            Array.Copy(b.ForeColors, 0, result.ForeColors, a.ForeColors.Length, b.ForeColors.Length);

            Array.Copy(a.BackColors, result.BackColors, a.BackColors.Length);
            Array.Copy(b.BackColors, 0, result.BackColors, a.BackColors.Length, b.BackColors.Length);

            return result;
        }

        public static ColoredString operator +(ColoredString a, string b)
        {
            return a + new ColoredString(b);
        }

        public static ColoredString operator +(ColoredString a, char b)
        {
            return a + new ColoredString(b.ToString());
        }

        public static ColoredString operator +(string a, ColoredString b)
        {
            return new ColoredString(a) + b;
        }

        public static ColoredString operator +(char a, ColoredString b)
        {
            return new ColoredString(a.ToString()) + b;
        }
    }
}