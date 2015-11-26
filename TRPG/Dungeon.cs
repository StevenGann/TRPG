using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    /// <summary>
    /// Tree-style data structure holding all the rooms of the dungeon.
    /// </summary>
    public class Dungeon
    {
        public Room Start;

        public Room CurrentRoom
        {
            get { return currentRoom; }
        }

        private Room currentRoom;
        private List<KeyValuePair<int, Room>> Rooms;

        public Dungeon()
        {
            Rooms = new List<KeyValuePair<int, Room>>();
            currentRoom = new Room();
            Start = currentRoom;
        }

        public int GoNorth()
        {
            int result = -1;

            if (currentRoom.North != -1)
            {
                foreach (KeyValuePair<int, Room> room in Rooms)
                {
                    if (room.Key == currentRoom.North)//Find the room with that ID
                    { currentRoom = room.Value; }
                }
                result = 1;
            }
            return result;
        }

        public int GoSouth()
        {
            int result = -1;

            if (currentRoom.South != -1)
            {
                foreach (KeyValuePair<int, Room> room in Rooms)
                {
                    if (room.Key == currentRoom.South)//Find the room with that ID
                    { currentRoom = room.Value; }
                }
                result = 1;
            }
            return result;
        }

        public int GoEast()
        {
            int result = -1;

            if (currentRoom.East != -1)
            {
                foreach (KeyValuePair<int, Room> room in Rooms)
                {
                    if (room.Key == currentRoom.East)//Find the room with that ID
                    { currentRoom = room.Value; }
                }
                result = 1;
            }
            return result;
        }

        public int GoWest()
        {
            int result = -1;

            if (currentRoom.West != -1)
            {
                foreach (KeyValuePair<int, Room> room in Rooms)
                {
                    if (room.Key == currentRoom.West)//Find the room with that ID
                    { currentRoom = room.Value; }
                }
                result = 1;
            }
            return result;
        }

        public void LoadFromFile(string _path)
        {
            //Eventually, load dungeon from a file.
        }

        public void GenerateRandom(int _seed, List<Item> _itemsMaster, List<Weapon> _weaponsMaster, List<Monster> _monsterMaster)
        {
            Random RNG = new Random(_seed);
        }
    }
}
