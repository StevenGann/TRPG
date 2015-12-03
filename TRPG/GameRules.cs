using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    /// <summary>
    /// Static methods executing actions defined by game rules
    /// </summary>
    public static class GameRules
    {
        /// <summary>
        /// Carries out a player's attack with a specified weapon against a specified monster
        /// </summary>
        /// <param name="_gameState">Main TRPG_core instance containing entire game state</param>
        /// <param name="_weapon">Weapon object to be used in the attack</param>
        /// <param name="_buffs">Sum of all buffs currently effecting the player</param>
        /// <param name="_monster">Monster object to be attacked</param>
        /// <returns>Returns a string describing the event</returns>
        public static string PlayerAttacksMonster(TRPG_core _gameState, Weapon _weapon, Buff _buffs, Monster _monster, int _seed)
        {
            Random RNG = new Random(_seed);
            Buff finalBuff = _buffs + _weapon.Buffs;
            finalBuff.Clamp();
            string result = "";
            result += "You attack the " + _monster.Name + " with your " + _weapon.Name + ".\n";

            //Check to see if the player actually hit
            if (RNG.Next(100) <= (_weapon.Accuracy + finalBuff.Dexterity))
            {
                int dmgDone = (_weapon.Damage + finalBuff.Strength) - (RNG.Next(Math.Max(1, _monster.Defense + _monster.Buffs.Intelligence)) + RNG.Next(Math.Max(1, _monster.Defense + _monster.Buffs.Wisdom)));
                if (dmgDone < 0) { dmgDone = 0; }
                _monster.Health -= dmgDone;

                if (dmgDone > (_weapon.Damage / 2))//If it is a good hit
                {
                    result += "You hit the " + _monster.Name + " and deal " + dmgDone + " damage.\n";
                }
                else
                {
                    result += "You hit the " + _monster.Name + ", but only deal " + dmgDone + " damage.\n";
                }

                result += "The " + _monster.Name + "'s health is now " + _monster.Health + ". ";
            }
            else
            {
                result += "Unfortunately, you miss and do no damage. ";
                if (finalBuff.Intelligence < finalBuff.Strength && RNG.Next(100) < 25)
                {
                    result += "Worse yet, you lose your footing and hit yourself instead.\n";
                    int dmgDone = RNG.Next(Math.Max(1, _weapon.Damage / 2));
                    _gameState.player.Health -= dmgDone;
                    result += "You lose " + dmgDone + " health! ";
                }
            }

            if (_monster.Health <= 0)
            {
                result += "The " + _monster.Name + " is killed! ";
                for (int i = 0; i < _gameState.dungeon.CurrentRoom.Contents.Count; i++)
                {
                    if (_gameState.dungeon.CurrentRoom.Contents[i] is Monster)
                    {
                        if (_gameState.dungeon.CurrentRoom.Contents[i] == _monster)
                        {
                            _gameState.dungeon.CurrentRoom.Contents.RemoveAt(i);
                        }
                    }
                }
            }

            return result + "\n";
        }

        public static string MonsterAttacksPlayer(TRPG_core _gameState, Buff _buffs, Monster _monster, int _seed)
        {
            Random RNG = new Random(_seed);
            Buff finalBuff = _buffs;
            finalBuff.Clamp();
            Buff monsterBuffs = _monster.Buffs;
            monsterBuffs.Clamp();

            string result = "";
            result += "The " + _monster.GetFullName() + " attacks you.\n";

            //Check to see if the monster actually hit
            if (RNG.Next(100) <= (_monster.Accuracy + monsterBuffs.Dexterity))
            {
                int dmgDone = (_monster.Damage + monsterBuffs.Strength) - (RNG.Next(Math.Max(1, _buffs.Intelligence)) + RNG.Next(Math.Max(1, _buffs.Wisdom)));
                if (dmgDone < 0) { dmgDone = 0; }
                _gameState.player.Health -= dmgDone;

                result += "The " + _monster.Name + " hits you and deals " + dmgDone + " damage.\n";
                result += "Your health is now " + _gameState.player.Health + ".\n";
            }
            else
            {
                result += "Fortunately, it misses and does no damage.\n";
            }

            return result;
        }
    }
}