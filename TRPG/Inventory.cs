using System;
using System.Collections;
using System.Collections.Generic;

namespace TRPG
{
    /// <summary>
    /// Container class for items
    /// </summary>
    [Serializable]
    public class Inventory
    {
        public int Capacity;
        public int MaxWeight;

        public int Weight
        {
            get
            {
                int result = 0;
                foreach (Item i in items)
                {
                    result += (int)i.Weight;
                }
                return result;
            }
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        public Item this[int index]
        {
            get
            {
                return items[index];
            }

            set
            {
                items[index] = value;
            }
        }

        private List<Item> items = null;

        public Inventory()
        {
            items = new List<Item>();
            Capacity = 100;
            MaxWeight = 100;
        }

        /// <summary>
        /// Add Item to inventory
        /// </summary>
        /// <param name="_item">Item to be added</param>
        /// <returns>Returns 1 for successful add, -1 if too heavy, -2 for too many.</returns>
        public int Add(Item _item)
        {
            if (MaxWeight == -1 || (Weight + _item.Weight) <= MaxWeight)
            {
                if (Capacity == -1 || items.Count < Capacity)
                {
                    items.Add(_item);
                    return 1;
                }
                return -2;
            }
            return -1;
        }

        // IEnumerable Member
        public IEnumerator GetEnumerator()
        {
            foreach (Item o in items)
            {
                if (o == null)
                {
                    break;
                }
                yield return o;
            }
        }

        //Avoid using this one.
        public string Take(string _text, Inventory _playerInventory)
        {
            string result = "You cannot take that.";

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name.ToLower() == _text.ToLower() && !(items[i] is Monster))
                {
                    _playerInventory.Add(items[i]);
                    items.RemoveAt(i);

                    result = "You take the " + _text + ".";
                    return result;
                }
            }

            return result;
        }

        public string Take(List<Token> _tokens, Inventory _playerInventory)
        {
            string result = "You cannot take that.";
            int tokenOffset = 0;

            if (_tokens.Count >= 2) //Make sure the given tokens are valid
            {
                while (_tokens[tokenOffset].Text.ToLower() != "take")
                {
                    tokenOffset++;
                }
                string targetName = _tokens[tokenOffset + 1].Text;
                List<string> targetAdjectives = _tokens[tokenOffset + 1].Adjectives;
                int bestMatch = 0;
                int bestMatchIndex = -1;
                for (int i = 0; i < items.Count; i++)
                {
                    int matchCount = 0;
                    //If the name is a match
                    if (items[i].Name.ToLower() == targetName.ToLower() && !(items[i] is Monster))
                    {
                        matchCount++;
                        if (items[i].Adjectives.Count > 0 && targetAdjectives.Count > 0)
                        {
                            foreach (Adjective adjectiveA in items[i].Adjectives)
                            {
                                foreach (string adjectiveB in targetAdjectives)
                                {
                                    if (adjectiveA.Text.ToLower() == adjectiveB.ToLower())
                                    {
                                        matchCount++;
                                    }
                                }
                            }
                        }
                        if (matchCount > bestMatch)
                        {
                            bestMatchIndex = i;
                        }
                    }
                }

                if (bestMatchIndex != -1)
                {
                    result = "You take the " + items[bestMatchIndex].GetFullName() + ".";
                    _playerInventory.Add(items[bestMatchIndex]);
                    items.RemoveAt(bestMatchIndex);
                    return result;
                }
            }
            return result;
        }

        public string Drop(string _text, Inventory _playerInventory)
        {
            string result = "You cannot drop that.";

            for (int i = 0; i < _playerInventory.Count; i++)
            {
                if (_playerInventory[i].Name.ToLower() == _text.ToLower())
                {
                    items.Add(_playerInventory[i]);
                    _playerInventory.RemoveAt(i);

                    result = "You drop the " + _text + ".";
                    return result;
                }
            }

            return result;
        }

        public string Drop(List<Token> _tokens, Inventory _playerInventory)
        {
            string result = "You cannot drop that.";
            int tokenOffset = 0;

            if (_tokens.Count >= 2) //Make sure the given tokens are valid
            {
                while (_tokens[tokenOffset].Text.ToLower() != "drop")
                {
                    tokenOffset++;
                }
                string targetName = _tokens[tokenOffset + 1].Text;
                List<string> targetAdjectives = _tokens[tokenOffset + 1].Adjectives;
                int bestMatch = 0;
                int bestMatchIndex = -1;
                for (int i = 0; i < _playerInventory.Count; i++)
                {
                    int matchCount = 0;
                    //If the name is a match
                    if (_playerInventory[i].Name.ToLower() == targetName.ToLower() && !(_playerInventory[i] is Monster))
                    {
                        matchCount++;
                        if (_playerInventory[i].Adjectives.Count > 0 && targetAdjectives.Count > 0)
                        {
                            foreach (Adjective adjectiveA in _playerInventory[i].Adjectives)
                            {
                                foreach (string adjectiveB in targetAdjectives)
                                {
                                    if (adjectiveA.Text.ToLower() == adjectiveB.ToLower())
                                    {
                                        matchCount++;
                                    }
                                }
                            }
                        }
                        if (matchCount > bestMatch)
                        {
                            bestMatchIndex = i;
                        }
                    }
                }

                if (bestMatchIndex != -1)
                {
                    result = "You drop the " + _playerInventory[bestMatchIndex].GetFullName() + ".";
                    items.Add(_playerInventory[bestMatchIndex]);
                    _playerInventory.RemoveAt(bestMatchIndex);
                    return result;
                }
            }
            return result;
        }

        public void RemoveAt(int i)
        {
            items.RemoveAt(i);
        }

        /// <summary>
        /// Search an Inventory for an Item matching a description
        /// </summary>
        /// <param name="_tokens"> List of Tokens of the search command </param>
        /// <returns>Returns first matching Item. Returns null if no match is found. </returns>
        public Item Find(List<Token> _tokens)
        {
            Item result = null;
            string targetName = _tokens[1].Text.ToLower();

            foreach (Item item in items)
            {
                if (item.Name.ToLower() == targetName)
                {
                    result = item;
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Search an Inventory for an Item matching a name
        /// </summary>
        /// <param name="_text"></param>
        /// <returns></returns>
        internal object Find(string _text)
        {
            Item result = null;
            string targetName = _text.ToLower();

            foreach (Item item in items)
            {
                if (item.Name.ToLower() == targetName)
                {
                    result = item;
                    return result;
                }
            }

            return result;
        }
    }
}