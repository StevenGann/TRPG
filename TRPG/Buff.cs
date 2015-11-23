using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    public class Buff
    {
        public int Strength = 0;
        public int Dexterity = 0;
        public int Constitution = 0;
        public int Intelligence = 0;
        public int Wisdom = 0;
        public int Charisma = 0;

        public static Buff Randomized(int _seed, int _scalar)
        {
            Buff result = new Buff();
            Random RNG = new Random(_seed);

            if (RNG.Next(100) <= 5) { result.Strength += RNG.Next(_scalar); }
            if (RNG.Next(100) <= 5) { result.Strength -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= 5) { result.Dexterity += RNG.Next(_scalar); }
            if (RNG.Next(100) <= 5) { result.Dexterity -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= 5) { result.Constitution += RNG.Next(_scalar); }
            if (RNG.Next(100) <= 5) { result.Constitution -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= 5) { result.Intelligence += RNG.Next(_scalar); }
            if (RNG.Next(100) <= 5) { result.Intelligence -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= 5) { result.Wisdom += RNG.Next(_scalar); }
            if (RNG.Next(100) <= 5) { result.Wisdom -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= 5) { result.Charisma += RNG.Next(_scalar); }
            if (RNG.Next(100) <= 5) { result.Charisma -= RNG.Next(_scalar); }

            return result;
        }

        public static Buff operator +(Buff a, Buff b)
        {
            Buff result = new Buff();

            result.Strength = a.Strength + b.Strength;
            result.Dexterity = a.Dexterity + b.Dexterity;
            result.Constitution = a.Constitution + b.Constitution;
            result.Intelligence = a.Intelligence + b.Intelligence;
            result.Wisdom = a.Wisdom + b.Wisdom;
            result.Charisma = a.Charisma + b.Charisma;

            return result;
        }
    }
}
