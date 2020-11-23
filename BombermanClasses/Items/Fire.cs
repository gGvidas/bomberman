using BombermanClasses.TemplateMethod;
using System.Drawing;

namespace BombermanClasses.Items
{
    public class Fire : DestructionTemplate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Fire(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.DrawImage(image, x, y, width, height);
        }
    }
}
