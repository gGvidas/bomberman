

using BombermanClasses.BombNameSpace;
/**
* @(#) IceFactory.cs
*/
namespace BombermanClasses.Items
{
	public class IceFactory : AbstractFactory
	{
		public override Bomb createBomb()
		{
			return new IceBomb();
		}
		public override Bomb createBomb(string id, int x, int y, IBombRadiusStrategy strategy, World subject)
		{
			return new IceBomb(id, x, y, strategy, subject);
		}

		public override Shield createShield(  )
		{
			return new IceShield();
		}
		
	}
	
}
