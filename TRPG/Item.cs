﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TRPG
{
    /// <summary>
    /// Generic Item
    /// </summary>
    [Serializable]
    public class Item : ICommandProvider
    {
        public string Name = "";        //Name of item
        public string Lore = "";        //Optional lore for item
        private double weight = 0;        //Weight of item for inventory tracking
        private double value = 0;         //Trading value of item
        public List<string> Adjectives; //Adjectives to precede name
        public Buff Buffs;              //Buffs given to player when in inventory
        private int damage = 0;          //Base damage when used as a weapon
        private int defense = 0;         //Base defense
        private int accuracy = 0;        //Base accuracy when used as a weapon
        private int health = 0;
        private int uses = -1;           //For consumable items, how many more times the item can be used (-1 for infinite)
        private int experience = 0;      //Players have experience, items and monster give it
        public Inventory Contents;      //For container objects, like lockboxes and bags of holding
        public string ID = "NULL";

        //Serializer Ignored
        [XmlIgnore]
        public List<Adjective> AdjectiveIndex = new List<Adjective>();

        public double Weight
        {
            get
            {
                return weight * SumAdjectives().Weight;
            }

            set
            {
                weight = value;
            }
        }

        public double Value
        {
            get
            {
                return value * SumAdjectives().Value;
            }

            set
            {
                this.value = value;
            }
        }

        public int Damage
        {
            get
            {
                return (int)(((float)damage) * SumAdjectives().Damage);
            }

            set
            {
                damage = value;
            }
        }

        public int Defense
        {
            get
            {
                return (int)(((float)defense) * SumAdjectives().Defense);
            }

            set
            {
                defense = value;
            }
        }

        public int Accuracy
        {
            get
            {
                return (int)(((float)accuracy) * SumAdjectives().Accuracy);
            }

            set
            {
                accuracy = value;
            }
        }

        public int Health
        {
            get
            {
                return (int)(((float)health) * SumAdjectives().Health);
            }

            set
            {
                health = value;
            }
        }

        public int Uses
        {
            get
            {
                return (int)(((float)uses) * SumAdjectives().Uses);
            }

            set
            {
                uses = value;
            }
        }

        public int Experience
        {
            get
            {
                return (int)(((float)experience) * SumAdjectives().Experience);
            }

            set
            {
                experience = value;
            }
        }

        public Item()
        {
            Adjectives = new List<string>();
            Buffs = new Buff();
        }

        public Item(string _name, string _id)
        {
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
            ID = _id;
        }

        public Item(string _name, string _id, int _value, int _weight)
        {
            Name = _name;
            Value = _value;
            Weight = _weight;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
            ID = _id;
        }

        private Adjective SumAdjectives()
        {
            Adjective result = new Adjective();

            if (Adjectives?.Count > 0)
            {
                foreach (string a in Adjectives)
                {
                    result += FindAdjective(a);
                }
            }

            return result;
        }

        private Adjective FindAdjective(string _adjective)
        {
            Adjective result = new Adjective();

            foreach (Adjective a in AdjectiveIndex)
            {
                if (a.Text == _adjective)
                {
                    result = a;
                }
            }

            return result;
        }

        public void Write()
        {
            Console.Write(Name);
            Console.Write(" (");
            Console.Write(Convert.ToString((int)Value));
            Console.Write("/");
            Console.Write(Convert.ToString((int)Weight));
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
            if ((a is null) || (b is null))
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
            return !(a == b);
        }

        public override string ToString()
        {
            string result = "";

            if (this is Weapon)
            {
                result += GetFullName() + " is a weapon that does ";
                result += Damage + " base damage, with a base accuracy of ";
                result += Accuracy + ". It weighs ";
                result += (int)Weight + "lbs. and is worth ";
                result += (int)Value + " gold. ";
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

                result += "You are an adventurer ";
                if (Experience > 0) { result += "with " + Experience + " experience, "; }
                result += "traveling through a mysterious dungeon seeking treasure and glory.\n";
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

        public string GetFullName()
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

        public Item Copy()
        {
            Item result = new Item
            {
                Name = Name,
                Lore = Lore,
                Weight = Weight,
                Value = Value,
                Adjectives = Adjectives,
                Buffs = Buffs,
                Damage = Damage,
                Defense = Defense,
                Accuracy = Accuracy,
                Health = Health,
                Uses = Uses,
                Experience = Experience,
                Contents = Contents
            };
            return result;
        }

        public override bool Equals(object obj)
        {
            return obj is Item;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public virtual string[] GetCommands()
        {
            return new string[]
            {
                "discard"
            };
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

        public Weapon(string _name, string _id)
        {
            Weight = 5;
            Value = 10;
            Damage = 10;
            Accuracy = 75;
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
            ID = _id;
        }

        public Weapon(string _name, string _id, int _value, int _weight)
        {
            Weight = 5;
            Value = 10;
            Damage = 10;
            Accuracy = 75;
            Name = _name;
            Accuracy = 1 + (Accuracy * (int)((float)_value / (float)Value));
            Damage = 1 + (Damage * (int)(_weight / (float)Weight));
            Value = _value;
            Weight = _weight;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
            ID = _id;
        }

        public Weapon(Adjective _adjective, string _name, string _id, int _value, int _weight)
        {
            Weight = 5;
            Value = 10;
            Damage = 10;
            Accuracy = 75;
            Name = _name;
            Accuracy = 1 + (Accuracy * (int)((float)_value / (float)Value));
            Damage = 1 + (Damage * (int)((float)_weight / (float)Weight));
            Value = _value;
            Weight = _weight;
            Adjectives = new List<string>
            {
                _adjective.Text
            };
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
            ID = _id;
        }

        new public Weapon Copy()
        {
            Weapon result = new Weapon
            {
                Name = Name,
                Lore = Lore,
                Weight = Weight,
                Value = Value,
                Adjectives = Adjectives,
                Buffs = Buffs,
                Damage = Damage,
                Defense = Defense,
                Accuracy = Accuracy,
                Health = Health,
                Uses = Uses,
                Experience = Experience,
                Contents = Contents,
                ID = ID + "_1"
            };
            return result;
        }

        public override string[] GetCommands()
        {
            return new string[]
            {
                "discard",
                "equip",
            };
        }
    }

    public class Monster : Item
    {
        public Monster()
        {
            Health = 25;
            Damage = 10;
            Defense = 10;
            Accuracy = 75;
            Adjectives = new List<string>();
            Buffs = new Buff();
        }

        public Monster(string _name, string _id)
        {
            Health = 25;
            Damage = 10;
            Accuracy = 75;
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 50);
            ID = _id;
        }

        public Monster(string _name, string _id, int _damage, int _accuracy)
        {
            Health = 25;
            Damage = _damage;
            Accuracy = _accuracy;
            Name = _name;
            Adjectives = new List<string>();
            Buffs = Buff.Randomized(_name.GetHashCode(), 10);
            ID = _id;
        }

        new public Monster Copy()
        {
            Monster result = new Monster
            {
                Name = Name,
                Lore = Lore,
                Weight = Weight,
                Value = Value,
                Adjectives = Adjectives,
                Buffs = Buffs,
                Damage = Damage,
                Defense = Defense,
                Accuracy = Accuracy,
                Health = Health,
                Uses = Uses,
                Experience = Experience,
                Contents = Contents,
                ID = ID + "_1"
            };
            return result;
        }

        public override string[] GetCommands()
        {
            return new string[]
            {
                "attack",
            };
        }
    }

    public class Player : Monster
    {
        public Player()
        {
            Health = 100;
            Adjectives = null;
            Contents = new Inventory();
        }

        new public Player Copy()
        {
            Player result = new Player
            {
                Name = Name,
                Lore = Lore,
                Weight = Weight,
                Value = Value,
                Adjectives = Adjectives,
                Buffs = Buffs,
                Damage = Damage,
                Defense = Defense,
                Accuracy = Accuracy,
                Health = Health,
                Uses = Uses,
                Experience = Experience,
                Contents = Contents
            };
            return result;
        }

        public static Item FindItem(string _id, List<Item> _index)
        {
            Item result = new Item();

            foreach (Item i in _index)
            {
                if (i.ID == _id)
                {
                    result = i;
                }
            }

            return result;
        }
    }
}