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

        public void Scramble(int Seed, int Scalar)
        {
            Random RNG = new Random(Seed);
            Strength += RNG.Next(Scalar) - RNG.Next(Scalar);
            Dexterity += RNG.Next(Scalar) - RNG.Next(Scalar);
            Constitution += RNG.Next(Scalar) - RNG.Next(Scalar);
            Intelligence += RNG.Next(Scalar) - RNG.Next(Scalar);
            Wisdom += RNG.Next(Scalar) - RNG.Next(Scalar);
            Charisma += RNG.Next(Scalar) - RNG.Next(Scalar);
        }

        public static Buff Randomized(int Seed, int Scalar)
        {
            Buff result = new Buff();
            Random RNG = new Random(Seed);
            int t = 5 * (Scalar / 10);

            if (RNG.Next(100) <= t) { result.Strength += RNG.Next(Scalar); }
            if (RNG.Next(100) <= t) { result.Strength -= RNG.Next(Scalar); }

            if (RNG.Next(100) <= t) { result.Dexterity += RNG.Next(Scalar); }
            if (RNG.Next(100) <= t) { result.Dexterity -= RNG.Next(Scalar); }

            if (RNG.Next(100) <= t) { result.Constitution += RNG.Next(Scalar); }
            if (RNG.Next(100) <= t) { result.Constitution -= RNG.Next(Scalar); }

            if (RNG.Next(100) <= t) { result.Intelligence += RNG.Next(Scalar); }
            if (RNG.Next(100) <= t) { result.Intelligence -= RNG.Next(Scalar); }

            if (RNG.Next(100) <= t) { result.Wisdom += RNG.Next(Scalar); }
            if (RNG.Next(100) <= t) { result.Wisdom -= RNG.Next(Scalar); }

            if (RNG.Next(100) <= t) { result.Charisma += RNG.Next(Scalar); }
            if (RNG.Next(100) <= t) { result.Charisma -= RNG.Next(Scalar); }

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