

using BombermanClasses.BombNameSpace;
/**
* @(#) FireFactory.cs
*/
namespace BombermanClasses.Items
{
	public class FireFactory : AbstractFactory
	{
		public override Bomb createBomb(int x, int y, IBombRadiusStrategy strategy, World subject)
		{
			return new FireBomb(x, y, strategy, subject);
		}

        public override Bomb createBomb()
        {
            return new FireBomb();
        }

        public override Shield createShield(  )
		{
			return new FireShield();
		}
		
	}
	
}
