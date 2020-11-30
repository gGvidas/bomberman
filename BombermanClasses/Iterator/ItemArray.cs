using BombermanClasses.Items;
using System;

namespace BombermanClasses.Iterator
{
    public class ItemArray : Aggregate
    {
        public static Item[] Items = new Item[4];

        public Iterator GetIterator()
        {
            ItemsMaker maker = new ItemsMaker();
            Items[0] = maker.GetFireBomb();
            Items[1] = maker.GetFireShield();

            Items[2] = maker.GetIceBomb();
            Items[3] = maker.GetIceShield();
            return new ItemIterator();
        }

        private class ItemIterator : Iterator
        {
            int index = 0;

            public override bool HasNext()
            {
                if (Items.Length <= 0)
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
                    return Items[0+index];
                else
                    return Items[2+index];
            }

            public override Item First()
            {
                return Items[0];
            }

            public override bool Remove(int index)
            {
                if(index >= 0 && HasNext())
                {
                    for (int i = index; i < Items.Length-1; i++)
                    {
                        Items[i] = Items[i+1];
                    }
                    Items[Items.Length] = null;
                    return true;
                }
                return false;
            }
        }
    }
}