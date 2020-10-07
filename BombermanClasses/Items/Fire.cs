using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BombermanClasses.Items
{
    public class Fire : IMapObject
    {
        public int x { get; set; }
        public int y { get; set; }

        public Fire(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw(SolidBrush color, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.FillRectangle(color, x, y, width, height);
        }
    }
}
