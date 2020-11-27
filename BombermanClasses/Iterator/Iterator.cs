using BombermanClasses.Items;

namespace BombermanClasses.Iterator
{
    public abstract class Iterator
    {
        public abstract bool HasNext();
        public abstract Item Next(int y, int size);
    }
}
