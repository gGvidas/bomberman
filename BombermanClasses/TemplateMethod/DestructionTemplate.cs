using BombermanClasses.Composite;
using System.Drawing;

namespace BombermanClasses.TemplateMethod
{
    public abstract class DestructionTemplate : IMapObject, IComposite
    {
        public abstract void Draw(Image image, int x, int y, int width, int height, Graphics graphics);
        public bool Destroy()
        {
            setIsDead();

            return canDestroy();
        }

        protected virtual void setIsDead()
        {

        }

        protected virtual bool canDestroy()
        {
            return true;
        }

        public abstract int getScore();
    }
}
