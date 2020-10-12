

using System.Drawing;
/**
* @(#) Item.cs
*/
namespace BombermanClasses.Items
{
	public class Item
	{
		public void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
		{
			graphics.DrawImage(image, x, y, width, height);
		}
	}
	
}
