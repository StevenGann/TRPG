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

        public int Magnitude
        {
            get
            {
                return Math.Abs(Strength) + Math.Abs(Dexterity) + Math.Abs(Constitution) + Math.Abs(Intelligence) + Math.Abs(Wisdom) + Math.Abs(Charisma);
            }
        }

        public Buff()
        {
        }

        public Buff(int _str, int _dex, int _con, int _int, int _wis, int _cha)
        {
            Strength = _str;
            Dexterity = _dex;
            Constitution = _con;
            Intelligence = _int;
            Wisdom = _wis;
            Charisma = _cha;
        }

        public void Scramble(int _seed, int _scalar)
        {
            Random RNG = new Random(_seed);
            Strength += RNG.Next(_scalar) - RNG.Next(_scalar);
            Dexterity += RNG.Next(_scalar) - RNG.Next(_scalar);
            Constitution += RNG.Next(_scalar) - RNG.Next(_scalar);
            Intelligence += RNG.Next(_scalar) - RNG.Next(_scalar);
            Wisdom += RNG.Next(_scalar) - RNG.Next(_scalar);
            Charisma += RNG.Next(_scalar) - RNG.Next(_scalar);
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

            return result[0..^1];
        }

        public static Buff operator +(Buff a, Buff b)
        {
            Buff result = new Buff
            {
                Strength = a.Strength + b.Strength,
                Dexterity = a.Dexterity + b.Dexterity,
                Constitution = a.Constitution + b.Constitution,
                Intelligence = a.Intelligence + b.Intelligence,
                Wisdom = a.Wisdom + b.Wisdom,
                Charisma = a.Charisma + b.Charisma
            };

            return result;
        }

        /// <summary>
        /// Combine all buffs on Items in the Inventory and combine with supplied base Buff.
        /// Mostly for use with calculating actual stats on Players.
        /// </summary>
        /// <param name="a">Base Buffs</param>
        /// <param name="b">Inventory to tally up</param>
        /// <returns></returns>
        public static Buff operator +(Buff a, Inventory b)
        {
            Buff result = a;

            foreach (Item item in b)
            {
                if (!(item is Monster) && !(item is Weapon))
                {
                    result += item.Buffs;
                }
            }

            result.Clamp();

            return result;
        }

        public void Clamp()
        {
            if (Strength < 0) { Strength = 0; }
            if (Dexterity < 0) { Dexterity = 0; }
            if (Constitution < 0) { Constitution = 0; }
            if (Intelligence < 0) { Intelligence = 0; }
            if (Wisdom < 0) { Wisdom = 0; }
            if (Charisma < 0) { Charisma = 0; }
        }
    }
}