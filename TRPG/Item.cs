using System;
using System.Collections.Generic;
using System.Linq;

namespace TRPG
{
    /// <summary>
    /// Generic Item
    /// </summary>
    public class Item
    {
        public string Name = "";        //Name of item
        public string Lore = "";        //Optional lore for item
        public float Weight = 0;        //Weight of item for inventory tracking
        public float Value = 0;         //Trading value of item
        public List<string> Adjectives; //Adjectives to precede name
        public Buff Buffs;              //Buffs given to player when in inventory
        public int Damage = 0;          //Base damage when used as a weapon
        public int Defense = 0;         //Base defense
        public int Accuracy = 0;        //Base accuracy when used as a weapon
        public int Health = 0;
        public int Uses = -1;           //For consumable items, how many more times the item can be used (-1 for infinite)
        public Inventory Contents;      //For container objects, like lockboxes and bags of holding

        public Item()
        {
            Adjectives = new List<string>();
            Buffs = new Buff();
        }

        public Item(string _name)
        {
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
        }

        public Item(string _name, int _value, int _weight)
        {
            Name = _name;
            Value = _value;
            Weight = _weight;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
        }

        public void Write()
        {
            Console.Write(Name);
            Console.Write(" (");
            Console.Write(Convert.ToString(Value));
            Console.Write("/");
            Console.Write(Convert.ToString(Weight));
            Console.Write(")");
        }

        public static Item operator +(Item a, Buff b)
        {
            a.Buffs.Strength += b.Strength;
            a.Buffs.Dexterity += b.Dexterity;
            a.Buffs.Constitution += b.Constitution;
            a.Buffs.Intelligence += b.Intelligence;
            a.Buffs.Wisdom += b.Wisdom;
            a.Buffs.Charisma += b.Charisma;
            if (b.Adjective != "")
            {
                a.Adjectives.Add(b.Adjective);
            }
            return a;
        }

        public static Item operator +(Item a, Item b)
        {
            Item result = a;

            if (a.Name == b.Name)
                result.Adjectives = a.Adjectives.Union(b.Adjectives).ToList();
            result.Buffs = a.Buffs + b.Buffs;

            return result;
        }

        public static bool operator ==(Item a, Item b)
        {
            bool result = false;

            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            //Some things *might* differ between a and b, but if they are identical "enough",
            //consider it a match. Hashing would probably be the best solution to finding
            //a perfect match, but I think there'd be consequences tot hat.
            //Too tired to think about this too much. Fix it, future self!
            if (a.Name == b.Name)
            {
                if (a.Adjectives == b.Adjectives &&
                    a.Buffs == b.Buffs &&
                    a.Health == b.Health)
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool operator !=(Item a, Item b)
        {
            bool result = false;

            result = !(a == b);

            return result;
        }

        public override string ToString()
        {
            string result = "";

            if (this is Weapon)
            {
                result += Name + " is a weapon that does ";
                result += Damage + " base damage, with a base accuracy of ";
                result += Accuracy + ". It weighs ";
                result += Weight + "lbs. and is worth ";
                result += Value + " gold. ";
                if (Buffs.Magnitude > 0)
                {
                    result += "\nThis " + Name + " has the following enchantments:\n";
                    result += Buffs.ToString();
                }
            }
            else if (this is Player)
            {
                if (Name != "")
                {
                    result += "Your name is " + Name + ".\n";
                }

                result += "You are an adventurer traveling through a mysterious dungeon seeking treasure and glory.\n";
                result += "Your health is " + Health + " and you are currently carrying " + Contents.Weight + "lbs. of loot.\n\n";
                result += "Your base stats are as follows:\n";
                result += Buffs + "\n\n";

                if (Contents.Count > 0)
                {
                    result += "After factoring in the effects of your inventory, your actual stats are as follows:\n";
                    Buff finalBuff = Buffs + Contents;
                    finalBuff.Clamp();
                    result += finalBuff + "\n\n";

                    result += "Here's a rundown of your inventory:\n";
                    result += "===================================\n\n";

                    foreach (Item item in Contents)
                    {
                        result += item.ToString();
                        result += "\n-----------------------------------\n\n";
                    }
                }
            }
            else if (this is Monster)
            {
                result += Name + " is a monster that does ";
                result += Damage + " base damage, with a base accuracy of ";
                result += Accuracy + ". Its health is ";
                result += Health + ".";
                if (Buffs.Magnitude > 0)
                {
                    result += "\nThis " + Name + " has the following stats:\n";
                    result += Buffs.ToString();
                }
            }
            else
            {
                result += "The " + Name + " weighs ";
                result += Weight + "lbs. and is worth ";
                result += Value + " gold. ";
                if (Buffs.Magnitude > 0)
                {
                    result += "\nThis " + Name + " confers the following buffs:\n";
                    result += Buffs.ToString();
                }
            }

            if (Lore != "") { result += "\n\n" + Lore; }

            return result;
        }

        private string GetFullName()
        {
            string result = "";
            if (Adjectives.Count > 0)
            {
                foreach (string adjective in Adjectives)
                {
                    result += adjective + " ";
                }
            }

            result += Name;

            return result;
        }
    }

    public class Weapon : Item
    {
        public Weapon()
        {
            Weight = 5;
            Value = 10;
            Damage = 10;
            Accuracy = 75;
            Adjectives = new List<string>();
            Buffs = new Buff();
        }

        public Weapon(string _name)
        {
            Weight = 5;
            Value = 10;
            Damage = 10;
            Accuracy = 75;
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
        }

        public Weapon(string _name, int _value, int _weight)
        {
            Weight = 5;
            Value = 10;
            Damage = 10;
            Accuracy = 75;
            Name = _name;
            Accuracy = 1 + Accuracy * (int)((float)_value / (float)Value);
            Damage = 1 + Damage * (int)((float)_weight / (float)Weight);
            Value = _value;
            Weight = _weight;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
        }

        public Weapon(string _adjective, string _name, int _value, int _weight)
        {
            Weight = 5;
            Value = 10;
            Damage = 10;
            Accuracy = 75;
            Name = _name;
            Accuracy = 1 + Accuracy * (int)((float)_value / (float)Value);
            Damage = 1 + Damage * (int)((float)_weight / (float)Weight);
            Value = _value;
            Weight = _weight;
            Adjectives = new List<string>();
            Adjectives.Add(_adjective);
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
        }
    }

    public class Monster : Item
    {
        public Monster()
        {
            Health = 100;
            Damage = 10;
            Defense = 10;
            Accuracy = 75;
            Adjectives = new List<string>();
            Buffs = new Buff();
        }

        public Monster(string _name)
        {
            Health = 100;
            Damage = 10;
            Accuracy = 75;
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 50);
        }

        public Monster(string _name, int _damage, int _accuracy)
        {
            Health = 100;
            Damage = _damage;
            Accuracy = _accuracy;
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 50);
        }
    }

    public class Player : Monster
    {
        public Player()
        {
            Health = 100;
            Adjectives = null;
            Buffs = new Buff(10, 10, 10, 10, 10, 10);
            Contents = new Inventory();
        }
    }
}