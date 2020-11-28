using BombermanClasses.BombNameSpace;
using BombermanClasses.Items;

namespace BombermanClasses
{
    class ItemsMaker
    {
        private AbstractFactory fireFactory;
        private AbstractFactory iceFactory;

        public ItemsMaker()
        {
            fireFactory = new FireFactory();
            iceFactory = new IceFactory();
        }

        public Shield GetFireShield()
        {
            return fireFactory.createShield();
        }
        public Shield GetIceShield()
        {
            return iceFactory.createShield();
        }
        public Bomb GetFireBomb()
        {
            return fireFactory.createBomb();
        }

        public Bomb GetFireBomb(string id, int x, int y, IBombRadiusStrategy strategy, World subject)
        {
            return fireFactory.createBomb(id, x, y, strategy, subject);
        }
        public Bomb GetIceBomb()
        {
            return iceFactory.createBomb();
        }
        public Bomb GetIceBomb(string id, int x, int y, IBombRadiusStrategy strategy, World subject)
        {
            return iceFactory.createBomb(id, x, y, strategy, subject);
        }
    }
}
