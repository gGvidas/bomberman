using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BombermanClasses.Walls
{
    [Serializable]
    public abstract class Wall : IMapObject
    {
        public void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.DrawImage(image, x, y, width, height);
        }
    }
}
