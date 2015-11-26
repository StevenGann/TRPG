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
    }
}