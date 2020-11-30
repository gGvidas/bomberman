using BombermanClasses.Items;
using BombermanClasses.Observer;
using System.Drawing;
using System.Threading.Tasks;

namespace BombermanClasses.BombNameSpace
{
    public class Bomb : Item, IMapObject, IObserver
    {
        public string playerId { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        private int explosionTime = 2;
        private int currentTime = 0;

        private World Subject;

        public IBombRadiusStrategy strategy { get; set; }

        public Bomb()
        {
        }
        public Bomb(string playerId, int x, int y, IBombRadiusStrategy strategy, World subject)
        {
            if (subject == null) return;
            this.playerId = playerId;
            this.x = x;
            this.y = y;
            this.strategy = strategy;
            Subject = subject;

            Subject.Attach(this);
        }

        public int explosionRadius(int a)
        {
            return strategy.calculateRadius(a);
        }

        public new void Draw(Image image, int x, int y, int width, int height, Graphics graphics)
        {
            graphics.DrawImage(image, x, y, width, height);
        }

        public async Task Update()
        {
                currentTime++;
                if (currentTime == explosionTime)
                {
                    Subject.Detach(this);
                    World.Instance.Explode(x, y);
                }
            
        }
    }

}
