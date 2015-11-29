using System;
using System.Collections;
using System.Collections.Generic;

namespace TRPG
{
    /// <summary>
    /// Container class for items
    /// </summary>
    public class Inventory : IEnumerable
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
    }
}