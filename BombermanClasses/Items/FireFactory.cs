

using BombermanClasses.BombNameSpace;
/**
* @(#) FireFactory.cs
*/
namespace BombermanClasses.Items
{
	public class FireFactory : AbstractFactory
	{
		public override Bomb createBomb(string id, int x, int y, IBombRadiusStrategy strategy, World subject)
		{
			return new FireBomb(id, x, y, strategy, subject);
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
