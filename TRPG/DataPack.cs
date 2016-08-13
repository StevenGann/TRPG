using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TRPG
{
    //Main static file handling for TRPG.
    //Directory of XML files containing all data needed for the engine.
    //  adjectives.xml
    //  monsters.xml
    //  weapons.xml
    //  items.xml
    //  dungeon.xml

    public class DataPack
    {
        public string Path = "";
        public string Title = "Untitled";
        public List<Item> ItemsMaster = new List<Item>();
        public List<Weapon> WeaponsMaster = new List<Weapon>();
        public List<Monster> MonstersMaster = new List<Monster>();

        //Constructor for loading from a single folder
        public DataPack(string _path)
        {
            Path = _path;
        }

        public void Load()
        {
            string path = Path + "\\" + Title + "\\";
            loadItems(path);
        }

        public void Save()
        {
            string path = Path + "\\" + Title + "\\";
            Console.WriteLine("Saving to " + path);
            Directory.CreateDirectory(path);
            saveItems(path);
            saveMonsters(path);
            saveWeapons(path);
        }

        private void saveItems(string _path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<Item>));
            StreamWriter file = new StreamWriter(_path + "items.xml");
            writer.Serialize(file, ItemsMaster);
            file.Close();
        }

        private void loadItems(string _path)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(List<Item>));

            FileStream myFileStream = new FileStream(_path + "items.xml", FileMode.Open);
            ItemsMaster = new List<Item>();
            ItemsMaster = (List<Item>)mySerializer.Deserialize(myFileStream);

            myFileStream.Close();
        }

        private void saveMonsters(string _path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<Monster>));
            StreamWriter file = new StreamWriter(_path + "monsters.xml");
            writer.Serialize(file, MonstersMaster);
            file.Close();
        }

        private void saveWeapons(string _path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<Weapon>));
            StreamWriter file = new StreamWriter(_path + "weapons.xml");
            writer.Serialize(file, WeaponsMaster);
            file.Close();
        }
    }
}
