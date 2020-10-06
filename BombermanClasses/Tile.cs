using BombermanClasses.BombNameSpace;
using System;

namespace BombermanClasses
{
    [Serializable]
    public class Tile
    {
        public Bomb bomb { get; set; }

        public IMapObject entity { get; set; }
    }
}
