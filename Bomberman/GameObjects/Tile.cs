using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomberman.GameObjects
{
    public class Tile
    {
        public int X;
        public int Y;
        public int Type;

        public Tile(int x, int y, int type)
        {
            X = x;
            Y = y;
            Type = type;
        }
    }
}
