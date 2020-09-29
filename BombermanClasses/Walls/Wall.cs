using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BombermanClasses.Walls
{
    [Serializable]
    public abstract class Wall : IMapObject
    {
        public void Draw(SolidBrush color, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.FillRectangle(color, x, y, width, height);
        }
    }
}
