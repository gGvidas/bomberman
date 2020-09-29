using BombermanClasses.Walls;
using System;

namespace BombermanClasses
{
    [Serializable]
    public class Tile
    {
        public Player player { get; set; }

        public IMapObject mapObject { get; set; }
    }
}
