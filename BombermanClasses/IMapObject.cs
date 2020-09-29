using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BombermanClasses
{
    public interface IMapObject
    {
        void Draw(SolidBrush color, int x, int y, int width, int height, Graphics graphics);
    }
}
