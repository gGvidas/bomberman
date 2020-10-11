using BombermanClasses.BombNameSpace;
using BombermanClasses.Items;
using System;

namespace BombermanClasses
{
    [Serializable]
    public class Tile
    {
        public Bomb bomb { get; set; } = null;
        public Item item { get; set; } = null;

        public IMapObject entity { get; set; }
    }
}
