using System;
using System.Collections.Generic;

namespace TRPG
{
    public class Room : ICommandProvider
    {
        public Inventory Contents;   //Everything in the room
        public string Description;   //General description of the room
        public string ExtraDescript; //Enhanced description of the room
        public Buff Buffs;
        public int North = -1;
        public int South = -1;
        public int East = -1;
        public int West = -1;
        public int ID = 0;

        public int DoorCount
        {
            get
            {
                int result = 0;
                if (North != -1)
                {
                    result++;
                }
                if (South != -1)
                {
                    result++;
                }
                if (East != -1)
                {
                    result++;
                }
                if (West != -1)
                {
                    result++;
                }
                return result;
            }
        }

        public int MonsterCount
        {
            get
            {
                int result = 0;
                foreach (Item item in Contents)
                {
                    if (item is Monster) { result++; }
                }
                return result;
            }
        }

        public List<KeyValuePair<string, int>> Neighbors
        {
            get
            {
                var result = new List<KeyValuePair<string, int>>();

                if (North != -1) { result.Add(new KeyValuePair<string, int>("North", North)); }
                if (South != -1) { result.Add(new KeyValuePair<string, int>("South", South)); }
                if (East != -1) { result.Add(new KeyValuePair<string, int>("East", East)); }
                if (West != -1) { result.Add(new KeyValuePair<string, int>("West", West)); }

                return result;
            }
        }

        public Room()
        {
            Contents = new Inventory();
            Buffs = new Buff();
            Description = "A blank, empty room.";
            ExtraDescript = "There's seriously nothing here. It's just a square room with ";
            ExtraDescript += "white walls. Where is that light even coming from? How do you ";
            ExtraDescript += "get out!?";
        }

        public string GetDoorsDescription()
        {
            string result = "";

            result += "There ";

            if (DoorCount == 0)
            {
                result += "is no way out of this room. You are trapped!";
                return result;
            }
            else if (DoorCount == 1)
            {
                result += "is one door to the ";
            }
            else if (DoorCount == 4)
            {
                result += "is a door in every direction, ";
            }
            else
            {
                if (DoorCount == 2) { result += "are two doors to the "; }
                if (DoorCount == 3) { result += "are three doors to the "; }
            }

            if (DoorCount == 1)
            {
                result += Neighbors[0].Key;
            }
            else if (DoorCount == 2)
            {
                result += Neighbors[0].Key;
                result += " and ";
                result += Neighbors[1].Key;
            }
            else if (DoorCount == 3)
            {
                result += Neighbors[0].Key;
                result += ", ";
                result += Neighbors[1].Key;
                result += ", and ";
                result += Neighbors[2].Key;
            }
            else if (DoorCount == 4)
            {
                result += Neighbors[0].Key;
                result += ", ";
                result += Neighbors[1].Key;
                result += ", ";
                result += Neighbors[2].Key;
                result += ", and ";
                result += Neighbors[3].Key;
            }

            result += ".";
            return result;
        }

        public string GetContentsDescription()
        {
            string result = "";

            result += "In this room you see";

            foreach (Item item in Contents)
            {
                if (!(item is Monster))
                {
                    result += " a ";
                    result += item.GetFullName();
                    result += ",";
                }
            }

            foreach (Item item in Contents)
            {
                if (item is Monster)
                {
                    result += " a ";
                    result += item.GetFullName();
                    result += " monster,";
                }
            }

            return result + " ID:" + Convert.ToString(ID);
        }

        public void GenerateRandom(int _seed, List<Item> _itemsMaster, List<Weapon> _weaponsMaster, List<Monster> _monstersMaster)
        {
            Random RNG = new Random(_seed);
            Buffs = Buff.Randomized(RNG.Next(), 10);
            Description = "You enter a randomly generated room. ";
            ExtraDescript = "This room was generated at random, so there's not much of a description. Sorry.";

            int rt = RNG.Next(5);

            if (rt == 0)
            {
                Description = "This room is poorly lit. ";
                ExtraDescript = "The only source of light in the room is a faint glow off some green fungus, ";
                ExtraDescript += "but from what you can see it is roughly square, with a dirt floor and rough ";
                ExtraDescript += "stone brick walls. The room stinks of mildew and decay. ";
            }
            else if (rt == 1)
            {
                Description = "You are in a large, round room. ";
                ExtraDescript = "This room is large and circular. It is brightly lit by an ornate, gold chandelier in the center, ";
                ExtraDescript += "and you can see file gold and lapis lazuli inlays in the marble floor. ";
                ExtraDescript += "The walls are ornamented with masterful bas relief sculptures of wars long past. ";
            }
            else if (rt == 2)
            {
                Description = "You are now standing in water. ";
                ExtraDescript = "This room is small and irregular in shape, and you are standing in ankle-deep water. ";
                ExtraDescript += "The water is murky with scum floating on top, but you feel silt beneath your feet ";
                ExtraDescript += "with no indication of a solid floor. ";
            }
            else if (rt == 3)
            {
                Description = "This room is warm and has a sandy floor. ";
                ExtraDescript = "This room is long and rectangular, with smooth stone brick walls and a floor covered in ";
                ExtraDescript += "pale yellow sand. It is very warm in here, and you sweat a little as you look around. ";
            }
            else if (rt == 4)
            {
                Description = "You are in a dim, hot room. ";
                ExtraDescript = "This is less of a room than a cave. The walls are craggy reddish stone covered in ";
                ExtraDescript += "scrapes, as if gouged by claws. It is very, very hot in here, and from cracks ";
                ExtraDescript += "in the floor you can hear screams rising from the dark. What is this awful place? ";
            }

            int n = RNG.Next(5);
            for (int i = 0; i < n; i++)
            {
                Contents.Add(_itemsMaster[(int)RNG.Next(_itemsMaster.Count)].Copy());
            }

            n = RNG.Next(3);
            for (int i = 0; i < n; i++)
            {
                Contents.Add(_weaponsMaster[(int)RNG.Next(_weaponsMaster.Count)].Copy());
            }

            n = RNG.Next(5);
            for (int i = 0; i < n; i++)
            {
                Contents.Add(_monstersMaster[(int)RNG.Next(_monstersMaster.Count)].Copy());
            }
            Description += "There ";
            if (n == 0)
            {
                Description += "are no monsters ";
            }
            else if (n == 1)
            {
                Description += "is one monster ";
            }
            else if (n == 2)
            {
                Description += "are two monsters ";
            }
            else
            {
                Description += "are several monsters ";
            }
            Description += "in here.\n";
        }

        public string[] GetCommands()
        {
            return new string[]
            {
                "look at",
                "examine"
            };
        }
    }
}