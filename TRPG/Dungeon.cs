using System;
using System.Collections.Generic;

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
        private readonly List<KeyValuePair<int, Room>> Rooms;

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
                if (currentRoom.MonsterCount > 0)
                {
                    return -2;
                }
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
                if (currentRoom.MonsterCount > 0)
                {
                    return -2;
                }
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
                if (currentRoom.MonsterCount > 0)
                {
                    return -2;
                }
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
                if (currentRoom.MonsterCount > 0)
                {
                    return -2;
                }
                foreach (KeyValuePair<int, Room> room in Rooms)
                {
                    if (room.Key == currentRoom.West)//Find the room with that ID
                    { currentRoom = room.Value; }
                }
                result = 1;
            }
            return result;
        }

        public void GenerateRandom(int _seed, List<Item> _itemsMaster, List<Weapon> _weaponsMaster, List<Monster> _monstersMaster)
        {
            Random RNG = new Random(_seed);

            currentRoom.GenerateRandom(RNG.Next(), _itemsMaster, _weaponsMaster, _monstersMaster);
            currentRoom.ID = 0;
            Rooms.Add(new KeyValuePair<int, Room>(0, currentRoom));
            Start = currentRoom;

            for (int j = 0; j <= 10; j++)
            {
                Room previousRoom = Rooms[RNG.Next(Rooms.Count - 1)].Value;

                for (int i = 1; i <= 3; i++)
                {
                    Room nextRoom = new Room();
                    nextRoom.GenerateRandom(RNG.Next(), _itemsMaster, _weaponsMaster, _monstersMaster);
                    nextRoom.ID = i;
                    bool dirPicked = false;
                    while (!dirPicked)
                    {
                        int dir = RNG.Next(4);
                        if (dir == 0 && previousRoom.North == -1) //New room is to the North of the old room
                        {
                            previousRoom.North = nextRoom.ID;
                            nextRoom.South = previousRoom.ID;
                            dirPicked = true;
                        }
                        if (dir == 1 && previousRoom.South == -1) //New room is to the South of the old room
                        {
                            previousRoom.South = nextRoom.ID;
                            nextRoom.North = previousRoom.ID;
                            dirPicked = true;
                        }
                        if (dir == 2 && previousRoom.East == -1) //New room is to the East of the old room
                        {
                            previousRoom.East = nextRoom.ID;
                            nextRoom.West = previousRoom.ID;
                            dirPicked = true;
                        }
                        if (dir == 3 && previousRoom.West == -1) //New room is to the West of the old room
                        {
                            previousRoom.West = nextRoom.ID;
                            nextRoom.East = previousRoom.ID;
                            dirPicked = true;
                        }
                    }

                    previousRoom = nextRoom;
                    Rooms.Add(new KeyValuePair<int, Room>(nextRoom.ID, nextRoom));
                }
            }
        }
    }
}