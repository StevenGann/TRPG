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
        public List<Room> Connections;

        public Room()
        {
            Contents = new Inventory();
            Description = "A blank, empty room.";
            ExtraDescript = "There's seriously nothing here. It's just a square room with ";
            ExtraDescript += "white walls. Where is that light even coming from? How do you ";
            ExtraDescript += "get out!?";
        }
    }
}
