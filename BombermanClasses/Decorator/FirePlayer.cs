

using System.Drawing;
using System.IO;
/**
* @(#) FirePlayer.cs
*/
namespace BombermanClasses
{
	public class FirePlayer : Decorator
	{
        public FirePlayer(IPlayer player) : base(player)
        {
        }

        public override void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
		{
			player.Draw(image, x, y, width, height, graphics);
			AddShield(x, y, width, height, graphics);
		}

		public void AddShield(int x, int y, int width, int height, Graphics graphics)
		{
			//string spritesFolder = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..");
			//Image shield = Image.FromFile(Path.Combine(spritesFolder, @"BombermanClient\Sprites\Fire_Shield.jpg"));
			graphics.FillRectangle(new SolidBrush(Color.Red), x, y, width/2, height/2);
		}

	}
	
}
