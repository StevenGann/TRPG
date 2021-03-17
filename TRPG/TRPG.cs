using System;
using System.Collections.Generic;
using System.IO;

namespace TRPG
{
    /// <summary>
    /// The core class of TRPG. Create one instance per player.
    /// </summary>
    public class TRPG_core
    {
        public Player player;
        public List<Message> messages = new List<Message>();
        public List<Item> ItemsMaster;   //}
        public List<Weapon> WeaponsMaster; //}>- These collections contain every Item, Weapon, and Monster
        public List<Monster> MonstersMaster;//}   used in this game.

        private Room CurrentRoom = new Room();

        private bool showingHelp = false;
        private readonly string helpText = "";
        private string underHelpText = "";
        private bool resetScroll = true;

        private readonly GUI gui;

        /// <summary>
        /// Most of the basic game configuration is done here.
        /// </summary>
        public TRPG_core()
        {
            player = new Player();
            gui = new GUI(100, 30, true);//Configure the size of the GUI, and enable/disable dynamic size
            messages = new List<Message>();

            LoadItems();

            player.Contents.Add(WeaponsMaster[1]);

            Random RNG = new Random();
            player.Buffs.Scramble(RNG.Next(), 5);

            //Define the help text that shows when the player says "help".
            helpText = "COMMON COMMANDS\n";
            helpText += "This is a small list of some common and useful commands ";
            helpText += "for navigating the game. There are many other commands, ";
            helpText += "but these few are necessary for manipulating the interface.\n\n";
            helpText += "\"open inventory\"             - Makes the inventory tray much larger.\n";
            helpText += "\"close inventory\"            - Collapses the inventory tray back to 1 line.\n";
            helpText += "\"scroll down\", \"sd\"        - Scrolls very large texts down.\n";
            helpText += "\"scroll up\",   \"su\"        - Scrolls very large texts up.\n";
            helpText += "\"go north\"                   - Moves the player to the adjacent room North.\n";
            helpText += "\"go south\"                   - Moves the player to the adjacent room South.\n";
            helpText += "\"go east\"                    - Moves the player to the adjacent room East.\n";
            helpText += "\"go west\"                    - Take a wild guess.\n";
            helpText += "\"examine\"                    - Examine items, monsters, self, or \"all\".\n";
            helpText += "\"attack MONSTER with WEAPON\" - name a monster and a weapon to use.\n";
            //helpText += "\"\"         - \n";
            helpText += "\"help\"            - shows and hides this help.\n";

            CurrentRoom.GenerateRandom(RNG.Next(), ItemsMaster, WeaponsMaster, MonstersMaster);
            CommandSystem.Clear();
            CommandSystem.Add(CurrentRoom);
            CommandSystem.Build();
            gui.MainText = CurrentRoom.Description;
        }

        /// <summary>
        /// Advance the game forward one step. Game logic goes here.
        /// </summary>
        public void Step()
        {
            foreach (Item monster in CurrentRoom.Contents)
            {
                if (monster is Monster monster2)
                {
                    gui.MainText += "\n" + GameRules.MonsterAttacksPlayer(this, player.Buffs + player.Contents, monster2, (int)DateTime.Now.Ticks & 0x0000FFFF);
                }

                if (player.Health <= 0)
                {
                    gui.MainText += "\n\nYou died? :(";
                    gui.Render(this);
                    Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("You have died.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// Update the interface. This is called every time a command is entered,
        /// so the command parser and GUI code go here.
        /// </summary>
        public void Update(string _input)
        {
            if (_input != "")
            {
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

            List<Adjective> Adjectives = new List<Adjective>
            {
                new Adjective(){ Text="large", Weight=1.5, Value=1.5, Damage=1.5, Defense=1.5, Health=1.5 },
                new Adjective(){ Text="small", Weight=0.75, Value=0.75, Damage=0.75, Defense=0.75, Health=0.75 },
                new Adjective(){ Text="sharp", Value=2.0, Damage=2.0, Intelligence=2.0, Accuracy=1.5 },
                new Adjective(){ Text="dull", Value=0.5, Damage=0.5, Defense=1.5, Intelligence=0.5},
                new Adjective(){ Text="polished", Value=2.0, Dexterity=2.0, Wisdom=2.0 },
                new Adjective(){ Text="dirty", Value=0.5 },
            };

            Random RNG = new Random();

            Monster tempMonster = new Monster("Grue", "M_00", 10, 75);
            MonstersMaster.Add(tempMonster);
            tempMonster = new Monster("Wraith", "M_01", 5, 150);
            MonstersMaster.Add(tempMonster);
            tempMonster = new Monster("Wendigo", "M_02", 20, 50);
            MonstersMaster.Add(tempMonster);
            tempMonster = new Monster("Vampire", "M_03", 10, 50);
            MonstersMaster.Add(tempMonster);
            tempMonster = new Monster("Horta", "M_04", 15, 35);
            MonstersMaster.Add(tempMonster);
            tempMonster = new Monster("Zombie", "M_05", 5, 25);
            MonstersMaster.Add(tempMonster);
            tempMonster = new Monster("Skeleton", "M_06", 50, 50);
            MonstersMaster.Add(tempMonster);

            for (int i = 0; i < 10; i++)
            {
                Weapon tempWeapon = new Weapon(Adjectives[RNG.Next(Adjectives.Count)], "sword", "W_00", 10, 10);
                WeaponsMaster.Add(tempWeapon);
                tempWeapon = new Weapon(Adjectives[RNG.Next(Adjectives.Count)], "knife", "W_01", 20, 2);
                WeaponsMaster.Add(tempWeapon);
                tempWeapon = new Weapon(Adjectives[RNG.Next(Adjectives.Count)], "club", "W_02", 7, 25);
                WeaponsMaster.Add(tempWeapon);
                tempWeapon = new Weapon(Adjectives[RNG.Next(Adjectives.Count)], "dagger", "W_03", 25, 5);
                WeaponsMaster.Add(tempWeapon);
                tempWeapon = new Weapon(Adjectives[RNG.Next(Adjectives.Count)], "saber", "W_04", 15, 7);
                WeaponsMaster.Add(tempWeapon);
                tempWeapon = new Weapon(Adjectives[RNG.Next(Adjectives.Count)], "stick", "W_05", 7, 1);
                WeaponsMaster.Add(tempWeapon);
            }

            Item tempItem = new Item("Gold", "I_00", 1, 0);
            ItemsMaster.Add(tempItem);
            tempItem = new Item("Amulet", "I_01", 200, 1);
            ItemsMaster.Add(tempItem);
            tempItem = new Item("Rock", "I_02", 0, 5);
            ItemsMaster.Add(tempItem);
            tempItem = new Item("Bone", "I_03", 0, 1);
            ItemsMaster.Add(tempItem);
            tempItem = new Item("Skull", "I_04", 5, 1);
            ItemsMaster.Add(tempItem);
            tempItem = new Item("Potion", "I_05", 10, 1);
            ItemsMaster.Add(tempItem);
            tempItem = new Item("Poison", "I_06", 10, 1);
            ItemsMaster.Add(tempItem);
            tempItem = new Item("Gem", "I_07", 100, 1);
            ItemsMaster.Add(tempItem);
        }
    }
}