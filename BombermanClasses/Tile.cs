using BombermanClasses.BombNameSpace;
using BombermanClasses.Items;
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

        public IMapObject entity { get; set; }

        public void destroy()
        {
            if (entity is Player)
            {
                Player player = entity as Player;
                player.isDead = true;
            }
            entity = null;
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
