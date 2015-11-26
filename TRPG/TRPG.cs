using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    /// <summary>
    /// The core class of TRPG. Create one instance per player.
    /// </summary>
    public class TRPG_core
    {
        public Inventory playerInventory;
        public List<Message> messages = new List<Message>();
        public Parser parser = new Parser();
        public List<Item> ItemsMaster;   //}
        public List<Weapon> WeaponsMaster; //}>- These collections contain every Item, Weapon, and Monster
        public List<Monster> MonstersMaster;//}   used in this game.
        public Dungeon dungeon;


        bool showingHelp = false;
        string helpText = "";
        string underHelpText = "";

        GUI gui;

        /// <summary>
        /// Most of the basic game configuration is done here.
        /// </summary>
        public TRPG_core()
        {
            playerInventory = new Inventory();
            gui = new GUI(100, 30, true);//Configure the size of the GUI, and enable/disable dynamic size
            messages = new List<Message>();
            parser = new Parser();
            dungeon = new Dungeon();

            LoadItems();
            dungeon.GenerateRandom(1337, ItemsMaster, WeaponsMaster, MonstersMaster);

            //Define the help text that shows when the player says "help".
            helpText = "COMMON COMMANDS\n";
            helpText += "This is a small list of some common and useful commands ";
            helpText += "for navigating the game. There are many other commands, ";
            helpText += "but these few are necessary for manipulating the interface.\n\n";
            helpText += "\"open inventory\"  - Makes the inventory tray much larger.\n";
            helpText += "\"close inventory\" - Collapses the inventory tray back to 1 line.\n";
            helpText += "\"scroll down\"     - Scrolls very large texts down one line.\n";
            helpText += "\"scroll up\"       - Scrolls very large texts up one line.\n";
            helpText += "\"help\"            - shows this help.\n";

            gui.MainText = dungeon.CurrentRoom.Description;

        }

        

        /// <summary>
        /// Advance the game forward one step. Game logic goes here.
        /// </summary>
        public void Step()
        {

        }

        /// <summary>
        /// Update the interface. This is called every time a command is entered,
        /// so the command parser and GUI code go here.
        /// </summary>
        public void Update(string _input)
        {
            if (_input != "")
            {
                messages.Add(new Message("Input: " + _input));
                Command newCommand = parser.Parse(_input);

                //Catch special commands outside of actual parsing
                //These commands do not advance the game
                if (newCommand.Text.ToLower() == "expand inventory")
                {
                    if (gui.InventorySize < 10) { gui.InventorySize++; }
                }
                else if (newCommand.Text.ToLower() == "shrink inventory")
                {
                    if (gui.InventorySize > 1) { gui.InventorySize--; }
                }
                else if (newCommand.Text.ToLower() == "open inventory")
                {
                    gui.InventorySize = 6;
                }
                else if (newCommand.Text.ToLower() == "close inventory")
                {
                    gui.InventorySize = 1;
                }
                else if (newCommand.Text.ToLower() == "scroll down")
                {
                    if (gui.MainScroll < 10) { gui.MainScroll++; }
                }
                else if (newCommand.Text.ToLower() == "scroll up")
                {
                    if (gui.MainScroll > 0) { gui.MainScroll--; }
                }
                else if (newCommand.Text.ToLower() == "help")
                {
                    if (!showingHelp)
                    {
                        showingHelp = true;
                        underHelpText = gui.MainText;
                        gui.MainText = helpText;
                    }
                    else
                    {
                        showingHelp = false;
                        gui.MainText = underHelpText;
                    }

                }
                else if (newCommand.Text.ToLower() == "examine room")
                {
                    gui.MainText = dungeon.CurrentRoom.ExtraDescript;
                    gui.MainText += "\n\n";
                    gui.MainText += dungeon.CurrentRoom.GetDoorsDescription();
                }
                else
                {
                    if (newCommand.Pattern != "")
                    {
                        messages.Add(new Message("Pattern: " + newCommand.Pattern));//For debugging. Remove later!
                        Step(); //The player has issued an action. Advance the game one step.
                    }
                    else
                    {
                        messages.Add(new Message("I did not understand any of that."));
                    }
                }
            }

            gui.Render(this);
        }

        /// <summary>
        /// Load all the items from file.
        /// Hardcoded items until file loading is added.
        /// </summary>
        private void LoadItems()
        {
            ItemsMaster = new List<Item>();
            WeaponsMaster = new List<Weapon>();
            MonstersMaster = new List<Monster>();

            Monster tempMonster;
            tempMonster = new Monster("Testoro", 50, 25);
            MonstersMaster.Add(tempMonster);
            tempMonster = new Monster("Testito", 50, 25);
            MonstersMaster.Add(tempMonster);
            tempMonster = new Monster("Testra", 50, 25);
            MonstersMaster.Add(tempMonster);

            Weapon tempWeapon;
            tempWeapon = new Weapon("Sword", 10, 10);
            WeaponsMaster.Add(tempWeapon);
            tempWeapon = new Weapon("Dagger", 20, 2);
            WeaponsMaster.Add(tempWeapon);
            tempWeapon = new Weapon("Club", 5, 25);
            WeaponsMaster.Add(tempWeapon);

            Item tempItem;
            tempItem = new Item("Gold", 1, 0);
            ItemsMaster.Add(tempItem);
            tempItem = new Item("Amulet", 200, 1);
            ItemsMaster.Add(tempItem);
            tempItem = new Item("Rock", 0, 5);
            ItemsMaster.Add(tempItem);
        }
    }
}
