using BombermanClasses.Items;
using System;
using System.Collections.Generic;

namespace BombermanClasses.Iterator
{
    public class ItemList : Aggregate
    {
        public static List<Item> Items = new List<Item>();

        public Iterator GetIterator()
        {
            ItemsMaker maker = new ItemsMaker();
            Items.Add(maker.GetFireBomb());
            Items.Add(maker.GetFireShield());

            Items.Add(maker.GetIceBomb());
            Items.Add(maker.GetIceShield());
            return new ItemIterator();
        }

        private class ItemIterator : Iterator
        {
            int index = 0;

            public override bool HasNext()
            {
                if (Items.Count <= 0)
                    return false;
                return true;
            }

            public override Item Next(int y, int size)
            {
                if (index == 0)
                    index++;
                else
                    index--;

                if (y > size / 2)
                    return Items[0 + index];
                else
                    return Items[2 + index];
            }

            public override Item First()
            {
                return Items[0];
            }

            public override bool Remove(int index)
            {
                return Items.Remove(Items[index]);
            }
        }
    }
}