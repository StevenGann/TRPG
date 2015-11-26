using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Accuracy = 0;        //Base accuracy when used as a weapon
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
    }

    public class Monster : Item
    {
        public int Health = 100;

        public Monster()
        {
            Damage = 10;
            Accuracy = 75;
            Adjectives = new List<string>();
            Buffs = new Buff();
        }

        public Monster(string _name)
        {
            Damage = 10;
            Accuracy = 75;
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 50);
        }

        public Monster(string _name, int _damage, int _accuracy)
        {
            Damage = _damage;
            Accuracy = _accuracy;
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 50);
        }
    }


}
