

using System.Drawing;
using System.IO;
/**
* @(#) IcePlayer.cs
*/
namespace BombermanClasses
{
	public class IcePlayer : Decorator
	{
        public IcePlayer(IPlayer player) : base(player)
        {
        }

        public override void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
		{
			player.Draw(image, x, y, width, height, graphics);
			AddShield(x, y, width, height, graphics);
		}
		
		public void AddShield(int x, int y, int width, int height, Graphics graphics)
		{
			//var spritesFolder = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..");
			//Image shield = Image.FromFile(Path.Combine(spritesFolder, @"BombermanClient\Sprites\Ice_Shield.jpg"));
			graphics.FillRectangle(new SolidBrush(Color.Blue), x, y, width / 2, height / 2);
		}
		
	}
	
}
