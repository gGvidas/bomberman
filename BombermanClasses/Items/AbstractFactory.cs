

using BombermanClasses.BombNameSpace;
/**
* @(#) AbstractFactory.cs
*/
namespace BombermanClasses.Items
{
	public abstract class AbstractFactory
	{
		public abstract Bomb createBomb();
		public abstract Bomb createBomb(string id, int x, int y, IBombRadiusStrategy strategy, World subject);

		public abstract Shield createShield(  );
		
	}
	
}
