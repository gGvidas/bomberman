using BombermanClasses.TemplateMethod;
using System;
using System.Drawing;

namespace BombermanClasses.Walls
{
    [Serializable]
    public abstract class Wall : DestructionTemplate, IWallFlyweight
    {
        public override void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.DrawImage(image, x, y, width, height);
        }
    }
}
