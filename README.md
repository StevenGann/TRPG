# TRPG
An OOP C# framework for Text RPG games, inspired by the improved console window in Windows 10.

# What is this?

TRPG is a very generic implementation of a rogue-like text RPG game, implemented entirely as a console application with an 
interface reminicent of Zork and the like.
Your soul means of contol is typing action commands for each turn. 
These commands are interprited and actions are taken, and the game provides you with a description of the locations and events.

![Screenshot](http://i.imgur.com/l21Y15K.png)

![Screenshot](http://i.imgur.com/0SdsB3w.png)

From the top down we see the major features of the UI

- The inventory tray holds all the items your character is carrying. It collapses to one line to make room for the rest of the UI,
but can be expanded to however large is needed to show all the loot. The top of the inventory tray shows [weight / weight limit] 
and [count / capacity] for the inventory.

- The largest section is the main text window. This area serves as the main window into the game world. It displays descriptions of rooms
and items, as well as describing events as the unfold.

- The messages tray is simply a log of the 3 most recent messages from the game. These may be echos of player commands, or summmaries of recent events.

- The final section is the command prompt itself. This simple textbox is where the user types the commands to interact with the game world.

# Planned Features

- Loading from XML files. Every detail of the game should be configurable via external XML files, from dungeons to items.
The only thing bound to the code itself will be the core game rules, but the OOP nature of the framework makes those simple to modify.

- GUI application to generate the XML files. A simple interface for defining dungeon maps, the contents of each room, and the stats 
on every piece of loot.

- Online multiplayer. By advancing the game according to a fixed clock, actions can be pushed to a server and game states sent back to 
clients, creating a simple online MUD.

# To Do:
- [x] Text GUI system
- [x] Inventory system
- [x] Ability to describe and navigate dungeons
- [ ] Load dungeons from files
- [ ] Load game rules from external DLLs
- [ ] Finish combat mechanics
- [ ] Improved random generation of items, monsters, and dungeons
- [ ] Non-combat NPCs, like merchants
- [ ] Improved generated text descriptions of items, monsters, and rooms
- [ ] Magic system and weapon classes (melee, ranged, etc.)
- [ ] Player skills
- [ ] Enchanting
- [ ] Crafting
- [ ] Survival
- [ ] Zombies

# How to help
There's a lot to be done. Feel free to fork this repository and start working on anything, preferably something off the To Do list above.
