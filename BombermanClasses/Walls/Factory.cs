using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Walls
{
    public abstract class Factory
    {
        public abstract Wall CreateWall(int type);
    }
}
