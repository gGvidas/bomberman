using BombermanClasses.BombNameSpace;
using BombermanClasses.Items;
using BombermanClasses.TemplateMethod;
using System;
using System.Drawing;

namespace BombermanClasses
{
    [Serializable]
    public class Tile
    {
        public Bomb bomb { get; set; } = null;
        public Item item { get; set; } = null;

        public FirePlayer firetype { get; set; } = null;
        public OnFirePlayer onfiretype { get; set; } = null;
        public IcePlayer icetype { get; set; } = null;

        public DestructionTemplate entity { get; set; }

        public void destroy()
        {
            if (entity != null)
            {
                bool canDestroy = entity.Destroy();
                if (canDestroy)
                {
                    entity = null;
                }
            }
        }

        public void clear()
        {
            bomb = null;
            item = null;
            firetype = null;
            onfiretype = null;
            icetype = null;
            entity = null;
        }
    }
}
