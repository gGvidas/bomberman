using System.Drawing;

namespace BombermanClasses
{
    public class Player : IMapObject
    {
        public string Id { get; set; }
        public int x { get; set; } 
        public int y { get; set; }
        
        public Player(string id, int x, int y)
        {
            Id = id;
            this.x = x;
            this.y = y;
        }

        public void Draw(SolidBrush color, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.FillRectangle(color, x, y, width, height);
        }
    }
}
