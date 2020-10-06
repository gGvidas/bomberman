using System.Drawing;

namespace BombermanClasses.BombNameSpace
{
    public class Bomb : IMapObject
    {
        public int x { get; set; }
        public int y { get; set; }

        public IBombRadiusStrategy strategy { get; set; }

        public Bomb(int x, int y, IBombRadiusStrategy strategy)
        {
            this.x = x;
            this.y = y;
            this.strategy = strategy;
        }

        public int explosionRadius(int a)
        {
            return strategy.calculateRadius(a);
        }

        public void Draw(SolidBrush color, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.FillRectangle(color, x, y, width, height);
        }
    }
}
