namespace BombermanClasses.BombNameSpace
{
    public class SmallBombRadiusStrategy : IBombRadiusStrategy
    {
        public int calculateRadius(int a)
        {
            return a;
        }
    }
    public class MediumBombRadiusStrategy : IBombRadiusStrategy
    {
        public int calculateRadius(int a)
        {
            return a*2;
        }
    }
    public class LargeBombRadiusStrategy : IBombRadiusStrategy
    {
        public int calculateRadius(int a)
        {
            return a*4;
        }
    }
    public class NuclearBombRadiusStrategy : IBombRadiusStrategy
    {
        public int calculateRadius(int a)
        {
            return a*8;
        }
    }
}
