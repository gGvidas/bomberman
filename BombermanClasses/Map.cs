using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses
{
    public class Map
    {
        public Tile[][] Objects { get; set; }

        public bool isGameOver { get; set; } = false;

    }
}
