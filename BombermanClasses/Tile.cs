using System;

namespace BombermanClasses
{
    [Serializable]
    public class Tile
    {
        //public Terrain terrain { get; set; }
        public bool wall { get; set; }
        //public Item item { get; set; }
        public Player player { get; set; }
    }
}
