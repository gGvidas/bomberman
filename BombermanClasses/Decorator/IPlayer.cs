

using System.Drawing;
/**
* @(#) IPlayer.cs
*/
namespace BombermanClasses
{
	public interface IPlayer
	{
		void Draw(Image image, int x, int y, int width, int height, Graphics graphics);
		
	}
	
}
