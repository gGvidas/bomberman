using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Walls
{
    public class WallFactory : Factory
    {
        public override Wall CreateWall(int type)
        {
            switch (type)
            {
                case 1:
                    return new DestructableWall();
                case 2:
                    return new IndestructableWall();
                case 3:
                    return new ItemDropWall();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
