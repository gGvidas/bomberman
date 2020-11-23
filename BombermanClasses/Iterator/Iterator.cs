using BombermanClasses.Items;

namespace BombermanClasses.Iterator
{
    public interface Iterator
    {
        public bool HasNext();
        public Item Next();
    }
}
