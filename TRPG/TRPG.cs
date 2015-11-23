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

        bool showingHelp = false;
        string helpText = "";
        string underHelpText = "";

        GUI gui;

        public TRPG_core()
        {
            playerInventory = new Inventory();
            gui = new GUI(100, 30, true);
            messages = new List<Message>();
            parser = new Parser();

            playerInventory.Add(new Item("Sword", 10, 10));
            playerInventory.Add(new Item("Dagger", 7, 5));
            playerInventory.Add(new Item("Amulet", 50, 1));

            helpText = "COMMON COMMANDS\n";
            helpText += "This is a small list of some common and useful commands ";
            helpText += "for navigating the game. There are many other commands, ";
            helpText += "but these few are necessary for manipulating the interface.\n\n";
            helpText += "\"open inventory\"  - Makes the inventory tray much larger.\n";
            helpText += "\"close inventory\" - Collapses the inventory tray back to 1 line.\n";
            helpText += "\"scroll down\"     - Scrolls very large texts down one line.\n";
            helpText += "\"scroll up\"       - Scrolls very large texts up one line.\n";
            helpText += "\"help\"            - shows this help.\n";

            gui.MainText = "Nothing to see here. :(";

        }

        /// <summary>
        /// Advance the game forward one step
        /// </summary>
        public void Step()
        {

        }

        /// <summary>
        /// Update the interface
        /// </summary>
        public void Update(string _input)
        {
            if (_input != "")
            {
                messages.Add(new Message("Input: " + _input));
                Command newCommand = parser.Parse(_input);

                //Catch special commands outside of actual parsing
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
                else
                {
                    if (newCommand.Pattern != "")
                    {
                        messages.Add(new Message("Pattern: " + newCommand.Pattern));//For debugging. Remove later
                    }
                    else
                    {
                        messages.Add(new Message("I did not understand any of that."));
                    }
                }
            }

            gui.Render(this);
        }
    }
}
