using BombermanClasses.Composite;
using BombermanClasses.Items;
using BombermanClasses.TemplateMethod;
using System.Drawing;

namespace BombermanClasses
{
    public class Player : DestructionTemplate, IPlayer
    {
        public string Id { get; set; }
        public int x { get; set; } 
        public int y { get; set; }
        public Item item { get; set; } = null;

        public bool isDead { get; set; } = false;

        public CompositeDirectory destroyedEntities { get; set; }

        public Player(string id, int x, int y)
        {
            Id = id;
            this.x = x;
            this.y = y;
            this.destroyedEntities = new CompositeDirectory();
        }

        public override void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.DrawImage(image, x, y, width, height);
        }

        public void moveUp()
        {
            y--;
        }
        public void moveDown()
        {
            y++;
        }
        public void moveRight()
        {
            x++;
        }
        public void moveLeft()
        {
            x--;
        }

        public override int getScore()
        {
            return 300;
        }

        protected sealed override void setIsDead()
        {
            isDead = true;
            base.setIsDead();
        }
    }
}
