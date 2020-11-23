using BombermanClasses.Items;
using System;

namespace BombermanClasses.Iterator
{
    public class ItemAggregate : Aggregate
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
            public override bool HasNext()
            {
                if (0 < Items.Length)
                {
                    return true;
                }
                return false;
            }

            public override Item Next()
            {
                Random r = new Random();

                int rand2 = r.Next(0, 100);

                if (rand2 < 25)
                    return Items[0];
                else if (rand2 < 50)
                    return Items[1];
                else if (rand2 < 75)
                    return Items[2];
                else if (rand2 < 101)
                    return Items[3];
                else
                    return null;
            }
        }
    }
}