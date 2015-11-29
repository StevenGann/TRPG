using System;

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
        public string Adjective = "";

        public int Magnitude
        {
            get
            {
                return Math.Abs(Strength) + Math.Abs(Dexterity) + Math.Abs(Constitution) + Math.Abs(Intelligence) + Math.Abs(Wisdom) + Math.Abs(Charisma);
            }
        }

        public static Buff Randomized(int _seed, int _scalar)
        {
            Buff result = new Buff();
            Random RNG = new Random(_seed);
            int t = 5 * (_scalar / 10);

            if (RNG.Next(100) <= t) { result.Strength += RNG.Next(_scalar); }
            if (RNG.Next(100) <= t) { result.Strength -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= t) { result.Dexterity += RNG.Next(_scalar); }
            if (RNG.Next(100) <= t) { result.Dexterity -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= t) { result.Constitution += RNG.Next(_scalar); }
            if (RNG.Next(100) <= t) { result.Constitution -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= t) { result.Intelligence += RNG.Next(_scalar); }
            if (RNG.Next(100) <= t) { result.Intelligence -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= t) { result.Wisdom += RNG.Next(_scalar); }
            if (RNG.Next(100) <= t) { result.Wisdom -= RNG.Next(_scalar); }

            if (RNG.Next(100) <= t) { result.Charisma += RNG.Next(_scalar); }
            if (RNG.Next(100) <= t) { result.Charisma -= RNG.Next(_scalar); }

            return result;
        }

        public override string ToString()
        {
            string result = "";

            if (Strength != 0)
            {
                if (Strength > 0) { result += "+"; }
                result += Convert.ToString(Strength);
                result += " Strength\n";
            }

            if (Dexterity != 0)
            {
                if (Dexterity > 0) { result += "+"; }
                result += Convert.ToString(Dexterity);
                result += " Dexterity\n";
            }

            if (Constitution != 0)
            {
                if (Constitution > 0) { result += "+"; }
                result += Convert.ToString(Constitution);
                result += " Constitution\n";
            }

            if (Intelligence != 0)
            {
                if (Intelligence > 0) { result += "+"; }
                result += Convert.ToString(Intelligence);
                result += " Intelligence\n";
            }

            if (Wisdom != 0)
            {
                if (Wisdom > 0) { result += "+"; }
                result += Convert.ToString(Wisdom);
                result += " Wisdom\n";
            }

            if (Charisma != 0)
            {
                if (Charisma > 0) { result += "+"; }
                result += Convert.ToString(Charisma);
                result += " Charisma\n";
            }

            result = result.Substring(0, result.Length - 1);

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

            if (a.Adjective == "" && b.Adjective != "")
            {
                result.Adjective = b.Adjective;
            }
            else if (a.Adjective != "" && b.Adjective == "")
            {
                result.Adjective = a.Adjective;
            }
            else if (a.Adjective != "" && b.Adjective != "")
            {
                result.Adjective = a.Adjective;
            }

            return result;
        }
    }
}