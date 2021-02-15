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
        public Parser parser = new Parser();
        public List<Item> ItemsMaster;   //}
        public List<Weapon> WeaponsMaster; //}>- These collections contain every Item, Weapon, and Monster
        public List<Monster> MonstersMaster;//}   used in this game.
        public Dungeon dungeon;

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
            parser = new Parser();
            dungeon = new Dungeon();

            LoadItems();

            player.Contents.Add(WeaponsMaster[1]);

            Random RNG = new Random();
            player.Buffs.Scramble(RNG.Next(), 5);
            dungeon.GenerateRandom(RNG.Next(), ItemsMaster, WeaponsMaster, MonstersMaster);

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

            gui.MainText = dungeon.CurrentRoom.Description;
        }

        /// <summary>
        /// Advance the game forward one step. Game logic goes here.
        /// </summary>
        public void Step()
        {
            foreach (Item monster in dungeon.CurrentRoom.Contents)
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
                messages.Add(new Message("Input: " + _input));
                messages[^1].Foreground = ConsoleColor.Cyan;
                Command newCommand = parser.Parse(_input);
                if (resetScroll)
                { gui.MainScroll = 0; }
                else
                { resetScroll = true; }

                //Catch special commands outside of actual parsing
                //These commands do not advance the game
                if (string.Equals(newCommand.Text, "expand inventory", StringComparison.OrdinalIgnoreCase))
                {
                    if (gui.InventorySize < 10) { gui.InventorySize++; }
                }
                else if (string.Equals(newCommand.Text, "shrink inventory", StringComparison.OrdinalIgnoreCase))
                {
                    if (gui.InventorySize > 1) { gui.InventorySize--; }
                }
                else if (string.Equals(newCommand.Text, "open inventory", StringComparison.OrdinalIgnoreCase))
                {
                    gui.InventorySize = 6;
                }
                else if (string.Equals(newCommand.Text, "close inventory", StringComparison.OrdinalIgnoreCase))
                {
                    gui.InventorySize = 1;
                }
                else if (string.Equals(newCommand.Text, "scroll down", StringComparison.OrdinalIgnoreCase) || string.Equals(newCommand.Text, "sd", StringComparison.OrdinalIgnoreCase))
                {
                    //int maxScroll = 100;
                    //if (gui.MainScroll < maxScroll) { gui.MainScroll += Math.Min(maxScroll - gui.MainScroll, 4); }
                    gui.MainScroll += gui.Height / 2;
                    resetScroll = false;
                }
                else if (string.Equals(newCommand.Text, "scroll up", StringComparison.OrdinalIgnoreCase) || string.Equals(newCommand.Text, "su", StringComparison.OrdinalIgnoreCase))
                {
                    if (gui.MainScroll > 0) { gui.MainScroll -= Math.Min(gui.MainScroll, Math.Max(1, (gui.Height / 2) - 5)); }
                    resetScroll = false;
                }
                else if (string.Equals(newCommand.Text, "help", StringComparison.OrdinalIgnoreCase))
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
                else if (string.Equals(newCommand.Text, "examine room", StringComparison.OrdinalIgnoreCase) || string.Equals(newCommand.Text, "er", StringComparison.OrdinalIgnoreCase))
                {
                    gui.MainText = dungeon.CurrentRoom.ExtraDescript;
                    gui.MainText += "\n\n";
                    gui.MainText += dungeon.CurrentRoom.GetContentsDescription();
                    gui.MainText += "\n\n";
                    gui.MainText += dungeon.CurrentRoom.GetDoorsDescription();
                }
                else if (string.Equals(newCommand.Text, "examine all", StringComparison.OrdinalIgnoreCase))
                {
                    gui.MainText = dungeon.CurrentRoom.ExtraDescript;
                    gui.MainText += "\n\n";
                    gui.MainText += dungeon.CurrentRoom.GetContentsDescription();
                    gui.MainText += "\n\n";
                    gui.MainText += dungeon.CurrentRoom.GetDoorsDescription();
                    gui.MainText += "\n\n";
                    gui.MainText += player.ToString();
                }
                else if (string.Equals(newCommand.Text, "examine self", StringComparison.OrdinalIgnoreCase))
                {
                    gui.MainText = player.ToString();
                }
                else if (string.Equals(newCommand.Text, "go north", StringComparison.OrdinalIgnoreCase))
                {
                    if (dungeon.GoNorth() > 0)
                    {
                        gui.MainText = dungeon.CurrentRoom.Description;
                    }
                    else if (dungeon.GoNorth() == -2)
                    {
                        gui.MainText = "The monsters block your exit.\n" + dungeon.CurrentRoom.ExtraDescript;
                    }
                    else
                    {
                        gui.MainText = "You cannot go that way.\n" + dungeon.CurrentRoom.ExtraDescript;
                    }
                }
                else if (string.Equals(newCommand.Text, "go south", StringComparison.OrdinalIgnoreCase))
                {
                    if (dungeon.GoSouth() > 0)
                    {
                        gui.MainText = dungeon.CurrentRoom.Description;
                    }
                    else if (dungeon.GoSouth() == -2)
                    {
                        gui.MainText = "The monsters block your exit.\n" + dungeon.CurrentRoom.ExtraDescript;
                    }
                    else
                    {
                        gui.MainText = "You cannot go that way.\n" + dungeon.CurrentRoom.ExtraDescript;
                    }
                }
                else if (string.Equals(newCommand.Text, "go east", StringComparison.OrdinalIgnoreCase))
                {
                    if (dungeon.GoEast() > 0)
                    {
                        gui.MainText = dungeon.CurrentRoom.Description;
                    }
                    else if (dungeon.GoEast() == -2)
                    {
                        gui.MainText = "The monsters block your exit.\n" + dungeon.CurrentRoom.ExtraDescript;
                    }
                    else
                    {
                        gui.MainText = "You cannot go that way.\n" + dungeon.CurrentRoom.ExtraDescript;
                    }
                }
                else if (string.Equals(newCommand.Text, "go west", StringComparison.OrdinalIgnoreCase))
                {
                    if (dungeon.GoWest() > 0)
                    {
                        gui.MainText = dungeon.CurrentRoom.Description;
                    }
                    else if (dungeon.GoWest() == -2)
                    {
                        gui.MainText = "The monsters block your exit.\n" + dungeon.CurrentRoom.ExtraDescript;
                    }
                    else
                    {
                        gui.MainText = "You cannot go that way.\n" + dungeon.CurrentRoom.ExtraDescript;
                    }
                }
                else
                {
                    if (newCommand.Pattern != "")
                    {
                        bool turnConsumed = false;
                        messages.Add(new Message("Pattern: " + newCommand.Pattern));//For debugging. Remove later!
                        messages[^1].Foreground = ConsoleColor.DarkGray;

                        if (newCommand.Tokens[0].Value == 1)//If the command starts with a verb
                        {
                            if (newCommand.Tokens[0].Text == "take")
                            {
                                gui.MainText = dungeon.CurrentRoom.Contents.Take(newCommand.Tokens, player.Contents);
                                turnConsumed = true;
                            }

                            if (newCommand.Tokens[0].Text == "drop")
                            {
                                gui.MainText = dungeon.CurrentRoom.Contents.Drop(newCommand.Tokens, player.Contents);
                                turnConsumed = true;
                            }

                            if (newCommand.Tokens[0].Text == "examine")
                            {
                                if (player.Contents.Find(newCommand.Tokens) != null)
                                {
                                    gui.MainText = player.Contents.Find(newCommand.Tokens).ToString();
                                }
                                else if (dungeon.CurrentRoom.Contents.Find(newCommand.Tokens) != null)
                                {
                                    gui.MainText = dungeon.CurrentRoom.Contents.Find(newCommand.Tokens).ToString();
                                }
                                else
                                {
                                    gui.MainText = "There is nothing like that to examine here.";
                                }
                            }

                            if (newCommand.Tokens.Count >= 3 && newCommand.Tokens[0].Text == "attack" && newCommand.Tokens[2].Text == "with")
                            {
                                if (dungeon.CurrentRoom.Contents.Find(newCommand.Tokens[1].Text) != null &&
                                    player.Contents.Find(newCommand.Tokens[3].Text) != null)
                                {
                                    try
                                    {
                                        gui.MainText = GameRules.PlayerAttacksMonster(this, (Weapon)player.Contents.Find(newCommand.Tokens[3].Text), player.Buffs + player.Contents, (Monster)dungeon.CurrentRoom.Contents.Find(newCommand.Tokens[1].Text), (int)DateTime.Now.Ticks & 0x0000FFFF);
                                        turnConsumed = true;
                                    }
                                    catch
                                    {
                                        gui.MainText = "You cannot do that.";
                                    }
                                }
                            }
                        }

                        if (turnConsumed)
                        {
                            Step(); //The player has issued an action. Advance the game one step.
                        }

                        gui.MainText += "\n--------------------------------\n";
                        gui.MainText += dungeon.CurrentRoom.ExtraDescript;
                        gui.MainText += "\n\n";
                        gui.MainText += dungeon.CurrentRoom.GetContentsDescription();
                        gui.MainText += "\n\n";
                        gui.MainText += dungeon.CurrentRoom.GetDoorsDescription();
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

            List<Adjective> Adjectives = new List<Adjective>
            {
                new Adjective("large", 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 1.5f, 1.75f, 1.5f, 1.0f, 1.1f, 0.9f, 1.0f, 1.0f, 1.0f),
                new Adjective("small", 1.0f, 1.5f, 1.0f, 1.0f, 1.0f, 0.75f, 0.75f, 0.75f, 1.0f, 0.9f, 1.25f, 1.0f, 1.0f, 1.0f),
                new Adjective("sharp", 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.5f, 1.0f, 1.75f, 2.0f, 1.0f, 1.5f, 1.0f, 1.0f, 1.0f),
                new Adjective("dull", 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 0.75f, 1.0f, 0.9f, 1.0f, 1.0f, 1.0f),
                new Adjective("polished", 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 3.0f, 1.1f, 2.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f),
                new Adjective("dirty", 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f)
            };
            Random RNG = new Random();

            Monster tempMonster;
            tempMonster = new Monster("Grue", "M_00", 10, 75);
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
                Weapon tempWeapon;
                tempWeapon = new Weapon(Adjectives[RNG.Next(Adjectives.Count)], "sword", "W_00", 10, 10);
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

            Item tempItem;
            tempItem = new Item("Gold", "I_00", 1, 0);
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

            DataPack datapack = new DataPack(Directory.GetCurrentDirectory());
            datapack.AdjectivesMaster = Adjectives;
            datapack.ItemsMaster = ItemsMaster;
            datapack.MonstersMaster = MonstersMaster;
            datapack.WeaponsMaster = WeaponsMaster;
            datapack.Save();
        }
    }
}