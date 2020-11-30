using BombermanClasses.Items;

namespace BombermanClasses.Iterator
{
    public abstract class Iterator
    {
        public abstract bool HasNext();
        public abstract Item Next(int y, int size);
        public abstract Item First();
        public abstract bool Remove(int index);
    }
}
