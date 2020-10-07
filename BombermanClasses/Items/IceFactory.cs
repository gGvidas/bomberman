

using BombermanClasses.BombNameSpace;
/**
* @(#) IceFactory.cs
*/
namespace BombermanClasses.Items
{
	public class IceFactory : AbstractFactory
	{
		public override Bomb createBomb(int x, int y, IBombRadiusStrategy strategy, World subject)
		{
			return new IceBomb(x, y, strategy, subject);
		}

		public override Shield createShield(  )
		{
			return new IceShield();
		}
		
	}
	
}
