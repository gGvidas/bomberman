using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BombermanClasses
{
    public class OnFirePlayer : Decorator
    {
        private FirePlayer fire;
        public OnFirePlayer(IPlayer player) : base(player)
        {
            fire = new FirePlayer(player);
        }

        public override void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
        {
            player.Draw(image, x, y, width, height, graphics);
            fire.AddShield(x, y, width, height, graphics);
            AddEffect(x, y, width, height, graphics);
        }

        public void AddEffect(int x, int y, int width, int height, Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(Color.Orange), x, y, width / 4, height / 4);
        }
    }
}
