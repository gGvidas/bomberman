

using System.Drawing;
/**
* @(#) Decorator.cs
*/
namespace BombermanClasses
{
	public abstract class Decorator
	{
		public IPlayer player;

		public Decorator(IPlayer player)
        {
			this.player = player;
        }

		public virtual void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
        {
			this.player.Draw(image, x, y, width, height, graphics);
        }
		
	}
	
}
