using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    public class Room
    {
        public Inventory Contents;   //Everything in the room
        public string Description;   //General description of the room
        public string ExtraDescript; //Enhanced description of the room
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
                if (DoorCount == 2) { result += "two doors to the "; }
                if (DoorCount == 3) { result += "three doors to the "; }
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

        public void GenerateRandom(int _seed, List<Item> _itemsMaster, List<Weapon> _weaponsMaster, List<Monster> _monstersMaster)
        {
            Random RNG = new Random(_seed);
            Description = "You enter a randomly generated room. ";
            ExtraDescript = "This room was generated at random, so there's not much of a description. Sorry.";

            int n = RNG.Next(10);
            for (int i = 0; i < n; i++)
            {
                Contents.Add(_itemsMaster[(int)RNG.Next(_itemsMaster.Count)]);
            }

            n = RNG.Next(5);
            for (int i = 0; i < n; i++)
            {
                Contents.Add(_weaponsMaster[(int)RNG.Next(_weaponsMaster.Count)]);
            }


            n = RNG.Next(5);
            for (int i = 0; i < n; i++)
            {
                Contents.Add(_monstersMaster[(int)RNG.Next(_monstersMaster.Count)]);
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
    }
}
